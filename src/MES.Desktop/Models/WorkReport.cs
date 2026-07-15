using MES.Desktop.Models;
namespace MES.Desktop.Models;
public class WorkReport : BaseEntity
{
    public long WorkOrderId { get; set; }
    public long WorkstationId { get; set; }
    public int GoodQuantity { get; set; }
    public int DefectQuantity { get; set; }
    public long ReportedBy { get; set; }
    public DateTime ReportedAt { get; set; } = DateTime.Now;
    public string? Remark { get; set; }
    public WorkOrder? WorkOrder { get; set; }
    public Workstation? Workstation { get; set; }
    public User? Reporter { get; set; }
}
