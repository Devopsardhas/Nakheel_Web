function fn_Drill_Change(id) {
    $(UI_Fields.SCHEDULE_DIV).show(100);
}
function Get_SCH_List(val) {
    let Zn = $(UI_Fields.ZONE).val();
    let Cm = $(UI_Fields.COMMUNITY_ID).val();
    let Bd = $(UI_Fields.BUILDING_ID).val();
    let Dt = $(UI_Fields.DRILL_TYPE).val();
    if (!Zn) {
        toastr["error"]("Select Zone!");
        $(UI_Fields.DRILL_TYPE).val("");
        $(UI_Fields.ZONE).focus();
        return false;
    }
    if (!Cm) {
        toastr["error"]("Select Community!");
        $(UI_Fields.DRILL_TYPE).val("");
        $(UI_Fields.COMMUNITY_ID).focus();
        return false;
    }
    if (!Dt) {
        if (val == 2) {
            toastr["error"]("Select Drill Type!");
        }
        $(UI_Fields.DRILL_TYPE).val("");
        $(UI_Fields.DRILL_TYPE).focus();
        return false;
    }
    if (!Bd) {
        Bd = '';
    }

    let modelData = {
        Zone_Id: Zn,
        Community_Id: Cm,
        Building_Id: Bd,
        Drill_Type_ID: Dt,
    };
    $(UI_Fields.SCHEDULE_DIV).empty();
    AjaxAsynHTML('/Emergency/ScheduleTbl', { _Param: modelData }, UI_Fields.SCHEDULE_DIV);

}