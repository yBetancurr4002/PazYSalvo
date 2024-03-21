using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public DateTime? FechaDeCreación { get; set; }

    public virtual ICollection<DetallesDeFactura> DetallesDeFacturas { get; set; } = new List<DetallesDeFactura>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
