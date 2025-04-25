using GomBuild_v2.Model;
using System.Configuration;
using Newtonsoft.Json;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Data.SQLite;
//using EverythingSearchClient;
using ClosedXML.Excel;

namespace GomBuild_v2
{
    public partial class Form1 : Form
    {
        private NotifyIcon trayIcon;
        private ContextMenuStrip trayMenu;

        public List<FileInfo> lstCurrent = new List<FileInfo>();
        public List<DupFile> lstFileTrung = new List<DupFile>();

        private List<string> lstProject = new List<string>();
        private List<BUILDS> lstVer = new List<BUILDS>();

        StringBuilder mess = new StringBuilder();
        private string SQLLITE_CONNECTION;
        public Form1()
        {
            InitializeComponent();

            List<TypeFile> typeFiles = new List<TypeFile>()
            {
                new TypeFile { ID = (int)_TypeFile.Stored, NAME = "Stored" }, new TypeFile { ID = (int)_TypeFile.StoredNonReport, NAME = "StoredNonReport" },
                new TypeFile { ID = (int)_TypeFile.Report, NAME = "Report" }, new TypeFile { ID = (int)_TypeFile.Form, NAME = "Form" },
                new TypeFile { ID = (int)_TypeFile.Script, NAME = "Script" }, new TypeFile { ID = (int)_TypeFile.Template, NAME = "Template" }, new TypeFile { ID = (int)_TypeFile.Other, NAME = "Other" },
                new TypeFile { ID = (int)_TypeFile.Style, NAME = "Style" },new TypeFile { ID = (int)_TypeFile.Image, NAME = "Image" }
            };
            List<TypeFile> List = typeFiles;

            (dataGridView1.Columns["Type"] as DataGridViewComboBoxColumn).DataSource = List;
            (dataGridView1.Columns["Type"] as DataGridViewComboBoxColumn).DisplayMember = "NAME";
            (dataGridView1.Columns["Type"] as DataGridViewComboBoxColumn).ValueMember = "ID";

            //this.Text = "Trung nè";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Chọn build version");
                return;
            }
            var SelectedBuild = (comboBox1.SelectedItem as BUILDS);
            string project = SelectedBuild.PROJECT;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "files (*.gz;*.rdl;*.rdlx;*.sql)|*.gz;*.rdl;*.rdlx;*.sql|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                openFileDialog.Multiselect = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    foreach (String file in openFileDialog.FileNames)
                    {
                        System.IO.FileInfo oFileInfo = new System.IO.FileInfo(file);

                        // kiểm tra đã tồn tại trong list hiện tại chưa
                        if (lstCurrent.Any(x => x.Name == oFileInfo.Name))
                            continue;

                        // Kiểm tra có trùng trong folder build chưa

                        var duplicate = CheckTrungfile(oFileInfo, oFileInfo.Extension, project);
                        string status = "OK";
                        bool ISDUP = false;
                        bool Override = false;
                        if (!string.IsNullOrEmpty(duplicate.NAME))
                        {
                            status = "Duplicate";
                            ISDUP = true;
                            Override = true;
                        }
                        lstCurrent.Add(oFileInfo);
                        this.dataGridView1.Rows.Add(oFileInfo.FullName, oFileInfo.Extension, GetTypeFile(oFileInfo.Extension), oFileInfo.Name, status, duplicate.BUILDPATH, ISDUP, Override, false);
                    }
                }

            }
        }


        private int? GetTypeFile(string extend)
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Nhập mã jira vô");
                return;
            }
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Chọn build ver");
                return;
            }
            if (dataGridView1.RowCount <= 0 && string.IsNullOrEmpty(textBox4.Text))
            {
                MessageBox.Show("Có file nào đâu mà commit");
                return;
            }
            if (string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Tên DEV không được để trống");
                return;
            }
            var SelectedBuild = (comboBox1.SelectedItem as BUILDS);
            if (SelectedBuild != null)
            {
                if (!SelectedBuild.ISCURRENT && checkBox1.Checked)
                {
                    MessageBox.Show("Chỉ hotfix trên build hiện tại");
                    return;
                }
            }
            bool check = false;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                if (string.IsNullOrEmpty(item.Cells[2].FormattedValue.ToString()))
                {
                    check = true;
                    break;
                }
            }
            if (check)
            {
                MessageBox.Show("Chọn loại file kìa....");
                return;
            }

            mess.Clear();
            var dev = comboBox2.Text;
            var project = SelectedBuild.PROJECT;
            string path = Path.Combine(textBox1.Text, project, comboBox1.Text, "4.FOLDER_BACKUP");
            bool exists = System.IO.Directory.Exists(Path.Combine(path, dev, textBox2.Text));
            if (!exists) { System.IO.Directory.CreateDirectory(Path.Combine(path, dev, textBox2.Text)); }

            if (checkBox1.Checked)
                mess.Append("*HOTFIX ");

            if (!string.IsNullOrEmpty(comboBox3.Text))
                mess.Append(comboBox3.Text + "\n");

            mess.Append("- JIRA: " + textBox2.Text + "\n");
            mess.Append("- BUILD: " + comboBox1.Text + "\n");

            if (!string.IsNullOrEmpty(textBox3.Text))
                mess.Append("- NỘI DUNG: " + textBox3.Text + "\n");


            string connectionString = SQLLITE_CONNECTION;

            try
            {
                using SQLiteConnection connection = new SQLiteConnection(connectionString);
                
                if (!string.IsNullOrEmpty(textBox4.Text))
                {
                    connection.Open();
                    mess.Append("- THAM SỐ: " + "\n");
                    for (int i = 0; i < textBox4.Lines.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(textBox4.Lines[i]))
                        {
                            SQLiteCommand add = new SQLiteCommand(connection);
                            string sqlinsert = "INSERT INTO APPSETTINGS (DEV,CODE,BUILD,JIRA,TIME_COMMIT) VALUES (@DEV,@CODE,@BUILD,@JIRA,@TIME_COMMIT)";
                            add.Parameters.AddWithValue("@DEV", comboBox2.Text);
                            add.Parameters.AddWithValue("@CODE", textBox4.Lines[i]);
                            add.Parameters.AddWithValue("@BUILD", comboBox1.Text);
                            add.Parameters.AddWithValue("@JIRA", textBox2.Text);
                            add.Parameters.AddWithValue("@TIME_COMMIT", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                            add.CommandText = sqlinsert;
#if DEBUG
                            //add.ExecuteNonQuery();
#else
                            add.ExecuteNonQuery();
#endif
                            add.Parameters.Clear();
                            mess.Append("    + " + textBox4.Lines[i] + "\n");

                        }
                    }
                    connection.Close();
                }
                mess.Append("- FILE: " + "\n");
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    string pathfile = item.Cells[0].Value.ToString();
                    var Filename = item.Cells[3].Value.ToString();

                    string type = item.Cells[2].FormattedValue.ToString();
                    string Extend = item.Cells[1].FormattedValue.ToString();

                    bool isDuplicate = (bool)(item.Cells[6] as DataGridViewCheckBoxCell).Value;
                    bool isOverride = (bool)(item.Cells[7] as DataGridViewCheckBoxCell).Value && isDuplicate;
                    bool isAddNew = (bool)(item.Cells[8] as DataGridViewCheckBoxCell).Value && isDuplicate;

                    var ssss = (item.Cells[2].Value is null) ? _TypeFile.Other : (_TypeFile)item.Cells[2].Value;

                    mess.Append("    + " + Filename + "\n");

                    //gen new file name
                    if (isDuplicate && isAddNew)
                    {
                        Filename = string.Format("{0}_{1}_{2}{3}", System.IO.Path.GetFileNameWithoutExtension(Filename), comboBox2.Text, DateTime.Now.ToString("ddMMyy"), Extend);
                    }

                    //Move file to detail folder
                    string pathdes = Path.Combine(path, dev, textBox2.Text) + @"\" + Filename;
                    CopyFileByExtend(pathfile, Filename, ssss, project);

                    //Move file to folder backup
                    System.IO.File.Copy(pathfile, pathdes, true);

                    //Hotfix
                    if (checkBox1.Checked)
                    {
                        string pathHothix = Path.Combine(textBox1.Text, project, comboBox1.Text, "6.HOTFIX");
                        if (!System.IO.Directory.Exists(pathHothix)) { System.IO.Directory.CreateDirectory(pathHothix); }
                        System.IO.File.Copy(pathfile, pathHothix + @"\" + Filename, true);

                        //Tạo folder theo jira để quản lý hotfix
                        string pathHf_backup = Path.Combine(pathHothix, "LIST_JIRA");

                        bool exists_v = System.IO.Directory.Exists(Path.Combine(pathHf_backup, textBox2.Text));
                        if (!exists) { System.IO.Directory.CreateDirectory(Path.Combine(pathHf_backup, textBox2.Text)); }

                        System.IO.File.Copy(pathfile, pathHf_backup + @"\" + textBox2.Text + @"\" + Filename, true);
                        if (textBox2.Text.StartsWith("HM_"))
                        {
                            using (StreamWriter writer = new StreamWriter(Path.Combine(pathHf_backup, textBox2.Text, "02 - JIRA.url")))
                            {
                                writer.WriteLine("[{000214A0-0000-0000-C000-000000000046}]");
                                writer.WriteLine("Prop3=19,11");
                                writer.WriteLine("[InternetShortcut]");
                                writer.WriteLine("IDList=");
                                writer.WriteLine("URL=" + string.Format("https://jira.fis.com.vn/browse/{0}", textBox2.Text));
                                writer.Flush();
                            }
                        }
                    }


                    if (!string.IsNullOrEmpty(textBox3.Text))
                    {
                        System.IO.File.WriteAllText(Path.Combine(path, dev, textBox2.Text, "01 - README.txt"), textBox3.Text, Encoding.UTF8);
                    }
                    if (!checkBox2.Checked)
                    {
                        using (StreamWriter writer = new StreamWriter(Path.Combine(path, dev, textBox2.Text, "02 - JIRA.url")))
                        {
                            writer.WriteLine("[{000214A0-0000-0000-C000-000000000046}]");
                            writer.WriteLine("Prop3=19,11");
                            writer.WriteLine("[InternetShortcut]");
                            writer.WriteLine("IDList=");
                            writer.WriteLine("URL=" + string.Format("https://jira.fis.com.vn/browse/{0}", textBox2.Text));
                            writer.Flush();
                        }
                    }


                    // Ghi vào tracking
                    //using SQLiteConnection connection = new SQLiteConnection(connectionString);
                    try
                    {
                        string sql = "INSERT INTO LOG (DEV,FILENAME,TYPE,MA_JIRA,LINK_JIRA,CONTENT,HOTFIX,SITE,VERSION,TIME_COMMIT,OVERRIDE,NEW_VERSION)" +
                            " VALUES (@DEV,@FILENAME,@TYPE,@MA_JIRA,@LINK_JIRA" +
                            ",@CONTENT,@HOTFIX,@SITE,@VERSION,@TIME_COMMIT,@OVERRIDE,@NEW_VERSION)";

                        connection.Open();
                        SQLiteCommand cmd = new SQLiteCommand(connection);
                        cmd.Parameters.AddWithValue("@DEV", comboBox2.Text);
                        cmd.Parameters.AddWithValue("@FILENAME", Filename);
                        cmd.Parameters.AddWithValue("@TYPE", type);
                        cmd.Parameters.AddWithValue("@MA_JIRA", textBox2.Text);
                        cmd.Parameters.AddWithValue("@LINK_JIRA", "https://jira.fis.com.vn/browse/" + textBox2.Text);
                        cmd.Parameters.AddWithValue("@CONTENT", textBox3.Text);
                        cmd.Parameters.AddWithValue("@HOTFIX", checkBox1.Checked);
                        cmd.Parameters.AddWithValue("@SITE", comboBox3.Text);
                        cmd.Parameters.AddWithValue("@VERSION", comboBox1.Text);
                        cmd.Parameters.AddWithValue("@TIME_COMMIT", DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@OVERRIDE", isOverride);
                        cmd.Parameters.AddWithValue("@NEW_VERSION", isAddNew);
                        cmd.CommandText = sql;

#if DEBUG
                        //cmd.ExecuteNonQuery();
#else
                        cmd.ExecuteNonQuery();
#endif
                        cmd.Parameters.Clear();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                var formRes = MessageBox.Show(mess.ToString() + "\n" + "Kiểm tra lại onedrive có sync không nha ^^", "Xong rồi đó (OK = copy text)");
                if (formRes == DialogResult.OK)
                {
                    System.Windows.Forms.Clipboard.SetText(mess.ToString());
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CopyFileByExtend(string pathfile, string filename, _TypeFile type, string project, bool o = true)
        {
            string FormFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "1.FORM");
            string ReportFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "2.REPORT");
            string TempFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "5.TEMPLATE");
            string ScriptFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "3.SQL", "SCRIPT");
            string StoredFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "3.SQL", "STORED");
            string StoredNonreportFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "3.SQL", "STOREDNonREPORT");
            string OtherFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "3.SQL");

            switch (type)
            {
                case _TypeFile.Report:
                    if (!System.IO.Directory.Exists(ReportFolder)) { System.IO.Directory.CreateDirectory(ReportFolder); }
                    System.IO.File.Copy(pathfile, ReportFolder + @"\" + filename, o);
                    break;
                case _TypeFile.Form:
                    if (!System.IO.Directory.Exists(FormFolder)) { System.IO.Directory.CreateDirectory(FormFolder); }
                    System.IO.File.Copy(pathfile, FormFolder + @"\" + filename, o);
                    break;
                case _TypeFile.Template:
                    if (!System.IO.Directory.Exists(TempFolder)) { System.IO.Directory.CreateDirectory(TempFolder); }
                    System.IO.File.Copy(pathfile, TempFolder + @"\" + filename, o);
                    break;
                case _TypeFile.Script:
                    if (!System.IO.Directory.Exists(ScriptFolder)) { System.IO.Directory.CreateDirectory(ScriptFolder); }
                    System.IO.File.Copy(pathfile, ScriptFolder + @"\" + filename, o);
                    break;
                case _TypeFile.Stored:
                    if (!System.IO.Directory.Exists(StoredFolder)) { System.IO.Directory.CreateDirectory(StoredFolder); }
                    System.IO.File.Copy(pathfile, StoredFolder + @"\" + filename, o);
                    break;
                case _TypeFile.StoredNonReport:
                    if (!System.IO.Directory.Exists(StoredNonreportFolder)) { System.IO.Directory.CreateDirectory(StoredNonreportFolder); }
                    System.IO.File.Copy(pathfile, StoredNonreportFolder + @"\" + filename, o);
                    break;
                case _TypeFile.Other:
                    if (!System.IO.Directory.Exists(OtherFolder)) { System.IO.Directory.CreateDirectory(OtherFolder); }
                    System.IO.File.Copy(pathfile, OtherFolder + @"\" + filename, o);
                    break;
                default:
                    if (!System.IO.Directory.Exists(OtherFolder)) { System.IO.Directory.CreateDirectory(OtherFolder); }
                    System.IO.File.Copy(pathfile, OtherFolder + @"\" + filename, o);
                    break;
            }
        }
        private void ClearForm()
        {
            lstCurrent.Clear();
            dataGridView1.Rows.Clear();
            lstFileTrung.Clear();
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            mess.Clear();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            string apiUrl = ConfigurationManager.AppSettings["API_SQLJSON"].ToString();
            string Master = ConfigurationManager.AppSettings["API_MASTER"].ToString();
            string Access = ConfigurationManager.AppSettings["API_ACCSESS"].ToString();
            string json = await FetchDataFromAPIWithHeaders(apiUrl, Master, Access);
            var _JsonString = JsonConvert.DeserializeObject<JsonString>(json);

            string PathCommit = ConfigurationManager.AppSettings["PathCommit"];

            lstProject = _JsonString.BUILDS.Select(x => x.PROJECT).Distinct().ToList();
            lstVer = _JsonString.BUILDS;
            foreach (var item in _JsonString.DEV.ToString().Split(","))
            {
                comboBox2.Items.Add(item);
            }
            foreach (var item in _JsonString.SITE.ToString().Split(","))
            {
                comboBox3.Items.Add(item);
            }



#if DEBUG
            //List<BUILDS> lst = new List<BUILDS>();
            //BUILDS b = new BUILDS();
            //b.STT = 1;
            //b.VERSION = "Test";
            //b.ISCURRENT = true;
            //b.PROJECT = "HOANMY";
            //b.NOTE = "đây là note";
            //lst.Add(b);
            //comboBox1.DataSource = lst;
            //cbo_Project.DataSource = lst;
            //cbo_BuildVer.DataSource = lst;
            comboBox1.DataSource = _JsonString.BUILDS;
            cbo_Project.DataSource = lstProject;
            cbo_BuildVer.DataSource = lstVer;
#else
            comboBox1.DataSource = _JsonString.BUILDS;
            cbo_Project.DataSource = lstProject;
            cbo_BuildVer.DataSource = lstVer;
#endif

            comboBox1.DisplayMember = "VERSION";
            comboBox1.ValueMember = "STT";
            comboBox1.SelectedIndex = -1;

            //cbo_Project.DisplayMember = "PROJECT";
            //cbo_Project.ValueMember = "STT";
            cbo_Project.SelectedIndex = -1;

            cbo_BuildVer.DisplayMember = "VERSION";
            cbo_BuildVer.ValueMember = "STT";
            cbo_BuildVer.SelectedIndex = -1;

            string DEV = ConfigurationManager.AppSettings["DEV"];
            textBox1.Text = PathCommit;
            comboBox2.SelectedIndex = comboBox2.Items.IndexOf(DEV);
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

        private void button3_Click(object sender, EventArgs e)
        {
            //lstCurrent.Clear();
            //dataGridView1.Rows.Clear();
            //lstFileTrung.Clear();
            ClearForm();
        }

        private DupFile CheckTrungfile(FileInfo oFileInfo, string extend, string project)
        {
            DupFile dupf = new DupFile();
            string FormFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "1.FORM");
            string ReportFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "2.REPORT");
            string StoredFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "3.SQL", "STORED");
            string StoredNonreportFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "3.SQL", "STOREDNonREPORT");
            string OtherFolder = Path.Combine(textBox1.Text, project, comboBox1.Text, "3.SQL");



            string[] usePaths = new string[3];
            switch (extend)
            {
                case ".gz":
                    usePaths = new string[] { FormFolder };
                    break;
                case ".rdl":
                case ".rdlx":
                    usePaths = new string[] { ReportFolder };
                    break;
                case ".sql":
                    usePaths = new string[] { StoredFolder, StoredNonreportFolder, OtherFolder };
                    break;
            }
            foreach (string usePath in usePaths)
            {
                if (!System.IO.Directory.Exists(usePath))
                    return dupf;
                var dir = new DirectoryInfo(usePath);
                FileInfo[] files = dir.GetFiles();
                if (files.Any(x => x.Name.Equals(oFileInfo.Name)))
                {
                    dupf.NAME = oFileInfo.Name;
                    dupf.EXTEND = extend;
                    dupf.SOURCEPATH = oFileInfo.FullName;
                    dupf.BUILDPATH = Path.Combine(usePath, oFileInfo.Name);
                    break;
                }
            }

            return dupf;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Chọn build trước");
                return;
            }
              
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column1")
            {
                string connectionString = SQLLITE_CONNECTION;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    string sqlString = "SELECT * from LOG WHERE FILENAME = @FILENAME and VERSION = @BUILD_VER order by TIME_COMMIT DESC limit 1";
                    SQLiteCommand cmd = new SQLiteCommand(sqlString, connection);
                    cmd.Parameters.AddWithValue("@FILENAME", dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    cmd.Parameters.AddWithValue("@BUILD_VER", comboBox1.Text);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        StringBuilder mess = new StringBuilder();
                        mess.AppendLine("DEV: " + reader["DEV"].ToString());
                        mess.AppendLine("JIRA: " + reader["MA_JIRA"].ToString());
                        mess.AppendLine("NỘI DUNG: " + reader["CONTENT"].ToString());
                        mess.AppendLine("TIME: " + reader["TIME_COMMIT"].ToString());

                        if (MessageBox.Show(mess.ToString(), "DEV commit cuối (Yes = copy jira)", MessageBoxButtons.YesNo, MessageBoxIcon.Question
                                ) == DialogResult.Yes)
                        {
                            System.Windows.Forms.Clipboard.SetText(reader["MA_JIRA"].ToString());
                        }
                    }
                    connection.Close();
                }
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Column3") // override
            {
                dataGridView1.Rows[e.RowIndex].Cells[8].Value = false;
            }
            else if (dataGridView1.Columns[e.ColumnIndex].Name == "Column4") // newver
            {
                dataGridView1.Rows[e.RowIndex].Cells[7].Value = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Chọn build version");
                return;
            }

            string sqlString = "select MAX(FILENAME) as STORED,MAX(REPORT) AS REPORT,REPORT_CODE,MA_JIRA, LINK_JIRA"
                               + " from ("
	                           + "    select FILENAME,'' as REPORT,R.REPORT_CODE as REPORT_CODE , MA_JIRA, LINK_JIRA"
                               + "     from LOG L"
	                           + "     LEFT JOIN (SELECT STRING_AGG(REPORT_CODE,char(10)) AS REPORT_CODE, STORED FROM TM_REPORT GROUP BY STORED) R ON R.STORED = L.FILENAME"
	                           + "     where L.type  = 'Stored' and L.VERSION = @VERSION"
                               + "     union all "
	                           + "     select '' as FILENAME,FILENAME,R.REPORT_CODE as REPORT_CODE ,MA_JIRA, LINK_JIRA"
                               + "     from LOG L"
	                           + "     LEFT JOIN (SELECT STRING_AGG(REPORT_CODE,char(10)) AS REPORT_CODE, TEMPLATE  FROM TM_REPORT GROUP BY STORED) R ON R.TEMPLATE = L.FILENAME"
                               + "     where type  = 'Report' and L.VERSION = @VERSION"
                               + " ) DATA "
                               + " GROUP by REPORT_CODE";
            var data = new List<dynamic>();

            try
            {
                string connectionString = SQLLITE_CONNECTION;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(sqlString, connection);
                    cmd.Parameters.AddWithValue("@VERSION", comboBox1.Text);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string STORED = reader.IsDBNull(0) ? string.Empty : reader.GetString(0);
                        string REPORT = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        string REPORT_CODE = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        string MA_JIRA = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        string LINK_JIRA = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                        dynamic a = new { STORED, REPORT, REPORT_CODE, MA_JIRA, LINK_JIRA };
                        data.Add(a);
                    }
                    connection.Close();
                }
                // File path
                string filePath = ConfigurationManager.AppSettings["ExportFile"];

                ExportToExcel(data, filePath, comboBox1.Text);
                MessageBox.Show("Excel file exported successfully to " + filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        static void ExportToExcel(IEnumerable<dynamic> data, string filePath, string version)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Data");
                int currentRow = 1;

                // Add headers
                worksheet.Cell(currentRow, 1).Value = "STT";
                worksheet.Cell(currentRow, 2).Value = "TÊN STORED";
                worksheet.Cell(currentRow, 3).Value = "FILE REPORT";
                worksheet.Cell(currentRow, 4).Value = "REPORT CODE";
                worksheet.Cell(currentRow, 5).Value = "VERSION";
                worksheet.Cell(currentRow, 6).Value = "MÃ JIRA";
                worksheet.Cell(currentRow, 7).Value = "LINK JIRA";

                // Apply header formatting
                var headerRange = worksheet.Range(currentRow, 1, currentRow, 7);
                headerRange.Style.Font.Bold = true;
                headerRange.Style.Fill.BackgroundColor = XLColor.LightGreen;
                headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                // Add data rows
                foreach (var item in data)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = currentRow;
                    worksheet.Cell(currentRow, 2).Value = item.STORED;
                    worksheet.Cell(currentRow, 3).Value = item.REPORT;
                    worksheet.Cell(currentRow, 4).Value = item.REPORT_CODE;
                    worksheet.Cell(currentRow, 5).Value = version;
                    worksheet.Cell(currentRow, 6).Value = item.MA_JIRA;

                    var hyperlinkCell = worksheet.Cell(currentRow, 7);
                    worksheet.Cell(currentRow, 7).Value = item.LINK_JIRA;
                    var a = new XLHyperlink(item.LINK_JIRA);
                    hyperlinkCell.SetHyperlink(a);
                }
                // Apply borders to the entire table
                var dataRange = worksheet.Range(1, 1, currentRow, 7);
                dataRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                // Auto-adjust column widths
                worksheet.Columns().AdjustToContents();
                // Save to file
                workbook.SaveAs(filePath);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                textBox2.Enabled = false;
                textBox2.Text = "NO_JIRA";
            }
            else
            {
                textBox2.Enabled = true;
                textBox2.Text = string.Empty;
            }    
        }


        private void Txt_SerchKey_TextChanged(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(Txt_SerchKey.Text))
            //    return;
            //IEverything everything = new Everything();
            //var results = everything.Search().Name.Contains(Txt_SerchKey.Text);



        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 || string.IsNullOrEmpty(textBox1.Text))
            {
                SQLLITE_CONNECTION = string.Empty;
                return; 
            }
            else
            {
                var SelectedBuild = (comboBox1.SelectedItem as BUILDS);
                label8.Text = SelectedBuild.NOTE;
                SQLLITE_CONNECTION = string.Format(@"Data Source={0}\{1}\TrackingCommit.db;Version=3;", textBox1.Text, SelectedBuild.PROJECT);
            }
        }



        #region Tab 2

        #endregion

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 1)
            {

            }
        }

        private void cbo_Project_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbo_Project.SelectedIndex < 0)
            {
                cbo_BuildVer.Enabled = false;
                cbo_BuildVer.SelectedIndex = -1;
            }
            else
            {
                cbo_BuildVer.Enabled = true;
                cbo_BuildVer.DataSource = lstVer.Where(x => x.PROJECT.Equals(cbo_Project.Text)).ToList();

            }
        }
    }
}