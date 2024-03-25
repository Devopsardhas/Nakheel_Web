using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Net.Http.Headers;
using System.Xml;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    public class ControlofWorkReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;
        private string Report_conn;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ControlofWorkReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            this.configuration = configuration;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            conn = configuration.GetConnectionString("DefaultConnection");
            Report_conn = configuration.GetConnectionString("ReportConnectionPath");
            _webHostEnvironment = webHostEnvironment;
        }
        #region [Confined Space Permit Report]
        [HttpPost]
        public IActionResult CSP_Requester_Details(int CSP_Id, string Unique_Id)
        {
            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Request_ReportTableAdapter Req = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Request_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Request_ReportDataTable Req_Dtl = Req.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Request_Questionnaire_ReportTableAdapter Ques = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Request_Questionnaire_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Request_Questionnaire_ReportDataTable Ques_Dtl = Ques.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Staff_Comp_Files_ReportTableAdapter Staff = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Staff_Comp_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Staff_Comp_Files_ReportDataTable Staff_Dtl = Staff.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Method_Statement_Files_ReportTableAdapter Method = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Method_Statement_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Method_Statement_Files_ReportDataTable Method_Dtl = Method.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Risk_Assess_Files_ReportTableAdapter Risk = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Risk_Assess_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Risk_Assess_Files_ReportDataTable Risk_Dtl = Risk.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Emr_Plan_Files_ReportTableAdapter Emr = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Emr_Plan_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Emr_Plan_Files_ReportDataTable Emr_Dtl = Emr.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_HSE_Plan_Files_ReportTableAdapter HSE = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_HSE_Plan_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_HSE_Plan_Files_ReportDataTable HSE_Dtl = HSE.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_Csp_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_Csp_Get_History_Approval_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_Csp_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_Csp_Get_Approval_CommentsTableAdapter App_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_Csp_Get_Approval_CommentsTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_Csp_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(CSP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Confined_Space_Permit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\CSP_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Confined Space Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Request_Questionnaire_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Staff_Comp_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Method_Statement_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Risk_Assess_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Emr_Plan_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_HSE_Plan_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_Csp_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_Csp_Get_Approval_Comments", (DataTable)App_Comm_Dtl));

                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "CSP_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        [HttpPost]
        public IActionResult Confined_Space_Report(int CSP_Id, string Unique_Id)
        {
            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Request_ReportTableAdapter Req = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Request_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Request_ReportDataTable Req_Dtl = Req.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Request_Questionnaire_ReportTableAdapter Ques = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Request_Questionnaire_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Request_Questionnaire_ReportDataTable Ques_Dtl = Ques.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Staff_Comp_Files_ReportTableAdapter Staff = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Staff_Comp_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Staff_Comp_Files_ReportDataTable Staff_Dtl = Staff.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Method_Statement_Files_ReportTableAdapter Method = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Method_Statement_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Method_Statement_Files_ReportDataTable Method_Dtl = Method.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Risk_Assess_Files_ReportTableAdapter Risk = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Risk_Assess_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Risk_Assess_Files_ReportDataTable Risk_Dtl = Risk.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Emr_Plan_Files_ReportTableAdapter Emr = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Emr_Plan_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Emr_Plan_Files_ReportDataTable Emr_Dtl = Emr.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_HSE_Plan_Files_ReportTableAdapter HSE = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_HSE_Plan_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_HSE_Plan_Files_ReportDataTable HSE_Dtl = HSE.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Renwal_Evd_Files_ReportTableAdapter EVD = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Renwal_Evd_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Renwal_Evd_Files_ReportDataTable EVD_Dtl = EVD.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Personal_Tools_ReportTableAdapter Tools = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Personal_Tools_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Personal_Tools_ReportDataTable Tools_Dtl = Tools.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Renewal_Details_ReportTableAdapter Renewal_get = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Renewal_Details_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Renewal_Details_ReportDataTable Renewal_get_Dtl = Renewal_get.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Closure_Evd_Files_ReportTableAdapter Closure = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_CSP_Closure_Evd_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_CSP_Closure_Evd_Files_ReportDataTable Closure_get_Dtl = Closure.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_Csp_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_Csp_Get_History_Approval_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_Csp_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_Csp_Get_Approval_CommentsTableAdapter App_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_Csp_Get_Approval_CommentsTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_Csp_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(CSP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Confined_Renewal_Evidence_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\CSP_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Confined Space Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Request_Questionnaire_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Staff_Comp_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Method_Statement_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Risk_Assess_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Emr_Plan_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_HSE_Plan_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Renwal_Evd_Files_Report", (DataTable)EVD_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Personal_Tools_Report", (DataTable)Tools_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Renewal_Details_Report", (DataTable)Renewal_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_CSP_Closure_Evd_Files_Report", (DataTable)Closure_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_Csp_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_Csp_Get_Approval_Comments", (DataTable)App_Comm_Dtl));


                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "CSP_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        #endregion

        #region [Electrical Work Permit Report]
        [HttpPost]
        public IActionResult EWP_Request_Report(int EWP_Id, string Unique_Id)
        {
            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Request_ReportTableAdapter Req = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Request_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Request_ReportDataTable Req_Dtl = Req.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Request_Questionnaire_ReportTableAdapter Ques = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Request_Questionnaire_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Request_Questionnaire_ReportDataTable Ques_Dtl = Ques.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Staff_Files_ReportTableAdapter Staff = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Staff_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Staff_Files_ReportDataTable Staff_Dtl = Staff.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Method_Files_ReportTableAdapter Method = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Method_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Method_Files_ReportDataTable Method_Dtl = Method.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Risk_Files_ReportTableAdapter Risk = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Risk_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Risk_Files_ReportDataTable Risk_Dtl = Risk.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Emr_Files_ReportTableAdapter Emr = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Emr_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Emr_Files_ReportDataTable Emr_Dtl = Emr.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_HSE_Files_ReportTableAdapter HSE = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_HSE_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_HSE_Files_ReportDataTable HSE_Dtl = HSE.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Get_History_Approval_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Get_Approval_CommentsTableAdapter App_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Get_Approval_CommentsTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(EWP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Electrical_Work_Permit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\EWP_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Eletrical Work Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Request_Questionnaire_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Staff_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Method_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Risk_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Emr_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_HSE_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Get_Approval_Comments", (DataTable)App_Comm_Dtl));

                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "EWP_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        [HttpPost]
        public IActionResult EWP_Work_Permit_Report(int EWP_Id, string Unique_Id)
        {
            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Request_ReportTableAdapter Req = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Request_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Request_ReportDataTable Req_Dtl = Req.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Request_Questionnaire_ReportTableAdapter Ques = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Request_Questionnaire_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Request_Questionnaire_ReportDataTable Ques_Dtl = Ques.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Staff_Files_ReportTableAdapter Staff = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Staff_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Staff_Files_ReportDataTable Staff_Dtl = Staff.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Method_Files_ReportTableAdapter Method = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Method_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Method_Files_ReportDataTable Method_Dtl = Method.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Risk_Files_ReportTableAdapter Risk = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Risk_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Risk_Files_ReportDataTable Risk_Dtl = Risk.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Emr_Files_ReportTableAdapter Emr = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Emr_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Emr_Files_ReportDataTable Emr_Dtl = Emr.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_HSE_Files_ReportTableAdapter HSE = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_HSE_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_HSE_Files_ReportDataTable HSE_Dtl = HSE.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Renewal_Evd_Files_ReportTableAdapter EVD = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Renewal_Evd_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Renewal_Evd_Files_ReportDataTable EVD_Dtl = EVD.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Renewal_Details_ReportTableAdapter Renewal_get = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Renewal_Details_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Renewal_Details_ReportDataTable Renewal_get_Dtl = Renewal_get.GetData(EWP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Closure_Evd_Files_ReportTableAdapter Closure = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_EWP_Closure_Evd_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_EWP_Closure_Evd_Files_ReportDataTable Closure_get_Dtl = Closure.GetData(EWP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Electrical_Work_Permit_Evidence_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\EWP_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Eletrical Work Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Request_Questionnaire_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Staff_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Method_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Risk_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Emr_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_HSE_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Renewal_Evd_Files_Report", (DataTable)EVD_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Renewal_Details_Report", (DataTable)Renewal_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_EWP_Closure_Evd_Files_Report", (DataTable)Closure_get_Dtl));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "EWP_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        #endregion

        #region [Hot Work Permit Report]
        [HttpPost]
        public IActionResult Hot_Work_Request_Report(int CSP_Id, string Unique_Id)
        {
            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Request_ReportTableAdapter Req = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Request_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Request_ReportDataTable Req_Dtl = Req.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Questionnaire_ReportTableAdapter Ques = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Questionnaire_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Questionnaire_ReportDataTable Ques_Dtl = Ques.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Staff_Files_ReportTableAdapter Staff = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Staff_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Staff_Files_ReportDataTable Staff_Dtl = Staff.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Method_Files_ReportTableAdapter Method = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Method_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Method_Files_ReportDataTable Method_Dtl = Method.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Risk_Files_ReportTableAdapter Risk = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Risk_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Risk_Files_ReportDataTable Risk_Dtl = Risk.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Emr_Files_ReportTableAdapter Emr = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Emr_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Emr_Files_ReportDataTable Emr_Dtl = Emr.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_HSE_Files_ReportTableAdapter HSE = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_HSE_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_HSE_Files_ReportDataTable HSE_Dtl = HSE.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Get_History_Approval_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Get_Approval_CommentsTableAdapter App_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Get_Approval_CommentsTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(CSP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Hot_Work_Permit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Hot_Work_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Hot Work Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Questionnaire_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Staff_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Method_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Risk_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Emr_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_HSE_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Get_Approval_Comments", (DataTable)App_Comm_Dtl));
                    lr.Refresh(); 
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Hot_Work_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        [HttpPost]
        public IActionResult Hot_Work_Report(int CSP_Id, string Unique_Id)
        {
            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Request_ReportTableAdapter Req = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Request_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Request_ReportDataTable Req_Dtl = Req.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Questionnaire_ReportTableAdapter Ques = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Questionnaire_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Questionnaire_ReportDataTable Ques_Dtl = Ques.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Staff_Files_ReportTableAdapter Staff = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Staff_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Staff_Files_ReportDataTable Staff_Dtl = Staff.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Method_Files_ReportTableAdapter Method = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Method_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Method_Files_ReportDataTable Method_Dtl = Method.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Risk_Files_ReportTableAdapter Risk = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Risk_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Risk_Files_ReportDataTable Risk_Dtl = Risk.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Emr_Files_ReportTableAdapter Emr = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Emr_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Emr_Files_ReportDataTable Emr_Dtl = Emr.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_HSE_Files_ReportTableAdapter HSE = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_HSE_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_HSE_Files_ReportDataTable HSE_Dtl = HSE.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Renewal_Evd_Files_ReportTableAdapter EVD = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Renewal_Evd_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Renewal_Evd_Files_ReportDataTable EVD_Dtl = EVD.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Renewal_Details_ReportTableAdapter Renewal_get = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Renewal_Details_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Renewal_Details_ReportDataTable Renewal_get_Dtl = Renewal_get.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Closure_Evd_Files_ReportTableAdapter Closure = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Closure_Evd_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Closure_Evd_Files_ReportDataTable Closure_get_Dtl = Closure.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Get_History_Approval_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Get_Approval_CommentsTableAdapter App_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_HW_Get_Approval_CommentsTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_HW_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(CSP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Hot_Work_Permit_Evidence_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Hot_Work_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Hot Work Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Questionnaire_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Staff_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Method_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Risk_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Emr_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_HSE_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Renewal_Evd_Files_Report", (DataTable)EVD_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Renewal_Details_Report", (DataTable)Renewal_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Closure_Evd_Files_Report", (DataTable)Closure_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_HW_Get_Approval_Comments", (DataTable)App_Comm_Dtl));

                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Hot_Work_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }


        #endregion

        #region [Work At Height Permit Report]
        [HttpPost]
        public IActionResult Work_Height_Request_Report(int CSP_Id, string Unique_Id)
        {
            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Request_ReportTableAdapter Req = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Request_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Request_ReportDataTable Req_Dtl = Req.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Questionnaire_ReportTableAdapter Ques = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Questionnaire_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Questionnaire_ReportDataTable Ques_Dtl = Ques.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Staff_Files_ReportTableAdapter Staff = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Staff_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Staff_Files_ReportDataTable Staff_Dtl = Staff.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Method_Files_ReportTableAdapter Method = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Method_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Method_Files_ReportDataTable Method_Dtl = Method.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Risk_Files_ReportTableAdapter Risk = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Risk_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Risk_Files_ReportDataTable Risk_Dtl = Risk.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Emr_Files_ReportTableAdapter Emr = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Emr_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Emr_Files_ReportDataTable Emr_Dtl = Emr.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_HSE_Files_ReportTableAdapter HSE = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_HSE_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_HSE_Files_ReportDataTable HSE_Dtl = HSE.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Get_History_Approval_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Get_Approval_CommentsTableAdapter App_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Get_Approval_CommentsTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(CSP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Work_At_Height_Permit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Work_At_Height_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Work At Height Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Questionnaire_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Staff_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Method_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Risk_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Emr_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_HSE_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Get_Approval_Comments", (DataTable)App_Comm_Dtl));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Work_At_Height_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        [HttpPost]
        public IActionResult Work_Height_Report(int CSP_Id, string Unique_Id)
        {
            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Request_ReportTableAdapter Req = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Request_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Request_ReportDataTable Req_Dtl = Req.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Questionnaire_ReportTableAdapter Ques = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Questionnaire_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Questionnaire_ReportDataTable Ques_Dtl = Ques.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Staff_Files_ReportTableAdapter Staff = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Staff_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Staff_Files_ReportDataTable Staff_Dtl = Staff.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Method_Files_ReportTableAdapter Method = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Method_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Method_Files_ReportDataTable Method_Dtl = Method.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Risk_Files_ReportTableAdapter Risk = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Risk_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Risk_Files_ReportDataTable Risk_Dtl = Risk.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Emr_Files_ReportTableAdapter Emr = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Emr_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Emr_Files_ReportDataTable Emr_Dtl = Emr.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_HSE_Files_ReportTableAdapter HSE = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_HSE_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_HSE_Files_ReportDataTable HSE_Dtl = HSE.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Renewal_Evd_Files_ReportTableAdapter EVD = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Renewal_Evd_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Renewal_Evd_Files_ReportDataTable EVD_Dtl = EVD.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Renewal_Details_ReportTableAdapter Renewal_get = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Renewal_Details_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Renewal_Details_ReportDataTable Renewal_get_Dtl = Renewal_get.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Closure_Evd_Files_ReportTableAdapter Closure = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Closure_Evd_Files_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Closure_Evd_Files_ReportDataTable Closure_get_Dtl = Closure.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Get_History_Approval_ReportTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(CSP_Id);

            Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Get_Approval_CommentsTableAdapter App_Comm = new Permit_Work_Data.Control_PermitWorkTableAdapters.sp_Ptw_WAH_Get_Approval_CommentsTableAdapter();
            Permit_Work_Data.Control_PermitWork.sp_Ptw_WAH_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(CSP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Work_At_Height_Permit_Evidence_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Work_At_Height_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Work At Height Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Questionnaire_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Staff_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Method_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Risk_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Emr_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_HSE_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Renewal_Evd_Files_Report", (DataTable)EVD_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Renewal_Details_Report", (DataTable)Renewal_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Closure_Evd_Files_Report", (DataTable)Closure_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_WAH_Get_Approval_Comments", (DataTable)App_Comm_Dtl));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Work_At_Height_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        #endregion


        #region [Fire & Safety Work Permit]
        [HttpPost]
        public IActionResult Fire_Permit_Request_Report(int CSP_Id, string Unique_Id)
        {
            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Request_ReportTableAdapter Req = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Request_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Request_ReportDataTable Req_Dtl = Req.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Question_ReportTableAdapter Ques = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Question_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Question_ReportDataTable Ques_Dtl = Ques.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Staff_Files_ReportTableAdapter Staff = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Staff_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Staff_Files_ReportDataTable Staff_Dtl = Staff.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Method_Files_ReportTableAdapter Method = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Method_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Method_Files_ReportDataTable Method_Dtl = Method.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Risk_Files_ReportTableAdapter Risk = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Risk_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Risk_Files_ReportDataTable Risk_Dtl = Risk.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Emr_Files_ReportTableAdapter Emr = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Emr_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Emr_Files_ReportDataTable Emr_Dtl = Emr.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_HSE_Files_ReportTableAdapter HSE = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_HSE_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_HSE_Files_ReportDataTable HSE_Dtl = HSE.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Extent_Area_Affected_ReportTableAdapter Ext_Area = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Extent_Area_Affected_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Extent_Area_Affected_ReportDataTable Ext_Areas_Dtl = Ext_Area.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Personnel_Affected_ReportTableAdapter Per_Aff = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Personnel_Affected_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Personnel_Affected_ReportDataTable Per_Aff_Dtl = Per_Aff.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Method_Isolation_ReportTableAdapter Method_ISO = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Method_Isolation_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Method_Isolation_ReportDataTable Method_ISO_Dtl = Method_ISO.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Get_History_Approval_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Get_Approval_CommentsTableAdapter App_Comm = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Get_Approval_CommentsTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(CSP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Fire_Safety_Work_Permit_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Fire_Safety_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Fire & Safety Work Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Question_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Staff_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Method_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Risk_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Emr_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_HSE_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Extent_Area_Affected_Report", (DataTable)Ext_Areas_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Personnel_Affected_Report", (DataTable)Per_Aff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Method_Isolation_Report", (DataTable)Method_ISO_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Get_Approval_Comments", (DataTable)App_Comm_Dtl));

                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Fire_Safety_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }

        [HttpPost]
        public IActionResult Fire_Safety_Report(int CSP_Id, string Unique_Id)
        {
            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Request_ReportTableAdapter Req = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Request_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Request_ReportDataTable Req_Dtl = Req.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Question_ReportTableAdapter Ques = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Question_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Question_ReportDataTable Ques_Dtl = Ques.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Staff_Files_ReportTableAdapter Staff = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Staff_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Staff_Files_ReportDataTable Staff_Dtl = Staff.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Method_Files_ReportTableAdapter Method = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Method_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Method_Files_ReportDataTable Method_Dtl = Method.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Risk_Files_ReportTableAdapter Risk = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Risk_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Risk_Files_ReportDataTable Risk_Dtl = Risk.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Emr_Files_ReportTableAdapter Emr = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Emr_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Emr_Files_ReportDataTable Emr_Dtl = Emr.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_HSE_Files_ReportTableAdapter HSE = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_HSE_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_HSE_Files_ReportDataTable HSE_Dtl = HSE.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Renewal_Evd_Files_ReportTableAdapter EVD = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Renewal_Evd_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Renewal_Evd_Files_ReportDataTable EVD_Dtl = EVD.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Renewal_Details_ReportTableAdapter Renewal_get = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Renewal_Details_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Renewal_Details_ReportDataTable Renewal_get_Dtl = Renewal_get.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Closure_Evd_Files_ReportTableAdapter Closure = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Closure_Evd_Files_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Closure_Evd_Files_ReportDataTable Closure_get_Dtl = Closure.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Extent_Area_Affected_ReportTableAdapter Ext_Area = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Extent_Area_Affected_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Extent_Area_Affected_ReportDataTable Ext_Areas_Dtl = Ext_Area.GetData(CSP_Id);


            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Personnel_Affected_ReportTableAdapter Per_Aff = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Personnel_Affected_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Personnel_Affected_ReportDataTable Per_Aff_Dtl = Per_Aff.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Method_Isolation_ReportTableAdapter Method_ISO = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Method_Isolation_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Method_Isolation_ReportDataTable Method_ISO_Dtl = Method_ISO.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Get_History_Approval_ReportTableAdapter Hst_Comm = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Get_History_Approval_ReportTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Get_History_Approval_ReportDataTable Hst_Comm_Dtl = Hst_Comm.GetData(CSP_Id);

            Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Get_Approval_CommentsTableAdapter App_Comm = new Permit_FireSafety_Dataset.Permit_Fire_SafetyTableAdapters.sp_Ptw_FS_Get_Approval_CommentsTableAdapter();
            Permit_FireSafety_Dataset.Permit_Fire_Safety.sp_Ptw_FS_Get_Approval_CommentsDataTable App_Comm_Dtl = App_Comm.GetData(CSP_Id);

            var path = $"{this._webHostEnvironment.WebRootPath}\\ControlofWork\\Fire_Safety_Work_Permit_Evidence_Report.rdlc";
            var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Fire_Safety_Pdf\\";
            if (!Directory.Exists(Savepath))
            {
                Directory.CreateDirectory(Savepath);
            }
            ReportParameter[] parameters = new ReportParameter[2];
            parameters[0] = new ReportParameter("prm", "Fire & Safety Work Permit");
            parameters[1] = new ReportParameter("Unique_Id_Prm", Req_Dtl[0].Unique_Id.ToString());
            using (LocalReport lr = new LocalReport())
            {
                lr.EnableHyperlinks = true;
                lr.EnableExternalImages = true;
                lr.ReportPath = path;
                lr.SetParameters(parameters);

                using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                {
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Request_Report", (DataTable)Req_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Question_Report", (DataTable)Ques_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Staff_Files_Report", (DataTable)Staff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Method_Files_Report", (DataTable)Method_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Risk_Files_Report", (DataTable)Risk_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Emr_Files_Report", (DataTable)Emr_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_HSE_Files_Report", (DataTable)HSE_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Renewal_Evd_Files_Report", (DataTable)EVD_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Renewal_Details_Report", (DataTable)Renewal_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Closure_Evd_Files_Report", (DataTable)Closure_get_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Extent_Area_Affected_Report", (DataTable)Ext_Areas_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Personnel_Affected_Report", (DataTable)Per_Aff_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Method_Isolation_Report", (DataTable)Method_ISO_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Get_History_Approval_Report", (DataTable)Hst_Comm_Dtl));
                    lr.DataSources.Add(new ReportDataSource("sp_Ptw_FS_Get_Approval_Comments", (DataTable)App_Comm_Dtl));
                    lr.Refresh();
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string filenameExtension;
                    byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                    var FilePath = Report_conn + "Fire_Safety_Pdf/" + Unique_Id + ".pdf";
                    return Json(FilePath);
                }
            }
        }
        #endregion

    }
}
