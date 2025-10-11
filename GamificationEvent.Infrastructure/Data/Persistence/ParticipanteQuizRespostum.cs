using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class ParticipanteQuizRespostum
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdQuizPergunta { get; set; }

    public Guid IdQuizAlternativa { get; set; }

    public TimeOnly HoraResposta { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual QuizAlternativa IdQuizAlternativaNavigation { get; set; } = null!;

    public virtual QuizPerguntum IdQuizPerguntaNavigation { get; set; } = null!;
}
