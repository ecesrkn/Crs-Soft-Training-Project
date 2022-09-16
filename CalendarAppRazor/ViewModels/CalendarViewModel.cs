using CalendarAppRazor.Model;

namespace CalendarAppRazor.ViewModels
{
    public class CalendarViewModel
    {
        public string Code { get; set; }
        public List<MonthPicturePair> MonthPicturePairs { get; set; }
    }
}
