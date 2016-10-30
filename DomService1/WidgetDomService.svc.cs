using DomService1.Contract;
using System.ServiceModel;
using Util;

namespace DomService1
{
  public class WidgetDomService : IWidgetDomService
  {

    #region Private properties - dependency references

    private readonly DataRepository _dataRepository;

    #endregion

    #region Constructors

    public WidgetDomService(DataRepository dataRepository)
    {
      _dataRepository = dataRepository;
    }

    public WidgetDomService()
      : this(new DataRepository("Dom"))
    { }

    #endregion

    #region IWidgetDomService implementation

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string Ping()
    {
      return "Pong";
    }

    [OperationBehavior(Impersonation = ImpersonationOption.Allowed)]
    public string GetWidgetDomData(string value)
    {
      var result = _dataRepository.GetData(value);
      return result;
    }

    #endregion
  }
}
