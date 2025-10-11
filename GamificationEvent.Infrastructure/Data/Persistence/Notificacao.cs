using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Notificacao
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Tipo { get; set; }

    public string Mensagem { get; set; } = null!;

    public DateTime DataHoraEnvio { get; set; }

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual ICollection<ParticipanteNotificacao> ParticipanteNotificacaos { get; set; } = new List<ParticipanteNotificacao>();
}
