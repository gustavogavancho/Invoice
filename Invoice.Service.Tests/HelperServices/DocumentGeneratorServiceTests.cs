using AutoFixture;
using Invoice.Service.HelperServices;
using Invoice.Shared.Request;
using UBLSunatPE;

namespace Invoice.Service.Tests.HelperServices;

public class DocumentGeneratorServiceTests
{
    [Fact]
    public void GenerateInvoiceTypeTest()
    {
        //Arrange
        var fixture = new Fixture();
        var invoiceRequest = fixture.Create<InvoiceRequest>();

        //Act
        var documentGeneratorService = new DocumentGeneratorService();
        var invoiceType = documentGeneratorService.GenerateInvoiceType(invoiceRequest);

        //Asset
        Assert.NotNull(invoiceType);
        Assert.IsType<InvoiceType>(invoiceType);
    }
}