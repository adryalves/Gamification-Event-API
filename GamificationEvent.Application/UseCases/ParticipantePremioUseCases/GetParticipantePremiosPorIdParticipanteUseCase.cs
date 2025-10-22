using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.ParticipantePremioUseCases
{
    public class GetParticipantePremiosPorIdParticipanteUseCase
    {
        private readonly IParticipantePremioRepository _participantePremioRepository;
        private readonly IParticipanteRepository _participanteRepository;

        public GetParticipantePremiosPorIdParticipanteUseCase(IParticipantePremioRepository participantePremioRepository, IParticipanteRepository participanteRepository)
        {
            _participantePremioRepository = participantePremioRepository;
            _participanteRepository = participanteRepository;
        }

        public async Task<Resultado<List<ParticipantePremio>>> GetParticipantePremiosPorIdParticipante(Guid idParticipante)
        {
            var participante = await _participanteRepository.GetParticipantePorId(idParticipante);
            if (participante == null) return Resultado<List<ParticipantePremio>>.Falha($"Esse id de participante não corresponde a nenhum participante válido");

            var resultado = await _participantePremioRepository.GetParticipantePremiosPorIdParticipante(idParticipante);
            return Resultado<List<ParticipantePremio>>.Ok(resultado);

        }
    }
}
