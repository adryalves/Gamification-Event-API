using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PublicacaoImagem
{
    public Guid Id { get; set; }

    public Guid IdPublicacao { get; set; }

    public string Imagem { get; set; } = null!;

    public virtual Publicacao IdPublicacaoNavigation { get; set; } = null!;
}
