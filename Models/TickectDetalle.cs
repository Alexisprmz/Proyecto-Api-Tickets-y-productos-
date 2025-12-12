public class TicketDetalle
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
    public int ProductoId { get; set; }
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public decimal Total { get; set; }
}
