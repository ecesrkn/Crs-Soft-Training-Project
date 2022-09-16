

using CalendarAppRazor.Model;
using System.Drawing;
using System.Text;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CalendarAppRazor.FileUploadService
{
    public class LocalFileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment environment;

        public LocalFileUploadService(IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, MonthPicturePair model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(model.Code);
            sb.Append("-");
            sb.Append(model.Month.ToString());
            var fileExtension = file.FileName.IndexOf(".");
            sb.Append(file.FileName.Substring(fileExtension));
            var filePath = Path.Combine(environment.ContentRootPath, @"wwwroot\images", sb.ToString());
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return filePath;
        }

        public async Task<string> DeleteFileAsync(MonthPicturePair model)
        {
            string str = fullPath(model);
            //var filePath = Path.Combine(environment.ContentRootPath, @"wwwroot\images", model.imageUrl);
            if (System.IO.File.Exists(str))
            {
                System.IO.File.Delete(str);
                return "success";
            }
            return "failed";
        }

        private string fullPath(MonthPicturePair model)
        {
            //str is something like \images\CODE-MONTH.jpg
            //convert to full path
            StringBuilder sb = new StringBuilder();
            sb.Append(model.Code);
            sb.Append("-");
            sb.Append(model.Month.ToString());
            var fileExtension = model.imageUrl.IndexOf(".");
            sb.Append(model.imageUrl.Substring(fileExtension));
            var filePath = Path.Combine(environment.ContentRootPath, @"wwwroot\images", sb.ToString());

            return filePath;
        }
    }
}
