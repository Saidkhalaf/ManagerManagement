using System.ComponentModel.DataAnnotations;
using System.Reflection.PortableExecutable;

namespace ManagerManagement.BL.Domain;

public class Client : IValidatableObject
{
    
    [Key]
    public int Id { get; set; } 
    [Required]
    public string Name { get; set; } 
    public string Email { get; set; }
    public ProjectStatus ProjectStatus { get; set; } 
    public DateTime BirthDate { get; set; } 
    public double AccountBalance { get; set; } 
    
    
    public int? PhoneNr { get; set; }
    
    //Relationship property
    public int ManagerId { get; set; }
    public DeveloperManager Manager { get; set; }

    public Client()
    {
    }
    
    public Client(int id, string name, DateTime birthDate, string email)
    {
        Id = id;
        Name = name;
        BirthDate = birthDate;
        Email = email;
    }

    public override string ToString()
    {
        return $"Client Name: {Name}\nEmail: {Email}\nClient ID: {Id}\nManager ID: {ManagerId}\nAge: {BirthDate}\nAccountBalance: {AccountBalance}";
    }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        var results = new List<ValidationResult>();

        if (string.IsNullOrEmpty(Name) || Name.Trim().Length < 2)
        {
            results.Add(new ValidationResult("Name should be at least 2 characters long.", new[] { nameof(Name) }));
        }
        
        if (BirthDate > DateTime.Now || BirthDate < DateTime.Parse("1900-01-01"))
        {
            results.Add(new ValidationResult("Invalid birthdate.", new[] { nameof(BirthDate) }));
        }
        
        if (string.IsNullOrEmpty(Email) || !IsValidEmail(Email))
        {
            results.Add(new ValidationResult("Invalid email address.", new[] { nameof(Email) }));
        }

        return results;
    }
    
    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}