using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Interesse
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual ICollection<ParticipanteInteresse> ParticipanteInteresses { get; set; } = new List<ParticipanteInteresse>();
}
