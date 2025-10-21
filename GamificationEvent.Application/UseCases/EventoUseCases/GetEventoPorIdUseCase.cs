using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.EventoUseCases
{
    public class GetEventoPorIdUseCase
    {
        private readonly IEventoRepository _eventoRepository;

        public GetEventoPorIdUseCase(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<Evento>> GetEventoPorId(Guid id)
        {
            var resultado = await _eventoRepository.GetEventoPorId(id);
           return Resultado<Evento>.Ok(resultado);
           

        }
    }
}
