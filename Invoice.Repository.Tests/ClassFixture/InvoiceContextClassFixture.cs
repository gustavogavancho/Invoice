﻿using Invoice.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Invoice.Repository.Tests.ClassFixture;

public class InvoiceContextClassFixture
{
    public InvoiceContext Context { get; private set; }

    private Guid _idIssuer1 = Guid.Parse("CCE03168-F901-4B23-AE9C-FC031D9DC888");
    private Guid _idIssuer2 = Guid.Parse("990A5761-BFEA-4572-B0FE-08DAB08EACF6");
    private Guid _idInvoice1 = Guid.Parse("ECE849FE-A441-4DEC-A452-A6723A38C9D0");
    private Guid _idInvoice2 = Guid.Parse("2467C21B-3ACF-45CD-8A3B-A898CE07B7FC");

    public InvoiceContextClassFixture()
    {
        var options = new DbContextOptionsBuilder<InvoiceContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new InvoiceContext(options);

        SeedIssuer();
        SeedInvoice();
    }

    private void SeedIssuer()
    {
        Context.Issuers.Add(new Issuer
        {
            Id = _idIssuer1,
            IssuerId = 20606022779,
            IssuerName = "SWIFTLINE SAC",
            IssuerType = "6",
            GeoCode = "220901",
            EstablishmentCode = "0000",
            Department = "SAN MARTIN",
            Province = "SAN MARTIN",
            District = "TARAPOTO",
            Address = "PSJE. LIMATAMBO 121"
        });

        Context.Issuers.Add(new Issuer
        {
            Id = _idIssuer2,
            IssuerId = 20606022779,
            IssuerName = "SWIFTLINE SAC 2",
            IssuerType = "6",
            GeoCode = "220901",
            EstablishmentCode = "0000",
            Department = "SAN MARTIN",
            Province = "SAN MARTIN",
            District = "TARAPOTO",
            Address = "PSJE. LIMATAMBO 121"
        });

        Context.SaveChanges();
    }

