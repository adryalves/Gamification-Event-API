using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizUseCases
{
    public class AdicionarPerguntaQuizUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public AdicionarPerguntaQuizUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<Guid>> AdicionarPerguntaQuiz(QuizPergunta quizPergunta)
        {
            var quiz = await _quizRepository.GetQuizPorId(quizPergunta.IdQuiz);
            if (quiz == null) return Resultado<Guid>.Falha("Não foi encontrado um Quiz referente a esse Id");

            var resultado = await _quizRepository.AdicionarPergunta(quizPergunta);
            return Resultado<Guid>.Ok(resultado);
        }
    }
}
