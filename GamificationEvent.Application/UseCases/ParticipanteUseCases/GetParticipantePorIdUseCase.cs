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
    public class GetParticipantePorIdUseCase
    {
        private readonly IParticipanteRepository _participanteRepository;

        public GetParticipantePorIdUseCase(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public async Task<Resultado<Participante>> GetParticipantePorId(Guid id)
        {
            var resultado = await _participanteRepository.GetParticipantePorId(id);
            return Resultado<Participante>.Ok(resultado);

        }
    }
}
