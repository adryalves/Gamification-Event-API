using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Mapa
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public string? ImagemUrl { get; set; }

    public string? Descricao { get; set; }

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual ICollection<PontoMapa> PontoMapas { get; set; } = new List<PontoMapa>();
}
