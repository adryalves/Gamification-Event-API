using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IValidaçãoPermissões
    {
        Task<bool> ParticipanteEhAdmin(ClaimsPrincipal user, Guid idEvento);
        Task<bool> ParticipanteEstaNesseEvento(ClaimsPrincipal user, Guid idEvento);

    }
}
