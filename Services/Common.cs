using GomBuild_v2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Version = GomBuild_v2.Model.Version;

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
        public static async Task<List<Version>> CallApiVersion()
        {
            string apiUrl = ConfigurationManager.AppSettings["API_VERJSON"].ToString();
            string Master = ConfigurationManager.AppSettings["API_MASTER"].ToString();
            string Access = ConfigurationManager.AppSettings["API_ACCSESS"].ToString();
            string json = await FetchDataFromAPIWithHeaders(apiUrl, Master, Access);
            var _JsonString = JsonConvert.DeserializeObject<List<Version>>(json);

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
            List<Version> result = await CallApiVersion();
            var obj = result.Where(x => x.KEY.Equals("GOMBUILD")).FirstOrDefault();
            if (!obj.VALUE.Contains("1.0.0"))
            {

            }


            return true;

            //WebClient webClient = new WebClient();
            //var client = new WebClient();
            //if (!webClient.DownloadString("link to web host/Version.txt").Contains("1.0.0"))
            //{
            //    if (MessageBox.Show("A new update is available! Do you want to download it?", "Demo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        try
            //        {
            //            if (File.Exists(@".\MyAppSetup.msi")) { File.Delete(@".\MyAppSetup.msi"); }
            //            client.DownloadFile("link to web host/MyAppSetup.zip", @"MyAppSetup.zip");
            //            string zipPath = @".\MyAppSetup.zip";
            //            string extractPath = @".\";
            //            ZipFile.ExtractToDirectory(zipPath, extractPath);
            //            Process process = new Process();
            //            process.StartInfo.FileName = "msiexec.exe";
            //            process.StartInfo.Arguments = string.Format("/i MyAppSetup.msi");
            //            //this.Close();
            //            process.Start();
            //        }
            //        catch
            //        {
            //        }
            //    }
            //}
        }
    }
}
