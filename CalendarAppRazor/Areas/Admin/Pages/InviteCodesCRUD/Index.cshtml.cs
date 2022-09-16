using CalendarAppRazor.Data;
using CalendarAppRazor.Model;
using CalendarAppRazor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace CalendarAppRazor.Areas.Admin.Pages.InviteCodesCRUD
{

    [Authorize(Roles="Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext db;

        public IEnumerable<InviteCode> InviteCodes { get; set; }
        public IEnumerable<AdminInviteCodeViewModel> ModelList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            this.db = db;
        }
        public void OnGet()
        {
            InviteCodes = db.InviteCodes;
            ModelList = new List<AdminInviteCodeViewModel>();
            foreach (var inviteCode in InviteCodes)
            {
                var adminICViewModel = new AdminInviteCodeViewModel()
                {
                    Code = inviteCode.Code,
                    ownerEmail = db.Users.Find(inviteCode.ownerId).Email,
                    numOfPictures = 0,
                    isActive = inviteCode.isActive
                };

                adminICViewModel.numOfPictures = (db.MonthPicturePairs.Where(x => x.Code == inviteCode.Code).ToList()).Count();
                ModelList = ModelList.Append(adminICViewModel);
            }
           

        }
       
    }
}
