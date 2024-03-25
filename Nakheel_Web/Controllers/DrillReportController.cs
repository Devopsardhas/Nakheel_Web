using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Net.Http.Headers;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    public class DrillReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();
        private readonly IConfiguration configuration;
        private string conn;
        private string Report_conn;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DrillReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {

            this.configuration = configuration;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            conn = configuration.GetConnectionString("DefaultConnection");
            Report_conn = configuration.GetConnectionString("ReportConnectionPath");
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public IActionResult Drill_Fire_Report(int Drill_ID, string Unique_Id)
        {
            try
            {
                string? Drill_TypeID = "";
                string path;
                Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_Schedule_RDLCTableAdapter schadp = new Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_Schedule_RDLCTableAdapter();
                Drill_Report_Data.Drill_Report.EMR_Drill_Schedule_RDLCDataTable DtlSch = schadp.GetData(Drill_ID);
                Drill_TypeID = DtlSch[0].Drill_Type_ID.ToString();



                Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_ImpAct_RDLCTableAdapter IMPadp = new Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_ImpAct_RDLCTableAdapter();
                Drill_Report_Data.Drill_Report.EMR_Drill_ImpAct_RDLCDataTable DtlIMP = IMPadp.GetData(Drill_ID);
                if (Drill_TypeID == "4")
                {
                    path = $"{this._webHostEnvironment.WebRootPath}\\DrillRdlc\\FireDrill.rdlc";
                }
                else
                {
                    path = $"{this._webHostEnvironment.WebRootPath}\\DrillRdlc\\DrillForms.rdlc";
                }


                var Savepath = $"{this._webHostEnvironment.WebRootPath}\\Drill_Report_PDF\\";
                if (!Directory.Exists(Savepath))
                {
                    Directory.CreateDirectory(Savepath);
                }
                ReportParameter[] parameters = new ReportParameter[2];
                parameters[0] = new ReportParameter("Drill_Type", DtlSch[0].Drill_Type.ToString());
                parameters[1] = new ReportParameter("Drill_Type_ID", Drill_TypeID);
                using (LocalReport lr = new LocalReport())
                {
                    lr.EnableHyperlinks = true;
                    lr.EnableExternalImages = true;
                    lr.ReportPath = path;
                    lr.SetParameters(parameters);

                    using (FileStream fs = new FileStream(Savepath + Unique_Id + ".pdf", FileMode.Create))
                    {
                        lr.DataSources.Add(new ReportDataSource("ScheduleDataSet", (DataTable)DtlSch));
                        lr.DataSources.Add(new ReportDataSource("Imp_Act", (DataTable)DtlIMP));

                        if (Drill_TypeID == "4")
                        {
                            Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_Fire_RdlcTableAdapter adp = new Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_Fire_RdlcTableAdapter();
                            Drill_Report_Data.Drill_Report.EMR_Drill_Fire_RdlcDataTable Dtl = adp.GetData(Drill_ID);

                            Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_Fire_Obsr_RDLCTableAdapter OBSadp = new Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_Fire_Obsr_RDLCTableAdapter();
                            Drill_Report_Data.Drill_Report.EMR_Drill_Fire_Obsr_RDLCDataTable DtlOBS = OBSadp.GetData(Drill_ID);

                            lr.DataSources.Add(new ReportDataSource("FireDataSet", (DataTable)Dtl));
                            lr.DataSources.Add(new ReportDataSource("OBS_DataSet", (DataTable)DtlOBS));
                        }
                        else
                        {
                            Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_GetCommon_RdlcTableAdapter adp = new Drill_Report_Data.Drill_ReportTableAdapters.EMR_Drill_GetCommon_RdlcTableAdapter();
                            Drill_Report_Data.Drill_Report.EMR_Drill_GetCommon_RdlcDataTable DtlCmm = adp.GetData(Drill_ID);
                            lr.DataSources.Add(new ReportDataSource("CMMFormsDataSet", (DataTable)DtlCmm));
                        }

                        lr.Refresh();
                        Warning[] warnings;
                        string[] streamids;
                        string mimeType;
                        string encoding;
                        string filenameExtension;
                        byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Close();
                        var FilePath = Report_conn + "Drill_Report_PDF/" + Unique_Id + ".pdf";
                        return Json(FilePath);
                    }
                }
            }
            catch (Exception)
            {

                return Json("404");
            }
           
        }
    }
}
