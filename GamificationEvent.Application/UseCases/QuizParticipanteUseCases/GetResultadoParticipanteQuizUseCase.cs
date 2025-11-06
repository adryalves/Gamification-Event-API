using GamificationEvent.Core.Entidades;
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
    public class GetResultadoParticipanteQuizUseCase
    {
        private readonly IQuizParticipanteRepository _quizParticipanteRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IParticipanteRepository _participanteRepository;

        public GetResultadoParticipanteQuizUseCase(IQuizParticipanteRepository quizParticipanteRepository, IQuizRepository quizRepository, IParticipanteRepository participanteRepository)
        {
            _quizParticipanteRepository = quizParticipanteRepository;
            _quizRepository = quizRepository;
            _participanteRepository = participanteRepository;
        }

        public async Task<Resultado<QuizParticipanteResultadoModel>> GetResultadoParticipanteQuiz(Guid idQuiz, Guid idParticipante)
        {
            var quiz = await _quizRepository.GetQuizPorId(idQuiz);
            if (quiz == null) return Resultado<QuizParticipanteResultadoModel>.Falha("Não foi encontrado um Quiz referente a esse Id");

            var participante = await _participanteRepository.GetParticipantePorId(idParticipante);
            if (participante == null) return Resultado<QuizParticipanteResultadoModel>.Falha($"Esse id de participante não corresponde a nenhum participante válido");


            var resultado = await _quizParticipanteRepository.GetResultadoParticipanteQuiz(idQuiz, idParticipante);
            return Resultado<QuizParticipanteResultadoModel>.Ok(resultado);
        }
    }
}
