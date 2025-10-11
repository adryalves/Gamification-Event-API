using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class InteracaoPublicacao
{
    public Guid Id { get; set; }

    public Guid IdPublicacao { get; set; }

    public Guid IdParticipante { get; set; }

    public Tipo_Interacao TipoInteracao { get; set; } 

    public string? TextoComentario { get; set; }

    public DateTime DataHora { get; set; }

    public bool Deletado { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual Publicacao IdPublicacaoNavigation { get; set; } = null!;
}
