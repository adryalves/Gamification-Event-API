using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizUseCases
{
    public class DeletarQuizUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public DeletarQuizUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<bool>> DeletarQuiz(Guid id)
        {
            var resultado = await _quizRepository.DeletarQuiz(id);
            if (!resultado) return Resultado<bool>.Falha("Não foi encontrado um Quiz com esse ID para ser deletado");

            return Resultado<bool>.Ok(resultado);
        }
    }
}
