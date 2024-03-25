using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Net.Http.Headers;

namespace Nakheel_Web.Controllers
{   
    public class AuditEnvReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;
        private string Report_conn;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuditEnvReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            this.configuration = configuration;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            conn = configuration.GetConnectionString("DefaultConnection");
            Report_conn = configuration.GetConnectionString("ReportConnectionPath");
            _webHostEnvironment = webHostEnvironment;
        }

        #region [Environmental Audit Report]
        [HttpPost]
        public IActionResult Environmental_Audit_Report(int Env_Audit_Id, string Unique_Id)
        {
            string Qus_List = "False";
            string Status_CA_Access = "False";
            string History_Access = "False";
            string Qus_Photo_List = "False";
            string Closure_Access = "False";
            AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Edit_Env_Audit_ReportTableAdapter Env_List = new AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Edit_Env_Audit_ReportTableAdapter();
            AuditEnv.Audit_Env_DataSet.sp_Am_Edit_Env_Audit_ReportDataTable Env_List_Dtl = Env_List.GetData(Env_Audit_Id);
            if (Env_List_Dtl[0].Status_CA_Access == "True")
            {
                Status_CA_Access = "True";
            }
            AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Env_Audit_Questionnaires_ReportTableAdapter Ques = new AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Env_Audit_Questionnaires_ReportTableAdapter();
            AuditEnv.Audit_Env_DataSet.sp_Am_Env_Audit_Questionnaires_ReportDataTable QuesDtl = Ques.GetData(Env_Audit_Id);
            if (QuesDtl.Count > 0)
            {
                Qus_List = "True";
            }

            AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Env_Audit_Approval_Comments_ReportTableAdapter Appr = new AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Env_Audit_Approval_Comments_ReportTableAdapter();
            AuditEnv.Audit_Env_DataSet.sp_Am_Env_Audit_Approval_Comments_ReportDataTable ApprvDtl = Appr.GetData(Env_Audit_Id);
            if (ApprvDtl.Count > 0)
            {
                History_Access = "True";
            }

            AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Env_Audit_Checklist_Evidence_ReportTableAdapter Evd = new AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Env_Audit_Checklist_Evidence_ReportTableAdapter();
            AuditEnv.Audit_Env_DataSet.sp_Am_Env_Audit_Checklist_Evidence_ReportDataTable EvdDtl = Evd.GetData(Env_Audit_Id);
            if (EvdDtl.Count > 0)
            {
                Qus_Photo_List = "True";
            }


            AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Env_Audit_Closure_Approval_Comments_ReportTableAdapter Cls = new AuditEnv.Audit_Env_DataSetTableAdapters.sp_Am_Env_Audit_Closure_Approval_Comments_ReportTableAdapter();
            AuditEnv.Audit_Env_DataSet.sp_Am_Env_Audit_Closure_Approval_Comments_ReportDataTable ClsDtl = Cls.GetData(Env_Audit_Id);
            if (ClsDtl.Count > 0)
            {
                Closure_Access = "True";
            }

            var path = $"{this._webHostEnvironment.WebRootPath}\\AuditEnvReport\\Environmental_Audit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Audit_Env_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[6];
            parameters[0] = new ReportParameter("Unique_Id_Prm", Env_List_Dtl[0].Unique_Id.ToString());
            parameters[1] = new ReportParameter("Qus_List", Qus_List);
            parameters[2] = new ReportParameter("Status_CA_Access", Status_CA_Access);
            parameters[3] = new ReportParameter("History_Access_Prm", History_Access);
            parameters[4] = new ReportParameter("Qus_Photo_List_Prm", Qus_Photo_List);
            parameters[5] = new ReportParameter("Closure_Access_Prm", Closure_Access);
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Am_Edit_Env_Audit_Report", (DataTable)Env_List_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Am_Env_Audit_Questionnaires_Report", (DataTable)QuesDtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Am_Env_Audit_Checklist_Evidence_Report", (DataTable)EvdDtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Am_Env_Audit_Approval_Comments_Report", (DataTable)ApprvDtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Am_Env_Audit_Closure_Approval_Comments_Report", (DataTable)ClsDtl));

                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Audit_Env_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        #endregion
    }
}
