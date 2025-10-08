using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Inscrito
{
    public string Cpf { get; set; } = null!;

    public Guid IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public virtual Evento IdEventoNavigation { get; set; } = null!;
}
