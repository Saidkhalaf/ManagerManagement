using System.ComponentModel.DataAnnotations;

namespace ManagerManagement.UI.MVC.Models.Dto;

public class ClientDto
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }    
    [Required]
    public DateTime BirthDate { get; set; }
    
    public string Email { get; set; }
    
}