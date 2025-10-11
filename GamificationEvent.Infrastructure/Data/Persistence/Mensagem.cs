using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Mensagem
{
    public Guid Id { get; set; }

    public Guid IdConversa { get; set; }

    public Guid IdParticipanteRemetente { get; set; }

    public string Texto { get; set; } = null!;

    public Status_Mensagem StatusMensagem { get; set; } 

    public DateTime DataHora { get; set; }

    public virtual Conversa IdConversaNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteRemetenteNavigation { get; set; } = null!;
}
