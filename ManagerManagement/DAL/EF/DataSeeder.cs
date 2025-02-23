using ManagerManagement.BL.Domain;
using Microsoft.AspNetCore.Identity;

namespace ManagerManagement.DAL.EF;

public class DataSeeder
{
    public static void Seed(ManagerDbContext managerDbContext)
    {
        
        SeedManagers(managerDbContext);
        SeedDevelopers(managerDbContext);
        SeedProjects(managerDbContext);
        SeedDeveloperTasks(managerDbContext);
        SeedClients(managerDbContext);

        ICollection<Developer> developers = new List<Developer>(managerDbContext.Developers);
        ICollection<DeveloperTask> tasks = new List<DeveloperTask>(managerDbContext.DeveloperTasks);
        
            foreach (Developer developer in developers)
            {
                foreach (DeveloperTask task in tasks.Where(t => t.DeveloperId == developer.Id))
                {
                    developer.AssignedTasks.Add(task);
                }
            }

            ICollection<Project> projects = new List<Project>(managerDbContext.Projects);

            foreach (Project project in projects)
            {
                foreach (DeveloperTask task in tasks.Where(t => t.ProjectId == project.Id))
                {
                    project.ProjectTasks.Add(task);
                }
            }
            
            ICollection<DeveloperManager> managers = new List<DeveloperManager>(managerDbContext.DeveloperManagers);

            foreach (DeveloperManager manager in managers)
            {
                foreach (Project project in projects.Where(p => p.ManagerId == manager.Id))
                {
                    manager.ManagedProjects.Add(project);
                }
            }

            ICollection<Client> clients = new List<Client>(managerDbContext.Clients);

            foreach (DeveloperManager manager in managers)
            {
                foreach (Client client in clients.Where(c => c.ManagerId == manager.Id))
                {
                    manager.ManagedClients.Add(client);
                }
            }
            
        managerDbContext.SaveChanges();
        //managerDbContext.ChangeTracker.Clear();

    }

    private static void SeedManagers(ManagerDbContext managerDbContext)
    {
        DeveloperManager manager1 = new DeveloperManager()
        {
            Id = 11,
            Name = "Sami",
            Department = "IT",
            ProjectStatus = ProjectStatus.InProgress,
            HireDate = new DateTime(2020, 04, 01),
            Salary = 30000.00,
            PhoneNr = 0465695753,
            ManagerUser = managerDbContext.Users.Single(user => user.UserName == "sami@kdg.be"),
            MaintainedBy = "sami@kdg.be",
            ManagedClients = new List<Client>(),
            ManagedProjects = new List<Project>()
        };
        
        DeveloperManager manager2 = new DeveloperManager()
        {
            Id = 111,
            Name = "Said",
            Department = "Finance",
            ProjectStatus = ProjectStatus.Completed,
            HireDate = new DateTime(2022, 07, 01),
            Salary = 35000.00,
            PhoneNr = 0466795767,
            ManagerUser = managerDbContext.Users.Single(user => user.UserName == "said@kdg.be"),
            MaintainedBy = "said@kdg.be",
            ManagedClients = new List<Client>(),
            ManagedProjects = new List<Project>()
        };
        
        DeveloperManager manager3 = new DeveloperManager
        {
            Id = 1111,
            Name = "Jan",
            Department = "Marketing",
            ProjectStatus = ProjectStatus.InProgress,
            HireDate = new DateTime(2019, 01, 01),
            Salary = 40000.00,
            PhoneNr = 0465695753,
            ManagerUser = managerDbContext.Users.Single(user => user.UserName == "jan@kdg.be"),
            MaintainedBy = "jan@kdg.be",
            ManagedClients = new List<Client>(),
            ManagedProjects = new List<Project>()
        };
        
        DeveloperManager manager4 = new DeveloperManager
        {
            Id = 11111,
            Name = "Peter",
            Department = "HR",
            ProjectStatus = ProjectStatus.Cancelled,
            HireDate = new DateTime(2018, 04, 01),
            Salary = 45000.00,
            PhoneNr = 0465695243,
            ManagerUser = managerDbContext.Users.Single(user => user.UserName == "peter@kdg.be"),
            MaintainedBy = "peter@kdg.be",
            ManagedClients = new List<Client>(),
            ManagedProjects = new List<Project>()
        };
        
        DeveloperManager manager5 = new DeveloperManager
        {
            Id = 111111,
            Name = "Tobias",
            Department = "Client-service",
            ProjectStatus = ProjectStatus.InProgress,
            HireDate = new DateTime(2020, 04, 01),
            Salary = 40000.00,
            PhoneNr = 0465634944,
            ManagerUser = managerDbContext.Users.Single(user => user.UserName == "tobias@kdg.be"),
            MaintainedBy = "tobias@kdg.be",
            ManagedClients = new List<Client>(),
            ManagedProjects = new List<Project>()
        };
        
        managerDbContext.DeveloperManagers.Add(manager1);
        managerDbContext.DeveloperManagers.Add(manager2);
        managerDbContext.DeveloperManagers.Add(manager3);
        managerDbContext.DeveloperManagers.Add(manager4);
        managerDbContext.DeveloperManagers.Add(manager5);
    }

