using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Mensagem
{
    public Guid Id { get; set; }

    public Guid IdConversa { get; set; }

    public Guid IdParticipanteRemetente { get; set; }

    public string Texto { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime DataHora { get; set; }

    public virtual Conversa IdConversaNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteRemetenteNavigation { get; set; } = null!;
}
