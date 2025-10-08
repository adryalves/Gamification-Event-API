using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Estande
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public Guid? IdPontoMapa { get; set; }

    public Guid? IdPatrocinador { get; set; }

    public string Nome { get; set; } = null!;

    public string? Produto { get; set; }

    public string? Categoria { get; set; }

    public string? Descricao { get; set; }

    public string? LocalEstande { get; set; }

    public DateTime DataInicio { get; set; }

    public DateTime DataFim { get; set; }

    public TimeSpan HorarioInicio { get; set; }

    public TimeSpan HorarioFim { get; set; }

    public string? Brindes { get; set; }

    public string? CodigoCheckin { get; set; }

    public bool Deletado { get; set; }

    public virtual ICollection<CheckinEstande> CheckinEstandes { get; set; } = new List<CheckinEstande>();

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual Patrocinador? IdPatrocinadorNavigation { get; set; }

    public virtual PontoMapa? IdPontoMapaNavigation { get; set; }

    public virtual ICollection<PassaporteItem> PassaporteItems { get; set; } = new List<PassaporteItem>();
}
