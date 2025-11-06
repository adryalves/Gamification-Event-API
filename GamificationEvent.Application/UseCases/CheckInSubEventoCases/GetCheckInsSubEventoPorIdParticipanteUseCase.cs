using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.CheckInSubEventoCases
{
    public class GetCheckInsSubEventoPorIdParticipanteUseCase
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly ICheckInSubEventoRepository _checkInSubEventoRepository;

        public GetCheckInsSubEventoPorIdParticipanteUseCase(IParticipanteRepository participanteRepository, ICheckInSubEventoRepository checkInSubEventoRepository)
        {
            _participanteRepository = participanteRepository;
            _checkInSubEventoRepository = checkInSubEventoRepository;
        }

        public async Task<Resultado<List<CheckInSubEvento>>> GetCheckInsSubEventoPorIdParticipante(Guid idParticipante)
        {
            var participante = await _participanteRepository.GetParticipantePorId(idParticipante);
            if (participante == null) return Resultado<List<CheckInSubEvento>>.Falha($"Esse id de participante não corresponde a nenhum participante válido");

            var resultado = await _checkInSubEventoRepository.GetCheckInFeitosPorIdParticipante(idParticipante);
            return Resultado<List<CheckInSubEvento>>.Ok(resultado);

        }
    }
}
