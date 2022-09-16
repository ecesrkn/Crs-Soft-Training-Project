namespace CalendarAppRazor.ViewModels
{
    public class UploadPictureWithCode
    {
        public string Code { get; set; }
        public IFormFile Picture { get; set; }
        public string YearMonth { get; set; }
    }
}