    private void SeedInvoice()
    {
        Context.Invoices.Add(new Entities.Models.Invoice
        {
            Id = _idInvoice1,
            InvoiceXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Invoice xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:cac=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" xmlns:cbc=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" xmlns:ccts=\"urn:un:unece:uncefact:documentation:2\" xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:ext=\"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" xmlns:qdt=\"urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2\" xmlns:udt=\"urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2\" xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2\"><ext:UBLExtensions><ext:UBLExtension> <ext:ExtensionContent><ds:Signature Id=\"SignSUNAT\"><SignedInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256\" /><Reference URI=\"\"><Transforms><Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" /></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#sha256\" /><DigestValue>XYPAYJ1GtdLQkCx3u1RSdbQgseul4U/E82Z9ExL0hPg=</DigestValue></Reference></SignedInfo><SignatureValue xmlns=\"http://www.w3.org/2000/09/xmldsig#\">uh/W4GE62hvCneaYX+twM3gyoLW6o5/+FUgzi91W9JMs2VB67fAvAo4hl2v2Ahal4qv5WgDIhVjwIMbEJZra2x+7q8u/nrf0Bc49wM6mRitoFTVHT8OoK2K8LxJhP4q5iPRSCuxegUvNMd6hebGoNducPNGAyM0n6anXiZoifcx2WmHTR71cyigUfqPAqMCm8HvPutu8Jm/FHs5mxgO+7cYZ8FHlrMhwPnn90ED55IaYIzIOobaabd17KSd1lJGAMjJpHGzAONUNSS60XU8uJfnsalgqu99F2zQfqcDDiPrit0mD8o8a6EH9ljt23Sap0MFWY+ckGP/z1VR5zDFO8w==</SignatureValue><KeyInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><X509Data><X509SubjectName>E=demo@llama.pe, CN=NOMBRE REPRESENTANTE LEGAL - CERTIFICADO PARA DEMOSTRACIÓN, OU=DNI 9999999 RUC 20606022779 - CERTIFICADO PARA DEMOSTRACIÓN, O=TU EMPRESA S.A., L=LIMA, S=LIMA, C=PE, DC=LLAMA.PE SA</X509SubjectName><X509Certificate>MIIFCDCCA/CgAwIBAgIJANR7qWRxreexMA0GCSqGSIb3DQEBCwUAMIIBDTEbMBkGCgmSJomT8ixkARkWC0xMQU1BLlBFIFNBMQswCQYDVQQGEwJQRTENMAsGA1UECAwETElNQTENMAsGA1UEBwwETElNQTEYMBYGA1UECgwPVFUgRU1QUkVTQSBTLkEuMUUwQwYDVQQLDDxETkkgOTk5OTk5OSBSVUMgMjA2MDYwMjI3NzkgLSBDRVJUSUZJQ0FETyBQQVJBIERFTU9TVFJBQ0nDk04xRDBCBgNVBAMMO05PTUJSRSBSRVBSRVNFTlRBTlRFIExFR0FMIC0gQ0VSVElGSUNBRE8gUEFSQSBERU1PU1RSQUNJw5NOMRwwGgYJKoZIhvcNAQkBFg1kZW1vQGxsYW1hLnBlMB4XDTIyMTAwNTE5MzgyMloXDTI0MTAwNDE5MzgyMlowggENMRswGQYKCZImiZPyLGQBGRYLTExBTUEuUEUgU0ExCzAJBgNVBAYTAlBFMQ0wCwYDVQQIDARMSU1BMQ0wCwYDVQQHDARMSU1BMRgwFgYDVQQKDA9UVSBFTVBSRVNBIFMuQS4xRTBDBgNVBAsMPEROSSA5OTk5OTk5IFJVQyAyMDYwNjAyMjc3OSAtIENFUlRJRklDQURPIFBBUkEgREVNT1NUUkFDScOTTjFEMEIGA1UEAww7Tk9NQlJFIFJFUFJFU0VOVEFOVEUgTEVHQUwgLSBDRVJUSUZJQ0FETyBQQVJBIERFTU9TVFJBQ0nDk04xHDAaBgkqhkiG9w0BCQEWDWRlbW9AbGxhbWEucGUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC/P6G4EKmPA8542sqfRG26DH4a/slKLw9haqD0HyUqyn+xPkYm6Jc87WHhCLqaexkn5bJzEsqpZkV5/gHhs4qMkXeSArtTmu2RKQ16NoVfOzOszzyVEdZ/mqDeaPTVaDOIpeXdwCtWyYHTcHznP7AFSuivR5cKEO1p8zN/yB9jYyeT1Z8zf3OmcaKrzPjIvzJgM+NirPL1kOUGlKs1ZTe6MNkrNelF2ZqR5LIJWdWB6kOQXFMPF34l52tVIWHqIVjbOz6Je8NCEy/T+BjSZK2igPmEXC23GiD8q/LylO2IeddQ3K53EZKWzJX5O5EohaukpengcoMKFGT0g+TBHL8/AgMBAAGjZzBlMB0GA1UdDgQWBBR7sP74vTIIILVzLIyPqtXXuGIcLzAfBgNVHSMEGDAWgBR7sP74vTIIILVzLIyPqtXXuGIcLzATBgNVHSUEDDAKBggrBgEFBQcDATAOBgNVHQ8BAf8EBAMCB4AwDQYJKoZIhvcNAQELBQADggEBAAZjOR23wD7onC4xQoKNZQFzF5eodR4QA2n4+2PXJPW7kIFF+xZLdV2ENU93m49u+p9qDE9uK0QQUW7B8hYrZp37BR9Nw8CAGcwOqVQgAs6PubHNU7C2a0vM8eRcEam9pVn3i0MemVS5kOF2WDQvvzYkk9xOUqNAhZelkpa73sn3YckGTV5ltXKSMzAFlx0EJYIRC5u2x3MzxVtkFX7OQcvRftNmrQnV878vXKW+kw6uJIhXFjor//yzH3eLfs5N8OoCoKDrmQSZ5cz1UXQu7Ftlw6q7eAFSsRH05n9ZJJwXvM9cq909hce5LnkDoGkj8N72AicKcJz2WVw2SiIgzH8=</X509Certificate></X509Data></KeyInfo></ds:Signature></ext:ExtensionContent></ext:UBLExtension></ext:UBLExtensions><cbc:UBLVersionID>2.1</cbc:UBLVersionID><cbc:CustomizationID>2.0</cbc:CustomizationID><cbc:ID>FA01-00000001</cbc:ID><cbc:IssueDate>2022-10-15</cbc:IssueDate><cbc:IssueTime>19:28:28.2980000Z</cbc:IssueTime><cbc:DueDate>2022-10-15</cbc:DueDate><cbc:InvoiceTypeCode listID=\"0101\">01</cbc:InvoiceTypeCode><cbc:Note languageLocaleID=\"1000\">MONTO EN SOLES</cbc:Note><cbc:DocumentCurrencyCode>PEN</cbc:DocumentCurrencyCode><cac:Signature><cbc:ID>20606022779</cbc:ID><cbc:Note>Mabe by SWIFTLINE SAC</cbc:Note><cac:SignatoryParty><cac:PartyIdentification><cbc:ID>20606022779</cbc:ID></cac:PartyIdentification><cac:PartyName><cbc:Name>SWIFTLINE SAC</cbc:Name></cac:PartyName></cac:SignatoryParty></cac:Signature><cac:AccountingSupplierParty><cac:Party><cac:PartyIdentification><cbc:ID schemeID=\"6\">20606022779</cbc:ID></cac:PartyIdentification><cac:PartyName><cbc:Name>SWIFTLINE SAC</cbc:Name></cac:PartyName><cac:PartyLegalEntity><cbc:RegistrationName>20606022779</cbc:RegistrationName><cac:RegistrationAddress><cbc:ID>220901</cbc:ID><cbc:AddressTypeCode>0000</cbc:AddressTypeCode><cbc:CityName>SAN MARTIN</cbc:CityName><cbc:CountrySubentity>SAN MARTIN</cbc:CountrySubentity><cbc:District>TARAPOTO</cbc:District><cac:AddressLine><cbc:Line>PSJE. LIMATAMBO 121</cbc:Line></cac:AddressLine><cac:Country><cbc:IdentificationCode>PE</cbc:IdentificationCode></cac:Country></cac:RegistrationAddress></cac:PartyLegalEntity></cac:Party></cac:AccountingSupplierParty><cac:AccountingCustomerParty><cac:Party><cac:PartyIdentification><cbc:ID schemeID=\"6\">20450150844</cbc:ID></cac:PartyIdentification><cac:PartyLegalEntity><cbc:RegistrationName>EDUCOM SAC</cbc:RegistrationName><cac:RegistrationAddress><cac:AddressLine><cbc:Line>PSJE. LIMATAMBO 121, TARAPOTO, SAN MARTIN</cbc:Line></cac:AddressLine></cac:RegistrationAddress></cac:PartyLegalEntity></cac:Party></cac:AccountingCustomerParty><cac:PaymentTerms><cbc:ID>FormaPago</cbc:ID><cbc:PaymentMeansID>Contado</cbc:PaymentMeansID></cac:PaymentTerms><cac:TaxTotal><cbc:TaxAmount currencyID=\"PEN\">3.6</cbc:TaxAmount><cac:TaxSubtotal><cbc:TaxableAmount currencyID=\"PEN\">20</cbc:TaxableAmount><cbc:TaxAmount currencyID=\"PEN\">3.6</cbc:TaxAmount><cac:TaxCategory><cac:TaxScheme><cbc:ID>1000</cbc:ID><cbc:Name>IGV</cbc:Name><cbc:TaxTypeCode>VAT</cbc:TaxTypeCode></cac:TaxScheme></cac:TaxCategory></cac:TaxSubtotal></cac:TaxTotal><cac:LegalMonetaryTotal><cbc:LineExtensionAmount currencyID=\"PEN\">20</cbc:LineExtensionAmount><cbc:TaxInclusiveAmount currencyID=\"PEN\">23.6</cbc:TaxInclusiveAmount><cbc:AllowanceTotalAmount currencyID=\"PEN\">0</cbc:AllowanceTotalAmount><cbc:ChargeTotalAmount currencyID=\"PEN\">0</cbc:ChargeTotalAmount><cbc:PrepaidAmount currencyID=\"PEN\">0</cbc:PrepaidAmount><cbc:PayableAmount currencyID=\"PEN\">23.6</cbc:PayableAmount></cac:LegalMonetaryTotal><cac:InvoiceLine><cbc:ID>1</cbc:ID><cbc:InvoicedQuantity unitCode=\"NIU\">1</cbc:InvoicedQuantity><cbc:LineExtensionAmount currencyID=\"PEN\">20</cbc:LineExtensionAmount><cac:PricingReference><cac:AlternativeConditionPrice><cbc:PriceAmount currencyID=\"PEN\">23.6</cbc:PriceAmount><cbc:PriceTypeCode>01</cbc:PriceTypeCode></cac:AlternativeConditionPrice></cac:PricingReference><cac:TaxTotal><cbc:TaxAmount currencyID=\"PEN\">3.6</cbc:TaxAmount><cac:TaxSubtotal><cbc:TaxableAmount currencyID=\"PEN\">20</cbc:TaxableAmount><cbc:TaxAmount currencyID=\"PEN\">3.6</cbc:TaxAmount><cac:TaxCategory><cbc:Percent>18</cbc:Percent><cbc:TaxExemptionReasonCode>10</cbc:TaxExemptionReasonCode><cac:TaxScheme><cbc:ID>1000</cbc:ID><cbc:Name>IGV</cbc:Name><cbc:TaxTypeCode>VAT</cbc:TaxTypeCode></cac:TaxScheme></cac:TaxCategory></cac:TaxSubtotal></cac:TaxTotal><cac:Item><cbc:Description>Teclado Generico Logitech</cbc:Description><cac:SellersItemIdentification><cbc:ID>1</cbc:ID></cac:SellersItemIdentification><cac:CommodityClassification><cbc:ItemClassificationCode listID=\"UNSPSC\" listAgencyName=\"GS1 US\" listName=\"Item Classification\">25172405</cbc:ItemClassificationCode></cac:CommodityClassification></cac:Item><cac:Price><cbc:PriceAmount currencyID=\"PEN\">20</cbc:PriceAmount></cac:Price></cac:InvoiceLine></Invoice>",
            IssuerId = _idIssuer1,
            CustomizationId = "2.0",
            UblVersionId = "2.1",
            SunatResponse = new byte[3],
            Observations = "LA factura ha sido aceptada",
            InvoiceDetail = new InvoiceDetail
            {
                Id = Guid.NewGuid(),
                Serie = "FA",
                SerialNumber = 1,
                CorrelativeNumber = 1,
                CurrencyCode = "PEN",
                OperationType = "0101",
                DocumentType = "03"
            }
        });

        Context.Invoices.Add(new Entities.Models.Invoice
        {
            Id = _idInvoice2,
            InvoiceXml = "<?xml version=\"1.0\" encoding=\"utf-16\"?><Invoice xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:cac=\"urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2\" xmlns:cbc=\"urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2\" xmlns:ccts=\"urn:un:unece:uncefact:documentation:2\" xmlns:ds=\"http://www.w3.org/2000/09/xmldsig#\" xmlns:ext=\"urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2\" xmlns:qdt=\"urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2\" xmlns:udt=\"urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2\" xmlns=\"urn:oasis:names:specification:ubl:schema:xsd:Invoice-2\"><ext:UBLExtensions><ext:UBLExtension> <ext:ExtensionContent><ds:Signature Id=\"SignSUNAT\"><SignedInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315\" /><SignatureMethod Algorithm=\"http://www.w3.org/2001/04/xmldsig-more#rsa-sha256\" /><Reference URI=\"\"><Transforms><Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\" /></Transforms><DigestMethod Algorithm=\"http://www.w3.org/2001/04/xmlenc#sha256\" /><DigestValue>XYPAYJ1GtdLQkCx3u1RSdbQgseul4U/E82Z9ExL0hPg=</DigestValue></Reference></SignedInfo><SignatureValue xmlns=\"http://www.w3.org/2000/09/xmldsig#\">uh/W4GE62hvCneaYX+twM3gyoLW6o5/+FUgzi91W9JMs2VB67fAvAo4hl2v2Ahal4qv5WgDIhVjwIMbEJZra2x+7q8u/nrf0Bc49wM6mRitoFTVHT8OoK2K8LxJhP4q5iPRSCuxegUvNMd6hebGoNducPNGAyM0n6anXiZoifcx2WmHTR71cyigUfqPAqMCm8HvPutu8Jm/FHs5mxgO+7cYZ8FHlrMhwPnn90ED55IaYIzIOobaabd17KSd1lJGAMjJpHGzAONUNSS60XU8uJfnsalgqu99F2zQfqcDDiPrit0mD8o8a6EH9ljt23Sap0MFWY+ckGP/z1VR5zDFO8w==</SignatureValue><KeyInfo xmlns=\"http://www.w3.org/2000/09/xmldsig#\"><X509Data><X509SubjectName>E=demo@llama.pe, CN=NOMBRE REPRESENTANTE LEGAL - CERTIFICADO PARA DEMOSTRACIÓN, OU=DNI 9999999 RUC 20606022779 - CERTIFICADO PARA DEMOSTRACIÓN, O=TU EMPRESA S.A., L=LIMA, S=LIMA, C=PE, DC=LLAMA.PE SA</X509SubjectName><X509Certificate>MIIFCDCCA/CgAwIBAgIJANR7qWRxreexMA0GCSqGSIb3DQEBCwUAMIIBDTEbMBkGCgmSJomT8ixkARkWC0xMQU1BLlBFIFNBMQswCQYDVQQGEwJQRTENMAsGA1UECAwETElNQTENMAsGA1UEBwwETElNQTEYMBYGA1UECgwPVFUgRU1QUkVTQSBTLkEuMUUwQwYDVQQLDDxETkkgOTk5OTk5OSBSVUMgMjA2MDYwMjI3NzkgLSBDRVJUSUZJQ0FETyBQQVJBIERFTU9TVFJBQ0nDk04xRDBCBgNVBAMMO05PTUJSRSBSRVBSRVNFTlRBTlRFIExFR0FMIC0gQ0VSVElGSUNBRE8gUEFSQSBERU1PU1RSQUNJw5NOMRwwGgYJKoZIhvcNAQkBFg1kZW1vQGxsYW1hLnBlMB4XDTIyMTAwNTE5MzgyMloXDTI0MTAwNDE5MzgyMlowggENMRswGQYKCZImiZPyLGQBGRYLTExBTUEuUEUgU0ExCzAJBgNVBAYTAlBFMQ0wCwYDVQQIDARMSU1BMQ0wCwYDVQQHDARMSU1BMRgwFgYDVQQKDA9UVSBFTVBSRVNBIFMuQS4xRTBDBgNVBAsMPEROSSA5OTk5OTk5IFJVQyAyMDYwNjAyMjc3OSAtIENFUlRJRklDQURPIFBBUkEgREVNT1NUUkFDScOTTjFEMEIGA1UEAww7Tk9NQlJFIFJFUFJFU0VOVEFOVEUgTEVHQUwgLSBDRVJUSUZJQ0FETyBQQVJBIERFTU9TVFJBQ0nDk04xHDAaBgkqhkiG9w0BCQEWDWRlbW9AbGxhbWEucGUwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC/P6G4EKmPA8542sqfRG26DH4a/slKLw9haqD0HyUqyn+xPkYm6Jc87WHhCLqaexkn5bJzEsqpZkV5/gHhs4qMkXeSArtTmu2RKQ16NoVfOzOszzyVEdZ/mqDeaPTVaDOIpeXdwCtWyYHTcHznP7AFSuivR5cKEO1p8zN/yB9jYyeT1Z8zf3OmcaKrzPjIvzJgM+NirPL1kOUGlKs1ZTe6MNkrNelF2ZqR5LIJWdWB6kOQXFMPF34l52tVIWHqIVjbOz6Je8NCEy/T+BjSZK2igPmEXC23GiD8q/LylO2IeddQ3K53EZKWzJX5O5EohaukpengcoMKFGT0g+TBHL8/AgMBAAGjZzBlMB0GA1UdDgQWBBR7sP74vTIIILVzLIyPqtXXuGIcLzAfBgNVHSMEGDAWgBR7sP74vTIIILVzLIyPqtXXuGIcLzATBgNVHSUEDDAKBggrBgEFBQcDATAOBgNVHQ8BAf8EBAMCB4AwDQYJKoZIhvcNAQELBQADggEBAAZjOR23wD7onC4xQoKNZQFzF5eodR4QA2n4+2PXJPW7kIFF+xZLdV2ENU93m49u+p9qDE9uK0QQUW7B8hYrZp37BR9Nw8CAGcwOqVQgAs6PubHNU7C2a0vM8eRcEam9pVn3i0MemVS5kOF2WDQvvzYkk9xOUqNAhZelkpa73sn3YckGTV5ltXKSMzAFlx0EJYIRC5u2x3MzxVtkFX7OQcvRftNmrQnV878vXKW+kw6uJIhXFjor//yzH3eLfs5N8OoCoKDrmQSZ5cz1UXQu7Ftlw6q7eAFSsRH05n9ZJJwXvM9cq909hce5LnkDoGkj8N72AicKcJz2WVw2SiIgzH8=</X509Certificate></X509Data></KeyInfo></ds:Signature></ext:ExtensionContent></ext:UBLExtension></ext:UBLExtensions><cbc:UBLVersionID>2.1</cbc:UBLVersionID><cbc:CustomizationID>2.0</cbc:CustomizationID><cbc:ID>FA01-00000001</cbc:ID><cbc:IssueDate>2022-10-15</cbc:IssueDate><cbc:IssueTime>19:28:28.2980000Z</cbc:IssueTime><cbc:DueDate>2022-10-15</cbc:DueDate><cbc:InvoiceTypeCode listID=\"0101\">01</cbc:InvoiceTypeCode><cbc:Note languageLocaleID=\"1000\">MONTO EN SOLES</cbc:Note><cbc:DocumentCurrencyCode>PEN</cbc:DocumentCurrencyCode><cac:Signature><cbc:ID>20606022779</cbc:ID><cbc:Note>Mabe by SWIFTLINE SAC</cbc:Note><cac:SignatoryParty><cac:PartyIdentification><cbc:ID>20606022779</cbc:ID></cac:PartyIdentification><cac:PartyName><cbc:Name>SWIFTLINE SAC</cbc:Name></cac:PartyName></cac:SignatoryParty></cac:Signature><cac:AccountingSupplierParty><cac:Party><cac:PartyIdentification><cbc:ID schemeID=\"6\">20606022779</cbc:ID></cac:PartyIdentification><cac:PartyName><cbc:Name>SWIFTLINE SAC</cbc:Name></cac:PartyName><cac:PartyLegalEntity><cbc:RegistrationName>20606022779</cbc:RegistrationName><cac:RegistrationAddress><cbc:ID>220901</cbc:ID><cbc:AddressTypeCode>0000</cbc:AddressTypeCode><cbc:CityName>SAN MARTIN</cbc:CityName><cbc:CountrySubentity>SAN MARTIN</cbc:CountrySubentity><cbc:District>TARAPOTO</cbc:District><cac:AddressLine><cbc:Line>PSJE. LIMATAMBO 121</cbc:Line></cac:AddressLine><cac:Country><cbc:IdentificationCode>PE</cbc:IdentificationCode></cac:Country></cac:RegistrationAddress></cac:PartyLegalEntity></cac:Party></cac:AccountingSupplierParty><cac:AccountingCustomerParty><cac:Party><cac:PartyIdentification><cbc:ID schemeID=\"6\">20450150844</cbc:ID></cac:PartyIdentification><cac:PartyLegalEntity><cbc:RegistrationName>EDUCOM SAC</cbc:RegistrationName><cac:RegistrationAddress><cac:AddressLine><cbc:Line>PSJE. LIMATAMBO 121, TARAPOTO, SAN MARTIN</cbc:Line></cac:AddressLine></cac:RegistrationAddress></cac:PartyLegalEntity></cac:Party></cac:AccountingCustomerParty><cac:PaymentTerms><cbc:ID>FormaPago</cbc:ID><cbc:PaymentMeansID>Contado</cbc:PaymentMeansID></cac:PaymentTerms><cac:TaxTotal><cbc:TaxAmount currencyID=\"PEN\">3.6</cbc:TaxAmount><cac:TaxSubtotal><cbc:TaxableAmount currencyID=\"PEN\">20</cbc:TaxableAmount><cbc:TaxAmount currencyID=\"PEN\">3.6</cbc:TaxAmount><cac:TaxCategory><cac:TaxScheme><cbc:ID>1000</cbc:ID><cbc:Name>IGV</cbc:Name><cbc:TaxTypeCode>VAT</cbc:TaxTypeCode></cac:TaxScheme></cac:TaxCategory></cac:TaxSubtotal></cac:TaxTotal><cac:LegalMonetaryTotal><cbc:LineExtensionAmount currencyID=\"PEN\">20</cbc:LineExtensionAmount><cbc:TaxInclusiveAmount currencyID=\"PEN\">23.6</cbc:TaxInclusiveAmount><cbc:AllowanceTotalAmount currencyID=\"PEN\">0</cbc:AllowanceTotalAmount><cbc:ChargeTotalAmount currencyID=\"PEN\">0</cbc:ChargeTotalAmount><cbc:PrepaidAmount currencyID=\"PEN\">0</cbc:PrepaidAmount><cbc:PayableAmount currencyID=\"PEN\">23.6</cbc:PayableAmount></cac:LegalMonetaryTotal><cac:InvoiceLine><cbc:ID>1</cbc:ID><cbc:InvoicedQuantity unitCode=\"NIU\">1</cbc:InvoicedQuantity><cbc:LineExtensionAmount currencyID=\"PEN\">20</cbc:LineExtensionAmount><cac:PricingReference><cac:AlternativeConditionPrice><cbc:PriceAmount currencyID=\"PEN\">23.6</cbc:PriceAmount><cbc:PriceTypeCode>01</cbc:PriceTypeCode></cac:AlternativeConditionPrice></cac:PricingReference><cac:TaxTotal><cbc:TaxAmount currencyID=\"PEN\">3.6</cbc:TaxAmount><cac:TaxSubtotal><cbc:TaxableAmount currencyID=\"PEN\">20</cbc:TaxableAmount><cbc:TaxAmount currencyID=\"PEN\">3.6</cbc:TaxAmount><cac:TaxCategory><cbc:Percent>18</cbc:Percent><cbc:TaxExemptionReasonCode>10</cbc:TaxExemptionReasonCode><cac:TaxScheme><cbc:ID>1000</cbc:ID><cbc:Name>IGV</cbc:Name><cbc:TaxTypeCode>VAT</cbc:TaxTypeCode></cac:TaxScheme></cac:TaxCategory></cac:TaxSubtotal></cac:TaxTotal><cac:Item><cbc:Description>Teclado Generico Logitech</cbc:Description><cac:SellersItemIdentification><cbc:ID>1</cbc:ID></cac:SellersItemIdentification><cac:CommodityClassification><cbc:ItemClassificationCode listID=\"UNSPSC\" listAgencyName=\"GS1 US\" listName=\"Item Classification\">25172405</cbc:ItemClassificationCode></cac:CommodityClassification></cac:Item><cac:Price><cbc:PriceAmount currencyID=\"PEN\">20</cbc:PriceAmount></cac:Price></cac:InvoiceLine></Invoice>",
            IssuerId = _idIssuer2,
            CustomizationId = "2.0",
            UblVersionId = "2.1",
            SunatResponse = new byte[3],
            Observations = "LA factura ha sido aceptada",
            InvoiceDetail = new InvoiceDetail
            {
                Id = Guid.NewGuid(),
                Serie = "FA",
                SerialNumber = 1,
                CorrelativeNumber = 2,
                CurrencyCode = "PEN",
                OperationType = "0101",
                DocumentType = "03"
            }
        });

        Context.SaveChanges();
    }
}