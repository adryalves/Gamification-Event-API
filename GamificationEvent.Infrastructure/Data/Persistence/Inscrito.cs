using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Inscrito
{
    public string Cpf { get; set; } = null!;

    public Guid IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public Cargo Cargo { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;
}
