namespace TaskManagementSystem.Core.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public TypeEnum Type { get; set; }
    public int TypeId { get; set; }
    public ActionTypeEnum ActionType { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public Dictionary<string, object> FiedsJson { get; set; } = new Dictionary<string, object>(); 
}
