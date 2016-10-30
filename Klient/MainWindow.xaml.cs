using AppService1.Contract;
using BusService1.Contract;
using DomService1.Contract;
using System;
using System.Configuration;
using System.Text;
using System.Windows;
using Util;

namespace Klient
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private readonly IWidgetAppService _widgetAppService;
    private readonly IWidgetBusService _widgetBusService;
    private readonly IWidgetDomService _widgetDomService;

    public MainWindow()
    {
      InitializeComponent();

      //Set a starting "default" value
      CreateNewValue();

      //Setup App Service Client
      var appUrl = ConfigurationManager.AppSettings.Get(ConstValues.Config_AppServiceUrl);
      var appSpn = ConfigurationManager.AppSettings.Get(ConstValues.Config_AppServiceSpn);
      _widgetAppService = KerberosClientHelper.CreateBasicHttpKerberosClient<IWidgetAppService>(appUrl, appSpn);
      //Setup Bus Service Client
      var busUrl = ConfigurationManager.AppSettings.Get(ConstValues.Config_BusServiceUrl);
      var busSpn = ConfigurationManager.AppSettings.Get(ConstValues.Config_BusServiceSpn);
      _widgetBusService = KerberosClientHelper.CreateBasicHttpKerberosClient<IWidgetBusService>(busUrl, busSpn);
      //Setup Dom Service Client
      var domUrl = ConfigurationManager.AppSettings.Get(ConstValues.Config_DomServiceUrl);
      var domSpn = ConfigurationManager.AppSettings.Get(ConstValues.Config_DomServiceSpn);
      _widgetDomService = KerberosClientHelper.CreateBasicHttpKerberosClient<IWidgetDomService>(domUrl, domSpn);
    }

    private void CreateNewValue()
    {
      textBoxInput.Text = Guid.NewGuid().ToString();
    }
    
    private static string BuildExceptionResult(Exception e)
    {
      StringBuilder result = new StringBuilder();
      result.Append(e.Message);
      if (null != e.InnerException)
      {
        result.Append(Environment.NewLine);
        result.Append(BuildExceptionResult(e.InnerException));
      }
      return result.ToString();
    }

    private void SetResult(string result)
    {
      textBlockResult.Text = result;
    }

    private void buttonNewValue_Click(object sender, RoutedEventArgs args)
    {
      CreateNewValue();
    }

    private void buttonCallApp_Click(object sender, RoutedEventArgs args)
    {
      try
      {
        var result = _widgetAppService.GetWidgetAppData(textBoxInput.Text);
        SetResult(result);
      }
      catch (Exception e)
      {
        SetResult(BuildExceptionResult(e));
      }
    }

    private void buttonCallBus_Click(object sender, RoutedEventArgs args)
    {
      try
      {
        var result = _widgetBusService.GetWidgetBusData(textBoxInput.Text);
        SetResult(result);
      }
      catch (Exception e)
      {
        SetResult(BuildExceptionResult(e));
      }
    }

    private void buttonCallDom_Click(object sender, RoutedEventArgs args)
    {
      try
      {
        var result = _widgetDomService.GetWidgetDomData(textBoxInput.Text);
        SetResult(result);
      }
      catch (Exception e)
      {
        SetResult(BuildExceptionResult(e));
      }
    }

    private void buttonCallAppBus_Click(object sender, RoutedEventArgs args)
    {
      try
      {
        var result = _widgetAppService.GetWidgetBusData(textBoxInput.Text);
        SetResult(result);
      }
      catch (Exception e)
      {
        SetResult(BuildExceptionResult(e));
      }
    }

    private void buttonCallBusDom_Click(object sender, RoutedEventArgs args)
    {
      try
      {
        var result = _widgetBusService.GetWidgetDomData(textBoxInput.Text);
        SetResult(result);
      }
      catch (Exception e)
      {
        SetResult(BuildExceptionResult(e));
      }
    }

    private void buttonCallAppBusDom_Click(object sender, RoutedEventArgs args)
    {
      try
      {
        var result = _widgetAppService.GetWidgetDomDataViaBus(textBoxInput.Text);
        SetResult(result);
      }
      catch (Exception e)
      {
        SetResult(BuildExceptionResult(e));
      }
    }

  }

}
