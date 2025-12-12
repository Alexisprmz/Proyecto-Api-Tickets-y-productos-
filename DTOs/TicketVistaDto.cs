using System;
using System.Collections.Generic;

public class TicketVistaDto
{
    public int Id { get; set; }
    public string Codigo { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Subtotal { get; set; }
    public decimal IVA { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; }
    public List<TicketDetalleDto> Detalles { get; set; }
}
