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

            UBLVersionID = new UBLVersionIDType { Value = request.UblVersionId },
            CustomizationID = new CustomizationIDType { Value = request.CustomizationId },

            #endregion

            #region Certificate

            UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType()
            },

            #endregion

            #region Serial Number

            ID = new IDType { Value = $"{request.InvoiceDetail.Serie}{request.InvoiceDetail.SerialNumber.ToString("00")}-{request.InvoiceDetail.CorrelativeNumber.ToString("00000000")}" },

            #endregion

            #region Dates

            IssueDate = new IssueDateType { Value = request.IssueDate },
            IssueTime = new IssueTimeType { Value = request.IssueDate },

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
        foreach (var taxSubTotal in request.TaxSubTotals)
        {
            taxSubTotals.Add(new TaxSubtotalType
            {
                TaxableAmount = new TaxableAmountType { currencyID = request.InvoiceDetail.CurrencyCode, Value = taxSubTotal.TaxableAmount },
                TaxAmount = new TaxAmountType { currencyID = request.InvoiceDetail.CurrencyCode, Value = taxSubTotal.TaxAmount },
                TaxCategory = new TaxCategoryType
                {
                    TaxScheme = new TaxSchemeType //Catalog 5
                    {
                        ID = new IDType { Value = taxSubTotal.TaxId },
                        Name = new NameType1 { Value = taxSubTotal.TaxName },
                        TaxTypeCode = new TaxTypeCodeType { Value = taxSubTotal.TaxCode }
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
                Value = request.TaxSubTotals.Sum(x => x.TaxableAmount)
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

        foreach (var detail in request.ProductsDetails)
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

    public CreditNoteType GenerateCreditNoteType(NoteRequest request, Issuer issuer)
    {
        var debitNoteType = new CreditNoteType
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

            UBLVersionID = new UBLVersionIDType { Value = request.UblVersionId },
            CustomizationID = new CustomizationIDType { Value = request.CustomizationId },

            #endregion

            #region Certificate

            UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType()
            },

            #endregion

            #region Serial Number

            ID = new IDType { Value = $"{request.NoteDetail.Serie}{request.NoteDetail.SerialNumber.ToString("00")}-{request.NoteDetail.CorrelativeNumber.ToString("00000000")}" },

            #endregion

            #region Dates

            IssueDate = new IssueDateType { Value = request.IssueDate },
            IssueTime = new IssueTimeType { Value = request.IssueDate },

            #endregion

            #region Discrepancy Response

            DiscrepancyResponse = new ResponseType[]
            {
                new ResponseType
                {
                    ReferenceID = new ReferenceIDType { Value = $"{request.NoteDetail.InvoiceSerie}{request.NoteDetail.InvoiceSerialNumber.ToString("00")}-{request.NoteDetail.InvoiceCorrelativeNumber.ToString("00000000")}" },
                    ResponseCode = new ResponseCodeType { Value = request.NoteDetail.ResponseCode },
                    Description = new DescriptionType[] { new DescriptionType {  Value = request.NoteDetail.ResponseCodeDescription } }
                }
            },

            #endregion

            #region Billing Reference 

            BillingReference = new BillingReferenceType[]
            {
                new BillingReferenceType
                {
                    InvoiceDocumentReference = new DocumentReferenceType
                    {
                        ID = new IDType { Value = $"{request.NoteDetail.InvoiceSerie}{request.NoteDetail.InvoiceSerialNumber.ToString("00")}-{request.NoteDetail.InvoiceCorrelativeNumber.ToString("00000000")}" },
                        DocumentTypeCode = new DocumentTypeCodeType { Value = request.NoteDetail.InvoiceDocumentType }
                    }
                }
            },

            #endregion

            #region Invoice

            DocumentCurrencyCode = new DocumentCurrencyCodeType
            {
                Value = request.NoteDetail.CurrencyCode, //ISO 4217 e. "PEN"
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

        #region Amount and Taxes

        var taxSubTotals = new List<TaxSubtotalType>();
        foreach (var taxSubTotal in request.TaxSubTotals)
        {
            taxSubTotals.Add(new TaxSubtotalType
            {
                TaxableAmount = new TaxableAmountType { currencyID = request.NoteDetail.CurrencyCode, Value = taxSubTotal.TaxableAmount },
                TaxAmount = new TaxAmountType { currencyID = request.NoteDetail.CurrencyCode, Value = taxSubTotal.TaxAmount },
                TaxCategory = new TaxCategoryType
                {
                    TaxScheme = new TaxSchemeType //Catalog 5
                    {
                        ID = new IDType { Value = taxSubTotal.TaxId },
                        Name = new NameType1 { Value = taxSubTotal.TaxName },
                        TaxTypeCode = new TaxTypeCodeType { Value = taxSubTotal.TaxCode }
                    }
                }
            });
        }

        debitNoteType.TaxTotal = new TaxTotalType[]
        {
            new TaxTotalType
            {
                TaxAmount = new TaxAmountType { currencyID = request.NoteDetail.CurrencyCode, Value = request.TaxTotalAmount },
                TaxSubtotal = taxSubTotals.ToArray()
            }
        };

        #endregion

        #region Products Detail

        debitNoteType.LegalMonetaryTotal = new MonetaryTotalType
        {
            PayableAmount = new PayableAmountType
            {
                currencyID = request.NoteDetail.CurrencyCode,
                Value = request.TotalAmount,
            }
        };

        var invoiceLineTypes = new List<CreditNoteLineType>();
        var count = 1;

        foreach (var detail in request.ProductsDetails)
        {
            invoiceLineTypes.Add(new CreditNoteLineType
            {
                ID = new IDType { Value = count.ToString() }, //Count
                CreditedQuantity = new CreditedQuantityType
                {
                    unitCode = detail.UnitCode,
                    Value = detail.Quantity
                },
                LineExtensionAmount = new LineExtensionAmountType
                {
                    currencyID = request.NoteDetail.CurrencyCode,
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
                                currencyID = request.NoteDetail.CurrencyCode,
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
                            currencyID = request.NoteDetail.CurrencyCode,
                            Value = detail.TaxAmount * detail.Quantity
                        },
                        TaxSubtotal = new TaxSubtotalType[]
                        {
                            new TaxSubtotalType
                            {
                                TaxableAmount = new TaxableAmountType
                                {
                                    currencyID = request.NoteDetail.CurrencyCode,
                                    Value = detail.UnitPrice * detail.Quantity,
                                },
                                TaxAmount = new TaxAmountType
                                {
                                    currencyID = request.NoteDetail.CurrencyCode,
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
                    PriceAmount = new PriceAmountType { currencyID = request.NoteDetail.CurrencyCode, Value = detail.UnitPrice }
                }
            });
            count++;
        }
        debitNoteType.CreditNoteLine = invoiceLineTypes.ToArray();

        #endregion

        return debitNoteType;
    }

    public DebitNoteType GenerateDebitNoteType(NoteRequest request, Issuer issuer)
    {
        var debitNoteType = new DebitNoteType
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

            UBLVersionID = new UBLVersionIDType { Value = request.UblVersionId },
            CustomizationID = new CustomizationIDType { Value = request.CustomizationId },

            #endregion

            #region Certificate

            UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType()
            },

            #endregion

            #region Serial Number

            ID = new IDType { Value = $"{request.NoteDetail.Serie}{request.NoteDetail.SerialNumber.ToString("00")}-{request.NoteDetail.CorrelativeNumber.ToString("00000000")}" },

            #endregion

            #region Dates

            IssueDate = new IssueDateType { Value = request.IssueDate },
            IssueTime = new IssueTimeType { Value = request.IssueDate },

            #endregion

            #region Discrepancy Response

            DiscrepancyResponse = new ResponseType[]
            {
                new ResponseType
                {
                    ReferenceID = new ReferenceIDType { Value = $"{request.NoteDetail.InvoiceSerie}{request.NoteDetail.InvoiceSerialNumber.ToString("00")}-{request.NoteDetail.InvoiceCorrelativeNumber.ToString("00000000")}" },
                    ResponseCode = new ResponseCodeType { Value = request.NoteDetail.ResponseCode },
                    Description = new DescriptionType[] { new DescriptionType {  Value = request.NoteDetail.ResponseCodeDescription } }
                }
            },

            #endregion

            #region Billing Reference 

            BillingReference = new BillingReferenceType[]
            {
                new BillingReferenceType
                {
                    InvoiceDocumentReference = new DocumentReferenceType
                    {
                        ID = new IDType { Value = $"{request.NoteDetail.InvoiceSerie}{request.NoteDetail.InvoiceSerialNumber.ToString("00")}-{request.NoteDetail.InvoiceCorrelativeNumber.ToString("00000000")}" },
                        DocumentTypeCode = new DocumentTypeCodeType { Value = request.NoteDetail.InvoiceDocumentType }
                    }
                }
            },

            #endregion

            #region Invoice

            DocumentCurrencyCode = new DocumentCurrencyCodeType
            {
                Value = request.NoteDetail.CurrencyCode, //ISO 4217 e. "PEN"
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
                        Amount = new AmountType2 { currencyID = request.NoteDetail.CurrencyCode, Value = paymentTerm.Amount },
                    });
                    break;
                case string a when a.Contains("Cuota"):
                    paymentTermList.Add(new PaymentTermsType
                    {
                        ID = new IDType { Value = paymentTerm.PaymentId },
                        PaymentMeansID = new PaymentMeansIDType[] { new PaymentMeansIDType { Value = paymentTerm.PaymentType } },
                        Amount = new AmountType2 { currencyID = request.NoteDetail.CurrencyCode, Value = paymentTerm.Amount },
                        PaymentDueDate = new PaymentDueDateType { Value = paymentTerm.DueDate }
                    });
                    break;
                default:
                    break;
            }
        }
        debitNoteType.PaymentTerms = paymentTermList.ToArray();

        #endregion

        #region Amount and Taxes

        var taxSubTotals = new List<TaxSubtotalType>();
        foreach (var taxSubTotal in request.TaxSubTotals)
        {
            taxSubTotals.Add(new TaxSubtotalType
            {
                TaxableAmount = new TaxableAmountType { currencyID = request.NoteDetail.CurrencyCode, Value = taxSubTotal.TaxableAmount },
                TaxAmount = new TaxAmountType { currencyID = request.NoteDetail.CurrencyCode, Value = taxSubTotal.TaxAmount },
                TaxCategory = new TaxCategoryType
                {
                    TaxScheme = new TaxSchemeType //Catalog 5
                    {
                        ID = new IDType { Value = taxSubTotal.TaxId },
                        Name = new NameType1 { Value = taxSubTotal.TaxName },
                        TaxTypeCode = new TaxTypeCodeType { Value = taxSubTotal.TaxCode }
                    }
                }
            });
        }

        debitNoteType.TaxTotal = new TaxTotalType[]
        {
            new TaxTotalType
            {
                TaxAmount = new TaxAmountType { currencyID = request.NoteDetail.CurrencyCode, Value = request.TaxTotalAmount },
                TaxSubtotal = taxSubTotals.ToArray()
            }
        };

        #endregion

        #region Products Detail

        debitNoteType.RequestedMonetaryTotal = new MonetaryTotalType
        {
            PayableAmount = new PayableAmountType
            {
                currencyID = request.NoteDetail.CurrencyCode,
                Value = request.TotalAmount,
            }
        };

        var invoiceLineTypes = new List<DebitNoteLineType>();
        var count = 1;

        foreach (var detail in request.ProductsDetails)
        {
            invoiceLineTypes.Add(new DebitNoteLineType
            {
                ID = new IDType { Value = count.ToString() }, //Count
                DebitedQuantity = new DebitedQuantityType
                {
                    unitCode = detail.UnitCode,
                    Value = detail.Quantity
                },
                LineExtensionAmount = new LineExtensionAmountType
                {
                    currencyID = request.NoteDetail.CurrencyCode,
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
                                currencyID = request.NoteDetail.CurrencyCode,
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
                            currencyID = request.NoteDetail.CurrencyCode,
                            Value = detail.TaxAmount * detail.Quantity
                        },
                        TaxSubtotal = new TaxSubtotalType[]
                        {
                            new TaxSubtotalType
                            {
                                TaxableAmount = new TaxableAmountType
                                {
                                    currencyID = request.NoteDetail.CurrencyCode,
                                    Value = detail.UnitPrice * detail.Quantity,
                                },
                                TaxAmount = new TaxAmountType
                                {
                                    currencyID = request.NoteDetail.CurrencyCode,
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
                    PriceAmount = new PriceAmountType { currencyID = request.NoteDetail.CurrencyCode, Value = detail.UnitPrice }
                }
            });
            count++;
        }
        debitNoteType.DebitNoteLine = invoiceLineTypes.ToArray();

        #endregion

        return debitNoteType;
    }

    public DespatchAdviceType GenerateDespatchAdviceType(DespatchAdviceRequest request, Issuer issuer)
    {
        var despatchAdviceType = new DespatchAdviceType
        {
            #region Headers

            Cac = SD.XmlnsCac,
            Cbc = SD.XmlnsCbc,
            Ds = SD.XmlnsDs,
            Ext = SD.XmlnsExt,

            #endregion

            #region Ubl and Schema

            UBLVersionID = new UBLVersionIDType { Value = request.UblVersionId },
            CustomizationID = new CustomizationIDType { Value = request.CustomizationId },

            #endregion

            #region Certificate

            UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType()
            },

            #endregion

            #region Serial Number

            ID = new IDType { Value = $"{request.DespatchDetail.Serie}{request.DespatchDetail.SerialNumber.ToString("00")}-{request.DespatchDetail.CorrelativeNumber.ToString("00000000")}" },

            #endregion

            #region Dates

            IssueDate = new IssueDateType { Value = request.IssueDate },
            IssueTime = new IssueTimeType { Value = request.IssueDate },

            #endregion

            #region Despatch Advice

            DespatchAdviceTypeCode = new DespatchAdviceTypeCodeType
            {
                Value = request.DespatchDetail.DocumentType //Catalog 1
            },

            Note = new NoteType[]
            {
                new NoteType
                {
                    Value = request.DespatchDetail.NoteType //e. "MONTO EN SOLES"
                }
            },

            OrderReference = new OrderReferenceType[]
            {
                new OrderReferenceType
                {
                    ID = new IDType { Value = $"{request.DespatchDetail.Serie}{request.DespatchDetail.SerialNumber.ToString("00")}-{request.DespatchDetail.CorrelativeNumber.ToString("00000000")}" },
                    OrderTypeCode = new OrderTypeCodeType { Value = request.DespatchDetail.DocumentType }
                }
            },

            AdditionalDocumentReference = new DocumentReferenceType[]
            {
                new DocumentReferenceType
                {
                    ID = new IDType { Value = request.DespatchDetail.DocumentReferenceId },
                    DocumentTypeCode = new DocumentTypeCodeType { Value = request.DespatchDetail.DocumentReferenceType },
                }
            },

            DespatchSupplierParty = new SupplierPartyType
            {
                CustomerAssignedAccountID = new CustomerAssignedAccountIDType
                {
                    schemeID = issuer.IssuerType,
                    Value = issuer.IssuerId.ToString()
                },
                Party = new PartyType
                {
                    PartyLegalEntity = new PartyLegalEntityType[]
                    {
                        new PartyLegalEntityType { RegistrationName = new RegistrationNameType { Value = issuer.IssuerName } }
                    },
                },
            },

            DeliveryCustomerParty = new CustomerPartyType
            {
                CustomerAssignedAccountID = new CustomerAssignedAccountIDType
                {
                    schemeID = request.DeliveryCustomer.DespatchPartyType,
                    Value = request.DeliveryCustomer.DespatchPartyId.ToString()
                },
                Party = new PartyType
                {
                    PartyLegalEntity = new PartyLegalEntityType[]
                    {
                        new PartyLegalEntityType { RegistrationName = new RegistrationNameType { Value = request.DeliveryCustomer.DespatchPartyName } }
                    }
                }
            },

            SellerSupplierParty = new SupplierPartyType
            {
                CustomerAssignedAccountID = new CustomerAssignedAccountIDType
                {
                    schemeID = request.SellerSupplier.DespatchPartyType,
                    Value = request.SellerSupplier.DespatchPartyId.ToString()
                },
                Party = new PartyType
                {
                    PartyLegalEntity = new PartyLegalEntityType[]
                    {
                        new PartyLegalEntityType { RegistrationName = new RegistrationNameType { Value = request.SellerSupplier.DespatchPartyName }}
                    }
                }
            },

            #endregion

            #region Shipment

            Shipment = new ShipmentType
            {
                ID = new IDType { Value = request.Shipment.IdNumber.ToString() },
                HandlingCode = new HandlingCodeType { Value = request.Shipment.HandlingCode },
                Information = new InformationType[] { new InformationType { Value = request.Shipment.Information } },
                GrossWeightMeasure = new GrossWeightMeasureType { unitCode = request.Shipment.UnitCode, Value = request.Shipment.GrossWeightMeasure },
                TotalTransportHandlingUnitQuantity = new TotalTransportHandlingUnitQuantityType { Value = request.Shipment.TotalTransportHandlingUnitQuantity },
                SplitConsignmentIndicator = new SplitConsignmentIndicatorType { Value = request.Shipment.SplitConsignmentIndicator },
                ShipmentStage = new ShipmentStageType[]
                {
                    new ShipmentStageType
                    {
                        TransportModeCode = new TransportModeCodeType { Value = request.Shipment.HandlingCode },
                        TransitPeriod = new PeriodType
                        {
                            StartDate = new StartDateType { Value = request.Shipment.TransitPeriod }
                        },
                        CarrierParty = new PartyType[]
                        {
                            new PartyType
                            {
                                PartyIdentification = new PartyIdentificationType[]
                                {
                                    new PartyIdentificationType
                                    {
                                        ID = new IDType { schemeID = request.Shipment.CarrierPartyType , Value = request.Shipment.CarrierPartyId.ToString()  }
                                    }
                                },
                                PartyName = new PartyNameType[]
                                {
                                    new PartyNameType { Name = new NameType1 { Value = request.Shipment.CarrierPartyName } }
                                }
                            }
                        },
                        TransportMeans = new TransportMeansType
                        {
                            RoadTransport = new RoadTransportType { LicensePlateID = new LicensePlateIDType { Value = request.Shipment.TransportLicensePlate } }
                        },
                        DriverPerson = new PersonType[]
                        {
                            new PersonType
                            {
                                ID = new IDType { schemeID = request.Shipment.DriverIdType, Value = request.Shipment.DriveId.ToString() }
                            }
                        }
                    }
                },
                Delivery = new DeliveryType
                {
                    DeliveryAddress = new AddressType
                    {
                        ID = new IDType { Value = request.Shipment.DeliveryGeoCode },
                        StreetName = new StreetNameType { Value = request.Shipment.DeliveryAddress }
                    }
                },
                TransportHandlingUnit = new TransportHandlingUnitType[]
                {
                    new TransportHandlingUnitType
                    {
                        ID = new IDType { Value = request.Shipment.TransportHandlingUnit }
                    }
                },
                OriginAddress = new AddressType
                {
                    ID = new IDType { Value = request.Shipment.OriginGeoCode },
                    StreetName = new StreetNameType { Value = request.Shipment.OriginAddress }
                },
                FirstArrivalPortLocation = new LocationType1
                {
                    ID = new IDType { Value = request.Shipment.FirstArrivalPortLocation }
                }
            },

            #endregion
        };

        #region Products Detail

        var despatchLineTypes = new List<DespatchLineType>();
        var count = 1;

        foreach (var detail in request.ProductsDetails)
        {
            despatchLineTypes.Add(new DespatchLineType
            {
                ID = new IDType { Value = count.ToString() }, //Count
                DeliveredQuantity = new DeliveredQuantityType
                {
                    unitCode = detail.UnitCode,
                    Value = detail.Quantity
                },
                OrderLineReference = new OrderLineReferenceType[]
                {
                    new OrderLineReferenceType
                    {
                        LineID = new LineIDType { Value = count.ToString() }
                    }
                },
                Item = new ItemType
                {
                    Name = new NameType1 { Value = detail.Description },
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
                }
            });
            count++;
        }

        despatchAdviceType.DespatchLine = despatchLineTypes.ToArray();

        #endregion


        return despatchAdviceType;
    }

    public SummaryDocumentsType GenerateSummaryDocumentsType(SummaryDocumentsRequest request, Issuer issuer, IEnumerable<Entities.Models.Invoice> tickets)
    {
        var summaryDocumentsType = new SummaryDocumentsType
        {
            #region Headers

            Sac = SD.XmlnsSac,
            Ext = SD.XmlnsExt,
            Ds = SD.XmlnsDs,
            Cbc = SD.XmlnsCbc,
            Cac = SD.XmlnsCac,

            #endregion

            #region Ubl and Schema

            UBLVersionID = new UBLVersionIDType { Value = request.UblVersionId },
            CustomizationID = new CustomizationIDType { Value = request.CustomizationId },

            #endregion

            #region Certificate

            UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType()
            },

            #endregion

            #region Serial Number

            ID = new IDType { Value = $"RC-{request.IssueDate.ToString("yyyyMMdd")}-{request.SummaryDocumentsId.ToString("00000")}" },

            #endregion

            #region Dates

            ReferenceDate = new ReferenceDateType { Value = request.ReferenceDate },
            IssueDate = new IssueDateType { Value = request.IssueDate },

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
                CustomerAssignedAccountID = new CustomerAssignedAccountIDType { Value = issuer.IssuerId.ToString() },
                AdditionalAccountID = new AdditionalAccountIDType[] { new AdditionalAccountIDType { Value = issuer.IssuerType } },
                Party = new PartyType
                {
                    PartyLegalEntity = new PartyLegalEntityType[]
                    {
                        new PartyLegalEntityType { RegistrationName = new RegistrationNameType { Value = issuer.IssuerId.ToString() } }
                    }
                }
            },

            #endregion
        };

        #region Summary Documents

        var summaryDocumentsTypes = new List<SummaryDocumentsLineType>();
        var taxSubTotal = new List<TaxTotalType>();
        var count = 1;

        foreach (var ticket in tickets)
        {
            var summaryDocumentsLineType = new SummaryDocumentsLineType
            {
                LineID = new LineIDType { Value = count.ToString() },
                DocumentTypeCode = new DocumentTypeCodeType { Value = ticket.InvoiceDetail.DocumentType },
                ID = new IDType { Value = $"{ticket.InvoiceDetail.Serie}{ticket.InvoiceDetail.SerialNumber.ToString("00")}-{ticket.InvoiceDetail.CorrelativeNumber.ToString("00000000")}" },
                AccountingCustomerParty = new CustomerPartyType
                {
                    CustomerAssignedAccountID = new CustomerAssignedAccountIDType { Value = ticket.Receiver.ReceiverId.ToString() },
                    AdditionalAccountID = new AdditionalAccountIDType[] { new AdditionalAccountIDType { Value = ticket.Receiver.ReceiverType } }
                },
                Status = new StatusType { ConditionCode = new ConditionCodeType { Value = ticket.Canceled ? "3" : "1" } },
                TotalAmount = new AmountType2 { currencyID = ticket.InvoiceDetail.CurrencyCode, Value = ticket.TotalAmount },
            };

            var billingPayments = new List<PaymentType>();
            foreach (var subTotal in ticket.TaxSubTotals)
            {
                billingPayments.Add(new PaymentType
                {
                    PaidAmount = new PaidAmountType { currencyID = ticket.InvoiceDetail.CurrencyCode, Value = subTotal.TaxableAmount },
                    InstructionID = new InstructionIDType { Value = subTotal.TaxId == "1000" ? "01" : "02" }
                });

                taxSubTotal.Add(new TaxTotalType
                {
                    TaxAmount = new TaxAmountType { currencyID = ticket.InvoiceDetail.CurrencyCode, Value = subTotal.TaxAmount },
                    TaxSubtotal = new TaxSubtotalType[]
                    {
                        new TaxSubtotalType
                        {
                            TaxAmount = new TaxAmountType { currencyID = ticket.InvoiceDetail.CurrencyCode, Value = subTotal.TaxAmount },
                            TaxCategory = new TaxCategoryType
                            {
                                TaxScheme = new TaxSchemeType
                                {
                                    ID = new IDType { Value = subTotal.TaxId },
                                    Name = new NameType1 { Value = subTotal.TaxName },
                                    TaxTypeCode = new TaxTypeCodeType { Value = subTotal.TaxCode }
                                }
                            }
                        }
                    }
                });
            }

            summaryDocumentsLineType.BillingPayment = billingPayments.ToArray();
            summaryDocumentsLineType.TaxTotal = taxSubTotal.ToArray();

            summaryDocumentsTypes.Add(summaryDocumentsLineType);
            count++;
        }

        summaryDocumentsType.SummaryDocumentsLine = summaryDocumentsTypes.ToArray();

        #endregion

        return summaryDocumentsType;
    }

    public VoidedDocumentsType GenerateVoidedDocumentsType(VoidedDocumentsRequest request, Issuer issuer)
    {
        var voidedDocumentsType = new VoidedDocumentsType
        {
            #region Headers

            Cac = SD.XmlnsCac,
            Cbc = SD.XmlnsCbc,
            Ds = SD.XmlnsDs,
            Ext = SD.XmlnsExt,
            Sac = SD.XmlnsSac,

            #endregion

            #region Ubl and Schema

            UBLVersionID = new UBLVersionIDType { Value = request.UblVersionId },
            CustomizationID = new CustomizationIDType { Value = request.CustomizationId },

            #endregion

            #region Certificate

            UBLExtensions = new UBLExtensionType[]
            {
                new UBLExtensionType()
            },

            #endregion

            #region Serial Number

            ID = new IDType { Value = $"RA-{request.IssueDate.ToString("yyyyMMdd")}-{request.VoidedDocumentsId.ToString("000")}" },

            #endregion

            #region Dates

            ReferenceDate = new ReferenceDateType { Value = request.ReferenceDate },
            IssueDate = new IssueDateType { Value = request.IssueDate },

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
                CustomerAssignedAccountID = new CustomerAssignedAccountIDType { Value = issuer.IssuerId.ToString() },
                AdditionalAccountID = new AdditionalAccountIDType[] { new AdditionalAccountIDType { Value = issuer.IssuerType } },
                Party = new PartyType
                {
                    PartyLegalEntity = new PartyLegalEntityType[]
                    {
                        new PartyLegalEntityType { RegistrationName = new RegistrationNameType { Value = issuer.IssuerId.ToString() } }
                    }
                }
            },

            #endregion
        };

        #region Voided Documents

        var voidedDocumentsLineTypes = new List<VoidedDocumentsLineType>();
        var count = 1;

        foreach (var documentToVoid in request.DocumentsToVoid)
        {
            voidedDocumentsLineTypes.Add(new VoidedDocumentsLineType
            {
                LineID = new LineIDType { Value = count.ToString() },
                DocumentTypeCode = new DocumentTypeCodeType { Value = documentToVoid.DocumentType },
                DocumentSerialID = new IdentifierType3 { Value = $"{documentToVoid.Serie}{documentToVoid.SerialNumber.ToString("00")}" },
                DocumentNumberID = new IdentifierType3 { Value = $"{documentToVoid.CorrelativeNumber.ToString("00000000")}" },
                VoidReasonDescription = new TextType3 { Value = documentToVoid.VoidReason }
            });
            count++;
        }

        voidedDocumentsType.VoidedDocumentsLine = voidedDocumentsLineTypes.ToArray();

        #endregion

        return voidedDocumentsType;
    }
}