using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class QuizPergunta
{
    public Guid Id { get; set; }

    public Guid IdQuiz { get; set; }

    public string Enunciado { get; set; } = null!;

    public bool Deletado { get; set; }

    public virtual Quiz IdQuizNavigation { get; set; } = null!;

    public virtual ICollection<ParticipanteQuizResposta> ParticipanteQuizResposta { get; set; } = new List<ParticipanteQuizResposta>();

    public virtual ICollection<QuizAlternativa> QuizAlternativas { get; set; } = new List<QuizAlternativa>();
}
