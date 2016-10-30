using BusService1.Contract;
using DomService1.Contract;
using System;
using System.Configuration;
using System.ServiceModel;
using System.Text;
using Util;

namespace BusService1
{
  public class WidgetBusService: IWidgetBusService
  {
    #region Private properties - dependency references

    private readonly DataRepository _dataRepository;
    private readonly IWidgetDomService _domService;

    #endregion

    #region Constructors

    public WidgetBusService(DataRepository dataRepository, IWidgetDomService widgetDomService)
    {
      _dataRepository = dataRepository;
      _domService = widgetDomService;
    }

    public WidgetBusService()
      : this(
            new DataRepository("Bus"),
            KerberosClientHelper.CreateBasicHttpKerberosClient<IWidgetDomService>(
              ConfigurationManager.AppSettings.Get(ConstValues.Config_DomServiceUrl),
              ConfigurationManager.AppSettings.Get(ConstValues.Config_DomServiceSpn))
          )
    { }
    
    #endregion

    #region IWidgetBusService implementation

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string Ping()
    {
      return "Pong";
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string GetWidgetBusData(string value)
    {
      var result = _dataRepository.GetData(value);
      return result;
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string GetWidgetDomData(string value)
    {
      //Call the Business service
      string domServerResponse = _domService.GetWidgetDomData(value);
      //Append the AppService result
      var result = new StringBuilder();
      result.Append(_dataRepository.GetData(value));
      result.Append(Environment.NewLine);
      result.Append(domServerResponse);
      return result.ToString();
    }

    #endregion
  }
}
