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
    public class AtualizarQuizPerguntaUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public AtualizarQuizPerguntaUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<bool>> AtualizarQuizPergunta(Guid id, QuizPergunta quizPergunta)
        {
            var resultado = await _quizRepository.AtualizarPergunta(id, quizPergunta);

            if (!resultado) return Resultado<bool>.Falha("Não foi encontrado uma pergunta com esse id");

            return Resultado<bool>.Ok(resultado);
        }
    }
}
