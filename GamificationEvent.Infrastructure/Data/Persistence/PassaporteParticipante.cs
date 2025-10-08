using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PassaporteParticipante
{
    public Guid Id { get; set; }

    public Guid IdPassaporte { get; set; }

    public Guid IdParticipante { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DataHoraConclusao { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual Passaporte IdPassaporteNavigation { get; set; } = null!;
}
