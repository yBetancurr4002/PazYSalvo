﻿using System;
using System.Collections.Generic;

namespace PazYSalvoAPP.Data.Context;

public partial class Persona
{
    public int Id { get; set; }

    public string Nombres { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string CorreoElectronico { get; set; } = null!;

    public string DocumentoIdentificacion { get; set; } = null!;

    public DateTime? FechaDeCreación { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
