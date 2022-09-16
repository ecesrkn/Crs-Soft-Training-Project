using CalendarAppRazor.Model;

namespace CalendarAppRazor.FileUploadService
{
    public interface IFileUploadService
    {
        Task<string> UploadFileAsync(IFormFile file, Model.MonthPicturePair temp);
        Task<string> DeleteFileAsync(MonthPicturePair model);
        
        }
}
