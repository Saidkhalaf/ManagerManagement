using ManagerManagement.BL;
using Microsoft.AspNetCore.Mvc;

namespace ManagerManagement.UI.MVC.Controllers;

public class DeveloperController : Controller
{
    private readonly IManager _manager;

    public DeveloperController(IManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var developers = _manager.GetAllDevelopers();
        return View(developers);
    }

    public IActionResult Details(long id)
    {
        var developer = _manager.GetDevelopersWithManagerId(id);
        return View(developer);
    }
}