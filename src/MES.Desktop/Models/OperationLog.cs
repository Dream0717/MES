using MES.Desktop.Models;
namespace MES.Desktop.Models;
public class OperationLog : BaseEntity
{
    public long UserId { get; set; }
    public string OperationType { get; set; } = "";
    public string? OperationDesc { get; set; }
    public string? IpAddress { get; set; }
}
