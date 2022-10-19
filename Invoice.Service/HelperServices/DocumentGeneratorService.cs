using Invoice.Entities.Models;
using Invoice.Service.Contracts.HelperServices;
using Invoice.Shared;
using Invoice.Shared.Request;
using UBLSunatPE;

namespace Invoice.Service.HelperServices;

public class DocumentGeneratorService : IDocumentGeneratorService
{
    public InvoiceType GenerateInvoiceType(InvoiceRequest request, Issuer issuer)
    {
        var invoiceType = new InvoiceType
        {
            #region Headers

            Cac = SD.XmlnsCac,
            Cbc = SD.XmlnsCbc,
            Ccts = SD.XmlnsCcts,
            Ds = SD.XmlnsDs,
            Ext = SD.XmlnsExt,
            Qdt = SD.XmlnsQdt,
            Udt = SD.XmlnsUdt,

            #endregion

            #region Ubl and Schema

            UBLVersionID = new UBLVersionIDType { Value = request.UblScheme.UblVersionId },
            CustomizationID = new CustomizationIDType { Value = request.UblScheme.CustomizationId },

            #endregion

            #region Certificate

            UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType()
            },

            #endregion

            #region Serial Number

            ID = new IDType { Value = $"{request.InvoiceDetail.Serie}{request.InvoiceDetail.SerialNumber.ToString("00")}-{request.InvoiceDetail.CorrelativeNumber}" },

            #endregion

            #region Dates

            IssueDate = new IssueDateType { Value = request.IssueDate },
            IssueTime = new IssueTimeType { Value = request.IssueDate },
            DueDate = new DueDateType { Value = request.IssueDate },

            #endregion

            #region Invoice

            InvoiceTypeCode = new InvoiceTypeCodeType
            {
                listID = request.InvoiceDetail.OperationType, //Catalog 51
                Value = request.InvoiceDetail.DocumentType //Catalog 1
            },

            Note = new NoteType[]
            {
                new NoteType
                {
                    languageLocaleID = request.InvoiceDetail.NoteTypeCode, //Catalogo 52
                    Value = request.InvoiceDetail.NoteType //e. "MONTO EN SOLES"
                }
            },

            DocumentCurrencyCode = new DocumentCurrencyCodeType
            {
                Value = request.InvoiceDetail.CurrencyCode, //ISO 4217 e. "PEN"
            },

            #endregion

            #region Issuer Information

            Signature = new SignatureType[]
            {
                new SignatureType
                {
                    ID = new IDType { Value = issuer.IssuerId.ToString() },
                    SignatoryParty = new PartyType
                    {
                        PartyIdentification = new PartyIdentificationType[]
                        {
                            new PartyIdentificationType { ID = new IDType { Value = issuer.IssuerId.ToString() }}
                        },
                        PartyName = new PartyNameType[]
                        {
                            new PartyNameType { Name = new NameType1 { Value = issuer.IssuerName }}
                        },
                    },
                    Note = new NoteType [] { new NoteType { Value = $"Mabe by {issuer.IssuerName}" } }
                }
            },

            AccountingSupplierParty = new SupplierPartyType
            {
                Party = new PartyType
                {
                    PartyIdentification = new PartyIdentificationType[]
                    {
                        new PartyIdentificationType
                        {
                            ID = new IDType
                            {
                                schemeID = issuer.IssuerType, //Catalogo 6
                                Value = issuer.IssuerId.ToString(),
                            },

                        }
                    },
                    PartyName = new PartyNameType[] { new PartyNameType { Name = new NameType1 { Value = issuer.IssuerName } } },
                    PartyLegalEntity = new PartyLegalEntityType[]
                    {
                        new PartyLegalEntityType
                        {
                            RegistrationName = new RegistrationNameType { Value = issuer.IssuerId.ToString() },
                            RegistrationAddress = new AddressType
                            {
                                ID = new IDType { Value = issuer.GeoCode },
                                AddressTypeCode = new AddressTypeCodeType { Value = issuer.EstablishmentCode }, //Default "0000",
                                CityName = new CityNameType { Value = issuer.Department },
                                CountrySubentity = new CountrySubentityType { Value = issuer.Province },
                                District = new DistrictType { Value = issuer.District },
                                AddressLine = new AddressLineType[]
                                {
                                    new AddressLineType { Line = new LineType { Value = issuer.Address }},
                                },
                                Country = new CountryType { IdentificationCode = new IdentificationCodeType { Value = "PE" }} //It's always going to be PE
                            }
                        }
                    }
                }
            },

            #endregion

            #region Receiver Information

            AccountingCustomerParty = new CustomerPartyType
            {
                Party = new PartyType
                {
                    PartyIdentification = new PartyIdentificationType[]
                    {
                        new PartyIdentificationType
                        {
                            ID = new IDType
                            {
                                schemeID = request.Receiver.ReceiverType, //Catalog 6
                                Value = request.Receiver.ReceiverId.ToString(),
                            }
                        }
                    },
                    PartyLegalEntity = new PartyLegalEntityType[]
                    {
                        new PartyLegalEntityType
                        {
                            RegistrationName = new RegistrationNameType { Value = request.Receiver.ReceiverName },
                            RegistrationAddress = new AddressType
                            {
                                AddressLine = new AddressLineType[]
                                {
                                    new AddressLineType { Line = new LineType { Value = request.Receiver.FullAddress } }
                                }
                            }
                        }
                    }
                }
            },

            #endregion
        };

