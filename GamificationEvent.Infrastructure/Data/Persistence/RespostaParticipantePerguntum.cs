using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class RespostaParticipantePerguntum
{
    public Guid Id { get; set; }

    public Guid IdPerguntaPesquisa { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid? IdOpcaoPerguntaPesquisa { get; set; }

    public string? TextoResposta { get; set; }

    public DateTime DataResposta { get; set; }

    public virtual OpcaoPerguntaPesquisa? IdOpcaoPerguntaPesquisaNavigation { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual PerguntaPesquisa IdPerguntaPesquisaNavigation { get; set; } = null!;
}
