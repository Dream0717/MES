using Microsoft.EntityFrameworkCore;
using MES.Desktop.Data;
using MES.Desktop.Models;

namespace MES.Desktop.Services;

public class AuthService
{
    readonly MesDbContext _db;
    public AuthService(MesDbContext db) => _db = db;

    public long? CurrentUserId { get; private set; }
    public string CurrentUserName { get; private set; } = "";
    public string CurrentRealName { get; private set; } = "";

    public bool Login(string username, string password)
    {
        var user = _db.Users.Include(u => u.Role).FirstOrDefault(u => u.Username == username && u.Status == EntityStatus.Enabled);
        if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash)) return false;
        CurrentUserId = user.Id;
        CurrentUserName = user.Username;
        CurrentRealName = user.RealName;
        return true;
    }
}
