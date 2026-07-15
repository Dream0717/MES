using MES.Desktop.Models;
namespace MES.Desktop.Models;
public class User : BaseEntity
{
    public string Username { get; set; } = "";
    public string PasswordHash { get; set; } = "";
    public string RealName { get; set; } = "";
    public long RoleId { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Enabled;
    public Role? Role { get; set; }
    public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
    public ICollection<WorkReport> WorkReports { get; set; } = new List<WorkReport>();
}
