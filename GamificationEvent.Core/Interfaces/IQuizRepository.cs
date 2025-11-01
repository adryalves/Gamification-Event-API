using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IQuizRepository
    {
        Task<Guid> AdicionarQuiz(Quiz quiz);
        Task<Guid> AdicionarPergunta(QuizPergunta pergunta);
        Task<List<QuizAlternativa>> AdicionarAlternativas(List<QuizAlternativa> alternativas);
        Task<List<Quiz>> GetQuizzesPorIdEvento(Guid idEvento);
        Task<QuizPerguntasEAlternativas> GetTodasAsPerguntasPorIdQuiz(Guid idQuiz);
        Task<bool> AtualizarQuiz(Guid id, Quiz quiz);
        Task<bool> DeletarQuiz(Guid id);
        Task<bool> AtualizarPergunta(Guid idPergunta, QuizPergunta pergunta);
        Task<bool> DeletarPergunta(Guid idPergunta);
        Task<bool> DeletarAlternativa(Guid idAlternativa);
        Task<Quiz> GetQuizPorId(Guid id);
        Task<QuizPergunta> GetPerguntaPorId(Guid idPergunta);


    }
}
