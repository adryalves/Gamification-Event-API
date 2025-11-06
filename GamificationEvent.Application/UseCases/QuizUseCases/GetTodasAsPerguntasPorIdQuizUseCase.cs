using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Models;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizUseCases
{
    public class GetTodasAsPerguntasPorIdQuizUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public GetTodasAsPerguntasPorIdQuizUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<QuizPerguntasEAlternativasModel>> GetTodasAsPerguntasPorIdQuiz(Guid idQuiz)
        {
            var quiz = await _quizRepository.GetQuizPorId(idQuiz);
            if (quiz == null) return Resultado<QuizPerguntasEAlternativasModel>.Falha("Não foi encontrado um Quiz referente a esse Id");

            var resultado = await _quizRepository.GetTodasAsPerguntasPorIdQuiz(idQuiz);

            return Resultado<QuizPerguntasEAlternativasModel>.Ok(resultado);

        }
    }
}
