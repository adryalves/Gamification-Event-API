using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PatrocinadorSubEvento
{
    public Guid Id { get; set; }

    public Guid IdPatrocinador { get; set; }

    public Guid IdSubEvento { get; set; }

    public virtual Patrocinador IdPatrocinadorNavigation { get; set; } = null!;

    public virtual SubEvento IdSubEventoNavigation { get; set; } = null!;
}
