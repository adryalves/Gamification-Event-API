using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class DesafioParticipante
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdDesafio { get; set; }

    public Status_Desafio StatusDesafio { get; set; } 

    public DateTime? DataHoraConclusao { get; set; }

    public virtual Desafio IdDesafioNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;
}
