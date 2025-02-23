using ManagerManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace ManagerManagement.DAL;

public class InMemoryRepository : IRepository
{
        private static List<DeveloperManager> managerList;
        private static List<Developer> developerList;
        private static int nextId;

        public InMemoryRepository()
        {
            managerList = new List<DeveloperManager>();
            developerList = new List<Developer>();
            nextId = 1;
        }


        public DeveloperManager ReadManager(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeveloperManager> ReadAllManagers()
        {
            return managerList;
        }

        public IEnumerable<DeveloperManager> ReadManagersByName(Func<DeveloperManager, bool> criteria)
        {
            return managerList.Where(criteria).ToList();
        }

        public void CreateManager(DeveloperManager entity)
        {
            if (entity is DeveloperManager manager)
            {
                managerList.Add(manager);
            }
        }

        public IEnumerable<DeveloperManager> ReadAllManagersWithProjects()
        {
            throw new NotImplementedException();
        }

        public void UpdateManager(int managerId, double newSalary)
        {
            throw new NotImplementedException();
        }

        public Developer ReadDeveloper(Predicate<Developer> id)
        {
            return developerList.FirstOrDefault(id.Invoke);
        }

        public IEnumerable<Developer> ReadAllDevelopers()
        {
            return developerList;
        }

        public IEnumerable<Developer> ReadDeveloperByNameAndProgrammingLanguage(string? name, string? programmingLanguage)
        {
            IEnumerable<Developer> developersResult = developerList;
            if (name != null)
            {
                developersResult = developersResult.Where(d => d.Name.Equals(name));
            }

            if (programmingLanguage != null)
            {
                developersResult = developersResult.Where(d => d.ProgrammingLanguage.Equals(programmingLanguage));
            }

            return developersResult;
        }

        public void CreateDeveloper(int id, Developer entity)
        {
            if (entity is Developer developer)
            {
                developer.Id = id;
                developerList.Add(developer);
            }
        }

        public IEnumerable<Developer> ReadAllDevelopersWithProjectTasks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> ReadALlProjects()
        {
            throw new NotImplementedException();
        }

        public void CreateProjectManager(int managerId, int projectId)
        {
            throw new NotImplementedException();
        }

        public void CreateProject(Project project)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> ReadAllProjectsByManager(int managerId)
        {
            throw new NotImplementedException();
        }

        public void DeleteProjectManager(int projectId, int managerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> ReadProjectsOfManager(int managerId)
        {
            throw new NotImplementedException();
        }

        public DeveloperManager ReadManagerWithDevelopers(long id)
        {
            throw new NotImplementedException();
        }

        public Developer ReadDevelopersWithManagerId(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> ReadAllClients()
        {
            throw new NotImplementedException();
        }

        public Client ReadClient(int id)
        {
            throw new NotImplementedException();
        }

        public void CreateClient(Client client)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeveloperTask> ReadAllDeveloperTasks()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeveloperTask> ReadTasksOfDeveloperWithProject(long developerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DeveloperTask> ReadTasksOfProject(int projectId)
        {
            throw new NotImplementedException();
        }

        public void CreateTask(DeveloperTask task)
        {
            throw new NotImplementedException();
        }

        public IdentityUser ReadUser(string userId)
        {
            throw new NotImplementedException();
        }
}
