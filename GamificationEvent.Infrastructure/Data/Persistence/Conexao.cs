using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Conexao
{
    public Guid Id { get; set; }

    public Guid IdParticipante1 { get; set; }

    public Guid IdParticipante2 { get; set; }

    public Status_Conexao StatusConexao { get; set; } 

    public DateTime DataHora { get; set; }

    public virtual Participante IdParticipante1Navigation { get; set; } = null!;

    public virtual Participante IdParticipante2Navigation { get; set; } = null!;
}
