using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Badge
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Icone { get; set; }

    public string Regra { get; set; } = null!;

    public bool Deletado { get; set; }

    public virtual ICollection<BadgeParticipante> BadgeParticipantes { get; set; } = new List<BadgeParticipante>();

    public virtual Evento IdEventoNavigation { get; set; } = null!;
}
