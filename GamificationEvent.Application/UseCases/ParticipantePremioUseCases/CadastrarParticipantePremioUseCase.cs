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
    public class CadastrarParticipantePremioUseCase
    {
        private readonly IParticipantePremioRepository _participantePremioRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IPremioRepository _premioRepository;


        public CadastrarParticipantePremioUseCase(IParticipantePremioRepository participantePremioRepository, IParticipanteRepository participanteRepository, IPremioRepository premioRepository)
        {
            _participantePremioRepository = participantePremioRepository;
            _participanteRepository = participanteRepository;
            _premioRepository = premioRepository;
        }

        public async Task<Resultado<Guid>> CadastrarParticipantePremio(ParticipantePremio participantePremio)
        {
            var participante = await _participanteRepository.GetParticipantePorId(participantePremio.IdParticipante);
            if (participante == null) return Resultado<Guid>.Falha($"Esse id de participante não corresponde a nenhum participante válido");
                
            var premio = await _premioRepository.GetPremioPorid(participantePremio.IdPremio);
            if (premio == null) return Resultado<Guid>.Falha("Esse id de premio não corresponde a nenhum premio válido");

            if (participante.IdEvento != premio.IdEvento) return Resultado<Guid>.Falha("Para atribuir um premio a um participante, eles precisam pertence ao mesmo evento");

            var resultado =  await _participantePremioRepository.AdicionarParticipantePremio(participantePremio);
            return Resultado<Guid>.Ok(resultado);     
        
        }
    }
}
