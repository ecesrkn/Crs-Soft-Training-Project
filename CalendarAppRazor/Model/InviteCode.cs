using System.ComponentModel.DataAnnotations;

namespace CalendarAppRazor.Model
{
    public class InviteCode
    {
        [Key]
        public string Code { get; set; }
        [Display(Name = "is active property")]
        public bool isActive { get; set; }
        
        [Display(Name = "owner id")]
        public string ownerId { get; set; }
        

        

    }
}
