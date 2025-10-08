using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class BadgeParticipante
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdBadge { get; set; }

    public DateTime? DataHoraConquista { get; set; }

    public virtual Badge IdBadgeNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;
}
