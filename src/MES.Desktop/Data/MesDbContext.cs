using Microsoft.EntityFrameworkCore;
using MES.Desktop.Models;

namespace MES.Desktop.Data;

public class MesDbContext : DbContext
{
    public MesDbContext(DbContextOptions<MesDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Workstation> Workstations => Set<Workstation>();
    public DbSet<WorkOrder> WorkOrders => Set<WorkOrder>();
    public DbSet<WorkReport> WorkReports => Set<WorkReport>();
    public DbSet<OperationLog> OperationLogs => Set<OperationLog>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<User>(e => { e.ToTable("users"); e.HasKey(x => x.Id); e.Property(x => x.Username).HasMaxLength(50).IsRequired(); e.HasIndex(x => x.Username).IsUnique(); e.Property(x => x.PasswordHash).HasMaxLength(255).IsRequired(); e.Property(x => x.RealName).HasMaxLength(50).IsRequired(); e.Property(x => x.Status).HasConversion<int>(); e.HasOne(x => x.Role).WithMany(r => r.Users).HasForeignKey(x => x.RoleId); });
        mb.Entity<Role>(e => { e.ToTable("roles"); e.HasKey(x => x.Id); e.Property(x => x.RoleName).HasMaxLength(50).IsRequired(); e.HasIndex(x => x.RoleName).IsUnique(); });
        mb.Entity<Product>(e => { e.ToTable("products"); e.HasKey(x => x.Id); e.Property(x => x.ProductCode).HasMaxLength(50).IsRequired(); e.HasIndex(x => x.ProductCode).IsUnique(); e.Property(x => x.ProductName).HasMaxLength(100).IsRequired(); e.Property(x => x.Status).HasConversion<int>(); });
        mb.Entity<Workstation>(e => { e.ToTable("workstations"); e.HasKey(x => x.Id); e.Property(x => x.StationCode).HasMaxLength(50).IsRequired(); e.HasIndex(x => x.StationCode).IsUnique(); e.Property(x => x.StationName).HasMaxLength(100).IsRequired(); e.Property(x => x.Status).HasConversion<int>(); });
        mb.Entity<WorkOrder>(e => { e.ToTable("work_orders"); e.HasKey(x => x.Id); e.Property(x => x.OrderNo).HasMaxLength(50).IsRequired(); e.HasIndex(x => x.OrderNo).IsUnique(); e.Property(x => x.Status).HasConversion<int>(); e.HasOne(x => x.Product).WithMany(p => p.WorkOrders).HasForeignKey(x => x.ProductId); e.HasOne(x => x.Creator).WithMany(u => u.WorkOrders).HasForeignKey(x => x.CreatedBy); });
        mb.Entity<WorkReport>(e => { e.ToTable("work_reports"); e.HasKey(x => x.Id); e.HasOne(x => x.WorkOrder).WithMany(w => w.WorkReports).HasForeignKey(x => x.WorkOrderId); e.HasOne(x => x.Workstation).WithMany(w => w.WorkReports).HasForeignKey(x => x.WorkstationId); e.HasOne(x => x.Reporter).WithMany(u => u.WorkReports).HasForeignKey(x => x.ReportedBy); });
        mb.Entity<OperationLog>(e => { e.ToTable("operation_logs"); e.HasKey(x => x.Id); e.Property(x => x.OperationType).HasMaxLength(50).IsRequired(); });
    }
}
