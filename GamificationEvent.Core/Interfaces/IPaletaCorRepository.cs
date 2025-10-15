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
         Task<bool> AtualizarCor(Cor cor);
         Task<bool> CorJaExiste(string hexCod);
         Task<PaletaCor> AdicionarPaleta(PaletaCor paleta);
         Task<List<PaletaCor>> GetPaletas();
         Task<PaletaCor> GetPaletaPorId(Guid id);
         Task<bool> AtualizarPaleta(PaletaCor paleta);
         Task<bool> DeletarPaleta(Guid id);
         Task<bool> PaletaExiste(Guid id);
         Task<bool> PaletaPertenceAEvento(Guid idPaleta);

    }
}
