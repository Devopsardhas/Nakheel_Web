namespace Nakheel_Web.Models.EMR_Drill
{
    public class Drill_Photos :Common_Tbl
    {
        public string? Drill_Photo_Id { get; set; }
        public string? Drill_Schedule_ID { get; set; }
        public string? Photo_File_Name { get; set; }
        public string? Photo_File_Path { get; set; }
        public string? Photo_File_Size { get; set; }
        public string? Photo_File_Type { get; set; }
    }
    public class Drill_Vedios : Common_Tbl
    {
        public string? Drill_Photo_Id { get; set; }
        public string? Drill_Schedule_ID { get; set; }
        public string? Video_File_Name { get; set; }
        public string? Video_File_Path { get; set; }
        public string? Video_File_Size { get; set; }
        public string? Video_File_Type { get; set; }

    }
}
