using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class ParticipantePremio
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdPremio { get; set; }

    public string? Motivo { get; set; }

    public DateTime DataConcessao { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual Premio IdPremioNavigation { get; set; } = null!;
}
