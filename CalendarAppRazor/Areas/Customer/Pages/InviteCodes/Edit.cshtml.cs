using CalendarAppRazor.Data;
using CalendarAppRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace CalendarAppRazor.Pages.InviteCodes
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
    
        public InviteCode InviteCode { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
    
        public void OnGet(string id)
        {
            InviteCode = _db.InviteCodes.Find(id);
        }
        public async Task<IActionResult> OnPost(InviteCode inviteCode)
        {
            
            if (ModelState.IsValid)
            {
                _db.Update(inviteCode);
                await _db.SaveChangesAsync();
                TempData["success"] = "InviteCode updated successfully.";
                return RedirectToPage("Index");
            }
            return Page();
   
        }
    }
}
