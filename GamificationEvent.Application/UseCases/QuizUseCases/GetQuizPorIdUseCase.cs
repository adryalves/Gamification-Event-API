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
    public class GetQuizPorIdUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public GetQuizPorIdUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<Quiz>> GetQuizPorId(Guid id)
        {
            var resultado = await _quizRepository.GetQuizPorId(id);
            return Resultado<Quiz>.Ok(resultado);
        }
    }
}
