using GomBuild_v2.Model;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GomBuild_v2.Services
{
    public static class Common
    {
        public static async Task<JsonString> CallApi()
        {
            string apiUrl = ConfigurationManager.AppSettings["API_SQLJSON"].ToString();
            string Master = ConfigurationManager.AppSettings["API_MASTER"].ToString();
            string Access = ConfigurationManager.AppSettings["API_ACCSESS"].ToString();
            string json = await FetchDataFromAPIWithHeaders(apiUrl, Master, Access);
            var _JsonString = JsonConvert.DeserializeObject<JsonString>(json);

            return _JsonString;
        }
        static async Task<string> FetchDataFromAPIWithHeaders(string apiUrl, string Master, string Access)
        {
            string responseData = string.Empty;
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Add("X-Master-Key", Master);
                    client.DefaultRequestHeaders.Add("X-Access-Key", Access);
                    client.DefaultRequestHeaders.Add("X-BIN-META", "false");
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        responseData = await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return responseData;
        }
        public static int? GetTypeFile(string extend)
        {
            int? res = null;
            switch (extend)
            {
                case ".rdl":
                case ".rdlx":
                    res = (int)_TypeFile.Report;
                    break;
                case ".gz":
                    res = (int)_TypeFile.Form;
                    break;
                case ".xls":
                case ".xlsx":
                    res = (int)_TypeFile.Template;
                    break;
                case ".json":
                    res = (int)_TypeFile.Style;
                    break;
                case ".png":
                case ".jpg":
                    res = (int)_TypeFile.Image;
                    break;
            }
            return res;
        }


        public static async Task<bool> CheckUpdateAsync()
        {
            string localVersion = "2.0.0";
            string jsonUrl = "https://raw.githubusercontent.com/ThiennTrung/GomBuild_v2/refs/heads/main/Update/Update.json";

            using var http = new HttpClient();
            var json = await http.GetStringAsync(jsonUrl);
            var updateInfo = System.Text.Json.JsonSerializer.Deserialize<UpdateInfo>(json);
            if (new Version(updateInfo.version) > new Version(localVersion))
            {
                return true;
            }
            return false;
        }
        public static async void UpdateVersion()
        {
            var isUpdate = await CheckUpdateAsync();
            if (isUpdate)
            {
                if (MessageBox.Show("Cập nhật nha, bug quá rồi!", "UPDATE 🔄", MessageBoxButtons.OKCancel, MessageBoxIcon.Information
                                ) == DialogResult.OK)
                {
                    Process.Start("GomBuildV2_Updater.exe");
                    Application.Exit();
                }
                else { return; }
            } 
        }
    }
}
