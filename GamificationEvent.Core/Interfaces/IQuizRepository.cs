using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Models;
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
        Task<QuizPerguntasEAlternativasModel> GetTodasAsPerguntasPorIdQuiz(Guid idQuiz);
        Task<bool> AtualizarQuiz(Guid id, Quiz quiz);
        Task<bool> DeletarQuiz(Guid id);
        Task<bool> AtualizarPergunta(Guid idPergunta, QuizPergunta pergunta);
        Task<bool> DeletarPergunta(Guid idPergunta);
        Task<bool> DeletarAlternativa(Guid idAlternativa);
        Task<Quiz> GetQuizPorId(Guid id);
        Task<QuizPergunta> GetPerguntaPorId(Guid idPergunta);
        Task<QuizAlternativa> GetAlternativaPoId(Guid idAlternativa, Guid idQuizPergunta);


    }
}
