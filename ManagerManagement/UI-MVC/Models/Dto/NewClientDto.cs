using System.ComponentModel.DataAnnotations;

namespace ManagerManagement.UI.MVC.Models.Dto;

public class NewClientDto
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    [Required]
    public string Email { get; set; }

    public int ManagerId { get; set; }
}