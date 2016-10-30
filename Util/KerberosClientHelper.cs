using System;
using System.ServiceModel;

namespace Util
{
  public class KerberosClientHelper
  {
    /// <summary>
    /// Creates a Kerberos client Channel (proxy) to call the web service.
    /// </summary>
    /// <typeparam name="T">Contract Interface of the service</typeparam>
    /// <param name="endpointAddress">Endpoint URL of the service</param>
    /// <param name="spnIdentity">SPN for Kerberos Auth</param>
    /// <returns>Created channel</returns>
    public static T CreateBasicHttpKerberosClient<T>(string endpointAddress, string spnIdentity)
    {
      var kerberosBinding = new BasicHttpBinding();
      kerberosBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;

      //Set transport security
      kerberosBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
      kerberosBinding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;

      //Create endpoint with spn identity
      var endpointAddr = new EndpointAddress(
          new Uri(endpointAddress),
          EndpointIdentity.CreateSpnIdentity(spnIdentity));

      //Configure channel factory
      var factory = new ChannelFactory<T>(kerberosBinding, endpointAddr);
      //Need to allow the service to impersonate this client.
      factory.Credentials.Windows.AllowedImpersonationLevel = System.Security.Principal.TokenImpersonationLevel.Delegation;

      //Return the created client channel
      return factory.CreateChannel();
    }

    
    /// <summary>
    /// Creates a Kerberos client Channel used to call the web service endpoint.
    /// </summary>
    /// <typeparam name="T">Contract Interface of the service</typeparam>
    /// <param name="endpointAddress">Endpoint URL of the service</param>
    /// <param name="spnIdentity">SPN for Kerberos Auth</param>
    /// <returns>created channel</returns>
    public static T NOT_TESTED__GenerateWSHttpKerberosClient<T>(string endpointAddress, string spnIdentity)
    {
      var kerberosBinding = new WSHttpBinding();

      kerberosBinding.Security.Mode = SecurityMode.Message;

      //set transport security
      kerberosBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
      kerberosBinding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;

      //set message security
      kerberosBinding.Security.Message.ClientCredentialType = MessageCredentialType.Windows;
      kerberosBinding.Security.Message.EstablishSecurityContext = false;
      kerberosBinding.Security.Message.NegotiateServiceCredential = false;
      kerberosBinding.Security.Message.AlgorithmSuite =
          System.ServiceModel.Security.SecurityAlgorithmSuite.Basic128;

      //create endpoint with spn identity
      var endpointAddr = new EndpointAddress(
          new Uri(endpointAddress),
          EndpointIdentity.CreateSpnIdentity(spnIdentity));

      //configure channel factory with transport/message/endpoint configuration            
      var factory = new ChannelFactory<T>(kerberosBinding, endpointAddr);

      //need to impersonate service account of the service.
      if (factory.Credentials != null)
        factory.Credentials.Windows.AllowedImpersonationLevel =
            System.Security.Principal.TokenImpersonationLevel.Impersonation;

      //return create client channel
      return factory.CreateChannel();
    }

  }
}
