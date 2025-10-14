using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IEventoRepository
    {
         Task<Evento> AdicionarEvento(Evento evento);
         Task<List<Evento>> GetEventos();
         Task<Evento> GetEventoPorId(Guid id);
         Task<bool> DeletarEvento(Guid id);
         Task<bool> AtualizarEvento(Evento evento);
    }
}
