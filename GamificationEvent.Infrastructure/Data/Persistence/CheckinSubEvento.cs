using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class CheckinSubEvento
{
    public Guid Id { get; set; }

    public Guid IdSubEvento { get; set; }

    public Guid IdParticipante { get; set; }

    public DateTime DataHora { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual SubEvento IdSubEventoNavigation { get; set; } = null!;
}
