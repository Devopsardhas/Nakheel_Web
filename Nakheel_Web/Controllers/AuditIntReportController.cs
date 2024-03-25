using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Net.Http.Headers;

namespace Nakheel_Web.Controllers
{
    public class AuditIntReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;
        private string Report_conn;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AuditIntReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            this.configuration = configuration;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            conn = configuration.GetConnectionString("DefaultConnection");
            Report_conn = configuration.GetConnectionString("ReportConnectionPath");
            _webHostEnvironment = webHostEnvironment;
        }
        #region [Internal Audit]
        [HttpPost]
        public IActionResult Audit_Internal_Report(int Audit_Internal_Id, string Unique_Id)
        {
            string Qns_List_Prm = "False";
            string Status_CA_Access_Prm = "False";
            string History_Access_Prm = "False";
            string History_Access_CA_Prm = "False";
            string Qns_Photo_List_Prm = "False";
            AuditData.Internal_AuditTableAdapters.Internal_AuditTableAdapter adp = new AuditData.Internal_AuditTableAdapters.Internal_AuditTableAdapter();
            AuditData.Internal_Audit.Internal_AuditDataTable Dtl = adp.GetData(Audit_Internal_Id);
            if (Dtl[0].Status_CA_Access == "True")
            {
                Status_CA_Access_Prm = "True";
            }
            AuditData.Internal_AuditTableAdapters.Internal_Audit_Qns_GetById_ReportTableAdapter adp1 = new AuditData.Internal_AuditTableAdapters.Internal_Audit_Qns_GetById_ReportTableAdapter();
            AuditData.Internal_Audit.Internal_Audit_Qns_GetById_ReportDataTable Dtl1 = adp1.GetData(Audit_Internal_Id);
            if (Dtl1.Count > 0)
            {
                Qns_List_Prm = "True";
            }

            AuditData.Internal_AuditTableAdapters.Internal_Audit_HA_GetbyId_ReportTableAdapter adp2 = new AuditData.Internal_AuditTableAdapters.Internal_Audit_HA_GetbyId_ReportTableAdapter();
            AuditData.Internal_Audit.Internal_Audit_HA_GetbyId_ReportDataTable Dtl2 = adp2.GetData(Audit_Internal_Id);
            if (Dtl2.Count > 0)
            {
                History_Access_Prm = "True";
            }
            AuditData.Internal_AuditTableAdapters.Internal_HA_CA_GetbyId_ReportTableAdapter adp3 = new AuditData.Internal_AuditTableAdapters.Internal_HA_CA_GetbyId_ReportTableAdapter();
            AuditData.Internal_Audit.Internal_HA_CA_GetbyId_ReportDataTable Dtl3 = adp3.GetData(Audit_Internal_Id);
            if (Dtl3.Count > 0)
            {
                History_Access_CA_Prm = "True";
            }

            AuditData.Internal_AuditTableAdapters.Qns_Photos_GetbyId_ReportTableAdapter adp4 = new AuditData.Internal_AuditTableAdapters.Qns_Photos_GetbyId_ReportTableAdapter();
            AuditData.Internal_Audit.Qns_Photos_GetbyId_ReportDataTable Dtl4 = adp4.GetData(Audit_Internal_Id);

            if (Dtl4.Count > 0)
            {
                Qns_Photo_List_Prm = "True";
            }
            var path = $"{this._webHostEnvironment.WebRootPath}\\AuditRdlcReport\\Internal_Audit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Audit_Internal_PDF\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[6];
            parameters[0] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            parameters[1] = new ReportParameter("Qns_List_Prm", Qns_List_Prm);
            parameters[2] = new ReportParameter("Status_CA_Access_Prm", Status_CA_Access_Prm);
            parameters[3] = new ReportParameter("History_Access_Prm", History_Access_Prm);
            parameters[4] = new ReportParameter("History_Access_CA_Prm", History_Access_Prm);
            parameters[5] = new ReportParameter("Qns_Photo_List_Prm", Qns_Photo_List_Prm);
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Internal_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Internal_Qns_DS", (DataTable)Dtl1));
                    lr.DataSources.Add(new ReportDataSource("History_Access_DS", (DataTable)Dtl2));
                    lr.DataSources.Add(new ReportDataSource("History_Access_CA_DS", (DataTable)Dtl3));
                    lr.DataSources.Add(new ReportDataSource("Qns_Photo_List_DS", (DataTable)Dtl4));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Audit_Internal_PDF/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        #endregion
    }
}
