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
    public class AtualizarQuizUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public AtualizarQuizUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<bool>> AtualizarQuiz(Guid id, Quiz quiz)
        {
            var resultado = await _quizRepository.AtualizarQuiz(id, quiz);

            if (!resultado) return Resultado<bool>.Falha("Não foi encontrado um quiz com esse id");

           return Resultado<bool>.Ok(resultado);
        }
    }
}