        #region Payment Terms

        var paymentTermList = new List<PaymentTermsType>();
        foreach (var paymentTerm in request.PaymentTerms)
        {
            switch (paymentTerm.PaymentType)
            {
                case "Contado":
                    paymentTermList.Add(new PaymentTermsType
                    {
                        ID = new IDType { Value = paymentTerm.PaymentId },
                        PaymentMeansID = new PaymentMeansIDType[] { new PaymentMeansIDType { Value = paymentTerm.PaymentType } }
                    });
                    break;
                case "Credito":
                    paymentTermList.Add(new PaymentTermsType
                    {
                        ID = new IDType { Value = paymentTerm.PaymentId },
                        PaymentMeansID = new PaymentMeansIDType[] { new PaymentMeansIDType { Value = paymentTerm.PaymentType } },
                        Amount = new AmountType2 { currencyID = request.InvoiceDetail.CurrencyCode, Value = paymentTerm.Amount },
                    });
                    break;
                case string a when a.Contains("Cuota"):
                    paymentTermList.Add(new PaymentTermsType
                    {
                        ID = new IDType { Value = paymentTerm.PaymentId },
                        PaymentMeansID = new PaymentMeansIDType[] { new PaymentMeansIDType { Value = paymentTerm.PaymentType } },
                        Amount = new AmountType2 { currencyID = request.InvoiceDetail.CurrencyCode, Value = paymentTerm.Amount },
                        PaymentDueDate = new PaymentDueDateType { Value = paymentTerm.DueDate }
                    });
                    break;
                default:
                    break;
            }
        }
        invoiceType.PaymentTerms = paymentTermList.ToArray();

        #endregion

        #region Amount and Taxes

        var taxSubTotals = new List<TaxSubtotalType>();
        foreach (var taxSubTotal in request.TaxSubTotal)
        {
            taxSubTotals.Add(new TaxSubtotalType
            {
                TaxableAmount = new TaxableAmountType { currencyID = request.InvoiceDetail.CurrencyCode, Value = taxSubTotal.TaxableAmount },
                TaxAmount = new TaxAmountType { currencyID = request.InvoiceDetail.CurrencyCode, Value = taxSubTotal.TaxAmount },
                TaxCategory = new TaxCategoryType
                {
                    TaxScheme = new TaxSchemeType //Catalog 5
                    {
                        ID = new IDType { Value = taxSubTotal.TaxCategory.TaxId },
                        Name = new NameType1 { Value = taxSubTotal.TaxCategory.TaxName },
                        TaxTypeCode = new TaxTypeCodeType { Value = taxSubTotal.TaxCategory.TaxCode }
                    }
                }
            });
        }

        invoiceType.TaxTotal = new TaxTotalType[]
        {
            new TaxTotalType
            {
                TaxAmount = new TaxAmountType { currencyID = request.InvoiceDetail.CurrencyCode, Value = request.TaxTotalAmount },
                TaxSubtotal = taxSubTotals.ToArray()
            }
        };

