namespace TaskManagementSystem.Core.Constants;

public static class PermissionCodeConstant
{
    public static class User
    {
        public const string CreateUser = "CREATE_USER";
        public const string ViewUser = "VIEW_USER";
        public const string UpdateUser = "UPDATE_USER";
        public const string DeleteUser = "DELETE_USER";
    }

    public static class Project
    {
        public const string CreateProject = "CREATE_PROJECT";
        public const string ViewProject = "VIEW_PROJECT";
        public const string UpdateProject = "UPDATE_PROJECT";
        public const string DeleteProject = "DELETE_PROJECT";
    }

    public static class Team
    {
        public const string CreateTeam = "CREATE_TEAM";
        public const string ViewTeam = "VIEW_TEAM";
        public const string UpdateTeam = "UPDATE_TEAM";
        public const string DeleteTeam = "DELETE_TEAM";
    }
}
