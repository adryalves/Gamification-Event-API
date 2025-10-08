using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Premio
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public Guid? IdPatrocinador { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Tipo { get; set; }

    public string? InfoResgate { get; set; }

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual Patrocinador? IdPatrocinadorNavigation { get; set; }

    public virtual ICollection<ParticipantePremio> ParticipantePremios { get; set; } = new List<ParticipantePremio>();
}
