namespace ManagerManagement.UI.MVC.Models.Dto;

public class DeveloperTaskDto
{
    public int  Id { get; set; }
    public string TaskName { get; set; }
    public int DeveloperId { get; set; }
    public int ProjectId { get; set; }
}