using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class ParticipanteInteresse
{
    public Guid Id { get; set; }

    public Guid IdInteresse { get; set; }

    public Guid IdParticipante { get; set; }

    public virtual Interesse IdInteresseNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;
}
