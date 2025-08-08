namespace EmergereJobCareerWebApi.Models
{
    public class Paging
    {
        public int page_number { get; set; }
        public int no_of_records { get; set; }
        public string sort_by { get; set; }
        public string sort_by_column { get; set; }
        public string isActive { get; set; }
        public Search search { get; set; }
    }
}