    private static void SeedDevelopers(ManagerDbContext managerDbContext)
    {
        Developer developer1 = new Developer
        {
            Id = 22,
            Name = "Mohammed",
            ProgrammingLanguage = "C#",
            JoinDate = new DateTime(2022, 11, 15),
            ProjectStatus = ProjectStatus.Cancelled,
            Salary = 30000.00,
            PhoneNr = 046756453,
            AssignedTasks = new List<DeveloperTask>()
        };
        
        Developer developer2 = new Developer
        {
            Id = 222,
            Name = "Peter",
            ProgrammingLanguage = "Java",
            JoinDate = new DateTime(2022, 01, 15),
            ProjectStatus = ProjectStatus.InProgress,
            Salary = 35000.00,
            PhoneNr = 046759056,
            AssignedTasks = new List<DeveloperTask>()
        };

        Developer developer3 = new Developer
        {
            Id = 2222,
            Name = "Mathias",
            ProgrammingLanguage = "Python",
            JoinDate = new DateTime(2023, 01, 15),
            ProjectStatus = ProjectStatus.InProgress,
            Salary = 34000.00,
            PhoneNr = 046759345,
            AssignedTasks = new List<DeveloperTask>()
        };
        Developer developer4 = new Developer
        {
            Id = 22222,
            Name = "Neils",
            ProgrammingLanguage = "JavaScript",
            JoinDate = new DateTime(2023, 06, 15),
            ProjectStatus = ProjectStatus.InProgress,
            Salary = 40000.00,
            PhoneNr = 046734545,
            AssignedTasks = new List<DeveloperTask>()
        };
        Developer developer5 = new Developer
        {
            Id = 222222,
            Name = "Anass",
            ProgrammingLanguage = "HTML",
            JoinDate = new DateTime(2023, 08, 15),
            ProjectStatus = ProjectStatus.InProgress,
            Salary = 45000.00,
            PhoneNr = 046734789,
            AssignedTasks = new List<DeveloperTask>()
        };
        
        managerDbContext.Developers.Add(developer1);
        managerDbContext.Developers.Add(developer2);
        managerDbContext.Developers.Add(developer3);
        managerDbContext.Developers.Add(developer4);
        managerDbContext.Developers.Add(developer5);
        
    }

