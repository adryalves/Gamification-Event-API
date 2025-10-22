using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IParticipantePremioRepository 
    {
        Task<Guid> AdicionarParticipantePremio(ParticipantePremio participantePremiumCore);
        Task<List<ParticipantePremio>> GetParticipantePremiosPorIdEvento(Guid idEvento);
        Task<List<ParticipantePremio>> GetParticipantePremiosPorIdParticipante(Guid idParticipante);
         Task<List<ParticipantePremio>> GetParticipantesPremioPorIdPremio(Guid idPremio);
        Task<ParticipantePremio> GetParticipantePremioPorId(Guid id);
        Task<bool> AtualizarParticipantePremio(ParticipantePremio premio);
    }
}
