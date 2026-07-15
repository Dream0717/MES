using MES.Desktop.Models;
namespace MES.Desktop.Models;
public class Product : BaseEntity
{
    public string ProductCode { get; set; } = "";
    public string ProductName { get; set; } = "";
    public string? Specification { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Enabled;
    public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
}
