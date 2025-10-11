using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PassaporteItem
{
    public Guid Id { get; set; }

    public Guid IdPassaporte { get; set; }

    public Guid? IdEstande { get; set; }

    public string? Categoria { get; set; }

    public bool Deletado { get; set; }

    public virtual Estande? IdEstandeNavigation { get; set; }

    public virtual Passaporte IdPassaporteNavigation { get; set; } = null!;

    public virtual ICollection<PassaporteParticipanteItem> PassaporteParticipanteItems { get; set; } = new List<PassaporteParticipanteItem>();
}
