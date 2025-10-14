using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.EventoUseCases
{
    public class DeletarEventoUseCase
    {
        private readonly IEventoRepository _eventoRepository;

        public DeletarEventoUseCase(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<bool> DeletarEvento(Guid id)
        {
            var evento = await _eventoRepository.GetEventoPorId(id);
            if(evento == null) throw new Exception("Evento não encontrado.");

            return await _eventoRepository.DeletarEvento(id);
        }
    }
}
