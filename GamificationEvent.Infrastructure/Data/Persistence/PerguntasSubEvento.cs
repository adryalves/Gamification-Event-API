using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PerguntasSubEvento
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdSubEvento { get; set; }

    public string? Assunto { get; set; }

    public string Pergunta { get; set; } = null!;

    public DateTime DataHora { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual SubEvento IdSubEventoNavigation { get; set; } = null!;
}
