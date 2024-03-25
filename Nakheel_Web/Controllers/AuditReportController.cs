using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace Nakheel_Web.Controllers
{
    public class AuditReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;
        private string Report_conn;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuditReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
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
        public IActionResult Audit_Service_Provider_Report(int Audit_Sp_Sch_Id, string Unique_Id)
        {
            string Qus_List = "False";
            string Status_CA_Access = "False";
            string History_Access = "False";
            string History_Access_Second = "False";
            string Qus_Photo_List = "False";
            AuditData.AuditSP_DataTableAdapters.Service_ProviderTableAdapter adp = new AuditData.AuditSP_DataTableAdapters.Service_ProviderTableAdapter();
            AuditData.AuditSP_Data.Service_ProviderDataTable Dtl = adp.GetData(Audit_Sp_Sch_Id);
            if (Dtl[0].Status_CA_Access == "True")
            {
                Status_CA_Access = "True";
            }
            AuditData.AuditSP_DataTableAdapters.Service_Provider_QuestionTableAdapter adp1 = new AuditData.AuditSP_DataTableAdapters.Service_Provider_QuestionTableAdapter();
            AuditData.AuditSP_Data.Service_Provider_QuestionDataTable Dtl1 = adp1.GetData(Audit_Sp_Sch_Id);
            if (Dtl1.Count > 0)
            {
                Qus_List = "True";
            }

            AuditData.AuditSP_DataTableAdapters.Am_Sp_Qus_PhotosTableAdapter adp3 = new AuditData.AuditSP_DataTableAdapters.Am_Sp_Qus_PhotosTableAdapter();
            AuditData.AuditSP_Data.Am_Sp_Qus_PhotosDataTable Dtl3 = adp3.GetData(Audit_Sp_Sch_Id);
            
            if (Dtl3.Count > 0)
            {
                Qus_Photo_List = "True";
            }

            AuditData.AuditSP_DataTableAdapters.AM_SP_History_ApprovalTableAdapter adp2 = new AuditData.AuditSP_DataTableAdapters.AM_SP_History_ApprovalTableAdapter();
            AuditData.AuditSP_Data.AM_SP_History_ApprovalDataTable Dtl2 = adp2.GetData(Audit_Sp_Sch_Id);
            if (Dtl2.Count > 0)
            {
                History_Access = "True";
            }

            AuditData.AuditSP_DataTableAdapters.Am_Sp_History_Approval_SecondTableAdapter adp4 = new AuditData.AuditSP_DataTableAdapters.Am_Sp_History_Approval_SecondTableAdapter();
            AuditData.AuditSP_Data.Am_Sp_History_Approval_SecondDataTable Dtl4 = adp4.GetData(Audit_Sp_Sch_Id);
            if (Dtl4.Count > 0)
            {
                History_Access_Second = "True";
            }

            var path = $"{this._webHostEnvironment.WebRootPath}\\AuditRdlcReport\\Service_Provider_Audit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Audit_SP_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[6];
            parameters[0] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            parameters[1] = new ReportParameter("Qus_List", Qus_List);
            parameters[2] = new ReportParameter("Status_CA_Access", Status_CA_Access);
            parameters[3] = new ReportParameter("History_Access_Prm", History_Access);
            parameters[4] = new ReportParameter("Qus_Photo_List_Prm", Qus_Photo_List);
            parameters[5] = new ReportParameter("History_Access_Second_Prm", History_Access_Second);
            
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
                    lr.DataSources.Add(new ReportDataSource("Qus_Photo_List_Ds", (DataTable)Dtl3));
                    lr.DataSources.Add(new ReportDataSource("History_Access_Second", (DataTable)Dtl4));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Audit_SP_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        #endregion


        #region [Non Conformance Report ]
        [HttpPost]
        public IActionResult Non_Conformance_Report(int Am_Sp_NCR_Id, string Unique_Id)
        {
            string CA_List = "False";
            string Non_Conform = "True";
            string NCR_History = "False";
            string NCR_History_Second = "False";
            AuditData.AuditSP_DataTableAdapters.Audit_NCR_FormTableAdapter adp = new AuditData.AuditSP_DataTableAdapters.Audit_NCR_FormTableAdapter();
            AuditData.AuditSP_Data.Audit_NCR_FormDataTable Dtl = adp.GetData(Am_Sp_NCR_Id);
            if (Dtl[0].Non_Conformity_Desc == "NA")
            {
                Non_Conform = "False";
            }

            AuditData.AuditSP_DataTableAdapters.Audit_NCR_CATableAdapter adp1 = new AuditData.AuditSP_DataTableAdapters.Audit_NCR_CATableAdapter();
            AuditData.AuditSP_Data.Audit_NCR_CADataTable Dtl1 = adp1.GetData(Am_Sp_NCR_Id);

            if (Dtl1.Count > 0)
            {
                CA_List = "True";
            }

            AuditData.AuditSP_DataTableAdapters.Audit_NCR_History_ApprovalTableAdapter adp2 = new AuditData.AuditSP_DataTableAdapters.Audit_NCR_History_ApprovalTableAdapter();
            AuditData.AuditSP_Data.Audit_NCR_History_ApprovalDataTable Dtl2 = adp2.GetData(Am_Sp_NCR_Id);

            if (Dtl2.Count > 0)
            {
                NCR_History = "True";
            }

            AuditData.AuditSP_DataTableAdapters.Audit_NCR_History_Approval_SecondTableAdapter adp3 = new AuditData.AuditSP_DataTableAdapters.Audit_NCR_History_Approval_SecondTableAdapter();
            AuditData.AuditSP_Data.Audit_NCR_History_Approval_SecondDataTable Dtl3 = adp3.GetData(Am_Sp_NCR_Id);

            if (Dtl3.Count > 0)
            {
                NCR_History_Second = "True";
            }

            var path = $"{this._webHostEnvironment.WebRootPath}\\AuditRdlcReport\\Non_Conformance_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Audit_NCR_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[6];
            parameters[0] = new ReportParameter("prm", "Non Conformance Report");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            parameters[2] = new ReportParameter("CA_List", CA_List);
            parameters[3] = new ReportParameter("Non_Conform", Non_Conform);
            parameters[4] = new ReportParameter("NCR_History", NCR_History);
            parameters[5] = new ReportParameter("NCR_History_Second", NCR_History_Second);
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Audit_NCR_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Audit_NCR_CA_DS", (DataTable)Dtl1));
                    lr.DataSources.Add(new ReportDataSource("Audit_NCR_History_DS", (DataTable)Dtl2));
                    lr.DataSources.Add(new ReportDataSource("Audit_NCR_History_Second_DS", (DataTable)Dtl3));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Audit_NCR_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        #endregion
    }
}
