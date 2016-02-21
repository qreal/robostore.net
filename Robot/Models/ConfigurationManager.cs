using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Store.ViewModels.Configuration;

namespace Robot.Models
{
  public class ConfigurationManager
  {
    private string serverUrl = "http://robstore.azurewebsites.net/api";

    public ConfigurationExport[] GetConfigurations(int RobotId = 4)
    {
      ConfigurationExport[] response;
      using (var wb = new WebClient())
      {
        wb.Encoding = Encoding.UTF8;
        var json = wb.DownloadString(serverUrl + "/configuration/get?RobotId=" + RobotId);
        response = JsonConvert.DeserializeObject<ConfigurationExport[]>(json);
      }
      return response;
    }

    public void PostConfiguration(ConfigurationImport configuration)
    {
      using (var wb = new WebClient())
      {
        var data = new NameValueCollection();
        data["Port"] = configuration.Port.ToString();
        data["RobotID"] = configuration.RobotID.ToString();
        wb.UploadValues(serverUrl + "/configuration/post", "POST", data);
      }
    }
  }
}
