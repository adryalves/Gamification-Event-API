using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Passaporte
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public string? Titulo { get; set; }

    public string? Descricao { get; set; }

    public bool? Ativo { get; set; }

    public int Pontuacao { get; set; }

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual ICollection<PassaporteItem> PassaporteItems { get; set; } = new List<PassaporteItem>();

    public virtual ICollection<PassaporteParticipante> PassaporteParticipantes { get; set; } = new List<PassaporteParticipante>();
}
