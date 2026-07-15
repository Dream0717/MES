using MES.Desktop.Models;
namespace MES.Desktop.Models;
public class WorkOrder : BaseEntity
{
    public string OrderNo { get; set; } = "";
    public long ProductId { get; set; }
    public int TargetQuantity { get; set; }
    public int CompletedQuantity { get; set; }
    public int DefectQuantity { get; set; }
    public WorkOrderStatus Status { get; set; } = WorkOrderStatus.Pending;
    public long CreatedBy { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public Product? Product { get; set; }
    public User? Creator { get; set; }
    public ICollection<WorkReport> WorkReports { get; set; } = new List<WorkReport>();
}
