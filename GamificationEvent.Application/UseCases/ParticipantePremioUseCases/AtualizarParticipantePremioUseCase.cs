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
    public class AtualizarParticipantePremioUseCase
    {
        private readonly IParticipantePremioRepository _participantePremioRepository;

        public AtualizarParticipantePremioUseCase(IParticipantePremioRepository participantePremioRepository)
        {
            _participantePremioRepository = participantePremioRepository;
        }

        public async Task<Resultado<bool>> AtualizarParticipantePremio(Guid id, ParticipantePremio participantePremio)
        {
            var participantePremioExistente = await _participantePremioRepository.GetParticipantePremioPorId(id);
            if (participantePremioExistente == null) return Resultado<bool>.Falha("Participante Premio não encontrado");

            participantePremio.Id = id;
            var resultado = await _participantePremioRepository.AtualizarParticipantePremio(participantePremio);

            if (resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na atualização");
        }

    }
}
