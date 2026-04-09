namespace TaskManagementSystem.Core.Dtos;

public abstract record GetUserInformationDto
{
    public string? CreatedByFirstName { get; set; }
    public string? CreatedByLastName { get; set; }

    public string? UpdatedByFirstName { get; set; }
    public string? UpdatedByLastName { get; set; }

}
