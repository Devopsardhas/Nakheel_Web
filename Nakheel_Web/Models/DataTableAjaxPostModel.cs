namespace Nakheel_Web.Models
{
    public class DataTableAjaxPostModel
    {
        public int Draw { get; set; }
        public List<Column>? Columns { get; set; }
        public List<Order>? Order { get; set; }
        public int? Start { get; set; }
        public int Length { get; set; }
        public int page { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
        public Search? Search { get; set; }
        public string? Zone_Id { get; set; }
        public string? Card_Id { get; set; }
    }

    public class Column
    {
        public string? Data { get; set; }
        public string? Name { get; set; }
        public bool? Searchable { get; set; }
        public bool? Orderable { get; set; }
        public Search? Search { get; set; }
    }

    public class Order
    {
        public int? Column { get; set; }
        public string? Dir { get; set; }
    }

    public class Search
    {
        public string? Value { get; set; }
        public bool Regex { get; set; }
    }

    public class Datatables_Param
    {
        public string? CreatedBy { get; set; }
        public string? SearchValue { get; set; }
        public string? PageNo { get; set; }
        public string? PageSize { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
    }
}
