using CalendarAppRazor.Data;
using CalendarAppRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;

namespace CalendarAppRazor.Pages.InviteCodes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
    
        public InviteCode InviteCode { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
    
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(InviteCode inviteCode)
        {

            if (ModelState.IsValid)
            {
                await _db.AddAsync(inviteCode);
                await _db.SaveChangesAsync();
                TempData["success"] = "InviteCode created successfully.";
                return RedirectToPage("Index");
            }
            return Page();
   
        }
    }
}
