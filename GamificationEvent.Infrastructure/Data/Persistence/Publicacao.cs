using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Publicacao
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdEvento { get; set; }

    public string? Texto { get; set; }

    public DateTime DataHora { get; set; }

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual ICollection<InteracaoPublicacao> InteracaoPublicacaos { get; set; } = new List<InteracaoPublicacao>();

    public virtual ICollection<PublicacaoImagem> PublicacaoImagems { get; set; } = new List<PublicacaoImagem>();
}
