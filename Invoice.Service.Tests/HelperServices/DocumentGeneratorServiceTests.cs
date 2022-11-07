using AutoFixture;
using Invoice.Entities.Models;
using Invoice.Service.HelperServices;
using Invoice.Shared.Request;
using UBLSunatPE;

namespace Invoice.Service.Tests.HelperServices;

public class DocumentGeneratorServiceTests
{
    private readonly Fixture _fixture;
    private readonly DocumentGeneratorService _documentGeneratorService;

    public DocumentGeneratorServiceTests()
    {
        _fixture = new Fixture();
        _documentGeneratorService = new DocumentGeneratorService();
    }

    [Fact]
    public void DocumentGeneratorService_GenerateInvoiceTypeTest()
    {
        //Arrange
        var invoiceRequest = _fixture.Create<InvoiceRequest>();
        var issuer = _fixture.Create<Issuer>();

        #region Fix amount

        invoiceRequest.TaxTotalAmount = 3.6m;
        invoiceRequest.TotalAmount = 23.6m;

        foreach (var item in invoiceRequest.TaxSubTotals)
        {
            item.TaxableAmount = 20;
            item.TaxAmount = 3.6m;
        }

        foreach (var item in invoiceRequest.ProductsDetails)
        {
            item.Quantity = 1;
            item.UnitPrice = 20;
            item.TaxAmount = 3.6m;
            item.TaxPercentage = 18;
        }

        #endregion

        //Act
        var invoiceType = _documentGeneratorService.GenerateInvoiceType(invoiceRequest, issuer);

        //Asset
        Assert.NotNull(invoiceType);
        Assert.IsType<InvoiceType>(invoiceType);
    }

    [Fact]
    public void DocumentGeneratorService_GenerateDebitNoteTypeTest()
    {
        //Arrange
        var debitNoteRequest = _fixture.Create<NoteRequest>();
        var issuer = _fixture.Create<Issuer>();

        #region Fix amount

        debitNoteRequest.TaxTotalAmount = 3.6m;
        debitNoteRequest.TotalAmount = 23.6m;

        foreach (var item in debitNoteRequest.TaxSubTotals)
        {
            item.TaxableAmount = 20;
            item.TaxAmount = 3.6m;
        }

        foreach (var item in debitNoteRequest.ProductsDetails)
        {
            item.Quantity = 1;
            item.UnitPrice = 20;
            item.TaxAmount = 3.6m;
            item.TaxPercentage = 18;
        }

        #endregion

        //Act
        var debitNotetype = _documentGeneratorService.GenerateDebitNoteType(debitNoteRequest, issuer);

        //Assert
        Assert.NotNull(debitNotetype);
        Assert.IsType<DebitNoteType>(debitNotetype);
    }

    [Fact]
    public void DocumentGeneratorService_GenerateCreditNoteTypeTest()
    {
        //Arrange
        var creditNoteRequest = _fixture.Create<NoteRequest>();
        var issuer = _fixture.Create<Issuer>();

        #region Fix amount

        creditNoteRequest.TaxTotalAmount = 3.6m;
        creditNoteRequest.TotalAmount = 23.6m;

        foreach (var item in creditNoteRequest.TaxSubTotals)
        {
            item.TaxableAmount = 20;
            item.TaxAmount = 3.6m;
        }

        foreach (var item in creditNoteRequest.ProductsDetails)
        {
            item.Quantity = 1;
            item.UnitPrice = 20;
            item.TaxAmount = 3.6m;
            item.TaxPercentage = 18;
        }

        #endregion

        //ActS
        var creditNotetype = _documentGeneratorService.GenerateCreditNoteType(creditNoteRequest, issuer);

        //Assert
        Assert.NotNull(creditNotetype);
        Assert.IsType<CreditNoteType>(creditNotetype);
    }

    [Fact]
    public void DocumentGeneratorService_GenerateSummaryDocumentsTypeTest()
    {
        //Arrange
        var summaryDocumentsRequest = _fixture.Create<SummaryDocumentsRequest>();
        var issuer = _fixture.Create<Issuer>();
        var invoices = _fixture.Create<IEnumerable<Entities.Models.Invoice>>();

        //Act
        var summaryDocuments = _documentGeneratorService.GenerateSummaryDocumentsType(summaryDocumentsRequest, issuer, invoices);

        //Asser
        Assert.NotNull(summaryDocuments);
        Assert.IsType<SummaryDocumentsType>(summaryDocuments);
    }

    [Fact]
    public void DocumentGeneratorService_GenerateVoidedDocumentsTypeTest()
    {
        //Arrange
        var voidedDocumentsRequest = _fixture.Create<VoidedDocumentsRequest>();
        var issuer = _fixture.Create<Issuer>();

        //Act
        var voidedDocuments = _documentGeneratorService.GenerateVoidedDocumentsType(voidedDocumentsRequest, issuer);

        //Asser
        Assert.NotNull(voidedDocuments);
        Assert.IsType<VoidedDocumentsType>(voidedDocuments);
    }

    [Fact]
    public void DocumentGeneratorService_GenerateDespatchAdviceTypeTest()
    {
        //Arrange
        var despatchAdviceRequest = _fixture.Create<DespatchAdviceRequest>();
        var issuer = _fixture.Create<Issuer>();

        #region Fix amount

        #endregion

        //Act
        var despatchAdviceType = _documentGeneratorService.GenerateDespatchAdviceType(despatchAdviceRequest, issuer);

        //Asset
        Assert.NotNull(despatchAdviceType);
        Assert.IsType<DespatchAdviceType>(despatchAdviceType);
    }
}