using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class QuizAlternativa
{
    public Guid Id { get; set; }

    public Guid IdQuizPergunta { get; set; }

    public string Resposta { get; set; } = null!;

    public bool ECorreta { get; set; }

    public bool Deletado { get; set; }

    public virtual QuizPergunta IdQuizPerguntaNavigation { get; set; } = null!;

    public virtual ICollection<ParticipanteQuizResposta> ParticipanteQuizResposta { get; set; } = new List<ParticipanteQuizResposta>();
}
