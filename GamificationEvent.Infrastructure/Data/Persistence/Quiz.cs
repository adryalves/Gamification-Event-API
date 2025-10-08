using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Quiz
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public Guid? IdSubEvento { get; set; }

    public Guid? IdPatrocinador { get; set; }

    public string Nome { get; set; } = null!;

    public string? Tema { get; set; }

    public DateTime DataQuiz { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFim { get; set; }

    public bool Deletado { get; set; }

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual Patrocinador? IdPatrocinadorNavigation { get; set; }

    public virtual SubEvento? IdSubEventoNavigation { get; set; }

    public virtual ICollection<QuizParticipante> QuizParticipantes { get; set; } = new List<QuizParticipante>();

    public virtual ICollection<QuizPerguntum> QuizPergunta { get; set; } = new List<QuizPerguntum>();
}
