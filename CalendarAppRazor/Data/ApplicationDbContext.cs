using CalendarAppRazor.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CalendarAppRazor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }
        public DbSet<InviteCode> InviteCodes { get; set; }
        public DbSet<MonthPicturePair> MonthPicturePairs { get; set; }
    }
}
