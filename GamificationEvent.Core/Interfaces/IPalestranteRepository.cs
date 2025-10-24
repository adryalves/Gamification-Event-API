using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IPalestranteRepository
    {
         Task<Guid> AdicionarPalestrante(Palestrante palestrante);
         Task<bool> AtualizarPalestrante(Palestrante palestrante);
         Task<bool> DeletarPalestrante(Guid id);
         Task<List<Palestrante>> GetPalestrantesPorIdEvento(Guid idEvento);
         Task<Palestrante> GetPalestrantePorId(Guid id);
         Task<List<Palestrante>> GetPalestrantesPorIdSubEvento(Guid idSubEvento);
    }
}
