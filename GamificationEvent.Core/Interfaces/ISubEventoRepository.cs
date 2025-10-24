using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface ISubEventoRepository
    {
         Task<Guid> AdicionarSubEvento(SubEvento subEvento);
         Task<bool> AtualizarSubEvento(SubEvento subEvento);
         Task<bool> DeletarSubEvento(Guid id);
         Task<Guid> AdicionarPerguntaProSubEvento(PerguntasSubEvento perguntaSubEvento);
         Task<List<SubEvento>> GetSubEventosPorIdEvento(Guid idEvento);
         Task<SubEvento> GetSubEventoPorId(Guid id);
         Task<List<PerguntasSubEvento>> GetPerguntasPorIdSubEvento(Guid idSubEvento);
         Task<bool> PalestranteJaEstaNesseSubEvento(Guid idSubEvento, Guid idPalestrante);
    }
}
