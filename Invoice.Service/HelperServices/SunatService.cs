using Invoice.Service.Contracts.HelperServices;
using Invoice.Service.Helpers;
using ServiceSunat;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Invoice.Service.HelperServices;

public class SunatService : ISunatService
{
    public async Task<byte[]> SendBill(string uri, string username, string password, string fileName, byte[] file)
    {
        var binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential)
        {
            Security =
                {
                    Transport =
                    {
                        ClientCredentialType = HttpClientCredentialType.None,
                        ProxyCredentialType = HttpProxyCredentialType.None,
                    },
                    Message =
                    {
                        ClientCredentialType = BasicHttpMessageCredentialType.UserName,
                    }
                }
        };

        using var servicio = new billServiceClient(binding, new EndpointAddress(uri));

        try
        {
            ServicePointManager.Expect100Continue = false;
            ServicePointManager.CheckCertificateRevocationList = true;

            servicio.ClientCredentials.UserName.UserName = username;
            servicio.ClientCredentials.UserName.Password = password;

            var customBinding = new CustomBinding(binding);

            var elements = customBinding.Elements;
            int i = -1;

            for (i = 0; i < elements.Count; i++)
            {
                if (typeof(MessageEncodingBindingElement).IsAssignableFrom(elements[i].GetType()))
                    break;
            }
            var mebe = (MessageEncodingBindingElement)elements[i];
            elements[i] = new CustomMessageEncodingBindingElement(mebe);

            servicio.Endpoint.Binding = new CustomBinding(elements);

            await servicio.OpenAsync();

            var sendBillResponse = await servicio.sendBillAsync(fileName, file, "0");

            return sendBillResponse.applicationResponse;
        }
        catch (FaultException fex)
        {
            throw new FaultException(fex.Message);
        }
        finally
        {
            await servicio.CloseAsync();
        }
    }
}