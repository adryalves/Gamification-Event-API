using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IInteresseRepository
    {
         Task<int> AdicionarInteresses(List<Interesse> interesses);
         Task<bool> DeletarInteresse(Guid id);
         Task<List<Interesse>> GetInteressesPorIdEvento(Guid idEvento);
         Task<Interesse> GetInteressePorId(Guid id);
    }
}
