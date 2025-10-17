using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.ParticipanteUseCases
{
    public class GetParticipantePorIdUseCase
    {
        private readonly IParticipanteRepository _participanteRepository;

        public GetParticipantePorIdUseCase(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public async Task<Participante> GetParticipantePorId(Guid id)
        {
            return await _participanteRepository.GetParticipantePorId(id);
        }
    }
}
