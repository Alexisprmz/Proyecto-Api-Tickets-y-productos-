public class Ticket
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Subtotal { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; }
    public ICollection<TicketDetalle> Detalles { get; set; }
}
