using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MES.Desktop.Data;

namespace MES.Desktop;

public class MesDbContextFactory : IDesignTimeDbContextFactory<MesDbContext>
{
    public MesDbContext CreateDbContext(string[] args)
    {
        var o = new DbContextOptionsBuilder<MesDbContext>();
        o.UseMySql("Server=localhost;Port=3306;Database=minimes;User=root;Password=Dream;",
            ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=minimes;User=root;Password=Dream;"));
        return new MesDbContext(o.Options);
    }
}
