using System;
using System.Security.Principal;

namespace Util
{
  public class DataRepository
  {
    private readonly string _name;

    public DataRepository(string name)
    {
      _name = name;
    }

    public string GetData(string value)
    {
      WindowsIdentity windowsIdentity = WindowsIdentity.GetCurrent();
      var data = string.Format("[{0}] service={1}, user='{2}', value='{3}'"
        , DateTime.Now.ToString(), _name, windowsIdentity.Name, value);
      return data;
    }
  }
}
