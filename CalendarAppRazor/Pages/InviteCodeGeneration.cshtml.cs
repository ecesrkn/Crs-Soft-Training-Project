using CalendarAppRazor.Data;
using CalendarAppRazor.Model;
using CalendarAppRazor.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Security.Claims;
using System.Text;

namespace CalendarAppRazor.Pages
{
    [Authorize]
    public class InviteCodeGenerationModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ApplicationDbContext db;
        public InviteCode inviteCode { get; set; }

        public InviteCodeGeneration Model { get; set; } = new InviteCodeGeneration();
        public InviteCodeGenerationModel(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.db = db;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if(Model.CodeDisplay == "")
            {
                GenerateCode();
                await db.AddAsync(inviteCode);
                await db.SaveChangesAsync();

                TempData["success"] = "Don't forget to copy and share with your friends.";
            }
            else
            {
                //Do nothing
            }

            return Page();
        }

        private async void GenerateCode()
        {
            Random rand = new Random();
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<8; i++)
            {
                int digit = rand.Next(2);
                int ascii = 0;
                if(digit == 0)
                {
                    ascii = rand.Next(65, 91);
                }
                else
                {
                    ascii = rand.Next(48, 58);
                }
                char charToAppend = Convert.ToChar(ascii);
                sb.Append(charToAppend);
            }
            Model.CodeDisplay = sb.ToString();
            if (Model.CodeDisplay != "")
            {
                var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                inviteCode = new InviteCode() { Code = Model.CodeDisplay, isActive = true, ownerId = userId };
                
            }
        }
       
    }
}
