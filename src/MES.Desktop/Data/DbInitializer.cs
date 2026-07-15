using Microsoft.EntityFrameworkCore;
using MES.Desktop.Models;

namespace MES.Desktop.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(MesDbContext db)
    {
        if (!await db.Roles.AnyAsync())
        {
            db.Roles.AddRange(
                new Role { RoleName = "Admin", Permissions = "[\"*\"]" },
                new Role { RoleName = "Planner", Permissions = "[\"workorder:read\",\"workorder:write\"]" },
                new Role { RoleName = "Technician", Permissions = "[\"product:read\",\"product:write\",\"workstation:read\",\"workstation:write\"]" },
                new Role { RoleName = "Operator", Permissions = "[\"workreport:read\",\"workreport:write\"]" });
            await db.SaveChangesAsync();
        }
        if (!await db.Users.AnyAsync())
        {
            var adminRole = await db.Roles.FirstAsync(r => r.RoleName == "Admin");
            db.Users.Add(new User
            {
                Username = "admin",
                PasswordHash = PasswordHasher.HashPassword("admin123"),
                RealName = "系统管理员",
                RoleId = adminRole.Id,
                Status = EntityStatus.Enabled
            });
            await db.SaveChangesAsync();
        }
    }
}
