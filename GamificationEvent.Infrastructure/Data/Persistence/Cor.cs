using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Cor
{
    public Guid Id { get; set; }

    public string HexCodigo { get; set; } = null!;

    public string? Nome { get; set; }

    public virtual ICollection<PaletaCor> PaletaCorIdCor1Navigations { get; set; } = new List<PaletaCor>();

    public virtual ICollection<PaletaCor> PaletaCorIdCor2Navigations { get; set; } = new List<PaletaCor>();

    public virtual ICollection<PaletaCor> PaletaCorIdCor3Navigations { get; set; } = new List<PaletaCor>();

    public virtual ICollection<PaletaCor> PaletaCorIdCor4Navigations { get; set; } = new List<PaletaCor>();
}
