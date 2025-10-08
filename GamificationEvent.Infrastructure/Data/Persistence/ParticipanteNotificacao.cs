using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class ParticipanteNotificacao
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdNotificacao { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DataHoraVisualizacao { get; set; }

    public virtual Notificacao IdNotificacaoNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;
}
