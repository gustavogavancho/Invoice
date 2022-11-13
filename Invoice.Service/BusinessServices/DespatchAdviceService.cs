using AutoMapper;
using Invoice.Contracts.Logger;
using Invoice.Contracts.Repositories;
using Invoice.Entities.Exceptions;
using Invoice.Entities.Models;
using Invoice.Service.Contracts.BusinessServices;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared.Request;
using Invoice.Shared.Response;
using UBLSunatPE;

namespace Invoice.Service.BusinessServices;

public class DespatchAdviceService : IDespatchAdviceService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IDocumentGeneratorService _documentGeneratorService;
    private readonly ISunatService _sunatService;

    public DespatchAdviceService(IRepositoryManager repository,
        ILoggerManager logger,
        IMapper mapper,
        IDocumentGeneratorService documentGeneratorService,
        ISunatService sunatService)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _documentGeneratorService = documentGeneratorService;
        _sunatService = sunatService;
    }

    public async Task<DespatchResponse> CreateDespatchAdviceAsync(Guid id, DespatchRequest request, bool trackChanges)
    {
        var issuer = await GetIssuerAndCheckIfItExists(id, trackChanges);

        var despatchAdvice = _documentGeneratorService.GenerateDespatchAdviceType(request, issuer);

        //Serialize to xml
        var xmlString = _sunatService.SerializeXmlDocument(typeof(DespatchAdviceType), despatchAdvice);

        //Sign xml
        var xmlDoc = _sunatService.SignXml(xmlString, issuer, request.DespatchDetail.DocumentType);

        //Zip xml
        var xmlFile = $"{issuer.IssuerId}-{request.DespatchDetail.DocumentType}-{request.DespatchDetail.Serie}{request.DespatchDetail.SerialNumber.ToString("00")}-{request.DespatchDetail.CorrelativeNumber.ToString("00000000")}.xml";
        var byteZippedXml = _sunatService.ZipXml(xmlDoc, Path.GetFileName(xmlFile));

        //Send bill
        var zippedFile = xmlFile.Replace(".xml", ".zip");
        var cdrByte = await _sunatService.SendBill("https://e-beta.sunat.gob.pe/ol-ti-itemision-guia-gem-beta/billService",
                "20606022779MODDATOS",
                "moddatos",
                zippedFile,
                byteZippedXml);

        //Read response
        var responses = _sunatService.ReadResponse(cdrByte);

        if (responses.Any(x => x.Contains("aceptado")))
        {
            var despatchDb = _mapper.Map<DespatchRequest, Despatch>(request);
            despatchDb.IssuerId = issuer.Id;
            despatchDb.DespatchXml = xmlDoc.OuterXml;
            despatchDb.Accepted = true;
            despatchDb.SunatResponse = cdrByte;
            despatchDb.Observations = string.Join("|", responses);
            _repository.Despatch.CreateDespatch(despatchDb);
            await _repository.SaveAsync();

            var despatchResponse = _mapper.Map<Despatch, DespatchResponse>(despatchDb);
            return despatchResponse;
        }

        return null;
    }

    public async Task<List<DespatchResponse>> GetDespatchesAsync(bool trackChanges)
    {
        var despatches = await _repository.Despatch.GetDespatchesAsync(trackChanges);

        var despatchesResponse = _mapper.Map<IEnumerable<Despatch>, List<DespatchResponse>>(despatches);

        return despatchesResponse;
    }

    public async Task<DespatchResponse> GetDespatchAdviceBySerieAsync(string serie, int serialNumber, int correlativeNumber, bool trackChanges)
    {
        var despatch = await GetDespatchBySerieAndCheckIfItExists(serie, serialNumber, correlativeNumber, trackChanges);

        var despatchResponse = _mapper.Map<Despatch, DespatchResponse>(despatch);

        return despatchResponse;
    }

    private async Task<Issuer> GetIssuerAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var issuer = await _repository.Issuer.GetIssuerAsync(id, trackChanges);

        if (issuer is null)
            throw new IssuerNotFoundException(id);

        return issuer;
    }

    private async Task<Despatch> GetDespatchBySerieAndCheckIfItExists(string serie, int serialNumber, int correlativeNumber, bool trackChanges)
    {
        var invoice = await _repository.Despatch.GetDespatchBySerieAsync(serie, serialNumber, correlativeNumber, trackChanges);

        if (invoice is null)
            throw new DespatchNotFoundException(serie, serialNumber, correlativeNumber);

        return invoice;
    }
}