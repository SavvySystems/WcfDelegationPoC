using AppService1.Contract;
using BusService1.Contract;
using DomService1.Contract;
using System;
using System.Configuration;
using System.ServiceModel;
using System.Text;
using Util;

namespace AppService1
{
  public class WidgetAppService : IWidgetAppService
  {
    #region Private properties - dependency references

    private readonly DataRepository _dataRepository;
    private readonly IWidgetBusService _busService;
    private readonly IWidgetDomService _domService;

    #endregion

    #region Constructors

    public WidgetAppService(DataRepository dataRepository, IWidgetBusService widgetBusService, IWidgetDomService widgetDomService)
    {
      _dataRepository = dataRepository;
      _busService = widgetBusService;
      _domService = widgetDomService;
    }

    public WidgetAppService()
      : this(
            new DataRepository("App"),
            KerberosClientHelper.CreateBasicHttpKerberosClient<IWidgetBusService>(
              ConfigurationManager.AppSettings.Get(ConstValues.Config_BusServiceUrl),
              ConfigurationManager.AppSettings.Get(ConstValues.Config_BusServiceSpn)),
            KerberosClientHelper.CreateBasicHttpKerberosClient<IWidgetDomService>(
              ConfigurationManager.AppSettings.Get(ConstValues.Config_DomServiceUrl),
              ConfigurationManager.AppSettings.Get(ConstValues.Config_DomServiceSpn))
          )
    { }

    #endregion

    #region IWidgetAppService implementation

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string Ping()
    {
      return "Pong";
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string GetWidgetAppData(string value)
    {
      //Simply return "App" data.
      var result = _dataRepository.GetData(value);
      return result;
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string GetWidgetBusData(string value)
    {
      //Call the Business service
      string busServerResponse = _busService.GetWidgetBusData(value);      
      //Append the AppService result
      var result = new StringBuilder();
      result.Append(_dataRepository.GetData(value));
      result.Append(Environment.NewLine);
      result.Append(busServerResponse);
      return result.ToString();
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string GetWidgetDomData(string value)
    {
      //Call the domain service
      string domServerResponse = _domService.GetWidgetDomData(value);
      //Append the AppService result
      var result = new StringBuilder();
      result.Append(_dataRepository.GetData(value));
      result.Append(Environment.NewLine);
      result.Append(domServerResponse);
      return result.ToString();
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string GetWidgetDomDataViaBus(string value)
    {
      //Call the Business service
      string busServerResponse = _busService.GetWidgetDomData(value);
      //Append the AppService result
      var result = new StringBuilder();
      result.Append(_dataRepository.GetData(value));
      result.Append(Environment.NewLine);
      result.Append(busServerResponse);
      return result.ToString();
    }

    #endregion
  }
}
