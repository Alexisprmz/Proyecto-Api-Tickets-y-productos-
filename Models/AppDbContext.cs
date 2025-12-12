using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Producto> Productos { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketDetalle> TicketDetalles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
