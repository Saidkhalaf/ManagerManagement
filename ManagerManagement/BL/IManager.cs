using ManagerManagement.BL.Domain;

namespace ManagerManagement.BL;

public interface IManager
{
    DeveloperManager GetManager(int id);
    IEnumerable<DeveloperManager> GetAllManagers();
    IEnumerable<DeveloperManager> GetManagersByDepartment(string department);
    DeveloperManager AddManager(string adminId, int id, string name, string department, ProjectStatus projectStatus, DateTime hireDate, double salary);
    IEnumerable<DeveloperManager> GetAllManagersWithProjects();
    void UpdateManager( int managerId, double newSalary);
    Developer GetDeveloper(Predicate<Developer> id);
    IEnumerable<Developer> GetAllDevelopers();
    IEnumerable<Developer> GetDevelopersByNameAndProgrammingLanguage(string name, string programmingLanguage);
    Developer AddDeveloper( int id, string name, string programmingLanguage);
    IEnumerable<Developer> GetAllDevelopersWithProjectTasks();
    IEnumerable<DeveloperTask> GetTasksOfDeveloperWithProject(long developerId);

    IEnumerable<Project> GetAllProjects();
    IEnumerable<Project> GetAllProjectsByManager(int managerId);
    void AddProjectManager(int managerId, int projectId);
    Project AddProject(int managerId, ICollection<DeveloperTask> tasks, string projectDescription);
    void DeleteProjectManager(int managerId, int projectId);

    DeveloperManager GetManagerWithDevelopers(long id);
    Developer GetDevelopersWithManagerId(long id);

    IEnumerable<Client> GetAllClients();
    Client GetClient(int id);
    Client AddClient(string name, DateTime birthDate, string email, int managerId);

    IEnumerable<DeveloperTask> GetAllDeveloperTasks();
    IEnumerable<DeveloperTask> GetTasksOfProject(int projectId);
    DeveloperTask AddTaskToProject(int projectId, int developerId, string taskName);

}