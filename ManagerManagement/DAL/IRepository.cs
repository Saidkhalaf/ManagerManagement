using ManagerManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace ManagerManagement.DAL;

public interface IRepository
{
    DeveloperManager ReadManager(int id);
    IEnumerable<DeveloperManager> ReadAllManagers();
    IEnumerable<DeveloperManager> ReadManagersByName(Func<DeveloperManager, bool> criteria);
    void CreateManager(DeveloperManager developerManager);
    IEnumerable<DeveloperManager> ReadAllManagersWithProjects();
    void UpdateManager(int managerId, double newSalary);
    
    
    Developer ReadDeveloper(Predicate<Developer> id);
    IEnumerable<Developer> ReadAllDevelopers();
    IEnumerable<Developer> ReadDeveloperByNameAndProgrammingLanguage(string? name, string? programmingLanguage);
    void CreateDeveloper(int id, Developer developer);
    IEnumerable<Developer> ReadAllDevelopersWithProjectTasks();

    IEnumerable<Project> ReadALlProjects();
    void CreateProjectManager(int managerId, int projectId);
    void CreateProject(Project project);
    IEnumerable<Project> ReadAllProjectsByManager(int managerId);
    void DeleteProjectManager(int managerId, int projectId);

    DeveloperManager ReadManagerWithDevelopers(long id);
    Developer ReadDevelopersWithManagerId(long id);

    IEnumerable<Client> ReadAllClients();
    Client ReadClient(int id);
    void CreateClient(Client client);

    IEnumerable<DeveloperTask> ReadAllDeveloperTasks();
    IEnumerable<DeveloperTask> ReadTasksOfDeveloperWithProject(long developerId);
    IEnumerable<DeveloperTask> ReadTasksOfProject(int projectId);
    void CreateTask(DeveloperTask task);
    IdentityUser ReadUser(string userId);

}

