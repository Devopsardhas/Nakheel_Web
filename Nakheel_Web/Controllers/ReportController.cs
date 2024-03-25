using System;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Reporting.NETCore;
using Nakheel_Web.Data;
using Nakheel_Web.Data.IncidentTableAdapters;
using Nakheel_Web.Data.KnowledgeShareTableAdapters;
using Nakheel_Web.Data.ObservationTableAdapters;

namespace Nakheel_Web.Controllers
{
    [AllowAnonymous]
    public class ReportController : Controller
    {
        private readonly HttpClient client = new HttpClient();

        private readonly IConfiguration configuration;

        private string conn;

        private string Report_conn;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ReportController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
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
        public IActionResult Incident_Report(int Inc_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string value3 = "False";
            Inc_Notification_byIdTableAdapter inc_Notification_byIdTableAdapter = new Inc_Notification_byIdTableAdapter();
            Incident.Inc_Notification_byIdDataTable data = inc_Notification_byIdTableAdapter.GetData(Inc_Id);
            if (data[0].Add_Description_2.ToString() == null || data[0].Add_Description_2.ToString() == "Yes")
            {
                value3 = "True";
            }
            Inc_Notification_Inve_Injury_TypeTableAdapter inc_Notification_Inve_Injury_TypeTableAdapter = new Inc_Notification_Inve_Injury_TypeTableAdapter();
            Incident.Inc_Notification_Inve_Injury_TypeDataTable data2 = inc_Notification_Inve_Injury_TypeTableAdapter.GetData(Inc_Id);
            Inc_Notification_Nature_InjuryTableAdapter inc_Notification_Nature_InjuryTableAdapter = new Inc_Notification_Nature_InjuryTableAdapter();
            Incident.Inc_Notification_Nature_InjuryDataTable data3 = inc_Notification_Nature_InjuryTableAdapter.GetData(Inc_Id);
            Inc_Notification_InjuredPerson_ListTableAdapter inc_Notification_InjuredPerson_ListTableAdapter = new Inc_Notification_InjuredPerson_ListTableAdapter();
            Incident.Inc_Notification_InjuredPerson_ListDataTable data4 = inc_Notification_InjuredPerson_ListTableAdapter.GetData(Inc_Id);
            Inc_Notification_Photos_byIdTableAdapter inc_Notification_Photos_byIdTableAdapter = new Inc_Notification_Photos_byIdTableAdapter();
            Incident.Inc_Notification_Photos_byIdDataTable data5 = inc_Notification_Photos_byIdTableAdapter.GetData(Inc_Id);
            if (data5.Count > 0)
            {
                value = "True";
            }
            Inc_Notification_Videos_by_IdTableAdapter inc_Notification_Videos_by_IdTableAdapter = new Inc_Notification_Videos_by_IdTableAdapter();
            Incident.Inc_Notification_Videos_by_IdDataTable data6 = inc_Notification_Videos_by_IdTableAdapter.GetData(Inc_Id);
            if (data6.Count > 0)
            {
                value2 = "True";
            }
            Inc_Vehicle_AccidentTableAdapter inc_Vehicle_AccidentTableAdapter = new Inc_Vehicle_AccidentTableAdapter();
            Incident.Inc_Vehicle_AccidentDataTable data7 = inc_Vehicle_AccidentTableAdapter.GetData(Inc_Id);
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Incident_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\IncidentReportPdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[12]
            {
            new ReportParameter("prm", "Incident Notification Details"),
            new ReportParameter("Inc_Category_Id", data[0].Inc_Category_Id.ToString()),
            new ReportParameter("Is_PropertyDamaged", data[0].Is_PropertyDamaged.ToString()),
            new ReportParameter("Is_InsuranceClaim", data[0].Is_InsuranceClaim.ToString()),
            new ReportParameter("Is_Injury_Illness", data[0].Is_Injury_Illness.ToString()),
            new ReportParameter("More_About_Injury", data[0].Injured_Person.ToString()),
            new ReportParameter("Inc_Type_Id", data[0].Inc_Type_Id.ToString()),
            new ReportParameter("Inc_Photo_List_Prm", value),
            new ReportParameter("Inc_Video_List_Prm", value2),
            new ReportParameter("Inc_Follow_required_Prm", data[0].Is_Follow_required.ToString()),
            new ReportParameter("Inc_Unique_Id_Prm", data[0].Unique_Id.ToString()),
            new ReportParameter("Inc_Add_Description", value3)
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("Inc_Notification_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injury_Type_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injury_Nature_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injured_Person_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("Inc_Photos_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("Inc_Videos_DS", (DataTable)data6));
            localReport.DataSources.Add(new ReportDataSource("Inc_Vehicle_Accident_DS", (DataTable)data7));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data8 = Report_conn + "IncidentReportPdf/" + Unique_Id + ".pdf";
            return Json(data8);
        }

        [HttpPost]
        public IActionResult Inc_Knowledge_Share_Report(int Inc_Id, string Unique_Id)
        {
            Knowledge_ShareTableAdapter knowledge_ShareTableAdapter = new Knowledge_ShareTableAdapter();
            KnowledgeShare.Knowledge_ShareDataTable data = knowledge_ShareTableAdapter.GetData(Inc_Id);
            Knowledge_Share_Key_PointsTableAdapter knowledge_Share_Key_PointsTableAdapter = new Knowledge_Share_Key_PointsTableAdapter();
            KnowledgeShare.Knowledge_Share_Key_PointsDataTable data2 = knowledge_Share_Key_PointsTableAdapter.GetData(Inc_Id);
            Knowledge_Share_RecommendationsTableAdapter knowledge_Share_RecommendationsTableAdapter = new Knowledge_Share_RecommendationsTableAdapter();
            KnowledgeShare.Knowledge_Share_RecommendationsDataTable data3 = knowledge_Share_RecommendationsTableAdapter.GetData(Inc_Id);
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Key_Lession_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\KnowledgeSharePdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[1]
            {
            new ReportParameter("prm", "Knowledge Share")
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + "KS" + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("Inc_KnowledgeShare_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("Inc_Key_Points_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("Inc_Recommendations_DS", (DataTable)data3));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data4 = Report_conn + "KnowledgeSharePdf/KS" + Unique_Id + ".pdf";
            return Json(data4);
        }

        [HttpPost]
        public IActionResult Investigation_Report(int Inc_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string text = "False";
            Inc_Notification_byIdTableAdapter inc_Notification_byIdTableAdapter = new Inc_Notification_byIdTableAdapter();
            Incident.Inc_Notification_byIdDataTable data = inc_Notification_byIdTableAdapter.GetData(Inc_Id);
            if (data[0].Lead_Investigator_Id == 0)
            {
                text = "True";
            }
            Inc_Notification_Inve_Injury_TypeTableAdapter inc_Notification_Inve_Injury_TypeTableAdapter = new Inc_Notification_Inve_Injury_TypeTableAdapter();
            Incident.Inc_Notification_Inve_Injury_TypeDataTable data2 = inc_Notification_Inve_Injury_TypeTableAdapter.GetData(Inc_Id);
            Inc_Notification_Nature_InjuryTableAdapter inc_Notification_Nature_InjuryTableAdapter = new Inc_Notification_Nature_InjuryTableAdapter();
            Incident.Inc_Notification_Nature_InjuryDataTable data3 = inc_Notification_Nature_InjuryTableAdapter.GetData(Inc_Id);
            Inc_Notification_InjuredPerson_ListTableAdapter inc_Notification_InjuredPerson_ListTableAdapter = new Inc_Notification_InjuredPerson_ListTableAdapter();
            Incident.Inc_Notification_InjuredPerson_ListDataTable data4 = inc_Notification_InjuredPerson_ListTableAdapter.GetData(Inc_Id);
            Inc_Notification_Photos_byIdTableAdapter inc_Notification_Photos_byIdTableAdapter = new Inc_Notification_Photos_byIdTableAdapter();
            Incident.Inc_Notification_Photos_byIdDataTable data5 = inc_Notification_Photos_byIdTableAdapter.GetData(Inc_Id);
            if (data5.Count > 0)
            {
                value = "True";
            }
            Inc_Notification_Videos_by_IdTableAdapter inc_Notification_Videos_by_IdTableAdapter = new Inc_Notification_Videos_by_IdTableAdapter();
            Incident.Inc_Notification_Videos_by_IdDataTable data6 = inc_Notification_Videos_by_IdTableAdapter.GetData(Inc_Id);
            if (data6.Count > 0)
            {
                value2 = "True";
            }
            Inc_Other_Parties_InvolvedTableAdapter inc_Other_Parties_InvolvedTableAdapter = new Inc_Other_Parties_InvolvedTableAdapter();
            Incident.Inc_Other_Parties_InvolvedDataTable data7 = inc_Other_Parties_InvolvedTableAdapter.GetData(Inc_Id);
            Inc_I_C_Unsafe_ActTableAdapter inc_I_C_Unsafe_ActTableAdapter = new Inc_I_C_Unsafe_ActTableAdapter();
            Incident.Inc_I_C_Unsafe_ActDataTable data8 = inc_I_C_Unsafe_ActTableAdapter.GetData(Inc_Id);
            Inc_I_C_Unsafe_CondTableAdapter inc_I_C_Unsafe_CondTableAdapter = new Inc_I_C_Unsafe_CondTableAdapter();
            Incident.Inc_I_C_Unsafe_CondDataTable data9 = inc_I_C_Unsafe_CondTableAdapter.GetData(Inc_Id);
            Inc_Root_Cause_SFTableAdapter inc_Root_Cause_SFTableAdapter = new Inc_Root_Cause_SFTableAdapter();
            Incident.Inc_Root_Cause_SFDataTable data10 = inc_Root_Cause_SFTableAdapter.GetData(Inc_Id);
            Inc_Root_Cause_PFTableAdapter inc_Root_Cause_PFTableAdapter = new Inc_Root_Cause_PFTableAdapter();
            Incident.Inc_Root_Cause_PFDataTable data11 = inc_Root_Cause_PFTableAdapter.GetData(Inc_Id);
            Inc_Mechanism_InjuryIllnessTableAdapter inc_Mechanism_InjuryIllnessTableAdapter = new Inc_Mechanism_InjuryIllnessTableAdapter();
            Incident.Inc_Mechanism_InjuryIllnessDataTable data12 = inc_Mechanism_InjuryIllnessTableAdapter.GetData(Inc_Id);
            Inc_AgencySource_InjuryIllnessTableAdapter inc_AgencySource_InjuryIllnessTableAdapter = new Inc_AgencySource_InjuryIllnessTableAdapter();
            Incident.Inc_AgencySource_InjuryIllnessDataTable data13 = inc_AgencySource_InjuryIllnessTableAdapter.GetData(Inc_Id);
            Inc_Actions_Taken_ImmediatelyTableAdapter inc_Actions_Taken_ImmediatelyTableAdapter = new Inc_Actions_Taken_ImmediatelyTableAdapter();
            Incident.Inc_Actions_Taken_ImmediatelyDataTable data14 = inc_Actions_Taken_ImmediatelyTableAdapter.GetData(Inc_Id);
            Inc_Incident_Root_CauseTableAdapter inc_Incident_Root_CauseTableAdapter = new Inc_Incident_Root_CauseTableAdapter();
            Incident.Inc_Incident_Root_CauseDataTable data15 = inc_Incident_Root_CauseTableAdapter.GetData(Inc_Id);
            Inc_Corrective_ActionsTableAdapter inc_Corrective_ActionsTableAdapter = new Inc_Corrective_ActionsTableAdapter();
            Incident.Inc_Corrective_ActionsDataTable data16 = inc_Corrective_ActionsTableAdapter.GetData(Inc_Id);
            Inc_Investigation_DetailsTableAdapter inc_Investigation_DetailsTableAdapter = new Inc_Investigation_DetailsTableAdapter();
            Incident.Inc_Investigation_DetailsDataTable data17 = inc_Investigation_DetailsTableAdapter.GetData(Inc_Id);
            Inc_Vehicle_AccidentTableAdapter inc_Vehicle_AccidentTableAdapter = new Inc_Vehicle_AccidentTableAdapter();
            Incident.Inc_Vehicle_AccidentDataTable data18 = inc_Vehicle_AccidentTableAdapter.GetData(Inc_Id);
            Inc_AttachementsTableAdapter inc_AttachementsTableAdapter = new Inc_AttachementsTableAdapter();
            Incident.Inc_AttachementsDataTable data19 = inc_AttachementsTableAdapter.GetData(Inc_Id);
            Get_History_Approval_Report_By_IdTableAdapter get_History_Approval_Report_By_IdTableAdapter = new Get_History_Approval_Report_By_IdTableAdapter();
            Incident.Get_History_Approval_Report_By_IdDataTable data20 = get_History_Approval_Report_By_IdTableAdapter.GetData(Inc_Id);
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Investigation_Report.rdlc";
            string text2 = _webHostEnvironment.WebRootPath + "\\IncidentFinalReportPdf\\";
            if (!Directory.Exists(text2))
            {
                Directory.CreateDirectory(text2);
            }
            ReportParameter[] parameters = new ReportParameter[11]
            {
            new ReportParameter("prm", "Incident Notification Details"),
            new ReportParameter("Inc_Category_Id", data[0].Inc_Category_Id.ToString()),
            new ReportParameter("Is_PropertyDamaged", data[0].Is_PropertyDamaged.ToString()),
            new ReportParameter("Is_InsuranceClaim", data[0].Is_InsuranceClaim.ToString()),
            new ReportParameter("Is_Injury_Illness", data[0].Is_Injury_Illness.ToString()),
            new ReportParameter("More_About_Injury", data[0].Injured_Person.ToString()),
            new ReportParameter("Inc_Type_Id", data[0].Inc_Type_Id.ToString()),
            new ReportParameter("Inc_Photo_List_Prm", value),
            new ReportParameter("Inc_Video_List_Prm", value2),
            new ReportParameter("Inc_Follow_required_Prm", data[0].Is_Follow_required.ToString()),
            new ReportParameter("Inc_Unique_Id_Prm", data[0].Unique_Id.ToString())
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text2 + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("Inc_Notification_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injury_Type_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injury_Nature_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injured_Person_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("Inc_Photos_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("Inc_Videos_DS", (DataTable)data6));
            localReport.DataSources.Add(new ReportDataSource("Inc_Other_Parties_DS", (DataTable)data7));
            localReport.DataSources.Add(new ReportDataSource("Inc_I_C_Unsafe_Act", (DataTable)data8));
            localReport.DataSources.Add(new ReportDataSource("Inc_I_C_Unsafe_Cond", (DataTable)data9));
            localReport.DataSources.Add(new ReportDataSource("Inc_Root_Cause_SF", (DataTable)data10));
            localReport.DataSources.Add(new ReportDataSource("Inc_Root_Cause_PF", (DataTable)data11));
            localReport.DataSources.Add(new ReportDataSource("Inc_Mechanism_Injury", (DataTable)data12));
            localReport.DataSources.Add(new ReportDataSource("Inc_AgencySource", (DataTable)data13));
            localReport.DataSources.Add(new ReportDataSource("Inc_Actions_Taken", (DataTable)data14));
            localReport.DataSources.Add(new ReportDataSource("Inc_Incident_Root_Cause", (DataTable)data15));
            localReport.DataSources.Add(new ReportDataSource("Inc_Corrective_Actions", (DataTable)data16));
            localReport.DataSources.Add(new ReportDataSource("Inc_Investigation_Details", (DataTable)data17));
            localReport.DataSources.Add(new ReportDataSource("Inc_Vehicle_Accident_DS", (DataTable)data18));
            localReport.DataSources.Add(new ReportDataSource("Inc_Attachements_DS", (DataTable)data19));
            localReport.DataSources.Add(new ReportDataSource("Inc_Action_Approve_by", (DataTable)data20));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data21 = Report_conn + "IncidentFinalReportPdf/" + Unique_Id + ".pdf";
            return Json(data21);
        }

        [HttpPost]
        public IActionResult Incident_Investigation_Report(int Inc_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string text = "False";
            Inc_Notification_byIdTableAdapter inc_Notification_byIdTableAdapter = new Inc_Notification_byIdTableAdapter();
            Incident.Inc_Notification_byIdDataTable data = inc_Notification_byIdTableAdapter.GetData(Inc_Id);
            if (data[0].Lead_Investigator_Id == 0)
            {
                text = "True";
            }
            Inc_Notification_Inve_Injury_TypeTableAdapter inc_Notification_Inve_Injury_TypeTableAdapter = new Inc_Notification_Inve_Injury_TypeTableAdapter();
            Incident.Inc_Notification_Inve_Injury_TypeDataTable data2 = inc_Notification_Inve_Injury_TypeTableAdapter.GetData(Inc_Id);
            Inc_Notification_Nature_InjuryTableAdapter inc_Notification_Nature_InjuryTableAdapter = new Inc_Notification_Nature_InjuryTableAdapter();
            Incident.Inc_Notification_Nature_InjuryDataTable data3 = inc_Notification_Nature_InjuryTableAdapter.GetData(Inc_Id);
            Inc_Notification_InjuredPerson_ListTableAdapter inc_Notification_InjuredPerson_ListTableAdapter = new Inc_Notification_InjuredPerson_ListTableAdapter();
            Incident.Inc_Notification_InjuredPerson_ListDataTable data4 = inc_Notification_InjuredPerson_ListTableAdapter.GetData(Inc_Id);
            Inc_Notification_Photos_byIdTableAdapter inc_Notification_Photos_byIdTableAdapter = new Inc_Notification_Photos_byIdTableAdapter();
            Incident.Inc_Notification_Photos_byIdDataTable data5 = inc_Notification_Photos_byIdTableAdapter.GetData(Inc_Id);
            if (data5.Count > 0)
            {
                value = "True";
            }
            Inc_Notification_Videos_by_IdTableAdapter inc_Notification_Videos_by_IdTableAdapter = new Inc_Notification_Videos_by_IdTableAdapter();
            Incident.Inc_Notification_Videos_by_IdDataTable data6 = inc_Notification_Videos_by_IdTableAdapter.GetData(Inc_Id);
            if (data6.Count > 0)
            {
                value2 = "True";
            }
            Inc_Other_Parties_InvolvedTableAdapter inc_Other_Parties_InvolvedTableAdapter = new Inc_Other_Parties_InvolvedTableAdapter();
            Incident.Inc_Other_Parties_InvolvedDataTable data7 = inc_Other_Parties_InvolvedTableAdapter.GetData(Inc_Id);
            Inc_I_C_Unsafe_ActTableAdapter inc_I_C_Unsafe_ActTableAdapter = new Inc_I_C_Unsafe_ActTableAdapter();
            Incident.Inc_I_C_Unsafe_ActDataTable data8 = inc_I_C_Unsafe_ActTableAdapter.GetData(Inc_Id);
            Inc_I_C_Unsafe_CondTableAdapter inc_I_C_Unsafe_CondTableAdapter = new Inc_I_C_Unsafe_CondTableAdapter();
            Incident.Inc_I_C_Unsafe_CondDataTable data9 = inc_I_C_Unsafe_CondTableAdapter.GetData(Inc_Id);
            Inc_Root_Cause_SFTableAdapter inc_Root_Cause_SFTableAdapter = new Inc_Root_Cause_SFTableAdapter();
            Incident.Inc_Root_Cause_SFDataTable data10 = inc_Root_Cause_SFTableAdapter.GetData(Inc_Id);
            Inc_Root_Cause_PFTableAdapter inc_Root_Cause_PFTableAdapter = new Inc_Root_Cause_PFTableAdapter();
            Incident.Inc_Root_Cause_PFDataTable data11 = inc_Root_Cause_PFTableAdapter.GetData(Inc_Id);
            Inc_Mechanism_InjuryIllnessTableAdapter inc_Mechanism_InjuryIllnessTableAdapter = new Inc_Mechanism_InjuryIllnessTableAdapter();
            Incident.Inc_Mechanism_InjuryIllnessDataTable data12 = inc_Mechanism_InjuryIllnessTableAdapter.GetData(Inc_Id);
            Inc_AgencySource_InjuryIllnessTableAdapter inc_AgencySource_InjuryIllnessTableAdapter = new Inc_AgencySource_InjuryIllnessTableAdapter();
            Incident.Inc_AgencySource_InjuryIllnessDataTable data13 = inc_AgencySource_InjuryIllnessTableAdapter.GetData(Inc_Id);
            Inc_Actions_Taken_ImmediatelyTableAdapter inc_Actions_Taken_ImmediatelyTableAdapter = new Inc_Actions_Taken_ImmediatelyTableAdapter();
            Incident.Inc_Actions_Taken_ImmediatelyDataTable data14 = inc_Actions_Taken_ImmediatelyTableAdapter.GetData(Inc_Id);
            Inc_Incident_Root_CauseTableAdapter inc_Incident_Root_CauseTableAdapter = new Inc_Incident_Root_CauseTableAdapter();
            Incident.Inc_Incident_Root_CauseDataTable data15 = inc_Incident_Root_CauseTableAdapter.GetData(Inc_Id);
            Inc_Inves_Corrective_Actions_ReportingTableAdapter inc_Inves_Corrective_Actions_ReportingTableAdapter = new Inc_Inves_Corrective_Actions_ReportingTableAdapter();
            Incident.Inc_Inves_Corrective_Actions_ReportingDataTable data16 = inc_Inves_Corrective_Actions_ReportingTableAdapter.GetData(Inc_Id);
            Inc_Investigation_DetailsTableAdapter inc_Investigation_DetailsTableAdapter = new Inc_Investigation_DetailsTableAdapter();
            Incident.Inc_Investigation_DetailsDataTable data17 = inc_Investigation_DetailsTableAdapter.GetData(Inc_Id);
            Inc_Vehicle_AccidentTableAdapter inc_Vehicle_AccidentTableAdapter = new Inc_Vehicle_AccidentTableAdapter();
            Incident.Inc_Vehicle_AccidentDataTable data18 = inc_Vehicle_AccidentTableAdapter.GetData(Inc_Id);
            Inc_AttachementsTableAdapter inc_AttachementsTableAdapter = new Inc_AttachementsTableAdapter();
            Incident.Inc_AttachementsDataTable data19 = inc_AttachementsTableAdapter.GetData(Inc_Id);
            Get_History_Approval_Report_By_IdTableAdapter get_History_Approval_Report_By_IdTableAdapter = new Get_History_Approval_Report_By_IdTableAdapter();
            Incident.Get_History_Approval_Report_By_IdDataTable data20 = get_History_Approval_Report_By_IdTableAdapter.GetData(Inc_Id);
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Incident_Investigation_Report.rdlc";
            string text2 = _webHostEnvironment.WebRootPath + "\\Incident_Investigation_ReportPdf\\";
            if (!Directory.Exists(text2))
            {
                Directory.CreateDirectory(text2);
            }
            ReportParameter[] parameters = new ReportParameter[11]
            {
            new ReportParameter("prm", "Incident Notification Details"),
            new ReportParameter("Inc_Category_Id", data[0].Inc_Category_Id.ToString()),
            new ReportParameter("Is_PropertyDamaged", data[0].Is_PropertyDamaged.ToString()),
            new ReportParameter("Is_InsuranceClaim", data[0].Is_InsuranceClaim.ToString()),
            new ReportParameter("Is_Injury_Illness", data[0].Is_Injury_Illness.ToString()),
            new ReportParameter("More_About_Injury", data[0].Injured_Person.ToString()),
            new ReportParameter("Inc_Type_Id", data[0].Inc_Type_Id.ToString()),
            new ReportParameter("Inc_Photo_List_Prm", value),
            new ReportParameter("Inc_Video_List_Prm", value2),
            new ReportParameter("Inc_Follow_required_Prm", data[0].Is_Follow_required.ToString()),
            new ReportParameter("Inc_Unique_Id_Prm", data[0].Unique_Id.ToString())
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text2 + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("Inc_Notification_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injury_Type_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injury_Nature_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injured_Person_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("Inc_Photos_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("Inc_Videos_DS", (DataTable)data6));
            localReport.DataSources.Add(new ReportDataSource("Inc_Other_Parties_DS", (DataTable)data7));
            localReport.DataSources.Add(new ReportDataSource("Inc_I_C_Unsafe_Act", (DataTable)data8));
            localReport.DataSources.Add(new ReportDataSource("Inc_I_C_Unsafe_Cond", (DataTable)data9));
            localReport.DataSources.Add(new ReportDataSource("Inc_Root_Cause_SF", (DataTable)data10));
            localReport.DataSources.Add(new ReportDataSource("Inc_Root_Cause_PF", (DataTable)data11));
            localReport.DataSources.Add(new ReportDataSource("Inc_Mechanism_Injury", (DataTable)data12));
            localReport.DataSources.Add(new ReportDataSource("Inc_AgencySource", (DataTable)data13));
            localReport.DataSources.Add(new ReportDataSource("Inc_Actions_Taken", (DataTable)data14));
            localReport.DataSources.Add(new ReportDataSource("Inc_Incident_Root_Cause", (DataTable)data15));
            localReport.DataSources.Add(new ReportDataSource("Inc_Corrective_Actions", (DataTable)data16));
            localReport.DataSources.Add(new ReportDataSource("Inc_Investigation_Details", (DataTable)data17));
            localReport.DataSources.Add(new ReportDataSource("Inc_Vehicle_Accident_DS", (DataTable)data18));
            localReport.DataSources.Add(new ReportDataSource("Inc_Attachements_DS", (DataTable)data19));
            localReport.DataSources.Add(new ReportDataSource("Inc_Action_Approve_by", (DataTable)data20));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data21 = Report_conn + "Incident_Investigation_ReportPdf/" + Unique_Id + ".pdf";
            return Json(data21);
        }

        [HttpPost]
        public IActionResult Observation_Report(int Inc_Obser_Report_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string value3 = "False";
            string value4 = "False";
            Obr_ObservationTableAdapter obr_ObservationTableAdapter = new Obr_ObservationTableAdapter();
            Observation.Obr_ObservationDataTable data = obr_ObservationTableAdapter.GetData(Inc_Obser_Report_Id);
            Obr_Observation_TypeTableAdapter obr_Observation_TypeTableAdapter = new Obr_Observation_TypeTableAdapter();
            Observation.Obr_Observation_TypeDataTable data2 = obr_Observation_TypeTableAdapter.GetData(Inc_Obser_Report_Id);
            Obr_Observation_PhotosTableAdapter obr_Observation_PhotosTableAdapter = new Obr_Observation_PhotosTableAdapter();
            Observation.Obr_Observation_PhotosDataTable data3 = obr_Observation_PhotosTableAdapter.GetData(Inc_Obser_Report_Id);
            if (data3.Count > 0)
            {
                value = "True";
            }
            Obr_Observation_VideosTableAdapter obr_Observation_VideosTableAdapter = new Obr_Observation_VideosTableAdapter();
            Observation.Obr_Observation_VideosDataTable data4 = obr_Observation_VideosTableAdapter.GetData(Inc_Obser_Report_Id);
            if (data4.Count > 0)
            {
                value2 = "True";
            }
            Obr_Corrective_ActionTableAdapter obr_Corrective_ActionTableAdapter = new Obr_Corrective_ActionTableAdapter();
            Observation.Obr_Corrective_ActionDataTable data5 = obr_Corrective_ActionTableAdapter.GetData(Inc_Obser_Report_Id);
            if (data5.Count > 0)
            {
                value3 = "True";
            }
            Obs_Action_ApprovedByTableAdapter obs_Action_ApprovedByTableAdapter = new Obs_Action_ApprovedByTableAdapter();
            Observation.Obs_Action_ApprovedByDataTable data6 = obs_Action_ApprovedByTableAdapter.GetData(Inc_Obser_Report_Id);
            if (data6.Count > 0)
            {
                value4 = "True";
            }
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Observation_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\ObservationReportPdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] array = new ReportParameter[6];
            array[0] = new ReportParameter("prm", "Observation Details");
            array[1] = new ReportParameter("OBR_Unique_Id_Prm", data[0].Unique_Id.ToString());
            array[2] = new ReportParameter("OBR_Photo_List_Prm", value);
            array[3] = new ReportParameter("OBR_Video_List_Prm", value2);
            array[4] = new ReportParameter("Observation_Type_Prm", data[0].Observation_Type.ToString());
            array[4] = new ReportParameter("Obr_Corrective_Action_Prm", value3);
            array[5] = new ReportParameter("Obs_Action_ApprovedBy_Prm", value4);
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(array);
            using FileStream fileStream = new FileStream(text + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("Observation_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("OB_Type_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("OB_Photo_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("OB_Video_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("OB_CA_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("OB_ApprovedBy_DS", (DataTable)data6));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array2 = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array2, 0, array2.Length);
            fileStream.Close();
            string data7 = Report_conn + "ObservationReportPdf/" + Unique_Id + ".pdf";
            return Json(data7);
        }

        [HttpPost]
        public byte[] Incident_Report_Mail_Aattc(int Inc_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string value3 = "False";
            Inc_Notification_byIdTableAdapter inc_Notification_byIdTableAdapter = new Inc_Notification_byIdTableAdapter();
            Incident.Inc_Notification_byIdDataTable data = inc_Notification_byIdTableAdapter.GetData(Inc_Id);
            if (data[0].Add_Description_2.ToString() == null || data[0].Add_Description_2.ToString() == "Yes")
            {
                value3 = "True";
            }
            Inc_Notification_Inve_Injury_TypeTableAdapter inc_Notification_Inve_Injury_TypeTableAdapter = new Inc_Notification_Inve_Injury_TypeTableAdapter();
            Incident.Inc_Notification_Inve_Injury_TypeDataTable data2 = inc_Notification_Inve_Injury_TypeTableAdapter.GetData(Inc_Id);
            Inc_Notification_Nature_InjuryTableAdapter inc_Notification_Nature_InjuryTableAdapter = new Inc_Notification_Nature_InjuryTableAdapter();
            Incident.Inc_Notification_Nature_InjuryDataTable data3 = inc_Notification_Nature_InjuryTableAdapter.GetData(Inc_Id);
            Inc_Notification_InjuredPerson_ListTableAdapter inc_Notification_InjuredPerson_ListTableAdapter = new Inc_Notification_InjuredPerson_ListTableAdapter();
            Incident.Inc_Notification_InjuredPerson_ListDataTable data4 = inc_Notification_InjuredPerson_ListTableAdapter.GetData(Inc_Id);
            Inc_Notification_Photos_byIdTableAdapter inc_Notification_Photos_byIdTableAdapter = new Inc_Notification_Photos_byIdTableAdapter();
            Incident.Inc_Notification_Photos_byIdDataTable data5 = inc_Notification_Photos_byIdTableAdapter.GetData(Inc_Id);
            if (data5.Count > 0)
            {
                value = "True";
            }
            Inc_Notification_Videos_by_IdTableAdapter inc_Notification_Videos_by_IdTableAdapter = new Inc_Notification_Videos_by_IdTableAdapter();
            Incident.Inc_Notification_Videos_by_IdDataTable data6 = inc_Notification_Videos_by_IdTableAdapter.GetData(Inc_Id);
            if (data6.Count > 0)
            {
                value2 = "True";
            }
            Inc_Vehicle_AccidentTableAdapter inc_Vehicle_AccidentTableAdapter = new Inc_Vehicle_AccidentTableAdapter();
            Incident.Inc_Vehicle_AccidentDataTable data7 = inc_Vehicle_AccidentTableAdapter.GetData(Inc_Id);
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Incident_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\IncidentReportPdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[12]
            {
            new ReportParameter("prm", "Incident Notification Details"),
            new ReportParameter("Inc_Category_Id", data[0].Inc_Category_Id.ToString()),
            new ReportParameter("Is_PropertyDamaged", data[0].Is_PropertyDamaged.ToString()),
            new ReportParameter("Is_InsuranceClaim", data[0].Is_InsuranceClaim.ToString()),
            new ReportParameter("Is_Injury_Illness", data[0].Is_Injury_Illness.ToString()),
            new ReportParameter("More_About_Injury", data[0].Injured_Person.ToString()),
            new ReportParameter("Inc_Type_Id", data[0].Inc_Type_Id.ToString()),
            new ReportParameter("Inc_Photo_List_Prm", value),
            new ReportParameter("Inc_Video_List_Prm", value2),
            new ReportParameter("Inc_Follow_required_Prm", data[0].Is_Follow_required.ToString()),
            new ReportParameter("Inc_Unique_Id_Prm", data[0].Unique_Id.ToString()),
            new ReportParameter("Inc_Add_Description", value3)
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("Inc_Notification_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injury_Type_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injury_Nature_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("Inc_Injured_Person_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("Inc_Photos_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("Inc_Videos_DS", (DataTable)data6));
            localReport.DataSources.Add(new ReportDataSource("Inc_Vehicle_Accident_DS", (DataTable)data7));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string text2 = Report_conn + "IncidentReportPdf/" + Unique_Id + ".pdf";
            return array;
        }

        [HttpPost]
        public byte[] Inc_Knowledge_Share_Report_Mail_Aattc(int Inc_Id, string Unique_Id)
        {
            Knowledge_ShareTableAdapter knowledge_ShareTableAdapter = new Knowledge_ShareTableAdapter();
            KnowledgeShare.Knowledge_ShareDataTable data = knowledge_ShareTableAdapter.GetData(Inc_Id);
            Knowledge_Share_Key_PointsTableAdapter knowledge_Share_Key_PointsTableAdapter = new Knowledge_Share_Key_PointsTableAdapter();
            KnowledgeShare.Knowledge_Share_Key_PointsDataTable data2 = knowledge_Share_Key_PointsTableAdapter.GetData(Inc_Id);
            Knowledge_Share_RecommendationsTableAdapter knowledge_Share_RecommendationsTableAdapter = new Knowledge_Share_RecommendationsTableAdapter();
            KnowledgeShare.Knowledge_Share_RecommendationsDataTable data3 = knowledge_Share_RecommendationsTableAdapter.GetData(Inc_Id);
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Key_Lession_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\KnowledgeSharePdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[1]
            {
            new ReportParameter("prm", "Knowledge Share")
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + "KnowledgeShare_" + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("Inc_KnowledgeShare_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("Inc_Key_Points_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("Inc_Recommendations_DS", (DataTable)data3));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string text2 = Report_conn + "KnowledgeSharePdf/KnowledgeShare_" + Unique_Id + ".pdf";
            return array;
        }

        [HttpPost]
        public IActionResult Genral_Work_Permit_Report(int Sign_Up_Id, string Unique_Id)
        {
            Service_Provider_Sign_Up_byIdTableAdapter service_Provider_Sign_Up_byIdTableAdapter = new Service_Provider_Sign_Up_byIdTableAdapter();
            KnowledgeShare.Service_Provider_Sign_Up_byIdDataTable data = service_Provider_Sign_Up_byIdTableAdapter.GetData(Sign_Up_Id);
            Service_Provider_Sign_UpReqAtt_byIdTableAdapter service_Provider_Sign_UpReqAtt_byIdTableAdapter = new Service_Provider_Sign_UpReqAtt_byIdTableAdapter();
            KnowledgeShare.Service_Provider_Sign_UpReqAtt_byIdDataTable data2 = service_Provider_Sign_UpReqAtt_byIdTableAdapter.GetData(Sign_Up_Id);
            Service_Provider_Sign_UpMajor_HSE_Risk_byIdTableAdapter service_Provider_Sign_UpMajor_HSE_Risk_byIdTableAdapter = new Service_Provider_Sign_UpMajor_HSE_Risk_byIdTableAdapter();
            KnowledgeShare.Service_Provider_Sign_UpMajor_HSE_Risk_byIdDataTable data3 = service_Provider_Sign_UpMajor_HSE_Risk_byIdTableAdapter.GetData(Sign_Up_Id);
            Service_Provider_Sign_UpUpdate_HistoryIdTableAdapter service_Provider_Sign_UpUpdate_HistoryIdTableAdapter = new Service_Provider_Sign_UpUpdate_HistoryIdTableAdapter();
            KnowledgeShare.Service_Provider_Sign_UpUpdate_HistoryIdDataTable data4 = service_Provider_Sign_UpUpdate_HistoryIdTableAdapter.GetData(Sign_Up_Id);
            Service_Provider_Building_List_ReportTableAdapter service_Provider_Building_List_ReportTableAdapter = new Service_Provider_Building_List_ReportTableAdapter();
            KnowledgeShare.Service_Provider_Building_List_ReportDataTable data5 = service_Provider_Building_List_ReportTableAdapter.GetData(Sign_Up_Id);
            Service_Provider_Sign_Work_Superviosor_ReportTableAdapter service_Provider_Sign_Work_Superviosor_ReportTableAdapter = new Service_Provider_Sign_Work_Superviosor_ReportTableAdapter();
            KnowledgeShare.Service_Provider_Sign_Work_Superviosor_ReportDataTable data6 = service_Provider_Sign_Work_Superviosor_ReportTableAdapter.GetData(Sign_Up_Id);
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Genral_Work_Permit_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\Genral_Work_Permit_Pdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[1]
            {
            new ReportParameter("prm", "Genral Work Permit")
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + "Genral_Work_Permit" + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("SP_Sign_Up_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("SP_Sign_UpReqAtt_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("SP_Sign_UpMajor_HSE_Risk_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("SP_Sign_UpUpdate_History_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("SP_Sign_Up_Buildinglist", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("SP_Sign_Work_Supervioser", (DataTable)data6));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data7 = Report_conn + "Genral_Work_Permit_Pdf/Genral_Work_Permit" + Unique_Id + ".pdf";
            return Json(data7);
        }

        //[HttpPost]
        //public IActionResult InternalAudit_Report(string Internal_Audit_Id, string Unique_Id, string Questionnaire_Id)
        //{
        //    string text = "AUD-000" + Unique_Id;
        //    int value = Convert.ToInt32(Internal_Audit_Id);
        //    int value2 = Convert.ToInt32(Questionnaire_Id);
        //    AUD_GETBYID_INTERNAL_AUDIT_REPORTTableAdapter aUD_GETBYID_INTERNAL_AUDIT_REPORTTableAdapter = new AUD_GETBYID_INTERNAL_AUDIT_REPORTTableAdapter();
        //    AuditDataSet.AUD_GETBYID_INTERNAL_AUDIT_REPORTDataTable data = aUD_GETBYID_INTERNAL_AUDIT_REPORTTableAdapter.GetData(value);
        //    AUD_GETBYID_INTERNAL_AUDIT_TEAM_REPORTTableAdapter aUD_GETBYID_INTERNAL_AUDIT_TEAM_REPORTTableAdapter = new AUD_GETBYID_INTERNAL_AUDIT_TEAM_REPORTTableAdapter();
        //    AuditDataSet.AUD_GETBYID_INTERNAL_AUDIT_TEAM_REPORTDataTable data2 = aUD_GETBYID_INTERNAL_AUDIT_TEAM_REPORTTableAdapter.GetData(value);
        //    Aud_GetById_Service_Provider_Team_ReportTableAdapter aud_GetById_Service_Provider_Team_ReportTableAdapter = new Aud_GetById_Service_Provider_Team_ReportTableAdapter();
        //    AuditDataSet.Aud_GetById_Service_Provider_Team_ReportDataTable data3 = aud_GetById_Service_Provider_Team_ReportTableAdapter.GetData(value);
        //    Aud_Internal_Audit_History_by_Aud_Id_ReportTableAdapter aud_Internal_Audit_History_by_Aud_Id_ReportTableAdapter = new Aud_Internal_Audit_History_by_Aud_Id_ReportTableAdapter();
        //    AuditDataSet.Aud_Internal_Audit_History_by_Aud_Id_ReportDataTable data4 = aud_Internal_Audit_History_by_Aud_Id_ReportTableAdapter.GetData(value);
        //    Aud_GetById_Internal_Audit_NCR_Details_ReportTableAdapter aud_GetById_Internal_Audit_NCR_Details_ReportTableAdapter = new Aud_GetById_Internal_Audit_NCR_Details_ReportTableAdapter();
        //    AuditDataSet.Aud_GetById_Internal_Audit_NCR_Details_ReportDataTable data5 = aud_GetById_Internal_Audit_NCR_Details_ReportTableAdapter.GetData(value, value2);
        //    string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Audit_Internal_Report.rdlc";
        //    string text2 = _webHostEnvironment.WebRootPath + "\\Audit_Internal_Report_Pdf\\";
        //    if (!Directory.Exists(text2))
        //    {
        //        Directory.CreateDirectory(text2);
        //    }
        //    ReportParameter[] parameters = new ReportParameter[1]
        //    {
        //    new ReportParameter("prm", "Audit Internal Report")
        //    };
        //    using LocalReport localReport = new LocalReport();
        //    localReport.EnableHyperlinks = true;
        //    localReport.EnableExternalImages = true;
        //    localReport.ReportPath = reportPath;
        //    localReport.SetParameters(parameters);
        //    using FileStream fileStream = new FileStream(text2 + "Audit_Internal" + text + ".pdf", FileMode.Create);
        //    localReport.DataSources.Add(new ReportDataSource("SP_InAudit_DS", (DataTable)data));
        //    localReport.DataSources.Add(new ReportDataSource("SP_InAudit_AuditTeam_DS", (DataTable)data2));
        //    localReport.DataSources.Add(new ReportDataSource("SP_InAudit_ServiceTeam_DS", (DataTable)data3));
        //    localReport.DataSources.Add(new ReportDataSource("SP_InAudit_Update_History_DS", (DataTable)data4));
        //    localReport.DataSources.Add(new ReportDataSource("SP_InAudit_Audit_NCR_Details_DS", (DataTable)data5));
        //    localReport.Refresh();
        //    string mimeType;
        //    string encoding;
        //    string fileNameExtension;
        //    string[] streams;
        //    Warning[] warnings;
        //    byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
        //    fileStream.Write(array, 0, array.Length);
        //    fileStream.Close();
        //    string data6 = Report_conn + "Audit_Internal_Report_Pdf/Audit_Internal" + text + ".pdf";
        //    return Json(data6);
        //}

        [HttpPost]
        public IActionResult Insp_HandOver_Report(int Insp_HndOver_Building_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string value3 = "False";
            string value4 = "False";
            string value5 = "False";
            HandOver_Building_GetByIdTableAdapter handOver_Building_GetByIdTableAdapter = new HandOver_Building_GetByIdTableAdapter();
            Observation.HandOver_Building_GetByIdDataTable data = handOver_Building_GetByIdTableAdapter.GetData(Insp_HndOver_Building_Id);
            HandOver_Building_PhotosTableAdapter handOver_Building_PhotosTableAdapter = new HandOver_Building_PhotosTableAdapter();
            Observation.HandOver_Building_PhotosDataTable data2 = handOver_Building_PhotosTableAdapter.GetData(Insp_HndOver_Building_Id);
            if (data2.Count > 0)
            {
                value = "True";
            }
            HandOver_CA_GetByIdTableAdapter handOver_CA_GetByIdTableAdapter = new HandOver_CA_GetByIdTableAdapter();
            Observation.HandOver_CA_GetByIdDataTable data3 = handOver_CA_GetByIdTableAdapter.GetData(Insp_HndOver_Building_Id);
            if (data3.Count > 0)
            {
                value2 = "True";
            }
            HandOver_Action_ApprovedByTableAdapter handOver_Action_ApprovedByTableAdapter = new HandOver_Action_ApprovedByTableAdapter();
            Observation.HandOver_Action_ApprovedByDataTable data4 = handOver_Action_ApprovedByTableAdapter.GetData(Insp_HndOver_Building_Id);
            if (data4.Count > 0)
            {
                value3 = "True";
            }
            HandOver_Findings_GetbyIdTableAdapter handOver_Findings_GetbyIdTableAdapter = new HandOver_Findings_GetbyIdTableAdapter();
            Observation.HandOver_Findings_GetbyIdDataTable data5 = handOver_Findings_GetbyIdTableAdapter.GetData(Insp_HndOver_Building_Id);
            if (data5.Count > 0)
            {
                value4 = "True";
            }
            HandOver_Obs_GetbyIdTableAdapter handOver_Obs_GetbyIdTableAdapter = new HandOver_Obs_GetbyIdTableAdapter();
            Observation.HandOver_Obs_GetbyIdDataTable data6 = handOver_Obs_GetbyIdTableAdapter.GetData(Insp_HndOver_Building_Id);
            if (data6.Count > 0)
            {
                value5 = "True";
            }
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Insp_Handover_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\InspHandoverReportPdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[7]
            {
            new ReportParameter("HndOver_Prm", "Handover Details"),
            new ReportParameter("HndOver_Unique_Id_Prm", data[0].Unique_Id.ToString()),
            new ReportParameter("HndOver_Photo_List_Prm", value),
            new ReportParameter("HndOver_Corrective_Action_Prm", value2),
            new ReportParameter("HndOver_Action_ApprovedBy_Prm", value3),
            new ReportParameter("HndOver_Findings_Prm", value4),
            new ReportParameter("HndOver_Obs_Prm", value5)
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("Insp_HandOver_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("HandOver_Photo_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("HandOver_CA_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("HandOver_ApprovedBy_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("HandOver_Findings_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("HandOver_Obs_DS", (DataTable)data6));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data7 = Report_conn + "InspHandoverReportPdf/" + Unique_Id + ".pdf";
            return Json(data7);
        }

        [HttpPost]
        public IActionResult Insp_HealthSafety_Report(int Insp_HealthSafety_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string value3 = "False";
            string value4 = "False";
            string value5 = "False";
            Insp_HealthSafety_GetByIdTableAdapter insp_HealthSafety_GetByIdTableAdapter = new Insp_HealthSafety_GetByIdTableAdapter();
            Observation.Insp_HealthSafety_GetByIdDataTable data = insp_HealthSafety_GetByIdTableAdapter.GetData(Insp_HealthSafety_Id);
            Insp_HealthSafety_Photos_GetByIdTableAdapter insp_HealthSafety_Photos_GetByIdTableAdapter = new Insp_HealthSafety_Photos_GetByIdTableAdapter();
            Observation.Insp_HealthSafety_Photos_GetByIdDataTable data2 = insp_HealthSafety_Photos_GetByIdTableAdapter.GetData(Insp_HealthSafety_Id);
            if (data2.Count > 0)
            {
                value = "True";
            }
            HealthSafety_Findings_GetbyIdTableAdapter healthSafety_Findings_GetbyIdTableAdapter = new HealthSafety_Findings_GetbyIdTableAdapter();
            Observation.HealthSafety_Findings_GetbyIdDataTable data3 = healthSafety_Findings_GetbyIdTableAdapter.GetData(Insp_HealthSafety_Id);
            if (data3.Count > 0)
            {
                value4 = "True";
            }
            Insp_HealthSafety_Obs_GetbyIdTableAdapter insp_HealthSafety_Obs_GetbyIdTableAdapter = new Insp_HealthSafety_Obs_GetbyIdTableAdapter();
            Observation.Insp_HealthSafety_Obs_GetbyIdDataTable data4 = insp_HealthSafety_Obs_GetbyIdTableAdapter.GetData(Insp_HealthSafety_Id);
            if (data4.Count > 0)
            {
                value5 = "True";
            }
            Insp_HealthSafety_CA_GetByIdTableAdapter insp_HealthSafety_CA_GetByIdTableAdapter = new Insp_HealthSafety_CA_GetByIdTableAdapter();
            Observation.Insp_HealthSafety_CA_GetByIdDataTable data5 = insp_HealthSafety_CA_GetByIdTableAdapter.GetData(Insp_HealthSafety_Id);
            if (data5.Count > 0)
            {
                value2 = "True";
            }
            Insp_HealthSafety_Action_ApprovedByTableAdapter insp_HealthSafety_Action_ApprovedByTableAdapter = new Insp_HealthSafety_Action_ApprovedByTableAdapter();
            Observation.Insp_HealthSafety_Action_ApprovedByDataTable data6 = insp_HealthSafety_Action_ApprovedByTableAdapter.GetData(Insp_HealthSafety_Id);
            if (data6.Count > 0)
            {
                value3 = "True";
            }
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Insp_HealthSafety_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\InspHealthSafetyReportPdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[7]
            {
            new ReportParameter("HealthSafety_Prm", "Health & Safety Details"),
            new ReportParameter("HealthSafety_Unique_Id_Prm", data[0].Unique_Id.ToString()),
            new ReportParameter("HealthSafety_Photo_List_Prm", value),
            new ReportParameter("HealthSafety_Findings_Prm", value4),
            new ReportParameter("HealthSafety_Obs_Prm", value5),
            new ReportParameter("HealthSafety_Corrective_Action_Prm", value2),
            new ReportParameter("HealthSafety_Action_ApprovedBy_Prm", value3)
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("HealthSafety_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("HealthSafety_Photo_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("HealthSafety_Findings_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("HealthSafety_Obs_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("HS_CA_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("HS_ApprovedBy_DS", (DataTable)data6));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data7 = Report_conn + "InspHealthSafetyReportPdf/" + Unique_Id + ".pdf";
            return Json(data7);
        }

        [HttpPost]
        public IActionResult Insp_ServiceProvider_Report(int Insp_ServiceProvider_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string value3 = "False";
            string value4 = "False";
            string value5 = "False";
            ServiceProvider_GetByIdTableAdapter serviceProvider_GetByIdTableAdapter = new ServiceProvider_GetByIdTableAdapter();
            Observation.ServiceProvider_GetByIdDataTable data = serviceProvider_GetByIdTableAdapter.GetData(Insp_ServiceProvider_Id);
            ServiceProvider_Photos_GetByIdTableAdapter serviceProvider_Photos_GetByIdTableAdapter = new ServiceProvider_Photos_GetByIdTableAdapter();
            Observation.ServiceProvider_Photos_GetByIdDataTable data2 = serviceProvider_Photos_GetByIdTableAdapter.GetData(Insp_ServiceProvider_Id);
            if (data2.Count > 0)
            {
                value = "True";
            }
            ServiceProvider_CA_GetByIdTableAdapter serviceProvider_CA_GetByIdTableAdapter = new ServiceProvider_CA_GetByIdTableAdapter();
            Observation.ServiceProvider_CA_GetByIdDataTable data3 = serviceProvider_CA_GetByIdTableAdapter.GetData(Insp_ServiceProvider_Id);
            if (data3.Count > 0)
            {
                value2 = "True";
            }
            ServiceProvider_Action_ApprovedByTableAdapter serviceProvider_Action_ApprovedByTableAdapter = new ServiceProvider_Action_ApprovedByTableAdapter();
            Observation.ServiceProvider_Action_ApprovedByDataTable data4 = serviceProvider_Action_ApprovedByTableAdapter.GetData(Insp_ServiceProvider_Id);
            if (data4.Count > 0)
            {
                value3 = "True";
            }
            ServiceProvider_Findings_GetbyIdTableAdapter serviceProvider_Findings_GetbyIdTableAdapter = new ServiceProvider_Findings_GetbyIdTableAdapter();
            Observation.ServiceProvider_Findings_GetbyIdDataTable data5 = serviceProvider_Findings_GetbyIdTableAdapter.GetData(Insp_ServiceProvider_Id);
            if (data5.Count > 0)
            {
                value4 = "True";
            }
            ServiceProvider_Obs_GetbyIdTableAdapter serviceProvider_Obs_GetbyIdTableAdapter = new ServiceProvider_Obs_GetbyIdTableAdapter();
            Observation.ServiceProvider_Obs_GetbyIdDataTable data6 = serviceProvider_Obs_GetbyIdTableAdapter.GetData(Insp_ServiceProvider_Id);
            if (data6.Count > 0)
            {
                value5 = "True";
            }
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Insp_ServiceProvider_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\InspServiceProviderReportPdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[7]
            {
            new ReportParameter("ServiceProvider_Prm", "Service Provider Details"),
            new ReportParameter("ServiceProvider_Unique_Id_Prm", data[0].Unique_Id.ToString()),
            new ReportParameter("ServiceProvider_Photo_List_Prm", value),
            new ReportParameter("ServiceProvider_Corrective_Action_Prm", value2),
            new ReportParameter("ServiceProvider_Action_ApprovedBy_Prm", value3),
            new ReportParameter("ServiceProvider_Findings_Prm", value4),
            new ReportParameter("ServiceProvider_Obs_Prm", value5)
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("ServiceProvider_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("ServiceProvider_Photo_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("SP_CA_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("SP_ApprovedBy_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("SP_Findings_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("SP_Obs_DS", (DataTable)data6));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data7 = Report_conn + "InspServiceProviderReportPdf/" + Unique_Id + ".pdf";
            return Json(data7);
        }

        [HttpPost]
        public IActionResult Insp_FireLifeSafety_Report(int Insp_Request_Id, string Unique_Id)
        {
            string value = "False";
            string value2 = "False";
            string value3 = "False";
            string value4 = "False";
            string value5 = "False";
            FireLifeSafety_GetByIdTableAdapter fireLifeSafety_GetByIdTableAdapter = new FireLifeSafety_GetByIdTableAdapter();
            Observation.FireLifeSafety_GetByIdDataTable data = fireLifeSafety_GetByIdTableAdapter.GetData(Insp_Request_Id);
            FireLifeSafety_PhotosTableAdapter fireLifeSafety_PhotosTableAdapter = new FireLifeSafety_PhotosTableAdapter();
            Observation.FireLifeSafety_PhotosDataTable data2 = fireLifeSafety_PhotosTableAdapter.GetData(Insp_Request_Id);
            if (data2.Count > 0)
            {
                value = "True";
            }
            FireLifeSafety_CA_GetByIdTableAdapter fireLifeSafety_CA_GetByIdTableAdapter = new FireLifeSafety_CA_GetByIdTableAdapter();
            Observation.FireLifeSafety_CA_GetByIdDataTable data3 = fireLifeSafety_CA_GetByIdTableAdapter.GetData(Insp_Request_Id);
            if (data3.Count > 0)
            {
                value2 = "True";
            }
            FireLifeSafety_Action_ApprovedByTableAdapter fireLifeSafety_Action_ApprovedByTableAdapter = new FireLifeSafety_Action_ApprovedByTableAdapter();
            Observation.FireLifeSafety_Action_ApprovedByDataTable data4 = fireLifeSafety_Action_ApprovedByTableAdapter.GetData(Insp_Request_Id);
            if (data4.Count > 0)
            {
                value3 = "True";
            }
            FireLifeSafety_Findings_GetbyIdTableAdapter fireLifeSafety_Findings_GetbyIdTableAdapter = new FireLifeSafety_Findings_GetbyIdTableAdapter();
            Observation.FireLifeSafety_Findings_GetbyIdDataTable data5 = fireLifeSafety_Findings_GetbyIdTableAdapter.GetData(Insp_Request_Id);
            if (data5.Count > 0)
            {
                value4 = "True";
            }
            FireLifeSafety_Obs_GetbyIdTableAdapter fireLifeSafety_Obs_GetbyIdTableAdapter = new FireLifeSafety_Obs_GetbyIdTableAdapter();
            Observation.FireLifeSafety_Obs_GetbyIdDataTable data6 = fireLifeSafety_Obs_GetbyIdTableAdapter.GetData(Insp_Request_Id);
            if (data6.Count > 0)
            {
                value5 = "True";
            }
            string reportPath = _webHostEnvironment.WebRootPath + "\\RdlcReport\\Insp_FireLifeSafety_Report.rdlc";
            string text = _webHostEnvironment.WebRootPath + "\\InspFireLifeSafetyReportPdf\\";
            if (!Directory.Exists(text))
            {
                Directory.CreateDirectory(text);
            }
            ReportParameter[] parameters = new ReportParameter[7]
            {
            new ReportParameter("FireLifeSafety_Prm", "Fire & Life Safety Details"),
            new ReportParameter("FireLifeSafety_Unique_Id_Prm", data[0].Unique_Id.ToString()),
            new ReportParameter("FireLifeSafety_Photo_List_Prm", value),
            new ReportParameter("FireLifeSafety_Corrective_Action_Prm", value2),
            new ReportParameter("FireLifeSafety_Action_ApprovedBy_Prm", value3),
            new ReportParameter("FireLifeSafety_Findings_Prm", value4),
            new ReportParameter("FireLifeSafety_Obs_Prm", value5)
            };
            using LocalReport localReport = new LocalReport();
            localReport.EnableHyperlinks = true;
            localReport.EnableExternalImages = true;
            localReport.ReportPath = reportPath;
            localReport.SetParameters(parameters);
            using FileStream fileStream = new FileStream(text + Unique_Id + ".pdf", FileMode.Create);
            localReport.DataSources.Add(new ReportDataSource("FireLifeSafety_DS", (DataTable)data));
            localReport.DataSources.Add(new ReportDataSource("FireLifeSafety_Photo_DS", (DataTable)data2));
            localReport.DataSources.Add(new ReportDataSource("FLS_CA_DS", (DataTable)data3));
            localReport.DataSources.Add(new ReportDataSource("FLS_ApprovedBy_DS", (DataTable)data4));
            localReport.DataSources.Add(new ReportDataSource("FLS_Findings_DS", (DataTable)data5));
            localReport.DataSources.Add(new ReportDataSource("FLS_Obs_DS", (DataTable)data6));
            localReport.Refresh();
            string mimeType;
            string encoding;
            string fileNameExtension;
            string[] streams;
            Warning[] warnings;
            byte[] array = localReport.Render("PDF", null, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            fileStream.Write(array, 0, array.Length);
            fileStream.Close();
            string data7 = Report_conn + "InspFireLifeSafetyReportPdf/" + Unique_Id + ".pdf";
            return Json(data7);
        }
    }
}
