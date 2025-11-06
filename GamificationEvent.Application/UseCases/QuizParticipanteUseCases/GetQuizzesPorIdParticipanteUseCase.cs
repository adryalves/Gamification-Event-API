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
    public class GetQuizzesPorIdParticipanteUseCase
    {
        private readonly IQuizParticipanteRepository _quizParticipanteRepository;
        private readonly IParticipanteRepository _participanteRepository;

        public GetQuizzesPorIdParticipanteUseCase(IQuizParticipanteRepository quizParticipanteRepository, IParticipanteRepository participanteRepository)
        {
            _quizParticipanteRepository = quizParticipanteRepository;
            _participanteRepository = participanteRepository;
        }

        public async Task<Resultado<List<QuizParticipante>>> GetQuizzesPorIdParticipante(Guid idParticipante)
        {
            var participante = await _participanteRepository.GetParticipantePorId(idParticipante);
            if (participante == null) return Resultado<List<QuizParticipante>>.Falha($"Esse id de participante não corresponde a nenhum participante válido");

            var resultado = await _quizParticipanteRepository.GetQuizzesPorIdParticipante(idParticipante);
            return Resultado<List<QuizParticipante>>.Ok(resultado);

        }
    }
}
