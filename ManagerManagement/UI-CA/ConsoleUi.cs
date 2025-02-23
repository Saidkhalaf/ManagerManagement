using ManagerManagement.BL;
using ManagerManagement.BL.Domain;

namespace ManagerManagement.UI.CA;
public class ConsoleUi
{
    private readonly IManager _manager;

    public ConsoleUi(Manager manager)
    {
        _manager = manager;
    }

    public void Run()
{
    SearchDeveloperByNameOrProgrammingLanguage();
    bool isRunning = true;
    
    while (isRunning)
    {
        ShowMenuOptions();

        string choice = Console.ReadLine();

        IEnumerable<Developer> developers;
        switch (choice)
        {
            case "0":
                isRunning = false;
                break;

            case "1":
                ShowAllManagers();
                break;

            case "2":
                ShowManagersWithDepartment();
                break;

            case "3":
                ShowAllDevelopers();
                break;
            
            case "4":
                ShowDeveloperByNameOrProgrammingLanguage();
                break;


            case "5":
                AddManager();
                break;

            case "6":
                AddDeveloper();
                break;
            
            case "7":
                AddProjectToManager();
                break;
            
            case "8":
                RemoveProjectFromManager();
                break;
            
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }
    }
}

    private void ShowMenuOptions()
    {
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("==========================");
        Console.WriteLine("0) Quit");
        Console.WriteLine("1) Show all managers");
        Console.WriteLine("2) Show managers with department");
        Console.WriteLine("3) Show all developers");
        Console.WriteLine("4) Show developers with Name and/or Programming Language");
        Console.WriteLine("5) Add a manager");
        Console.WriteLine("6) Add a developer");
        Console.WriteLine("7) Add Project to Manager ");
        Console.WriteLine("8) Remove Project from Manager");
        Console.Write("Choice (0-8): ");
    }
    
    private void ShowAllManagers()
    {
        Console.WriteLine("All Managers:");
        IEnumerable<DeveloperManager> managers = _manager.GetAllManagersWithProjects();
        foreach (DeveloperManager manager in managers)
        {
            Console.WriteLine($"- Id: {manager.Id}, Name: {manager.Name}, Department: {manager.Department}");
            foreach (var project in manager.ManagedProjects)
            {
                Console.WriteLine($"      Project: {project.ProjectDescription}");
            }
        }
    }
    
    private void ShowManagersWithDepartment()
    {
        Console.WriteLine("Enter Department: ");
        string department = Console.ReadLine();
        IEnumerable<DeveloperManager> managersWithDepartments = _manager.GetManagersByDepartment(department);
        foreach (DeveloperManager manager in managersWithDepartments)
        {
            Console.WriteLine($"- Name: {manager.Name}, Department: {manager.Department}");
        }
    }
    
    private void ShowAllDevelopers()
    {
        Console.WriteLine("All Developers:");
        IEnumerable<Developer> developers = _manager.GetAllDevelopersWithProjectTasks();
        foreach (Developer developer in developers)
        {
            Console.WriteLine($"-Id: {developer.Id}, Name: {developer.Name}, Language: {developer.ProgrammingLanguage}");

            foreach (var task in developer.AssignedTasks)
            {
                Console.WriteLine($"     Task: {task.TaskName}");
            }
        }
    }

