using ManagerManagement.BL;
using ManagerManagement.BL.Domain;
using ManagerManagement.UI.MVC.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagerManagement.UI.MVC.Controllers.API;

[Route("/api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly IManager _manager;

    public ClientsController(IManager manager)
    {
        _manager = manager;
    }

    [HttpGet]
    public IEnumerable<Client> GetAllClients()
    {
        var clients = _manager.GetAllClients();
        return clients;
    }
    
    [HttpGet("{id}")]
    public ActionResult<ClientDto> GetClient(int id)
    {
        var client = _manager.GetClient(id);

        if (client == null)
        {
            return NotFound();
        }

        return Ok(new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            BirthDate = client.BirthDate,   
            Email = client.Email
        });
    }

    [HttpPost] 
    [Authorize]
    public IActionResult AddNewClient(NewClientDto clientDto)
    {
        var createdClient = _manager.
            AddClient(clientDto.Name, clientDto.BirthDate, clientDto.Email, clientDto.ManagerId);
        
        return CreatedAtAction("GetClient",
            new { id = createdClient.Id }, new ClientDto
            {   
                Name = createdClient.Name,
                BirthDate = createdClient.BirthDate,
                Email = createdClient.Email
            });
    } 
    
    
}