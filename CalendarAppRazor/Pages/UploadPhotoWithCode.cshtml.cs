using CalendarAppRazor.Data;
using CalendarAppRazor.FileUploadService;
using CalendarAppRazor.Model;
using CalendarAppRazor.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace CalendarAppRazor.Pages
{
    public class UploadPhotoWithCodeModel : PageModel
    {
        private readonly ApplicationDbContext db;
        private readonly IFileUploadService fileUpload;

        [BindProperty]
        public UploadPictureWithCode Model { get; set; }
        public UploadPhotoWithCodeModel(ApplicationDbContext db, IFileUploadService fileUpload)
        {
            this.db = db;
            this.fileUpload = fileUpload;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            //Check if invite code is valid
            var result = db.InviteCodes.Find(Model.Code);
            if (result==null)
            {
                ModelState.AddModelError("", "Invalid invite code.");
                return Page();
            }
            else
            {
                //Check if code is active

                if (!result.isActive)
                {
                    ModelState.AddModelError("", "Invite code expired.");
                    return Page();
                }

                //Check if month is available
                // 1. extract month
                int month = Convert.ToInt32(Model.YearMonth.Substring(5));
                var codeMonthPair = db.MonthPicturePairs.Where(x => x.Code == Model.Code).Where(x => x.Month == month).FirstOrDefault();
                // if codeMonthPair is null, add it to the database
                if (codeMonthPair==null)
                {
                    MonthPicturePair temp = new MonthPicturePair() { Code = Model.Code, Month = month };
                    if(Model.Picture!=null)
                    {
                        var imageFilePath = await fileUpload.UploadFileAsync(Model.Picture, temp);
                        StringBuilder sb = new StringBuilder();
                        sb.Append("/images/");
                        sb.Append(temp.Code);
                        sb.Append("-");
                        sb.Append(temp.Month.ToString());
                        var fileExtension = Model.Picture.FileName.IndexOf(".");
                        sb.Append(Model.Picture.FileName.Substring(fileExtension));

                        temp.imageUrl = sb.ToString();
                        db.MonthPicturePairs.Add(temp);
                        db.SaveChanges();
                        TempData["success"] = "Image uploaded successfully.";
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Try another month!");
                    return Page();
                }
            }




            return Page();
        }
    }
}