    private static void SeedProjects(ManagerDbContext managerDbContext)
    {
        Project project1 = new Project
        {
            Id = 1,
            ManagerId = 11,
            ProjectDate = new DateTime(2023, 11, 1),
            ProjectDescription = "Project 1 Description",
            ContributionOrder = 1,
            ProjectTasks = new List<DeveloperTask>()
        };
        Project project2 = new Project
        {
            Id = 2,
            ManagerId = 111,
            ProjectDate = new DateTime(2023, 11, 5),
            ProjectDescription = "Project 2 Description",
            ContributionOrder = 2,
            ProjectTasks = new List<DeveloperTask>()
        };
        Project project3 = new Project
        {
            Id = 3,
            ManagerId = 1111,
            ProjectDate = new DateTime(2023, 11, 10),
            ProjectDescription = "Project 3 Description",
            ContributionOrder = 3,
            ProjectTasks = new List<DeveloperTask>()
        };
        Project project4 = new Project
        {
            Id = 4,
            ManagerId = 11111,
            ProjectDate = new DateTime(2023, 11, 15),
            ProjectDescription = "Project 4 Description",
            ContributionOrder = 4,
            ProjectTasks = new List<DeveloperTask>()
        };
        Project project5 = new Project
        {
            Id = 5,
            ManagerId = 111111,
            ProjectDate = new DateTime(2023, 08, 15),
            ProjectDescription = "Project 5 Description",
            ContributionOrder = 5,
            ProjectTasks = new List<DeveloperTask>()
        };
        
        managerDbContext.Projects.Add(project1);
        managerDbContext.Projects.Add(project2);
        managerDbContext.Projects.Add(project3);
        managerDbContext.Projects.Add(project4);
        managerDbContext.Projects.Add(project5);
    }

    private static void SeedDeveloperTasks(ManagerDbContext managerDbContext)
    {
        DeveloperTask developerTask1 = new DeveloperTask
        {
            Id = 1,
            ProjectId = 1,
            DeveloperId = 22,
            TaskName = "Task 1"
        };
        DeveloperTask developerTask2 = new DeveloperTask
        {
            Id = 2,
            ProjectId = 2,
            DeveloperId = 222,
            TaskName = "Task 2"

        };
        DeveloperTask developerTask3 = new DeveloperTask
        {
            Id = 3,
            ProjectId = 3,
            DeveloperId = 2222,
            TaskName = "Task 3"
        };
        DeveloperTask developerTask4 = new DeveloperTask
        {
            Id = 4,
            ProjectId = 4,
            DeveloperId = 22222,
            TaskName = "Task 7"
        };
        DeveloperTask developerTask5 = new DeveloperTask
        {
            Id = 5,
            ProjectId = 4,
            DeveloperId = 222222,
            TaskName = "Task 4"
        };
        DeveloperTask developerTask6 = new DeveloperTask
        {
            Id = 6,
            ProjectId = 5,
            DeveloperId = 222222,
            TaskName = "Task 5"
        };
        
        managerDbContext.DeveloperTasks.Add(developerTask1);
        managerDbContext.DeveloperTasks.Add(developerTask2);
        managerDbContext.DeveloperTasks.Add(developerTask3);
        managerDbContext.DeveloperTasks.Add(developerTask4);
        managerDbContext.DeveloperTasks.Add(developerTask5);
        managerDbContext.DeveloperTasks.Add(developerTask6);
    }

    private static void SeedClients(ManagerDbContext managerDbContext)
    {
        
        var managers = managerDbContext.DeveloperManagers.ToList();
        
        Client client1 = new Client
        {
            Id = 33,
            Name = "Fadi",
            BirthDate = new DateTime(1980, 02, 10),
            Email = "Fadi123@gmail.com",
            ManagerId = 11
        };
        Client client2 = new Client
        {
            Id = 333,
            Name = "Ibrahim",
            BirthDate = new DateTime(1975, 08, 12),
            Email = "Ibrahim123@gmail.com",
            ManagerId = 11
        };
        Client client3 = new Client
        {
            Id = 3333,
            Name = "Roland",
            BirthDate = new DateTime(1985, 06, 15),
            Email = "Roland123@gmail.com",
            ManagerId = 111
        };
        Client client4 = new Client
        {
            Id = 33333,
            Name = "Hans",
            BirthDate = new DateTime(1970, 09, 05),
            Email = "Hans123@gmail.com",
            ManagerId = 1111
        };
        Client client5 = new Client
        {
            Id = 333333,
            Name = "Jan",
            BirthDate = new DateTime(1965, 04, 21),
            Email = "Jan123@gmail.com",
            ManagerId = 11111
        };
        
        managerDbContext.Clients.Add(client1);
        managerDbContext.Clients.Add(client2);
        managerDbContext.Clients.Add(client3);
        managerDbContext.Clients.Add(client4);
        managerDbContext.Clients.Add(client5);

    }
}