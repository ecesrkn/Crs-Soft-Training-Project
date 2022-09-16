using System.ComponentModel.DataAnnotations;

namespace CalendarAppRazor.Model
{
    public class MonthPicturePair
    {
        [Key]
        public int Id { get; set; }
        public string imageUrl { get; set; }
        [Range(1,12)]
        public int Month { get; set; }
        public string Code { get; set; }

        

    }
}
