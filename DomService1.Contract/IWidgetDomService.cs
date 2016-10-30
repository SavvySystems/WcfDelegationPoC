using System.ServiceModel;

namespace DomService1.Contract
{
  [ServiceContract]
  public interface IWidgetDomService
  {
    [OperationContract]
    string Ping();

    [OperationContract]
    string GetWidgetDomData(string value);
  }
}
