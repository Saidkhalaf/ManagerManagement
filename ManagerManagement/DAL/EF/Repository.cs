using ManagerManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ManagerManagement.DAL.EF;

public class Repository : IRepository
{

    private readonly ManagerDbContext _dbContext;

    public Repository(ManagerDbContext managerDbContext)
    {
        _dbContext = managerDbContext;
    }

    public DeveloperManager ReadManager(int id)
    {
        return _dbContext.DeveloperManagers.Find(id);
    }

    public IEnumerable<DeveloperManager> ReadAllManagers()
    {
        return _dbContext.DeveloperManagers;
    }

    public IEnumerable<DeveloperManager> ReadManagersByName(Func<DeveloperManager, bool> criteria)
    {
        IQueryable<DeveloperManager> managersResult = _dbContext.DeveloperManagers;

        if (criteria != null)
        {
            managersResult = managersResult.Where(m => criteria(m));
        }

        return managersResult.ToList();
    }


    public void CreateManager(DeveloperManager developerManager)
    {
        _dbContext.DeveloperManagers.Add(developerManager);
        _dbContext.SaveChanges();
    }

    public IEnumerable<DeveloperManager> ReadAllManagersWithProjects()
    {
        return _dbContext.DeveloperManagers.Include(manager => manager.ManagedProjects);
    }

    public void UpdateManager(int managerId, double newSalary)
    {
        var manager = 
            _dbContext.DeveloperManagers.Find(managerId);

        if (manager != null)
        {
            manager.Salary = newSalary;
            _dbContext.SaveChanges();
        }
    }

    public Developer ReadDeveloper(Predicate<Developer> id)
    {
       return _dbContext.Developers.Find(id);
    }

    public IEnumerable<Developer> ReadAllDevelopers()
    {
        return _dbContext.Developers;
    }

    public IEnumerable<Developer> ReadDeveloperByNameAndProgrammingLanguage(string name, string programmingLanguage)
    {
        IQueryable<Developer> developersResult = _dbContext.Developers;
        if (name != null)
        {
            developersResult = developersResult.Where(d => d.Name == name);
        }

        if (programmingLanguage != null)
        {
            developersResult = developersResult.Where(d => d.ProgrammingLanguage == programmingLanguage);
        }

        return developersResult;
    }

    public void CreateDeveloper(int id, Developer developer)
    {
        _dbContext.Developers.Add(developer);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Developer> ReadAllDevelopersWithProjectTasks()
    {
        return _dbContext.Developers.Include(developer => developer.AssignedTasks);
    }

    public IEnumerable<Project> ReadALlProjects()
    {
        return _dbContext.Projects;
    }

    public void CreateProjectManager(int managerId, int projectId)
    {
        var manager = _dbContext.DeveloperManagers.Include(m => m.ManagedProjects).FirstOrDefault(m => m.Id == managerId);
        var project = _dbContext.Projects.Single(p => p.Id == projectId);

        if (manager != null && project != null)
        {
            manager.ManagedProjects.Add(project);
            _dbContext.SaveChanges();
        }
        else
        {
            throw new ArgumentException("Manager or project not found.");
        }
        
    }

    public void CreateProject(Project project)
    {
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Project> ReadAllProjectsByManager(int managerId)
    {
        return _dbContext.Projects.Where(p => p.ManagerId == managerId);
    }

    public void DeleteProjectManager(int managerId, int projectId)
    {
        var manager = _dbContext.DeveloperManagers
            .Include(m => m.ManagedProjects)
            .FirstOrDefault(m => m.Id == managerId);

        if (manager != null)
        {
            var projectToRemove = manager.ManagedProjects.FirstOrDefault(p => p.Id == projectId);

            if (projectToRemove != null)
            {
                manager.ManagedProjects.Remove(projectToRemove);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Project not associated with this manager.");
            }
        }
        else
        {
            throw new ArgumentException("Manager not found.");
        }
    }

    public IEnumerable<Project> ReadProjectsOfManager(int managerId)
    {
        throw new NotImplementedException();
    }

    public DeveloperManager ReadManagerWithDevelopers(long id)
    {
        return _dbContext.DeveloperManagers
            .Include(manager => manager.ManagedProjects)
            .ThenInclude(project => project.ProjectTasks) 
            .ThenInclude(task => task.Developer) 
            .FirstOrDefault(manager => manager.Id == id);
    }

    public Developer ReadDevelopersWithManagerId(long id)
    {
        return _dbContext.Developers
            .Include(developer => developer.AssignedTasks)
            .ThenInclude(task => task.Project)
            .ThenInclude(project => project.DeveloperManager)
            .FirstOrDefault(manager => manager.Id == id);
    }

    public IEnumerable<Client> ReadAllClients()
    {
        return _dbContext.Clients;
    }

    public Client ReadClient(int id)
    {
        return _dbContext.Clients.Find(id);
    }

    public void CreateClient(Client client)
    {
        _dbContext.Clients.Add(client);
        _dbContext.SaveChanges();
    }

    public IEnumerable<DeveloperTask> ReadAllDeveloperTasks()
    {
        return _dbContext.DeveloperTasks;
    }

    public IEnumerable<DeveloperTask> ReadTasksOfDeveloperWithProject(long developerId)
    {
        return _dbContext.DeveloperTasks
            .Where(task => task.DeveloperId == developerId)
            .Include(task => task.Project);
    }

    public IEnumerable<DeveloperTask> ReadTasksOfProject(int projectId)
    {
        return _dbContext.DeveloperTasks
            .Where(task => task.ProjectId == projectId);
    }

    public void CreateTask(DeveloperTask task)
    {
        _dbContext.DeveloperTasks.Add(task);
        _dbContext.SaveChanges();
    }

    public IdentityUser ReadUser(string userId)
    {
        return _dbContext.Users.Find(userId);
    }
}