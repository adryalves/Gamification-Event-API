using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IPaletaCorRepository
    {
         Task<Cor> AdicionarCor(Cor cor);
         Task<List<Cor>> GetCores();
         Task<Cor> GetCorPorid(Guid id);
         Task<PaletaCor> AdicionarPaleta(PaletaCor paleta);
         Task<List<PaletaCor>> GetPaletas();
         Task<PaletaCor> GetPaletaPorId(Guid id);
         Task AtualizarPaleta(PaletaCor paleta);
         Task<bool> DeletarPaleta(Guid id);
    }
}
