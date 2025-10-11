using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Desafio
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public string? Regra { get; set; }

    public int Pontuacao { get; set; }

    public Tipo_Desafio TipoDesafio { get; set; } 

    public int QuantidadeDesafio { get; set; }

    public DateTime? DataHoraInicio { get; set; }

    public DateTime? DataHoraFim { get; set; }

    public bool Deletado { get; set; }

    public virtual ICollection<DesafioParticipante> DesafioParticipantes { get; set; } = new List<DesafioParticipante>();

    public virtual Evento IdEventoNavigation { get; set; } = null!;
}
