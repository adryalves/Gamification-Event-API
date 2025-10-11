using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Palestrante
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public string? Email { get; set; }

    public string? Telefone { get; set; }

    public string? Profissao { get; set; }

    public DateOnly? DataNascimento { get; set; }

    public string? Linkedin { get; set; }

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual ICollection<PalestranteSubEvento> PalestranteSubEventos { get; set; } = new List<PalestranteSubEvento>();
}
