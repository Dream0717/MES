using MES.Desktop.Models;
namespace MES.Desktop.Models;
public class Role : BaseEntity
{
    public string RoleName { get; set; } = "";
    public string Permissions { get; set; } = "";
    public ICollection<User> Users { get; set; } = new List<User>();
}
