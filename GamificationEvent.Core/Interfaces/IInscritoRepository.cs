using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IInscritoRepository
    {
         Task<int> AdicionarTodosOsInscrito(List<Inscrito> inscritosCore);
         Task<Inscrito> AdicionarInscrito(Inscrito inscritoCore);
        Task<bool> DeletarInscrito(string cpf, Guid idEvento);
         Task<List<Inscrito>> GetInscritos();
         Task<List<Inscrito>> GetInscritosPorIdEvento(Guid idEvento);
         Task<Inscrito> JaExisteEsseInscrito(string cpf, Guid idEvento);
    }
}
