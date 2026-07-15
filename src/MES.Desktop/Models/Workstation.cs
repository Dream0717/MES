using MES.Desktop.Models;
namespace MES.Desktop.Models;
public class Workstation : BaseEntity
{
    public string StationCode { get; set; } = "";
    public string StationName { get; set; } = "";
    public string? Workshop { get; set; }
    public EntityStatus Status { get; set; } = EntityStatus.Enabled;
    public ICollection<WorkReport> WorkReports { get; set; } = new List<WorkReport>();
}
