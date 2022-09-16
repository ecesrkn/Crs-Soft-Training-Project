using CalendarAppRazor.Data;
using CalendarAppRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.Linq;

namespace CalendarAppRazor.Pages.InviteCodes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public InviteCode InviteCode { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
    
        public void OnGet(string id)
        {
            InviteCode = _db.InviteCodes.Find(id);
        }
        public async Task<IActionResult> OnPost()
        {
            var objFromDb = _db.InviteCodes.Find(InviteCode.Code);
            if (objFromDb!=null)
            {
                _db.InviteCodes.Remove(objFromDb);
                await _db.SaveChangesAsync();
                TempData["success"] = "InviteCode deleted successfully.";
                return RedirectToPage("Index");
            }
         
            return Page();
   
        }
    }
}
