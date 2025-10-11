using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class QuizParticipante
{
    public Guid Id { get; set; }

    public Guid IdParticipante { get; set; }

    public Guid IdQuiz { get; set; }

    public virtual Participante IdParticipanteNavigation { get; set; } = null!;

    public virtual Quiz IdQuizNavigation { get; set; } = null!;
}
