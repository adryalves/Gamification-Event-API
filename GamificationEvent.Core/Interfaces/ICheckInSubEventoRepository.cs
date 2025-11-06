using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface ICheckInSubEventoRepository
    {
        Task<Guid> AdicionarCheckIn(CheckInSubEvento checkIn);
        Task<List<CheckInSubEvento>> GetCheckInFeitosPorIdParticipante(Guid idParticipante);
        Task<List<CheckInSubEvento>> GetCheckInFeitosPorIdSubEvento(Guid idSubEvento);
        Task<CheckInSubEvento> GetCheckInPorId(Guid id);
        Task<bool> ParticipanteRealizouCheckIn(Guid idSubEvento, Guid idParticipante)
;
    }
}
