using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PerguntaPesquisa
{
    public Guid Id { get; set; }

    public Guid IdPesquisa { get; set; }

    public string TextoPergunta { get; set; } = null!;

    public string TipoPergunta { get; set; } = null!;

    public bool Deletado { get; set; }

    public virtual Pesquisa IdPesquisaNavigation { get; set; } = null!;

    public virtual ICollection<OpcaoPerguntaPesquisa> OpcaoPerguntaPesquisas { get; set; } = new List<OpcaoPerguntaPesquisa>();

    public virtual ICollection<RespostaParticipantePerguntum> RespostaParticipantePergunta { get; set; } = new List<RespostaParticipantePerguntum>();
}
