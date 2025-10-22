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
    public class GetParticipantePorCpfUseCase
    {
        private readonly IParticipanteRepository _participanteRepository;

        public GetParticipantePorCpfUseCase(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }
         
       public async Task<Resultado<Participante>> GetParticipantePorCpf(string cpf)
        {
            var cpfFormatado = cpf.Trim().Replace(".", "").Replace("-", "");
            var resultado = await _participanteRepository.GetParticipantePorCpf(cpfFormatado);
            return Resultado<Participante>.Ok(resultado);

        }
    }
}
