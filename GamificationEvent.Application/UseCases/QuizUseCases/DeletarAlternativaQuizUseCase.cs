using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizUseCases
{
    public class DeletarAlternativaQuizUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public DeletarAlternativaQuizUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<bool>> DeletarAlternativaQuiz(Guid id)
        {
            var resultado = await _quizRepository.DeletarAlternativa(id);

            if (!resultado) return Resultado<bool>.Falha("Não foi encontrado uma alternativa com esse Id");

            return Resultado<bool>.Ok(resultado);
        }
    }
}
