namespace TaskManagementSystem.Core.Entities
{
    public sealed class Role: BaseEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<UserRole> UserRoles = [];
        public ICollection<RolePermission> RolePermissions = [];
    }
}
