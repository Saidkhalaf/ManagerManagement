using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ManagerManagement.BL.Domain;

public class DeveloperManager 
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Department { get; set; }
        
        public ProjectStatus ProjectStatus { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HireDate { get; set; }
        
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a non-negative value.")]
        public double Salary { get; set; }
        
        // Navigation property to link the Identity user
        [Required]
        public IdentityUser ManagerUser { get; set; }
        
        // New property to track the user who last maintained the record
        public string MaintainedBy { get; set; }
        public ICollection<Client> ManagedClients { get; set; } 
        
        public ICollection<Project> ManagedProjects { get; set; }
        
        
        public int? PhoneNr { get; set; }

        public DeveloperManager(int id, string name, string department, double salary)
        {
            Id = id;
            Name = name;
            Department = department;
            Salary = salary;
            ManagedClients = new List<Client>();
            ManagedProjects = new List<Project>();
        }

        public DeveloperManager(int id, string name, string department, ProjectStatus projectStatus, DateTime hireDate, double salary)
        {
            Id = id;
            Name = name;
            Department = department;
            ProjectStatus = projectStatus;
            HireDate = hireDate;
            Salary = salary;
            ManagedClients = new List<Client>();
            ManagedProjects = new List<Project>();
        }

        public DeveloperManager()
        {
        }
        
        public override string ToString()
        {
            return $"Manager Name: {Name}\nManager ID: {Id}\nDepartment: {Department}\nProject Status: {ProjectStatus}" +
                   $"\nHire Date: {HireDate.ToShortDateString()}\nSalary: {Salary}";
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            
            if (Id <= 0)
            {
                results.Add(new ValidationResult("Invalid Id. Id should be greater than 0!", 
                    new[] {nameof(Id)}));
            }
            
            if (string.IsNullOrWhiteSpace(Name) || Name.Length < 3)
            {
                results.Add(new ValidationResult("Name should have at least 3 characters.",
                    new []{nameof(Name)}));
            }
            
            if (string.IsNullOrWhiteSpace(Department))
            {
                results.Add(new ValidationResult("Department should have a value.",
                    new []{nameof(Department)}));
            }
            
            if (HireDate > DateTime.Today)
            {
                results.Add(new ValidationResult("The Hire date must be older than Today!",
                    new []{nameof(HireDate)}));
            }
            
            if (Salary < 0 )
            {
                results.Add(new ValidationResult("Salary must be a non-negative value.",
                    new []{nameof(Salary)}));
            }
            return results;
        }
    }