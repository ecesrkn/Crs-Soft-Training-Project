namespace CalendarAppRazor.ViewModels
{
    public class AdminInviteCodeViewModel
    {
        public string Code { get; set; }
        public string ownerEmail { get; set; } 
        public int numOfPictures { get; set; }
        public bool isActive { get; set; }
    }
}
