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
    public class GetParticipantePremioPorIdUseCase
    {
        private readonly IParticipantePremioRepository _participantePremioRepository;

        public GetParticipantePremioPorIdUseCase(IParticipantePremioRepository participantePremioRepository)
        {
            _participantePremioRepository = participantePremioRepository;
        }

        public async Task<Resultado<ParticipantePremio>> GetParticipantePremioPorId(Guid id)
        {
            var participantePremio = await _participantePremioRepository.GetParticipantePremioPorId(id);
            return Resultado<ParticipantePremio>.Ok(participantePremio);
        }
    }
}
