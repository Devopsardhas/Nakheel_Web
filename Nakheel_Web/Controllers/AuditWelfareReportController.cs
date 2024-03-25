using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Nakheel_Web.Controllers
{
    public class AuditWelfareReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;
        private string Report_conn;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuditWelfareReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            this.configuration = configuration;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            conn = configuration.GetConnectionString("DefaultConnection");
            Report_conn = configuration.GetConnectionString("ReportConnectionPath");
            _webHostEnvironment = webHostEnvironment;
        }



        #region [ Audit Service Provider ]
        [HttpPost]
        public IActionResult Audit_Welfare_Report(int Audit_Welfare_Sch_Id, string Unique_Id)
        {
            string Qus_List = "False";
            string Status_CA_Access = "False";
            string History_Access = "False";
            string Qus_Photo_List = "False";
            AuditData.Audit_WelfareTableAdapters.Welfare_Service_ProviderTableAdapter adp = new AuditData.Audit_WelfareTableAdapters.Welfare_Service_ProviderTableAdapter();
            AuditData.Audit_Welfare.Welfare_Service_ProviderDataTable Dtl = adp.GetData(Audit_Welfare_Sch_Id);
            if (Dtl[0].Status_CA_Access == "True")
            {
                Status_CA_Access = "True";
            }
            AuditData.Audit_WelfareTableAdapters.Welfare_Audit_QuestionnairesTableAdapter adp1 = new AuditData.Audit_WelfareTableAdapters.Welfare_Audit_QuestionnairesTableAdapter();
            AuditData.Audit_Welfare.Welfare_Audit_QuestionnairesDataTable Dtl1 = adp1.GetData(Audit_Welfare_Sch_Id);
            if (Dtl1.Count > 0)
            {
                Qus_List = "True";
            }

            //AuditData.AuditSP_DataTableAdapters.Am_Sp_Qus_PhotosTableAdapter adp3 = new AuditData.AuditSP_DataTableAdapters.Am_Sp_Qus_PhotosTableAdapter();
            //AuditData.AuditSP_Data.Am_Sp_Qus_PhotosDataTable Dtl3 = adp3.GetData(Audit_Welfare_Sch_Id);

            //if (Dtl3.Count > 0)
            //{
            //    Qus_Photo_List = "True";
            //}

            AuditData.Audit_WelfareTableAdapters.Welfare_History_ApprovalTableAdapter adp2 = new AuditData.Audit_WelfareTableAdapters.Welfare_History_ApprovalTableAdapter();
            AuditData.Audit_Welfare.Welfare_History_ApprovalDataTable Dtl2 = adp2.GetData(Audit_Welfare_Sch_Id);
            if (Dtl2.Count > 0)
            {
                History_Access = "True";
            }

            var path = $"{this._webHostEnvironment.WebRootPath}\\AuditRdlcReport\\Welfare_Audit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Audit_Welfare_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[5];
            parameters[0] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            parameters[1] = new ReportParameter("Qus_List", Qus_List);
            parameters[2] = new ReportParameter("Status_CA_Access", Status_CA_Access);
            parameters[3] = new ReportParameter("History_Access_Prm", History_Access);
            parameters[4] = new ReportParameter("Qus_Photo_List_Prm", Qus_Photo_List);
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Service_Provider_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("SP_Qustions", (DataTable)Dtl1));
                    lr.DataSources.Add(new ReportDataSource("History_Access", (DataTable)Dtl2));
                    //lr.DataSources.Add(new ReportDataSource("Qus_Photo_List_Ds", (DataTable)Dtl3));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Audit_Welfare_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        #endregion
    }
}
