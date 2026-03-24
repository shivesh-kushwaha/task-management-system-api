namespace TaskManagementSystem.Infrastructure.Persistence.Constants;

public static class TableNames
{
    public static class Identity
    {
        public const string Permissions = "Permissions";
        public const string PermissionGroups = "PermissionGroups";
        public const string Roles = "Roles";
        public const string RolePermissions = "RolePermissions";
        public const string Users = "Users";
        public const string UserRoles = "UserRoels";
        public const string RefreshTokens = "RefreshTokens";
    }

    public static class Management
    {
        public const string Projects = "Projects";
        public const string WorkItems = "WorkItems";
        public const string WorkItemTypes = "WorkItemTypes";
        public const string Comments = "Comments";
    }

    public static class Organization
    {
        public const string Teams = "Teams";
        public const string TeamMembers = "TeamMembers";
    }

    public static class Communication
    {
        public const string Notifications = "Notifications";
    }
}
