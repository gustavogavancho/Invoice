using AutoFixture;
using Invoice.Entities.Models;
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
        var issuer = fixture.Create<Issuer>();

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
        var documentGeneratorService = new DocumentGeneratorService();
        var invoiceType = documentGeneratorService.GenerateInvoiceType(invoiceRequest, issuer);

        //Asset
        Assert.NotNull(invoiceType);
        Assert.IsType<InvoiceType>(invoiceType);
    }
}