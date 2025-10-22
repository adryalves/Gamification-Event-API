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
    public class GetParticipantesPremioPorIdPremioUseCase
    {
        private readonly IParticipantePremioRepository _participantePremioRepository;
        private readonly IPremioRepository _premioRepository;

        public GetParticipantesPremioPorIdPremioUseCase(IParticipantePremioRepository participantePremioRepository, IPremioRepository premioRepository)
        {
            _participantePremioRepository = participantePremioRepository;
            _premioRepository = premioRepository;
        }

        public async Task<Resultado<List<ParticipantePremio>>> GetParticipantesPremioPorIdPremio(Guid idPremio)
        {
            var premio = await _premioRepository.GetPremioPorid(idPremio);
            if (premio == null) return Resultado<List<ParticipantePremio>>.Falha("Esse id de premio não corresponde a nenhum premio válido");

            var participantesPremio = await _participantePremioRepository.GetParticipantesPremioPorIdPremio(idPremio);
            return Resultado<List<ParticipantePremio>>.Ok(participantesPremio);


        }
    }
}
