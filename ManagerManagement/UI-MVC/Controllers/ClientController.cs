using ManagerManagement.BL;
using ManagerManagement.UI.MVC.Models;
using ManagerManagement.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerManagement.UI.MVC.Controllers;

public class ClientController : Controller
{
    private readonly IManager _Manager;

    public ClientController(IManager manager)
    {
        _Manager = manager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var clients = _Manager.GetAllClients();
        return View(clients);
    }
    
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [Authorize]
    public  IActionResult Add(NewClientModel client)
    {
        if (!ModelState.IsValid)
        {
            return View(client);
        }

        _Manager.AddClient(client.Name, client.BirthDate, client.Email, client.ManagerId);
        ModelState.Clear();
        return RedirectToAction("AddClientConfirmation", "Client");
    }
    
    
    [HttpGet]
    public IActionResult Details(int id)
    {
        var client = _Manager.GetClient(id);
        return View(client);
    }
    
    public IActionResult AddClientConfirmation(NewClientModel manager)
    {
        var vm = new AddClientConfirmationModel();
        vm.Name = manager.Name;
        vm.Email = manager.Email;

        return View(vm);
    }
    
}