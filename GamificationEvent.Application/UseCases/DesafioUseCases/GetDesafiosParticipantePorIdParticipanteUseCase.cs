using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.DesafioUseCases
{
    public class GetDesafiosParticipantePorIdParticipanteUseCase
    {
        private readonly IDesafioRepository _desafioRepository;
        private readonly IParticipanteRepository _participanteRepository;

        public GetDesafiosParticipantePorIdParticipanteUseCase(IDesafioRepository desafioRepository, IParticipanteRepository participanteRepository)
        {
            _desafioRepository = desafioRepository;
            _participanteRepository = participanteRepository;
        }

        public async Task<Resultado<List<DesafioParticipante>>> GetDesafiosPaticipantePorIdParticipante(Guid idParticipante)
        {

            var participante = await _participanteRepository.GetParticipantePorId(idParticipante);
            if (participante == null) return Resultado<List<DesafioParticipante>>.Falha($"Esse id de participante não corresponde a nenhum participante válido");

            var desafiosParticipante = await _desafioRepository.GetDesafiosParticipantePorIdParticipante(idParticipante);
            return Resultado<List<DesafioParticipante>>.Ok(desafiosParticipante);

        }
    }
}
