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
    public class GetParticipantePremiosPorIdEventoUseCase
    {
        private readonly IParticipantePremioRepository _participantePremioRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetParticipantePremiosPorIdEventoUseCase(IParticipantePremioRepository participantePremioRepository, IEventoRepository eventoRepository)
        {
            _participantePremioRepository = participantePremioRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<ParticipantePremio>>> GetParticipantePremioPorIdEvento(Guid idEvento)
        {
            var eventoExistente = await _eventoRepository.GetEventoPorId(idEvento);
            if (eventoExistente == null)
                return Resultado<List<ParticipantePremio>>.Falha($"Evento com id: {idEvento} inválido");

            var participantePremio = await _participantePremioRepository.GetParticipantePremiosPorIdEvento(idEvento);
            return Resultado<List<ParticipantePremio>>.Ok(participantePremio);
            
                
        }
    }
}
