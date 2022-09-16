using CalendarAppRazor.Data;
using CalendarAppRazor.Model;
using CalendarAppRazor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Security.Claims;

namespace CalendarAppRazor.Pages.ManageInviteCodes
{
    [Authorize]
    public class CalendarPageModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext db;

        [BindProperty]
        public InviteCode InviteCode { get; set; }
        public CalendarViewModel Model { get; set; }
        public CalendarPageModel(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }
        public void OnGet(string id)
        {
            var icFromDb = db.InviteCodes.Find(id);
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //if code.ownerId and signed in user id doesn't match, return Error page
            if (icFromDb.ownerId != userId)
            {
                RedirectToPage("Error");
            }
            Model = new CalendarViewModel();
            Model.Code = icFromDb.Code;
            Model.MonthPicturePairs = new List<MonthPicturePair>();
            for(int month = 1; month<=12; month++)
            {
                Model.MonthPicturePairs.Insert(month-1, db.MonthPicturePairs.Where(x => x.Code == Model.Code).Where(x => x.Month == month).ToList().FirstOrDefault());
            }
            RedirectToPage("Index");
           

        }

        //ic: InviteCode

    }
}
