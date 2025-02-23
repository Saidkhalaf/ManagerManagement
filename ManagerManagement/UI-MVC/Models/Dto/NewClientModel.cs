using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace ManagerManagement.UI.MVC.Models.Dto;

public class NewClientModel
{
    [Required]
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public int ManagerId { get; set; }
}