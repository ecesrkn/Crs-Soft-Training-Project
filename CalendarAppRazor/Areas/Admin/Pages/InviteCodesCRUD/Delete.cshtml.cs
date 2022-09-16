using CalendarAppRazor.Data;
using CalendarAppRazor.FileUploadService;
using CalendarAppRazor.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace CalendarAppRazor.Areas.Admin.Pages.InviteCodesCRUD
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileUploadService fileUpload;

        [BindProperty]
        public InviteCode InviteCode { get; set; }

        public DeleteModel(ApplicationDbContext db, IFileUploadService fileUpload)
        {
            _db = db;
            this.fileUpload = fileUpload;
        }
    
        public void OnGet(string id)
        {
            InviteCode = _db.InviteCodes.Find(id);
        }
        public async Task<IActionResult> OnPost(string id)
        {
            var objFromDb = _db.InviteCodes.Find(id);
            if (objFromDb!=null)
            {
                _db.InviteCodes.Remove(objFromDb);

                //remove all pictures with this code.
                foreach (var mppair in _db.MonthPicturePairs)
                {
                    if (mppair.Code == id)
                    {
                        _db.MonthPicturePairs.Remove(mppair);

                        var result = await fileUpload.DeleteFileAsync(mppair);
                        if (result == "success")
                        {
                            TempData["success"] = "InviteCode and pictures deleted successfully.";
                        }
                        else if (result == "failed")
                        {
                            return Page();
                        }
                    }
                }


                await _db.SaveChangesAsync();
                TempData["success"] = "InviteCode deleted successfully.";
                return RedirectToPage("Index");
            }
         
            return Page();
   
        }
    }
}
