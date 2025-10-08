using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class DesafioParticipante
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdDesafio { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DataHoraConclusao { get; set; }

    public virtual Desafio IdDesafioNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;
}
