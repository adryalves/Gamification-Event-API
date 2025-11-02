using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizParticipanteUseCases
{
    public class GetParticipantesQuizPorIdQuizUseCase
    {
        private readonly IQuizParticipanteRepository _quizParticipanteRepository;
        private readonly IQuizRepository _quizRepository;

        public GetParticipantesQuizPorIdQuizUseCase(IQuizParticipanteRepository quizParticipanteRepository, IQuizRepository quizRepository)
        {
            _quizParticipanteRepository = quizParticipanteRepository;
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<List<QuizParticipante>>> GetParticipantesQuizPorIdQuiz(Guid idQuiz)
        {
            var quiz = await _quizRepository.GetQuizPorId(idQuiz);
            if (quiz == null) return Resultado<List<QuizParticipante>>.Falha("Não foi encontrado um Quiz referente a esse Id");

            var resultado = await _quizParticipanteRepository.GetParticipantesQuizPorIdQuiz(idQuiz);
            return Resultado<List<QuizParticipante>>.Ok(resultado);


        }
    }
}