        invoiceType.LegalMonetaryTotal = new MonetaryTotalType
        {
            LineExtensionAmount = new LineExtensionAmountType
            {
                currencyID = request.InvoiceDetail.CurrencyCode,
                Value = request.TaxSubTotal.Sum(x => x.TaxableAmount)
            },
            TaxInclusiveAmount = new TaxInclusiveAmountType
            {
                currencyID = request.InvoiceDetail.CurrencyCode,
                Value = request.TotalAmount
            },
            ChargeTotalAmount = new ChargeTotalAmountType
            {
                currencyID = request.InvoiceDetail.CurrencyCode,
                Value = 0,
            },
            PayableAmount = new PayableAmountType
            {
                currencyID = request.InvoiceDetail.CurrencyCode,
                Value = request.TotalAmount
            },
            AllowanceTotalAmount = new AllowanceTotalAmountType
            {
                currencyID = request.InvoiceDetail.CurrencyCode,
                Value = 0,
            },
            PrepaidAmount = new PrepaidAmountType
            {
                currencyID = request.InvoiceDetail.CurrencyCode,
                Value = 0,
            }
        };

        #endregion

        #region Products Detail

        var invoiceLineTypes = new List<InvoiceLineType>();
        var count = 1;

        foreach (var detail in request.ProductsDetail)
        {
            invoiceLineTypes.Add(new InvoiceLineType
            {
                ID = new IDType { Value = count.ToString() }, //Count
                InvoicedQuantity = new InvoicedQuantityType
                {
                    unitCode = detail.UnitCode,
                    Value = detail.Quantity
                },
                LineExtensionAmount = new LineExtensionAmountType
                {
                    currencyID = request.InvoiceDetail.CurrencyCode,
                    Value = detail.Quantity * detail.UnitPrice
                },
                PricingReference = new PricingReferenceType
                {
                    AlternativeConditionPrice = new PriceType[]
                    {
                        new PriceType
                        {
                            PriceAmount = new PriceAmountType
                            {
                                currencyID = request.InvoiceDetail.CurrencyCode,
                                Value = detail.UnitPrice + detail.TaxAmount,
                            },
                            PriceTypeCode = new PriceTypeCodeType
                            {
                                Value = detail.PriceType //Catalog 16
                            }
                        }
                    }
                },
                TaxTotal = new TaxTotalType[]
                {
                    new TaxTotalType
                    {
                        TaxAmount = new TaxAmountType
                        {
                            currencyID = request.InvoiceDetail.CurrencyCode,
                            Value = detail.TaxAmount * detail.Quantity
                        },
                        TaxSubtotal = new TaxSubtotalType[]
                        {
                            new TaxSubtotalType
                            {
                                TaxableAmount = new TaxableAmountType
                                {
                                    currencyID = request.InvoiceDetail.CurrencyCode,
                                    Value = detail.UnitPrice * detail.Quantity,
                                },
                                TaxAmount = new TaxAmountType
                                {
                                    currencyID = request.InvoiceDetail.CurrencyCode,
                                    Value = detail.TaxAmount * detail.Quantity
                                },
                                TaxCategory = new TaxCategoryType
                                {
                                    Percent = new PercentType1 { Value = detail.TaxPercentage },
                                    TaxExemptionReasonCode = new TaxExemptionReasonCodeType
                                    {
                                        Value = detail.TaxExemptionReasonCode, //Catalog 7
                                    },
                                    //Catalog 5
                                    TaxScheme = new TaxSchemeType
                                    {
                                        ID = new IDType { Value = detail.TaxId },
                                        Name = new NameType1 { Value = detail.TaxName },
                                        TaxTypeCode = new TaxTypeCodeType { Value = detail.TaxCode },
                                    }
                                }
                            }
                        }
                    }
                },
                Item = new ItemType
                {
                    Description = new DescriptionType[] { new DescriptionType() { Value = detail.Description } },
                    SellersItemIdentification = new ItemIdentificationType { ID = new IDType { Value = detail.SellerItemIdentification } },
                    CommodityClassification = new CommodityClassificationType[]
                    {
                        new CommodityClassificationType
                        {
                            ItemClassificationCode = new ItemClassificationCodeType
                            {
                                listID = "UNSPSC",
                                listAgencyName = "GS1 US",
                                listName = "Item Classification",
                                Value = detail.ItemClassificationCode,
                            }
                        }
                    }
                },
                Price = new PriceType
                {
                    PriceAmount = new PriceAmountType { currencyID = request.InvoiceDetail.CurrencyCode, Value = detail.UnitPrice }
                }
            });
            count++;
        }
        invoiceType.InvoiceLine = invoiceLineTypes.ToArray();

        #endregion

        return invoiceType;
    }
}