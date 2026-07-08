namespace TaskManagementSystem.Core.Constants;

public static class PermissionCodeConstant
{
    public static class User
    {
        public const string AddUser = "ADD_USER";
        public const string ViewUser = "VIEW_USER";
        public const string UpdateUser = "UPDATE_USER";
        public const string DeleteUser = "DELETE_USER";
    }

    public static class Project
    {
        public const string AddProject = "ADD_PROJECT";
        public const string ViewProject = "VIEW_PROJECT";
        public const string UpdateProject = "UPDATE_PROJECT";
        public const string DeleteProject = "DELETE_PROJECT";
    }

    public static class Team
    {
        public const string AddTeam = "ADD_TEAM";
        public const string ViewTeam = "VIEW_TEAM";
        public const string UpdateTeam = "UPDATE_TEAM";
        public const string DeleteTeam = "DELETE_TEAM";
    }

    public static class WorkItem
    {
        public const string AddWorkItem = "ADD_WORK_ITEM";
        public const string ViewWorkItem = "VIEW_WORK_ITEM";
        public const string UpdateWorkItem = "UPDATE_WORK_ITEM";
        public const string DeleteWorkItem = "DELETE_WORK_ITEM";
    }

    public static class Message
    {
        public const string AddMessage = "ADD_MESSAGE";
        public const string ViewMessage = "VIEW_MESSAGE";
        public const string UpdateMessage = "UPDATE_MESSAGE";
        public const string DeleteMessage = "DELETE_MESSAGE";
    }
}
