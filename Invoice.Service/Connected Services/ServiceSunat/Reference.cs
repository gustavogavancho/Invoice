﻿namespace ServiceSunat
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://service.sunat.gob.pe", ConfigurationName="ServiceSunat.billService")]
    public interface billService
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:getStatus", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<ServiceSunat.getStatusResponse> getStatusAsync(ServiceSunat.getStatusRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendBill", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<ServiceSunat.sendBillResponse> sendBillAsync(ServiceSunat.sendBillRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendPack", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<ServiceSunat.sendPackResponse> sendPackAsync(ServiceSunat.sendPackRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:sendSummary", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Threading.Tasks.Task<ServiceSunat.sendSummaryResponse> sendSummaryAsync(ServiceSunat.sendSummaryRequest request);
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://service.sunat.gob.pe")]
    public partial class statusResponse
    {
        
        private byte[] contentField;
        
        private string statusCodeField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary", Order=0)]
        public byte[] content
        {
            get
            {
                return this.contentField;
            }
            set
            {
                this.contentField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, Order=1)]
        public string statusCode
        {
            get
            {
                return this.statusCodeField;
            }
            set
            {
                this.statusCodeField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getStatus", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class getStatusRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public getStatusRequest()
        {
        }
        
        public getStatusRequest(string ticket)
        {
            this.ticket = ticket;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="getStatusResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class getStatusResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public ServiceSunat.statusResponse status;
        
        public getStatusResponse()
        {
        }
        
        public getStatusResponse(ServiceSunat.statusResponse status)
        {
            this.status = status;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendBill", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendBillRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendBillRequest()
        {
        }
        
        public sendBillRequest(string fileName, byte[] contentFile, string partyType)
        {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendBillResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendBillResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] applicationResponse;
        
        public sendBillResponse()
        {
        }
        
        public sendBillResponse(byte[] applicationResponse)
        {
            this.applicationResponse = applicationResponse;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendPack", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendPackRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendPackRequest()
        {
        }
        
        public sendPackRequest(string fileName, byte[] contentFile, string partyType)
        {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendPackResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendPackResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public sendPackResponse()
        {
        }
        
        public sendPackResponse(string ticket)
        {
            this.ticket = ticket;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendSummary", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendSummaryRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string fileName;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=1)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="base64Binary")]
        public byte[] contentFile;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=2)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string partyType;
        
        public sendSummaryRequest()
        {
        }
        
        public sendSummaryRequest(string fileName, byte[] contentFile, string partyType)
        {
            this.fileName = fileName;
            this.contentFile = contentFile;
            this.partyType = partyType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="sendSummaryResponse", WrapperNamespace="http://service.sunat.gob.pe", IsWrapped=true)]
    public partial class sendSummaryResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://service.sunat.gob.pe", Order=0)]
        [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ticket;
        
        public sendSummaryResponse()
        {
        }
        
        public sendSummaryResponse(string ticket)
        {
            this.ticket = ticket;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public interface billServiceChannel : ServiceSunat.billService, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    public partial class billServiceClient : System.ServiceModel.ClientBase<ServiceSunat.billService>, ServiceSunat.billService
    {
        
        /// <summary>
        /// Implement this partial method to configure the service endpoint.
        /// </summary>
        /// <param name="serviceEndpoint">The endpoint to configure</param>
        /// <param name="clientCredentials">The client credentials</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public billServiceClient(EndpointConfiguration endpointConfiguration) : 
                base(billServiceClient.GetBindingForEndpoint(endpointConfiguration), billServiceClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public billServiceClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(billServiceClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public billServiceClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(billServiceClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public billServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceSunat.getStatusResponse> ServiceSunat.billService.getStatusAsync(ServiceSunat.getStatusRequest request)
        {
            return base.Channel.getStatusAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceSunat.getStatusResponse> getStatusAsync(string ticket)
        {
            ServiceSunat.getStatusRequest inValue = new ServiceSunat.getStatusRequest();
            inValue.ticket = ticket;
            return ((ServiceSunat.billService)(this)).getStatusAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceSunat.sendBillResponse> ServiceSunat.billService.sendBillAsync(ServiceSunat.sendBillRequest request)
        {
            return base.Channel.sendBillAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceSunat.sendBillResponse> sendBillAsync(string fileName, byte[] contentFile, string partyType)
        {
            ServiceSunat.sendBillRequest inValue = new ServiceSunat.sendBillRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((ServiceSunat.billService)(this)).sendBillAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceSunat.sendPackResponse> ServiceSunat.billService.sendPackAsync(ServiceSunat.sendPackRequest request)
        {
            return base.Channel.sendPackAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceSunat.sendPackResponse> sendPackAsync(string fileName, byte[] contentFile, string partyType)
        {
            ServiceSunat.sendPackRequest inValue = new ServiceSunat.sendPackRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((ServiceSunat.billService)(this)).sendPackAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<ServiceSunat.sendSummaryResponse> ServiceSunat.billService.sendSummaryAsync(ServiceSunat.sendSummaryRequest request)
        {
            return base.Channel.sendSummaryAsync(request);
        }
        
        public System.Threading.Tasks.Task<ServiceSunat.sendSummaryResponse> sendSummaryAsync(string fileName, byte[] contentFile, string partyType)
        {
            ServiceSunat.sendSummaryRequest inValue = new ServiceSunat.sendSummaryRequest();
            inValue.fileName = fileName;
            inValue.contentFile = contentFile;
            inValue.partyType = partyType;
            return ((ServiceSunat.billService)(this)).sendSummaryAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BillServicePort))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.BillServicePort_0))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpsTransportBindingElement httpsBindingElement = new System.ServiceModel.Channels.HttpsTransportBindingElement();
                httpsBindingElement.AllowCookies = true;
                httpsBindingElement.MaxBufferSize = int.MaxValue;
                httpsBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpsBindingElement);
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.BillServicePort_3))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                result.Security.Mode = System.ServiceModel.BasicHttpSecurityMode.Transport;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BillServicePort))
            {
                return new System.ServiceModel.EndpointAddress("https://e-factura.sunat.gob.pe/ol-ti-itcpfegem/billService");
            }
            if ((endpointConfiguration == EndpointConfiguration.BillServicePort_0))
            {
                return new System.ServiceModel.EndpointAddress("https://e-factura.sunat.gob.pe/ol-ti-itcpfegem/billService");
            }
            if ((endpointConfiguration == EndpointConfiguration.BillServicePort_3))
            {
                return new System.ServiceModel.EndpointAddress("https://e-factura.sunat.gob.pe/ol-ti-itcpfegem/billService");
            }
            throw new System.InvalidOperationException(string.Format("Could not find endpoint with name \'{0}\'.", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            BillServicePort,
            
            BillServicePort_0,
            
            BillServicePort_3,
        }
    }
}
