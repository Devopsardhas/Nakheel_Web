using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;

using System.Net.Http.Headers;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    public class SecurityIncReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;
        private string Report_conn;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SecurityIncReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            conn = configuration.GetConnectionString("DefaultConnection");
            Report_conn = configuration.GetConnectionString("ReportConnectionPath");
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SecurityIncident_Report(int Sec_Inc_Report_Id, string Unique_Id)
        {
            string Sec_Photo_List_Prm = "False";
            string Sec_ActionTaken_Prm = "False";
            string Sec_FollowUp_Prm = "False";
            string Sec_Action_ApprovedBy_Prm = "False";
            string Sec_Video_List_Prm = "False";
            string U_ID = "False";
            string Sec_Category_Name = "False";
            string Zone_Name = "False";
            string Community_Name = "False";
            string File_Name = "False";

            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_GetByIdTableAdapter adp = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_GetByIdTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_GetByIdDataTable Dtl = adp.GetData(Sec_Inc_Report_Id);
            U_ID = Dtl[0].Unique_Id;
            Sec_Category_Name = Dtl[0].Category_Name;
            Zone_Name = Dtl[0].Zone_Name;
            Community_Name = Dtl[0].Community_Name;
            File_Name = "SIR_" + U_ID + "_" + Sec_Category_Name + "_" + Zone_Name + "_" + Community_Name;
            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_Photos_GetByIdTableAdapter adp2 = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_Photos_GetByIdTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_Photos_GetByIdDataTable Dtl2 = adp2.GetData(Sec_Inc_Report_Id);

            if (Dtl2.Count > 0)
            {
                Sec_Photo_List_Prm = "True";
            }

            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_ActionTaken_GetByIdTableAdapter adp3 = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_ActionTaken_GetByIdTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_ActionTaken_GetByIdDataTable Dtl3 = adp3.GetData(Sec_Inc_Report_Id);
            if (Dtl3.Count > 0)
            {
                Sec_ActionTaken_Prm = "True";
            }
            
            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_FollowUp_GetByIdTableAdapter adp4 = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_FollowUp_GetByIdTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_FollowUp_GetByIdDataTable Dtl4 = adp4.GetData(Sec_Inc_Report_Id);
            if (Dtl4.Count > 0)
            {
                Sec_FollowUp_Prm = "True";
            }
            
            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_Action_ApprovedByTableAdapter adp5 = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_Action_ApprovedByTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_Action_ApprovedByDataTable Dtl5 = adp5.GetData(Sec_Inc_Report_Id);
            if (Dtl5.Count > 0)
            {
                Sec_Action_ApprovedBy_Prm = "True";
            }
            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_Videos_GetByIdTableAdapter adp6 = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_Videos_GetByIdTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_Videos_GetByIdDataTable Dtl6 = adp6.GetData(Sec_Inc_Report_Id);
            if (Dtl6.Count > 0)
            {
                Sec_Video_List_Prm = "True";
            }
            var path = $"{this._webHostEnvironment.WebRootPath}\\SecurityIncRdlcReport\\SecIncident_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\SecIncidentReportPdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[7];
            parameters[0] = new ReportParameter("SecurityPrm", "Security Incident Details");
            parameters[1] = new ReportParameter("SecIncident_Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            parameters[2] = new ReportParameter("Sec_Photo_List_Prm", Sec_Photo_List_Prm);
            parameters[3] = new ReportParameter("Sec_ActionTaken_Prm", Sec_ActionTaken_Prm);
            parameters[4] = new ReportParameter("Sec_FollowUp_Prm", Sec_FollowUp_Prm);
            parameters[5] = new ReportParameter("Sec_Action_ApprovedBy_Prm", Sec_Action_ApprovedBy_Prm);
            parameters[6] = new ReportParameter("Sec_Video_List_Prm", Sec_Video_List_Prm);

            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + File_Name + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("SecIncident_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Sec_Photo_DS", (DataTable)Dtl2));
                    lr.DataSources.Add(new ReportDataSource("Sec_ActionTaken_DS", (DataTable)Dtl3));
                    lr.DataSources.Add(new ReportDataSource("Sec_FollowUp_DS", (DataTable)Dtl4));
                    lr.DataSources.Add(new ReportDataSource("Sec_ApprovedBy_DS", (DataTable)Dtl5));
                    lr.DataSources.Add(new ReportDataSource("Sec_Video_DS", (DataTable)Dtl6));

                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "SecIncidentReportPdf/" + File_Name + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult SecurityIncidentWOA_Report(int Sec_Inc_Report_Id, string Unique_Id)
        {
            string Sec_Photo_List_Prm = "False";
            string Sec_FollowUp_Prm = "False";

            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_GetByIdTableAdapter adp = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_GetByIdTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_GetByIdDataTable Dtl = adp.GetData(Sec_Inc_Report_Id);

            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_Photos_GetByIdTableAdapter adp2 = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_Photos_GetByIdTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_Photos_GetByIdDataTable Dtl2 = adp2.GetData(Sec_Inc_Report_Id);

            if (Dtl2.Count > 0)
            {
                Sec_Photo_List_Prm = "True";
            }

            Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_FollowUp_GetByIdTableAdapter adp3 = new Security_Incident_Dataset.Security_IncidentTableAdapters.Sec_Incident_FollowUp_GetByIdTableAdapter();
            Security_Incident_Dataset.Security_Incident.Sec_Incident_FollowUp_GetByIdDataTable Dtl3 = adp3.GetData(Sec_Inc_Report_Id);
            if (Dtl3.Count > 0)
            {
                Sec_FollowUp_Prm = "True";
            }
            var path = $"{this._webHostEnvironment.WebRootPath}\\SecurityIncRdlcReport\\SecIncidentWOA_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\SecIncidentReportPdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[4];
            parameters[0] = new ReportParameter("SecurityPrm", "Security Incident Details");
            parameters[1] = new ReportParameter("SecIncident_Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            parameters[2] = new ReportParameter("Sec_Photo_List_Prm", Sec_Photo_List_Prm);
            parameters[3] = new ReportParameter("Sec_FollowUp_Prm", Sec_FollowUp_Prm);

            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("SecIncident_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Sec_Photo_DS", (DataTable)Dtl2));
                    lr.DataSources.Add(new ReportDataSource("Sec_FollowUp_DS", (DataTable)Dtl3));
                    //lr.DataSources.Add(new ReportDataSource("Sec_ApprovedBy_DS", (DataTable)Dtl3));

                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "SecIncidentReportPdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
    }
}
