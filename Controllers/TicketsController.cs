using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly TicketService _ticketService;
    public TicketsController(TicketService ticketService) { _ticketService = ticketService; }

    [HttpPost]
public async Task<IActionResult> Crear([FromBody] List<CartItemDto> items)
{
    var ticket = await _ticketService.GenerarTicketAsync(items);
    
    
    return Ok(new 
    { 
        message = "Ticket generado exitosamente",
        ticket = new 
        { 
            id = ticket.Id,
            fecha = ticket.Fecha,
            total = ticket.Total  
        }
    });
}


    [HttpGet]
    public async Task<IActionResult> Historial()
    {
        var historial = await _ticketService.GetHistorialTicketsDtoAsync();
        return Ok(historial);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Detalle(int id)
    {
        var ticket = await _ticketService.GetDetalleTicketDtoAsync(id);
        return ticket != null ? Ok(ticket) : NotFound();
    }
}
