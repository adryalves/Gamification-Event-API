using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.ParticipanteUseCases
{
    public class GetParticipantesPorIdEventoUseCase
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetParticipantesPorIdEventoUseCase(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<Participante>>> GetParticipantesPorIdEvento(Guid idEvento)
        {
            var eventoExistente = await _eventoRepository.GetEventoPorId(idEvento);
            if (eventoExistente == null)
                return Resultado<List<Participante>>.Falha($"Evento com id: {idEvento} não encontrado");

            var resultado = await _participanteRepository.GetParticipantesPorIdEvento(idEvento);
            return Resultado<List<Participante>>.Ok(resultado);

        }
    }
}
