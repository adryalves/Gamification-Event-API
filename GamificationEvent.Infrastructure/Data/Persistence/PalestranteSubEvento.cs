using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PalestranteSubEvento
{
    public Guid Id { get; set; }

    public Guid IdSubEvento { get; set; }

    public Guid IdPalestrante { get; set; }

    public virtual Palestrante IdPalestranteNavigation { get; set; } = null!;

    public virtual SubEvento IdSubEventoNavigation { get; set; } = null!;
}