    private void ShowDeveloperByNameOrProgrammingLanguage()
    {
        bool found = false;

        Console.Write("Enter developer Name: ");
        string name = Console.ReadLine();
        
        foreach (var developer in _manager.GetAllDevelopers())
        {
            if (developer.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine(
                    $"- Id: {developer.Id}, Name: {developer.Name}, Language: {developer.ProgrammingLanguage}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No developers found with the entered name.");
        }

        // If developer found by name, skip language search
        if (!found)
        {
            Console.Write("Enter Programming Language: ");
            string developerProgrammingLanguage = Console.ReadLine();

            // Display developers by programming language
            foreach (Developer developer in _manager.GetAllDevelopers())
            {
                if (developer.ProgrammingLanguage.Equals(developerProgrammingLanguage,
                        StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine(
                        $"- Id: {developer.Id}, Name: {developer.Name}, Language: {developer.ProgrammingLanguage}");
                    found = true;
                }
            }

            // If no developers were found by programming language, provide a message.
            if (!found)
            {
                Console.WriteLine("No developers found with the entered programming language.");
            }
        }
    }

    private void AddManager()
{
    // Add a manager
    Console.WriteLine("Add Manager");
    Console.WriteLine("=============");
    Console.WriteLine("Enter Manager Id: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Enter a Manager name:");
        string managerName = Console.ReadLine();
        Console.Write("Enter department: ");
        string managerDepartment = Console.ReadLine();
        Console.Write("Geef een userId: ");
        var userId = Console.ReadLine();

        Console.WriteLine("Enter project status (NotStarted, InProgress, Completed): ");
        if (Enum.TryParse<ProjectStatus>(Console.ReadLine(), out ProjectStatus projectStatus))
        {
            Console.WriteLine("Enter hire date (yyyy-MM-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime hireDate))
            {
                Console.WriteLine("Enter salary: ");
                if (double.TryParse(Console.ReadLine(), out double salary))
                {
                    // Create a new Manager object and add it
                    DeveloperManager newDeveloperManager = _manager.AddManager(userId, id, managerName, managerDepartment, projectStatus, hireDate, salary);
                    if (newDeveloperManager != null)
                    {
                        Console.WriteLine($"Added Manager: Id - {newDeveloperManager.Id}, Name - {newDeveloperManager.Name}, Department - {newDeveloperManager.Department}");
                    }
                    else
                    {
                        Console.WriteLine("Error: Manager validation failed. Please try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Error: Invalid salary. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid hire date. Please enter a valid date in the format yyyy-MM-dd.");
            }
        }
        else
        {
            Console.WriteLine("Error: Invalid project status. Please enter a valid status (NotStarted, InProgress, Completed).");
        }
    }
    else
    {
        Console.WriteLine("Error: Invalid Manager Id. Please enter a valid number.");
    }
}


    private void AddDeveloper()
    {
        // Add a developer
        Console.WriteLine("Add Developer");
        Console.WriteLine("=============");
        Console.WriteLine("Enter Developer Id: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Enter a developer Name: ");
            string developerName = Console.ReadLine();
            Console.Write("Enter a Programming Language: ");
            string programmingLanguage = Console.ReadLine();

            // Create a new Developer object and add it
            var newDeveloper = _manager.AddDeveloper(id, developerName, programmingLanguage);
            if (newDeveloper != null)
            {
                Console.WriteLine($"Added Developer: Id - {newDeveloper.Id}, Name - {newDeveloper.Name}, Language - {newDeveloper.ProgrammingLanguage}");
            }
            else
            {
                Console.WriteLine("Error: Developer validation failed. Please try again.");
            }
        }
        else
        {
            Console.WriteLine("Error: Invalid Developer Id. Please enter a valid number.");
        }
    }

    private void AddProjectToManager()
    {
        Console.WriteLine("Which Manager would you like to add a project to?");
                IEnumerable<DeveloperManager> managers = _manager.GetAllManagers();

                int managerCount = managers.Count();
                int managerIndex = -1;
    
                for (int i = 0; i < managerCount; i++)
                {
                    Console.WriteLine($"[{i + 1}] {managers.ElementAt(i).Name}");
                }
    
                Console.Write("Please enter a Manager ID: ");
                if (int.TryParse(Console.ReadLine(), out managerIndex) && managerIndex > 0 &&
                    managerIndex <= managerCount)
                {
                    var selectedManager = managers.ElementAt(managerIndex - 1);
                    Console.WriteLine($"You've selected: {selectedManager.Name}");

                    Console.WriteLine("Which Project would you like to assign to this Manager?");
                    IEnumerable<Project> projects = _manager.GetAllProjects();
        
                    int projectCount = projects.Count();
                    int projectIndex = -1;
                    for (int i = 0; i < projectCount; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {projects.ElementAt(i).ProjectDescription}");
                    }
        
                    Console.Write("Enter a Project ID: ");
                    if (int.TryParse(Console.ReadLine(), out projectIndex) && projectIndex > 0 &&
                        projectIndex <= projectCount)
                    {
                        var selectedProject = projects.ElementAt(projectIndex - 1);
                        Console.WriteLine($"You've selected: {selectedProject.ProjectDescription}");

                        // Call the method to add the selected project to the selected manager
                        _manager.AddProjectManager(selectedManager.Id, selectedProject.Id);
                    }
                    else
                    {
                        Console.WriteLine("Invalid project selection. Please choose a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid manager selection. Please choose a valid number.");
                }
    }

    private void RemoveProjectFromManager()
    {
        Console.WriteLine("Which Manager would you like to remove a project from?");
                IEnumerable<DeveloperManager> managersWithProjects = _manager.GetAllManagersWithProjects();
                int managerWithProjectCount = managersWithProjects.Count();

                for (int i = 0; i < managerWithProjectCount; i++)
                {
                    Console.WriteLine($"[{i + 1}] {managersWithProjects.ElementAt(i).Name}");
                }
                Console.Write("Please enter a Manager ID: ");
                if (int.TryParse(Console.ReadLine(), out int managerInd) && managerInd > 0 &&
                    managerInd <= managerWithProjectCount)
                {
                    var selectedManager = managersWithProjects.ElementAt(managerInd - 1);
                    Console.WriteLine($"You've selected: {selectedManager.Name}");

                    Console.WriteLine($"Which project would you like to remove from Manager: {selectedManager.Name} ?");
                    IEnumerable<Project> projects = _manager.GetAllProjectsByManager(selectedManager.Id);
                    
                    int projectCount = projects.Count();

                    for (int i = 0; i < projectCount; i++)
                    {
                        Console.WriteLine($"[{i + 1}] {projects.ElementAt(i).ProjectDescription}");
                    }

                    Console.Write("Please enter a Project ID to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int projectIndex) && projectIndex > 0 &&
                        projectIndex <= projectCount)
                    {
                        var selectedProject = projects.ElementAt(projectIndex - 1);
                        Console.WriteLine($"You've selected: {selectedProject.ProjectDescription}");
                        
                        // Your logic to remove the selected project from the manager's projects
                        _manager.DeleteProjectManager(selectedManager.Id, selectedProject.Id);

                        Console.WriteLine($"Project '{selectedProject.ProjectDescription}' removed from Manager '{selectedManager.Name}' successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid Project ID. Please choose a valid number.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Manager ID. Please choose a valid number.");
                }
    }


    private void SearchDeveloperByNameOrProgrammingLanguage()
    {
        //foreach (var developer in _manager.GetDevelopersByNameAndProgrammingLanguage("Mohammed", "C#"))
        foreach (var developer in _manager.GetDevelopersByNameAndProgrammingLanguage("Peter", null)) //it work because it is nullable 
        //foreach (var developer in _manager.GetDevelopersByNameAndProgrammingLanguage(null, "Java"))//it work because it is nullable 
        //foreach (var developer in _manager.GetDevelopersByNameAndProgrammingLanguage(null, null))
        {
            Console.WriteLine(
                $"- Id: {developer.Id}, Name: {developer.Name}, Language: {developer.ProgrammingLanguage}," +
                $"JoinDate: {developer.JoinDate}, ProjectStatus: {developer.ProjectStatus}, salary: {developer.Salary}, PhoneNr: {developer.PhoneNr}");
        }


    }

}