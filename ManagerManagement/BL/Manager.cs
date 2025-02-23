using System.ComponentModel.DataAnnotations;
using ManagerManagement.BL.Domain;
using ManagerManagement.DAL;

namespace ManagerManagement.BL;

public class Manager : IManager
{
   private IRepository _repository;

   public Manager(IRepository repository)
   {
      _repository = repository;
   }


   public DeveloperManager GetManager(int id)
   {
      return _repository.ReadManager(id);
   }

   public IEnumerable<DeveloperManager> GetAllManagers()
   {
      return _repository.ReadAllManagers();
   }

   public IEnumerable<DeveloperManager> GetManagersByDepartment(string department)
   {
      var allManagers = _repository.ReadAllManagers();
      return allManagers.Where(manager => manager.Department == department);
   }



   public DeveloperManager AddManager(string adminId, int id, string name, string department, ProjectStatus projectStatus,
      DateTime hireDate, double salary)
   {
      var theManager = _repository.ReadUser(adminId);
      DeveloperManager developerManager = new DeveloperManager
      {
         Id = id,
         Name = name,
         Department = department,
         ProjectStatus = projectStatus,
         HireDate = hireDate,
         Salary = salary, 
         ManagerUser = theManager
      };
      
      Validator.ValidateObject(developerManager, new ValidationContext(developerManager), true);
      _repository.CreateManager(developerManager);
      return developerManager;
   }

   public IEnumerable<DeveloperManager> GetAllManagersWithProjects()
   {
      return _repository.ReadAllManagersWithProjects();
   }

   public void UpdateManager(int managerId, double newSalary)
   {
      _repository.UpdateManager(managerId, newSalary);
   }

   public Developer GetDeveloper(Predicate<Developer> id)
   {
      return _repository.ReadDeveloper(id);
   }

   public IEnumerable<Developer> GetAllDevelopers()
   {
      return _repository.ReadAllDevelopers();
   }

   public IEnumerable<Developer> GetDevelopersByNameAndProgrammingLanguage(string name, string programmingLanguage)
   {
      return _repository.ReadDeveloperByNameAndProgrammingLanguage(name, programmingLanguage).ToList();
   }

   public Developer AddDeveloper(int id, string name, string programmingLanguage)
   {
      Developer developer = new Developer(id, name, programmingLanguage);
      
      var context = new ValidationContext(developer, serviceProvider: null, items: null);
      var results = new List<ValidationResult>();
      if (!Validator.TryValidateObject(developer, context, results, validateAllProperties: true))
      {
         return null;
      }
      
      _repository.CreateDeveloper(id, developer);
      return developer;
   }

   public IEnumerable<Developer> GetAllDevelopersWithProjectTasks()
   {
      return _repository.ReadAllDevelopersWithProjectTasks();
   }

   public IEnumerable<DeveloperTask> GetTasksOfDeveloperWithProject(long developerId)
   {
      return _repository.ReadTasksOfDeveloperWithProject(developerId);
   }

   public IEnumerable<Project> GetAllProjects()
   {
      return _repository.ReadALlProjects();
   }

   public IEnumerable<Project> GetAllProjectsByManager(int managerId)
   {
      var manager = _repository.ReadAllManagersWithProjects().FirstOrDefault(m => m.Id == managerId);

      if (manager != null)
      {
         return manager.ManagedProjects.ToList();
      }
      else
      {
         throw new ArgumentException("Manager not found!");
      }

   }

   public void AddProjectManager(int managerId, int projectId)
   {
      _repository.CreateProjectManager(managerId, projectId);
   }

   public Project AddProject(int managerId, ICollection<DeveloperTask> tasks, string projectDescription)
   {
      // Check if the managerId corresponds to an existing DeveloperManager
      DeveloperManager manager = _repository.ReadManager(managerId);
      if (manager == null)
      {
         throw new ValidationException($"Manager with ID {managerId} does not exist.");
      }

      var newProject = new Project
      {
         ManagerId = managerId,
         ProjectTasks = tasks,
         ProjectDescription = projectDescription
      };
      
      Validator.ValidateObject(newProject, new ValidationContext(newProject), true);
      _repository.CreateProject(newProject);
      return newProject;
   }

   public void DeleteProjectManager(int managerId, int projectId)
   {
      _repository.DeleteProjectManager(managerId, projectId);
   }

   public DeveloperManager GetManagerWithDevelopers(long id)
   {
      return _repository.ReadManagerWithDevelopers(id);
   }

   public Developer GetDevelopersWithManagerId(long id)
   {
      return _repository.ReadDevelopersWithManagerId(id);
   }

   public IEnumerable<Client> GetAllClients()
   {
      return _repository.ReadAllClients();
   }

   public Client GetClient(int id)
   {
      return _repository.ReadClient(id);
   }

   public Client AddClient(string name, DateTime birthDate, string email, int managerId)
   {
      var client = new Client
      {
         Name = name,
         BirthDate = birthDate,
         Email = email,
         ManagerId = managerId
      };
      Validator.ValidateObject(client, new ValidationContext(client), true);
      _repository.CreateClient(client);
      return client;
   }

   public IEnumerable<DeveloperTask> GetAllDeveloperTasks()
   {
      return _repository.ReadAllDeveloperTasks();
   }

   public IEnumerable<DeveloperTask> GetTasksOfProject(int projectId)
   {
      return _repository.ReadTasksOfProject(projectId);
   }

   public DeveloperTask AddTaskToProject(int projectId, int developerId, string taskName)
   {
      Project project = _repository.ReadALlProjects().FirstOrDefault(p => p.Id == projectId);

      if (project == null)
      {
         throw new ArgumentException("Project not found!");
      }
      
      DeveloperTask newTask = new DeveloperTask
      {
         ProjectId = projectId,
         DeveloperId = developerId,
         TaskName = taskName
      };
      
      var validationContext = new ValidationContext(newTask);
      Validator.ValidateObject(newTask, validationContext, true);

      _repository.CreateTask(newTask);
      return newTask;
   }
   
}