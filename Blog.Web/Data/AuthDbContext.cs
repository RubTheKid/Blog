using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blog.Web.Data;

public class AuthDbContext : IdentityDbContext
{
    public AuthDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        var AdminId = "642da2b8-7ec9-4e85-978a-a935ea187c3a";
        var SuperAdminId = "187cd807-b7d4-4d42-9f4a-85b01d3aa4a6";
        var UserId = "5fb90452-843a-4a57-b35b-42c35292a2a6";

        //papéis (commonuser, admin, superadmin)
        var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "Admin",
                Id = AdminId,
                ConcurrencyStamp = AdminId
            },
            new IdentityRole
            {
                Name = "SuperAdmin",
                NormalizedName = "SuperAdmin",
                Id = SuperAdminId,
                ConcurrencyStamp = SuperAdminId
            },
            new IdentityRole
            {
                Name = "User",
                NormalizedName = "User",
                Id = UserId,
                ConcurrencyStamp = UserId
            },
        };

        builder.Entity<IdentityRole>().HasData(roles);

        //superadmin
        var superAdminId = "30280c3d-1370-4704-9f5e-563f0375c173";
        var superAdminUser = new IdentityUser
        {
            UserName = "superadmin@blog.com",
            Email = "superadmin@blog.com",
            NormalizedEmail = "superadmin@blog.com".ToUpper(),
            NormalizedUserName = "superadmin@blog.com".ToUpper(),
            Id = SuperAdminId
        };

        superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>()
            .HashPassword(superAdminUser, "superadmin123");

        builder.Entity<IdentityUser>().HasData(superAdminUser);


        //todos os papeis para o superadmin
        var superAdminRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string>
            {
                RoleId = AdminId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = SuperAdminId,
                UserId = superAdminId
            },
            new IdentityUserRole<string>
            {
                RoleId = UserId,
                UserId = superAdminId
            }
        };

        builder.Entity<IdentityUserRole<string>>().HasData(superAdminRoles);

    }

}
