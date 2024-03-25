using Microsoft.AspNetCore.Mvc;
using Nakheel_Web.Models.AuditMaster;
using System.ComponentModel.DataAnnotations;

namespace Nakheel_Web.Models.InspectionMaster
{
    #region [Inspection Category]
    public class M_Insp_Category : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Category_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Insp_Category_Name { get; set; }
    }
    public class M_Get_Insp_Category
    {
        public IReadOnlyCollection<M_Insp_Category>? Get_All { get; set; }
        public M_Insp_Category? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion

    #region [Inspection Sub Category]
    public class M_Insp_Sub_Category : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Sub_Category_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Insp_Category_Id { get; set; }
        public string? Insp_Category_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Insp_Sub_Category_Name { get; set; }
    }
    public class M_Get_Insp_Sub_Category
    {
        public IReadOnlyCollection<M_Insp_Sub_Category>? Get_All { get; set; }
        public M_Insp_Sub_Category? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    #endregion

    #region [Inspection Landscaping Master]
    public class M_Insp_Landscap_Master : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Landscap_Mas_Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Insp_Landscap_Mas_Name { get; set; }
    }
    public class M_Get_Insp_Landscap_Master
    {
        public IReadOnlyCollection<M_Insp_Landscap_Master>? Get_All { get; set; }
        public M_Insp_Landscap_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion

    #region [Inspection Landscaping Sub Master]
    public class M_Insp_Landscap_Sub_Master : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Landscap_Sub_Mas_Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string? Insp_Landscap_Mas_Id { get; set; }
        public string? Insp_Landscap_Mas_Name { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Insp_Landscap_Sub_Mas_Name { get; set; }
    }
    public class M_Get_Insp_Landscap_Sub_Master
    {
        public IReadOnlyCollection<M_Insp_Landscap_Sub_Master>? Get_All { get; set; }
        public M_Insp_Landscap_Sub_Master? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }
    }
    #endregion

    #region [Inspection Type]
    public class M_Insp_Type : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Type_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Insp_Type_Name { get; set; }
    }
    public class M_Get_Insp_Type
    {
        public IReadOnlyCollection<M_Insp_Type>? Get_All { get; set; }
        public M_Insp_Type? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion

    #region [Inspection Observation]
    public class M_Insp_Observation : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Observation_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Insp_Observation_Name { get; set; }
    }
    public class M_Get_Insp_Observation
    {
        public IReadOnlyCollection<M_Insp_Observation>? Get_All { get; set; }
        public M_Insp_Observation? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion

    #region [Inspection Topic]
    public class M_Insp_Topic : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Topic_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Insp_Type_Id { get; set; }
        public string? Insp_Type_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Insp_Topic_Name { get; set; }
    }
    public class M_Get_Insp_Topic
    {
        public IReadOnlyCollection<M_Insp_Topic>? Get_All { get; set; }
        public M_Insp_Topic? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion

    #region [Inspection Questionnaires]
    public class M_Insp_Questionnaires : M_Common_Insp_Master
    {
        [ScaffoldColumn(false)]
        [HiddenInput]
        public string? Insp_Questionnaires_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Insp_Topic_Id { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string? Insp_Type_Id { get; set; }

        public string? Insp_Topic_Name { get; set; }
        public string? Insp_Type_Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Text)]
        public string? Insp_Questionnaires_Name { get; set; }
    }
    public class M_Get_Insp_Questionnaires
    {
        public IReadOnlyCollection<M_Insp_Questionnaires>? Get_All { get; set; }
        public M_Insp_Questionnaires? Get_ById { get; set; }
        public string? MESSAGE { get; set; }
        public string? STATUS { get; set; }
        public string? STATUS_CODE { get; set; }

    }
    #endregion
}
