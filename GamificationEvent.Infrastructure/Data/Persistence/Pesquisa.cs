using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Pesquisa
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public Guid? IdSubEvento { get; set; }

    public Guid? IdPatrocinador { get; set; }

    public string Nome { get; set; } = null!;

    public int? Pontuacao { get; set; }

    public string? Categoria { get; set; }

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual Patrocinador? IdPatrocinadorNavigation { get; set; }

    public virtual SubEvento? IdSubEventoNavigation { get; set; }

    public virtual ICollection<PerguntaPesquisa> PerguntaPesquisas { get; set; } = new List<PerguntaPesquisa>();
}
