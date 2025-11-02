using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Models;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizParticipanteUseCases
{
    public class GetQuizRankingUseCase
    {
        private readonly IQuizParticipanteRepository _quizParticipanteRepository;
        private readonly IQuizRepository _quizRepository;

        public GetQuizRankingUseCase(IQuizParticipanteRepository quizParticipanteRepository, IQuizRepository quizRepository)
        {
            _quizParticipanteRepository = quizParticipanteRepository;
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<QuizRankingModel>> GetQuizRanking(Guid idQuiz, Guid? idParticipante = null, int top = 10)
        {
            var quiz = await _quizRepository.GetQuizPorId(idQuiz);
            if (quiz == null) return Resultado<QuizRankingModel>.Falha("Não foi encontrado um Quiz referente a esse Id");

            var resultado = await _quizParticipanteRepository.GetQuizRanking(idQuiz,idParticipante, top);
            return Resultado<QuizRankingModel>.Ok(resultado);
        }
    }
}
