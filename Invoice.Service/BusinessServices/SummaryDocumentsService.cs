﻿using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.ConfigurationModels;
using Invoice.Entities.Exceptions;
using Invoice.Entities.Models;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using Microsoft.Extensions.Options;
using UBLSunatPE;

namespace Invoice.Service.BusinessServices;

public class SummaryDocumentsService : ISummaryDocumentsService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;
    private readonly IOptions<SunatConfiguration> _configuration;

    public SummaryDocumentsService(IRepositoryManager repository, 
        ILoggerManager logger, 
        IMapper mapper, 
        IDocumentGeneratorService documentGeneratorService, 
        ISunatService sunatService,
        IOptions<SunatConfiguration> configuration)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _sunatService = sunatService;
        _configuration = configuration;
    }

    public async Task<DocumentsResponse> CreateSummaryDocumentsAsync(Guid id, SummaryDocumentsRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var tickets = await GetInvoicesByIssueDateAndCheckIfItExists(request.ReferenceDate, null, trackChanges);

        //Generate xml
        var summaryDocuments = _documentGeneratorService.GenerateSummaryDocumentsType(request, issuer, tickets);

        //Serialize to xml
        var xmlString = _sunatService.SerializeXmlDocument(typeof(SummaryDocumentsType), summaryDocuments);

        //Sign xml
        var xmlDoc = _sunatService.SignXml(xmlString, issuer, "RC");

        //Zip xml
        var xmlFile = $"{issuer.IssuerId}-{summaryDocuments.ID.Value}.xml";
        var byteZippedXml = _sunatService.ZipXml(xmlDoc, Path.GetFileName(xmlFile));

        //Send bill
        var zippedFile = xmlFile.Replace(".xml", ".zip");

        var ticketNumber = await _sunatService.SendSummary(_configuration.Value.UrlInvoice,
                _configuration.Value.Username,
                _configuration.Value.Password,
                zippedFile,
                byteZippedXml);

        if (ticketNumber != null)
        {
            foreach (var ticket in tickets)
            {
                ticket.SummaryDocumentStatus = false;
                ticket.Ticket = ticketNumber;
            }

            _repository.Ticket.CreateTicket(new Ticket 
            {
                IssueDate = request.IssueDate,
                TicketType = "Summary",
                TicketNumber = ticketNumber,
                DocumentsXml = xmlDoc.OuterXml
            });

            await _repository.SaveAsync();

            var summaryDocumentsResponse = new DocumentsResponse
            {
                TicketType = "Summary",
                SummarySended = true,
                Ticket = ticketNumber
            };

            return summaryDocumentsResponse;
        }

        return null;
    }

    public async Task<IEnumerable<Entities.Models.Invoice>> GetInvoicesByIssueDateAndCheckIfItExists(DateTime issueDate, bool? summaryStatus, bool trackChanges)
    {
        var invoices = await _repository.Invoice.GetTicketsByIssueDateAsync(issueDate, summaryStatus, trackChanges);

        if (!invoices.Any())
            throw new InvoiceNotFoundException(issueDate);

        return invoices;
    }

    public async Task<InvoiceResponse> GetInvoiceAsync(Guid id, bool trackChanges)
    {
        var invoice = await GetInvoiceAndCheckIfItExists(id, trackChanges);

        var invoiceResponse = _mapper.Map<Entities.Models.Invoice, InvoiceResponse>(invoice);
        return invoiceResponse;
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }

    private async Task<Entities.Models.Invoice> GetInvoiceAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var invoice = await _repository.Invoice.GetInvoiceAsync(id, trackChanges);

        if (invoice is null)
            throw new IssuerNotFoundException(id);

        return invoice;
    }
}