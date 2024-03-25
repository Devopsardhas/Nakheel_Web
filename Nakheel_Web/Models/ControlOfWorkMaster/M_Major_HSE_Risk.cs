using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Nakheel_Web.Models.Masters;
using Nakheel_Web.Models.SecurityIncidentMaster;
using Nakheel_Web.Models.InspectionMaster;
using Nakheel_Web.Models.AuditMaster;

namespace Nakheel_Web.Models.ControlOfWorkMaster
{
    public class M_Major_HSE_Risk: CommonControlWorkMaster
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Major_HSE_Work_Id { get; set; }
        [Required(ErrorMessage ="This Field is Required")]
        //[RegularExpression("^[a-zA-Z0-9- &/().]*$", ErrorMessage = "Only Alphabets and Numbers allowed.")]
        [MaxLength(50)]
        [DataType(DataType.Text)]
        public string? Major_HSE_Work_Name { get; set; }
    }
    public class GET_Major_HSE_Risk
    {
        public IReadOnlyCollection<M_Major_HSE_Risk>? Get_All { get; set; }
        public M_Major_HSE_Risk? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #region [Permit Work Questionnaries]
    public class M_Major_HSE_QUestion : CommonControlWorkMaster
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Major_HSE_Ques_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Major_HSE_Work_Id { get; set; }
        public string? Major_HSE_Work_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Major_Questionnaires_Name { get; set; }
        
        
    }
    public class M_Get_Major_HSE_QUestion
    {
        public IReadOnlyCollection<M_Major_HSE_QUestion>? Get_All { get; set; }
        public List<M_Major_HSE_QUestion>? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    public class M_PTW_Questionnaries_Details
    {
        public string? Major_HSE_Work_Id { get; set; }
        public string? Major_HSE_Work_Name { get; set; }
        public List<M_Major_HSE_QUestion>? Load_All_Question { get; set; }
    }
    public class M_Get_PTW_Questionnaries_Details
    {
        //public IReadOnlyCollection<M_PTW_Questionnaries_Details>? Get_All_Questionnaire { get; set; }
        public IReadOnlyCollection<M_Major_HSE_QUestion>? Get_All_Questionnaire { get; set; }
        public IReadOnlyCollection<M_Major_HSE_QUestion>? Get_All { get; set; }
        public List<M_Major_HSE_QUestion>? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion

}
