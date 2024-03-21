using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class DetallesDeFactura
{
    public int Id { get; set; }

    public int? FacturaId { get; set; }

    public int? ServicioId { get; set; }

    public DateTime? FechaDeCreación { get; set; }

    public virtual Factura? Factura { get; set; }

    public virtual Servicio? Servicio { get; set; }
}
