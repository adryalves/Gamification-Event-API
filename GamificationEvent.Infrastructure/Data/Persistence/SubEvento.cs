using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class SubEvento
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public Guid? IdPontoMapa { get; set; }

    public string Nome { get; set; } = null!;

    public string? LocalSubEvento { get; set; }

    public string? Assunto { get; set; }

    public string? Tipo { get; set; }

    public string? Categoria { get; set; }

    public string Modalidade { get; set; } = null!;

    public DateTime DataSubEvento { get; set; }

    public TimeSpan HorarioInicio { get; set; }

    public TimeSpan? HorarioFim { get; set; }

    public string? CodigoCheckin { get; set; }

    public bool Deletado { get; set; }

    public virtual ICollection<CheckinSubEvento> CheckinSubEventos { get; set; } = new List<CheckinSubEvento>();

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual PontoMapa? IdPontoMapaNavigation { get; set; }

    public virtual ICollection<PalestranteSubEvento> PalestranteSubEventos { get; set; } = new List<PalestranteSubEvento>();

    public virtual ICollection<PatrocinadorSubEvento> PatrocinadorSubEventos { get; set; } = new List<PatrocinadorSubEvento>();

    public virtual ICollection<PerguntasSubEvento> PerguntasSubEventos { get; set; } = new List<PerguntasSubEvento>();

    public virtual ICollection<Pesquisa> Pesquisas { get; set; } = new List<Pesquisa>();

    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
}
