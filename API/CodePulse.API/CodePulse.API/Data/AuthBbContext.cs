using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Data
{
    public class AuthBbContext : IdentityDbContext
    {
        public AuthBbContext(DbContextOptions<AuthBbContext> options) : base(options) { }        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "a5027ff6-abc8-46ae-b8fa-c09381573bde";
            //var readerRoleId = Guid.NewGuid().ToString();
            var writerRoleId = "83b6fa27-7599-49ec-8508-f64d79ed6600";
            //var writerRoleId = Guid.NewGuid().ToString();
            // Create Reader and Write Role

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id = readerRoleId,
                    Name="Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleId
                },
                new IdentityRole()
                {
                    Id=writerRoleId,
                    Name= "Writer".ToUpper(),
                    ConcurrencyStamp=writerRoleId
                }
            };

            // seed the roles

            builder.Entity<IdentityRole>().HasData(roles);

            // Create an Admin user

            var adminUserId = "e003a301-4e7e-4fc3-8cfd-42410d40670e";
            //var adminUserId = Guid.NewGuid().ToString();
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin@codepulse.com",
                Email = "admin@codepulse.com".ToUpper(),
                NormalizedEmail = "admin@codepulse.com".ToUpper(),                
            };
            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            //Roles to Admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>() 
                {
                    UserId = adminUserId,
                    RoleId= readerRoleId,

                },
                new IdentityUserRole<string>()
                {
                    UserId = adminUserId,
                    RoleId= writerRoleId,
                }

            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }
    }
}
