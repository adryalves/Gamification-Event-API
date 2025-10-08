using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Cor
{
    public Guid Id { get; set; }

    public string HexCodigo { get; set; } = null!;

    public string? Nome { get; set; }

    public virtual ICollection<Paletum> PaletumIdCor1Navigations { get; set; } = new List<Paletum>();

    public virtual ICollection<Paletum> PaletumIdCor2Navigations { get; set; } = new List<Paletum>();

    public virtual ICollection<Paletum> PaletumIdCor3Navigations { get; set; } = new List<Paletum>();

    public virtual ICollection<Paletum> PaletumIdCor4Navigations { get; set; } = new List<Paletum>();
}
