using System.Collections.Specialized;
using System.Net;
using System.Text;
using Domain.Configurations;
using Newtonsoft.Json;
using Store.ViewModels;

namespace Robot.Models
{
  public class ConfigurationManager : Manager
  {
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
