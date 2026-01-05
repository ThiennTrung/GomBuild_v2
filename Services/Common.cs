using GomBuild_v2.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        public static List<Dictionary<string, object>> Decompress(FileInfo fileToDecompress)
        {
            using (FileStream fileStream = fileToDecompress.OpenRead())
            {
                string fullName = fileToDecompress.FullName;
                using (GZipStream gzipStream = new GZipStream((Stream)fileStream, CompressionMode.Decompress))
                {
                    using (StreamReader streamReader = new StreamReader((Stream)gzipStream))
                        return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(streamReader.ReadToEnd());
                }
            }
        }

        public static void WriteJsonToGzipFile(List<Dictionary<string, object>> data, string outputPath)
        {
            string str = System.Text.Json.JsonSerializer.Serialize<List<Dictionary<string, object>>>(data, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
            using (FileStream fileStream = new FileStream(outputPath, FileMode.Create))
            {
                using (GZipStream gzipStream = new GZipStream((Stream)fileStream, CompressionMode.Compress))
                {
                    using (StreamWriter streamWriter = new StreamWriter((Stream)gzipStream, Encoding.UTF8))
                        streamWriter.Write(str);
                }
            }
        }


        public static void GenCodeEvent(Dictionary<string, object> dict, StringBuilder Events, string command)
        {
            if (dict.ContainsKey(command))
            {
                if (dict[command] != null)
                {
                    if (!string.IsNullOrEmpty(dict[command].ToString()))
                    {
                        if (command == "Command_Click")
                        { 
                            Events.AppendLine("//[Permission(Name = \"\", Description = \"\")]"); 
                        }
                        Events.AppendLine(string.Concat("private void ", dict[command].ToString(), " (ICmdParameter p) \n{\n\r}\n"));
                    }
                }
            }
        }
        public static void GenCodeModel(Dictionary<string, object> dict, StringBuilder ModelScript)
        {
            if (dict.ContainsKey("DataBindingName"))
            {
                if (dict["DataBindingName"] != null)
                {
                    if (!string.IsNullOrEmpty(dict["DataBindingName"].ToString()))
                    {
                        string data_type = GetDataType(dict["Template"].ToString());

                        ModelScript.AppendLine(string.Concat("public ", data_type," ", dict["DataBindingName"].ToString(), " { get; set; } \n"));
                    }
                }
            }
        }
        private static string GetDataType(string template)
        {
            string result = string.Empty; 
            switch (template)
            {
                case "CheckBox":
                    result = "bool";
                    break;
                case "DatePicker":
                    result = "DateTime";
                    break;
                case "SearchEntry":
                case "ComboBox":
                case "RadioList":
                    result = "int";
                    break;
                default:
                    result = "string";
                    break;
            }
            return result;
        }
    }
}
