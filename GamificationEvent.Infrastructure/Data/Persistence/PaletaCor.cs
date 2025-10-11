using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PaletaCor
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = null!;

    public Guid IdCor1 { get; set; }

    public Guid IdCor2 { get; set; }

    public Guid IdCor3 { get; set; }

    public Guid IdCor4 { get; set; }

    public bool Deletado { get; set; }

    public virtual ICollection<Evento> Eventos { get; set; } = new List<Evento>();

    public virtual Cor IdCor1Navigation { get; set; } = null!;

    public virtual Cor IdCor2Navigation { get; set; } = null!;

    public virtual Cor IdCor3Navigation { get; set; } = null!;

    public virtual Cor IdCor4Navigation { get; set; } = null!;
}
