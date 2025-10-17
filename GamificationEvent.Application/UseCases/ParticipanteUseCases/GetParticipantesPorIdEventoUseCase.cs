using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
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

        public GetParticipantesPorIdEventoUseCase(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public async Task<List<Participante>> GetParticipantesPorIdEvento(Guid idEvento)
        {
            return await _participanteRepository.GetParticipantesPorIdEvento(idEvento);
        }
    }
}
