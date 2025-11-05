using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizUseCases
{
    public class DeletarPerguntaQuizUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public DeletarPerguntaQuizUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<bool>> DeletarPerguntaQuiz(Guid id)
        {
            var quizPossuiresposta = await _quizRepository.QuizPossuiRespostas(id);
            if (quizPossuiresposta) return Resultado<bool>.Falha("Não é possível deletar uma pergunta quando um quiz já possui resppsta, delete as resposta do quiz para prosseguir");


            var resultado = await _quizRepository.DeletarPergunta(id);

            if (!resultado) return Resultado<bool>.Falha("Não foi encontrado uma pergunta com esse id");

            return Resultado<bool>.Ok(resultado);
        }
    }
}
