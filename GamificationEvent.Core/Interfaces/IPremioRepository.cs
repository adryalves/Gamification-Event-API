using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IPremioRepository
    {
         Task<Guid> AdicionarPremio(Premio premio);
         Task<List<Premio>> GetPremiosPorIdEvento(Guid idEvento);
         Task<Premio> GetPremioPorid(Guid id);
         Task<bool> AtualizarPremio(Premio premio);
         Task<bool> DeletarPremio(Guid id);
    }
}
