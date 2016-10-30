using System.ServiceModel;

namespace BusService1.Contract
{
  [ServiceContract]
  public interface IWidgetBusService
  {
    [OperationContract]
    string Ping();

    [OperationContract]
    string GetWidgetBusData(string value);

    [OperationContract]
    string GetWidgetDomData(string value);
  }
}
