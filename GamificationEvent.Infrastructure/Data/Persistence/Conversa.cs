using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Conversa
{
    public Guid Id { get; set; }

    public Guid IdParticipante1 { get; set; }

    public Guid IdParticipante2 { get; set; }

    public DateTime DataHoraInicio { get; set; }

    public virtual Participante IdParticipante1Navigation { get; set; } = null!;

    public virtual Participante IdParticipante2Navigation { get; set; } = null!;

    public virtual ICollection<Mensagem> Mensagems { get; set; } = new List<Mensagem>();
}
