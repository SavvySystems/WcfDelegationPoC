using System.ServiceModel;

namespace AppService1.Contract
{
  [ServiceContract]
  public interface IWidgetAppService
  {
    [OperationContract]
    string Ping();

    [OperationContract]
    string GetWidgetAppData(string value);

    [OperationContract]
    string GetWidgetBusData(string value);

    [OperationContract]
    string GetWidgetDomData(string value);

    [OperationContract]
    string GetWidgetDomDataViaBus(string value);
  }
}
