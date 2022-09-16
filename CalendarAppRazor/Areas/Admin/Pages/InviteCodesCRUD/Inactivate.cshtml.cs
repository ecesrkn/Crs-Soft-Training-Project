using CalendarAppRazor.Data;
using CalendarAppRazor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CalendarAppRazor.Areas.Admin.Pages.InviteCodesCRUD
{
    public class InactivateModel : PageModel
    {
            private readonly ApplicationDbContext _db;

            public InviteCode InviteCode { get; set; }

            public InactivateModel(ApplicationDbContext db)
            {
                _db = db;
            }

            public void OnGet(string id)
            {
                InviteCode = _db.InviteCodes.Find(id);
            }
            public async Task<IActionResult> OnPost(InviteCode inviteCode)
            {

                    _db.Update(inviteCode);
                    await _db.SaveChangesAsync();
                    TempData["success"] = "InviteCode updated successfully.";
                    return RedirectToPage("Index");
                

            }
        }
    }
