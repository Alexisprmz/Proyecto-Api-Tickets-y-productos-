using Microsoft.EntityFrameworkCore;

public class TicketService
{
    private readonly AppDbContext _context;
    public TicketService(AppDbContext context) { _context = context; }

    public async Task<Ticket> GenerarTicketAsync(List<CartItemDto> items)
{
    decimal subtotal = 0;
    
    foreach(var item in items)
    {
        var producto = await _context.Productos.FindAsync(item.ProductoId);
        if(producto == null || producto.Stock < item.Cantidad) 
            throw new Exception("Producto no disponible");
        
        producto.Stock -= item.Cantidad; 
        var totalItem = producto.Precio * item.Cantidad;
        subtotal += totalItem;
    }
    
    decimal iva = Math.Round(subtotal * 0.16m, 2);
    decimal total = subtotal + iva;
    
    var ticket = new Ticket
    {
        Codigo = "TKT-" + DateTime.Now.Ticks.ToString().Substring(8),
        Fecha = DateTime.Now,
        Subtotal = subtotal,
        IVA = iva,
        Total = total,
        Estado = "Completado"
    };
    
    _context.Tickets.Add(ticket);
    await _context.SaveChangesAsync(); 
    
    foreach(var item in items)
    {
        var producto = await _context.Productos.FindAsync(item.ProductoId);
        var totalItem = producto.Precio * item.Cantidad;
        
        var detalle = new TicketDetalle 
        { 
            TicketId = ticket.Id,  
            ProductoId = producto.Id, 
            Cantidad = item.Cantidad, 
            Precio = producto.Precio, 
            Total = totalItem 
        };
        _context.TicketDetalles.Add(detalle); 
    }
    
    await _context.SaveChangesAsync(); 
    
    return ticket; 
}


    public async Task<TicketVistaDto> GetDetalleTicketDtoAsync(int ticketId)
    {
        var ticket = await _context.Tickets
            .Include(t => t.Detalles)
            .ThenInclude(d => d.Producto)
            .FirstOrDefaultAsync(t => t.Id == ticketId);

        if (ticket == null) return null;

        return new TicketVistaDto
        {
            Id = ticket.Id,
            Codigo = ticket.Codigo,
            Fecha = ticket.Fecha,
            Subtotal = ticket.Subtotal,
            IVA = ticket.IVA,
            Total = ticket.Total,
            Estado = ticket.Estado,
            Detalles = ticket.Detalles.Select(d => new TicketDetalleDto
            {
                ProductoId = d.ProductoId,
                ProductoNombre = d.Producto.Nombre,
                Cantidad = d.Cantidad,
                Precio = d.Precio,
                Total = d.Total
            }).ToList()
        };
    }

    public async Task<List<TicketVistaDto>> GetHistorialTicketsDtoAsync()
    {
        var tickets = await _context.Tickets
            .Include(t => t.Detalles)
            .ThenInclude(d => d.Producto)
            .OrderByDescending(t => t.Fecha)
            .ToListAsync();

        return tickets.Select(ticket => new TicketVistaDto
        {
            Id = ticket.Id,
            Codigo = ticket.Codigo,
            Fecha = ticket.Fecha,
            Subtotal = ticket.Subtotal,
            IVA = ticket.IVA,
            Total = ticket.Total,
            Estado = ticket.Estado,
            Detalles = ticket.Detalles.Select(d => new TicketDetalleDto
            {
                ProductoId = d.ProductoId,
                ProductoNombre = d.Producto.Nombre,
                Cantidad = d.Cantidad,
                Precio = d.Precio,
                Total = d.Total
            }).ToList()
        }).ToList();
    }
}
