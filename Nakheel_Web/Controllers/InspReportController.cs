using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Drawing.Text;
using System.Drawing;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    public class InspReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;
        private string Report_conn;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public InspReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            this.configuration = configuration;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            conn = configuration.GetConnectionString("DefaultConnection");
            Report_conn = configuration.GetConnectionString("ReportConnectionPath");
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpPost]
        public IActionResult Spot_Insp_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspData.InspSpotTableAdapters.Insp_RequestTableAdapter adp = new InspData.InspSpotTableAdapters.Insp_RequestTableAdapter();
            InspData.InspSpot.Insp_RequestDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Spot_Sub_FindingTableAdapter adp_Sub = new InspData.InspSpotTableAdapters.Insp_Spot_Sub_FindingTableAdapter();
            InspData.InspSpot.Insp_Spot_Sub_FindingDataTable Dtl_Sub = adp_Sub.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Spot_History_ApprovalTableAdapter adp_His = new InspData.InspSpotTableAdapters.Insp_Spot_History_ApprovalTableAdapter();
            InspData.InspSpot.Insp_Spot_History_ApprovalDataTable Dtl_His = adp_His.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Spot_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\InspSpotPdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Spot Inspection Details");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_Request_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Insp_Sub_Find_DS", (DataTable)Dtl_Sub));
                    lr.DataSources.Add(new ReportDataSource("Insp_His_DS", (DataTable)Dtl_His));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "InspSpotPdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Spot_Pre_Insp_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspData.InspSpotTableAdapters.Insp_RequestTableAdapter adp = new InspData.InspSpotTableAdapters.Insp_RequestTableAdapter();
            InspData.InspSpot.Insp_RequestDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Spot_Sub_Pre_FindingTableAdapter adp_Sub = new InspData.InspSpotTableAdapters.Insp_Spot_Sub_Pre_FindingTableAdapter();
            InspData.InspSpot.Insp_Spot_Sub_Pre_FindingDataTable Dtl_Sub = adp_Sub.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Spot_History_ApprovalTableAdapter adp_His = new InspData.InspSpotTableAdapters.Insp_Spot_History_ApprovalTableAdapter();
            InspData.InspSpot.Insp_Spot_History_ApprovalDataTable Dtl_His = adp_His.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Spot_Pre_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Insp_Spot_Pre_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Spot Inspection Details");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_Request_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Insp_Sub_Find_DS", (DataTable)Dtl_Sub));
                    lr.DataSources.Add(new ReportDataSource("Insp_His_DS", (DataTable)Dtl_His));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Insp_Spot_Pre_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Insp_Spot_Document_Review_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspData.InspSpotTableAdapters.Insp_Request_DocTableAdapter adp = new InspData.InspSpotTableAdapters.Insp_Request_DocTableAdapter();
            InspData.InspSpot.Insp_Request_DocDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Request_Sub_DocTableAdapter adpType = new InspData.InspSpotTableAdapters.Insp_Request_Sub_DocTableAdapter();
            InspData.InspSpot.Insp_Request_Sub_DocDataTable Dtl_Sub = adpType.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Spot_Document_Review.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Insp_Spot_Doc_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Document Review");
            parameters[1] = new ReportParameter("Unique_Id", Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Spot_Doc", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Spot_Sub_Doc", (DataTable)Dtl_Sub));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Insp_Spot_Doc_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Insp_Safety_Violation_Report(int Safety_Violation_Id, string Unique_Id)
        {
            string Inc_Photo_List_Prm = "False";

            InspData.InspSpotTableAdapters.Safety_ViolationTableAdapter adp = new InspData.InspSpotTableAdapters.Safety_ViolationTableAdapter();
            InspData.InspSpot.Safety_ViolationDataTable Dtl = adp.GetData(Safety_Violation_Id);

            InspData.InspSpotTableAdapters.Safety_Violation_Sub_TypeTableAdapter adpType = new InspData.InspSpotTableAdapters.Safety_Violation_Sub_TypeTableAdapter();
            InspData.InspSpot.Safety_Violation_Sub_TypeDataTable Dtl_Type = adpType.GetData(Safety_Violation_Id);

            InspData.InspSpotTableAdapters.Safety_Vio_Corrective_ActionTableAdapter adpCA = new InspData.InspSpotTableAdapters.Safety_Vio_Corrective_ActionTableAdapter();
            InspData.InspSpot.Safety_Vio_Corrective_ActionDataTable Dtl_CA = adpCA.GetData(Safety_Violation_Id);

            InspData.InspSpotTableAdapters.Safe_Vio_History_ApprovalTableAdapter adpHA = new InspData.InspSpotTableAdapters.Safe_Vio_History_ApprovalTableAdapter();
            InspData.InspSpot.Safe_Vio_History_ApprovalDataTable Dtl_HA = adpHA.GetData(Safety_Violation_Id);

            InspData.InspSpotTableAdapters.Safety_Violation_PhotosTableAdapter adpPhoto = new InspData.InspSpotTableAdapters.Safety_Violation_PhotosTableAdapter();
            InspData.InspSpot.Safety_Violation_PhotosDataTable Dtl_Photo = adpPhoto.GetData(Safety_Violation_Id);

            if (Dtl_Photo.Count > 0)
            {
                Inc_Photo_List_Prm = "True";
            }

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Safety_Violation_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\InspSafetyVioPdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("prm", "Safety Violation");
            parameters[1] = new ReportParameter("Unique_Id", Dtl[0].Unique_Id.ToString());
            parameters[2] = new ReportParameter("Inc_Photo_List_Prm", Inc_Photo_List_Prm);
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_Type_DS", (DataTable)Dtl_Type));
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_CA_DS", (DataTable)Dtl_CA));
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_HA_DS", (DataTable)Dtl_HA));
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_Photo_DS", (DataTable)Dtl_Photo));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "InspSafetyVioPdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Insp_Safety_Violation_Pre_Report(int Safety_Violation_Id, string Unique_Id)
        {
            string Inc_Photo_List_Prm = "False";

            InspData.InspSpotTableAdapters.Safety_ViolationTableAdapter adp = new InspData.InspSpotTableAdapters.Safety_ViolationTableAdapter();
            InspData.InspSpot.Safety_ViolationDataTable Dtl = adp.GetData(Safety_Violation_Id);

            InspData.InspSpotTableAdapters.Safety_Violation_Sub_TypeTableAdapter adpType = new InspData.InspSpotTableAdapters.Safety_Violation_Sub_TypeTableAdapter();
            InspData.InspSpot.Safety_Violation_Sub_TypeDataTable Dtl_Type = adpType.GetData(Safety_Violation_Id);

            InspData.InspSpotTableAdapters.Insp_Safety_Vio_Pre_Corrective_ActionTableAdapter adpCA = new InspData.InspSpotTableAdapters.Insp_Safety_Vio_Pre_Corrective_ActionTableAdapter();
            InspData.InspSpot.Insp_Safety_Vio_Pre_Corrective_ActionDataTable Dtl_CA = adpCA.GetData(Safety_Violation_Id);

            InspData.InspSpotTableAdapters.Safe_Vio_History_ApprovalTableAdapter adpHA = new InspData.InspSpotTableAdapters.Safe_Vio_History_ApprovalTableAdapter();
            InspData.InspSpot.Safe_Vio_History_ApprovalDataTable Dtl_HA = adpHA.GetData(Safety_Violation_Id);

            InspData.InspSpotTableAdapters.Safety_Violation_PhotosTableAdapter adpPhoto = new InspData.InspSpotTableAdapters.Safety_Violation_PhotosTableAdapter();
            InspData.InspSpot.Safety_Violation_PhotosDataTable Dtl_Photo = adpPhoto.GetData(Safety_Violation_Id);

            if (Dtl_Photo.Count > 0)
            {
                Inc_Photo_List_Prm = "True";
            }

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Safety_Violation_Pre_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Insp_Safety_Vio_Pre_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("prm", "Safety Violation");
            parameters[1] = new ReportParameter("Unique_Id", Dtl[0].Unique_Id.ToString());
            parameters[2] = new ReportParameter("Inc_Photo_List_Prm", Inc_Photo_List_Prm);
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_Type_DS", (DataTable)Dtl_Type));
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_CA_DS", (DataTable)Dtl_CA));
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_HA_DS", (DataTable)Dtl_HA));
                    lr.DataSources.Add(new ReportDataSource("Safe_Vio_Photo_DS", (DataTable)Dtl_Photo));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Insp_Safety_Vio_Pre_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Joint_Insp_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspData.InspSpotTableAdapters.Insp_Joint_RequestTableAdapter adp = new InspData.InspSpotTableAdapters.Insp_Joint_RequestTableAdapter();
            InspData.InspSpot.Insp_Joint_RequestDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Joint_Sub_FindingTableAdapter adp_Sub = new InspData.InspSpotTableAdapters.Insp_Joint_Sub_FindingTableAdapter();
            InspData.InspSpot.Insp_Joint_Sub_FindingDataTable Dtl_Sub = adp_Sub.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Joint_History_ApprovalTableAdapter adp_His = new InspData.InspSpotTableAdapters.Insp_Joint_History_ApprovalTableAdapter();
            InspData.InspSpot.Insp_Joint_History_ApprovalDataTable Dtl_His = adp_His.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Joint_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\InspJointPdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Joint Inspection Details");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_Request_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Insp_Sub_Find_DS", (DataTable)Dtl_Sub));
                    lr.DataSources.Add(new ReportDataSource("Insp_His_DS", (DataTable)Dtl_His));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "InspJointPdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Joint_Pre_Insp_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspData.InspSpotTableAdapters.Insp_Joint_RequestTableAdapter adp = new InspData.InspSpotTableAdapters.Insp_Joint_RequestTableAdapter();
            InspData.InspSpot.Insp_Joint_RequestDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Joint_Pre_Sub_FindingTableAdapter adp_Sub = new InspData.InspSpotTableAdapters.Insp_Joint_Pre_Sub_FindingTableAdapter();
            InspData.InspSpot.Insp_Joint_Pre_Sub_FindingDataTable Dtl_Sub = adp_Sub.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Joint_History_ApprovalTableAdapter adp_His = new InspData.InspSpotTableAdapters.Insp_Joint_History_ApprovalTableAdapter();
            InspData.InspSpot.Insp_Joint_History_ApprovalDataTable Dtl_His = adp_His.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Joint_Pre_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Insp_Joint_Pre_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Joint Inspection Details");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_Request_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Insp_Sub_Find_DS", (DataTable)Dtl_Sub));
                    lr.DataSources.Add(new ReportDataSource("Insp_His_DS", (DataTable)Dtl_His));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Insp_Joint_Pre_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Leader_Insp_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspData.InspSpotTableAdapters.Insp_Leader_RequestTableAdapter adp = new InspData.InspSpotTableAdapters.Insp_Leader_RequestTableAdapter();
            InspData.InspSpot.Insp_Leader_RequestDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Leader_Sub_FindingTableAdapter adp_Sub = new InspData.InspSpotTableAdapters.Insp_Leader_Sub_FindingTableAdapter();
            InspData.InspSpot.Insp_Leader_Sub_FindingDataTable Dtl_Sub = adp_Sub.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Leader_History_ApprovalTableAdapter adp_His = new InspData.InspSpotTableAdapters.Insp_Leader_History_ApprovalTableAdapter();
            InspData.InspSpot.Insp_Leader_History_ApprovalDataTable Dtl_His = adp_His.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Leader_Find_Sub_PhotosTableAdapter adp_Find_P = new InspData.InspSpotTableAdapters.Insp_Leader_Find_Sub_PhotosTableAdapter();
            InspData.InspSpot.Insp_Leader_Find_Sub_PhotosDataTable Dtl_Find_P = adp_Find_P.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Leader_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\InspLeaderPdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Leader Inspection Details");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_Request_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Insp_Sub_Find_DS", (DataTable)Dtl_Sub));
                    lr.DataSources.Add(new ReportDataSource("Insp_His_DS", (DataTable)Dtl_His));
                    lr.DataSources.Add(new ReportDataSource("Dtl_Find_Photo", (DataTable)Dtl_Find_P));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "InspLeaderPdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Leader_Pre_Insp_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspData.InspSpotTableAdapters.Insp_Leader_RequestTableAdapter adp = new InspData.InspSpotTableAdapters.Insp_Leader_RequestTableAdapter();
            InspData.InspSpot.Insp_Leader_RequestDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Leader_Pre_Sub_FindingTableAdapter adp_Sub = new InspData.InspSpotTableAdapters.Insp_Leader_Pre_Sub_FindingTableAdapter();
            InspData.InspSpot.Insp_Leader_Pre_Sub_FindingDataTable Dtl_Sub = adp_Sub.GetData(Insp_Request_Id);

            InspData.InspSpotTableAdapters.Insp_Leader_History_ApprovalTableAdapter adp_His = new InspData.InspSpotTableAdapters.Insp_Leader_History_ApprovalTableAdapter();
            InspData.InspSpot.Insp_Leader_History_ApprovalDataTable Dtl_His = adp_His.GetData(Insp_Request_Id);


            InspData.InspSpotTableAdapters.Insp_Leader_Find_Sub_PhotosTableAdapter adp_Find_P = new InspData.InspSpotTableAdapters.Insp_Leader_Find_Sub_PhotosTableAdapter();
            InspData.InspSpot.Insp_Leader_Find_Sub_PhotosDataTable Dtl_Find_P = adp_Find_P.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Leader_Pre_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Insp_Leader_Pre_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Leader Inspection Details");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_Request_DS", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Insp_Sub_Find_DS", (DataTable)Dtl_Sub));
                    lr.DataSources.Add(new ReportDataSource("Insp_His_DS", (DataTable)Dtl_His));
                    lr.DataSources.Add(new ReportDataSource("Dtl_Find_Photo", (DataTable)Dtl_Find_P));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Insp_Leader_Pre_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Pre_Advisory_Notice_Report(int Complaint_Id, string Unique_Id)
        {
            InspData.InspSpotTableAdapters.Insp_Pre_Advisory_Notice_GetbyIdTableAdapter adp = new InspData.InspSpotTableAdapters.Insp_Pre_Advisory_Notice_GetbyIdTableAdapter();
            InspData.InspSpot.Insp_Pre_Advisory_Notice_GetbyIdDataTable Dtl = adp.GetData(Complaint_Id);
            DateTime StartDate = Convert.ToDateTime(Dtl[0].Date_Complaint);
            string Date = StartDate.ToString("dd-MMM-yyyy");
            

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Preview_Advisory_Notice.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\InspPreAdvisoryNoticePdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Preview Advisory Notice");
            parameters[1] = new ReportParameter("prm_Date", Date);
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_Advisory_Notice_DS", (DataTable)Dtl));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "InspPreAdvisoryNoticePdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Insp_Landscape_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspLandscapeData.InspLandDataTableAdapters.Insp_Landscape_ReportTableAdapter adp = new InspLandscapeData.InspLandDataTableAdapters.Insp_Landscape_ReportTableAdapter();
            InspLandscapeData.InspLandData.Insp_Landscape_ReportDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspLandscapeData.InspLandDataTableAdapters.Insp_Landscape_Sub_Cat_ReportTableAdapter adp1 = new InspLandscapeData.InspLandDataTableAdapters.Insp_Landscape_Sub_Cat_ReportTableAdapter();
            InspLandscapeData.InspLandData.Insp_Landscape_Sub_Cat_ReportDataTable Dtl1 = adp1.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_Land_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\InspLandPdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("prm", "Landscape Inspection Details");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            parameters[2] = new ReportParameter("Ca_Action_Prm", Dtl[0].Walk_In_Insp_Name.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_Land", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Insp_Land_Sub", (DataTable)Dtl1));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "InspLandPdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        [HttpPost]
        public IActionResult Insp_SoftService_Report(int Insp_Request_Id, string Unique_Id)
        {
            InspLandscapeData.InspLandDataTableAdapters.Insp_SoftService_ReportTableAdapter adp = new InspLandscapeData.InspLandDataTableAdapters.Insp_SoftService_ReportTableAdapter();
            InspLandscapeData.InspLandData.Insp_SoftService_ReportDataTable Dtl = adp.GetData(Insp_Request_Id);

            InspLandscapeData.InspLandDataTableAdapters.Insp_SoftService_Sub_Cat_ReportTableAdapter adp1 = new InspLandscapeData.InspLandDataTableAdapters.Insp_SoftService_Sub_Cat_ReportTableAdapter();
            InspLandscapeData.InspLandData.Insp_SoftService_Sub_Cat_ReportDataTable Dtl1 = adp1.GetData(Insp_Request_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\InspRdlcReport\\Insp_SoftService_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\InspSoftServicePdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[3];
            parameters[0] = new ReportParameter("prm", "SoftService Inspection Details");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Dtl[0].Unique_Id.ToString());
            parameters[2] = new ReportParameter("Ca_Action_Prm", Dtl[0].Walk_In_Insp_Name.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("Insp_SoftService", (DataTable)Dtl));
                    lr.DataSources.Add(new ReportDataSource("Insp_SoftService_Sub", (DataTable)Dtl1));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "InspSoftServicePdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
    }
}
