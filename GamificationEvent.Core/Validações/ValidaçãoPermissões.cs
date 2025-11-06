using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Validações
{
    public class ValidaçãoPermissões : IValidaçãoPermissões
    {
        private readonly IParticipanteRepository _participanteRepository;

        public ValidaçãoPermissões(IParticipanteRepository participanteRepository)
        {
            _participanteRepository = participanteRepository;
        }

        public async Task<bool> ParticipanteEhAdmin(ClaimsPrincipal user, Guid idEvento)
        {
            var idUsuarioClaim = user.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(idUsuarioClaim))
                return false;

            if (!Guid.TryParse(idUsuarioClaim, out var id))

                return false;

            var participante = await _participanteRepository.GetParticipantePorIdUsuarioEIdEvento(id, idEvento);

            if(participante == null) return false;  

            if (participante.Cargo == Enums.Cargo.Admin) return true;

            return false;
        }


        public async Task<bool> ParticipanteEstaNesseEvento(ClaimsPrincipal user, Guid idEvento)
        {
            var idUsuarioClaim = user.FindFirst("id")?.Value;

            if (string.IsNullOrEmpty(idUsuarioClaim))
                return false;

            if (!Guid.TryParse(idUsuarioClaim, out var id))

                return false;

            var participante = await _participanteRepository.GetParticipantePorIdUsuarioEIdEvento(id, idEvento);

            if (participante == null) return false;

            return true;
        }
    }
}
