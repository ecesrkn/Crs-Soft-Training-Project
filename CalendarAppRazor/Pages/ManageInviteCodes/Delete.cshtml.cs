using CalendarAppRazor.Data;
using CalendarAppRazor.FileUploadService;
using CalendarAppRazor.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Linq;

namespace CalendarAppRazor.Pages.ManageInviteCodes
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileUploadService fileUpload;
        private string code;

        [BindProperty]
        public InviteCode InviteCode { get; set; }

        public DeleteModel(ApplicationDbContext db, IFileUploadService fileUpload)
        {
            _db = db;
            this.fileUpload = fileUpload;
        }

        public void OnGet(string id)
        {
            InviteCode = new InviteCode();
            this.InviteCode = _db.InviteCodes.Find(id);
            code = id;
        }

        public async Task<IActionResult> OnPost(string id)
        {
            //remove code from database
            //remove pictures with the code from database
            InviteCode = new InviteCode();
            this.InviteCode = _db.InviteCodes.Find(id);
            code = id;
            var objFromDb = _db.InviteCodes.Find(InviteCode.Code);
            var obj2 = _db.InviteCodes.Find(code);
            if (objFromDb != null)
            {
                _db.InviteCodes.Remove(objFromDb);
                //await _db.SaveChangesAsync();

                foreach (var mppair in _db.MonthPicturePairs)
                {
                    if(mppair.Code == InviteCode.Code)
                    {
                        _db.MonthPicturePairs.Remove(mppair);
                        
                        var result = await fileUpload.DeleteFileAsync(mppair);
                        if (result == "success")
                        {
                            TempData["success"] = "InviteCode and pictures deleted successfully.";
                        }else if (result == "failed")
                        {
                            return Page();
                        }
                    }
                }
                await _db.SaveChangesAsync();

                return RedirectToPage("Index");
            }

            return Page();

        }
    }
}
