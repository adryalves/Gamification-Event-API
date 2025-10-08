using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class OpcaoPerguntaPesquisa
{
    public Guid Id { get; set; }

    public Guid IdPerguntaPesquisa { get; set; }

    public string TextoOpcaoResposta { get; set; } = null!;

    public bool Deletado { get; set; }

    public virtual PerguntaPesquisa IdPerguntaPesquisaNavigation { get; set; } = null!;

    public virtual ICollection<RespostaParticipantePerguntum> RespostaParticipantePergunta { get; set; } = new List<RespostaParticipantePerguntum>();
}
