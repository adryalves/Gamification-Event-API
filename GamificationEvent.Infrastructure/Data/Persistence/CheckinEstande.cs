using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class CheckinEstande
{
    public Guid Id { get; set; }

    public Guid IdEstande { get; set; }

    public Guid IdParticipante { get; set; }

    public DateTime DataHora { get; set; }

    public virtual Estande IdEstandeNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;
}
