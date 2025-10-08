using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class PassaporteParticipanteItem
{
    public Guid Id { get; set; }

    public Guid IdPassaporteItem { get; set; }

    public Guid IdParticipante { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? DataHoraConclusao { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual PassaporteItem IdPassaporteItemNavigation { get; set; } = null!;
}
