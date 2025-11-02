
using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Models;

namespace GamificationEvent.Core.Interfaces
{
    public interface IQuizParticipanteRepository 
    {
        Task<Guid> AdicionarQuizParticipante(QuizParticipante participanteQuiz);
        Task<Guid> AdicionarParticipanteQuizResposta(ParticipanteQuizResposta participanteQuizResposta);
        Task<List<QuizParticipante>> GetQuizzesPorIdParticipante(Guid idParticipante);
        Task<List<QuizParticipante>> GetParticipantesQuizPorIdQuiz(Guid idQuiz);
        Task<QuizParticipanteResultadoModel> GetResultadoParticipanteQuiz(Guid idQuiz, Guid idParticipante);
        Task<QuizRankingModel> GetQuizRanking(Guid idQuiz, Guid? idParticipante = null, int take = 10);



    }
}