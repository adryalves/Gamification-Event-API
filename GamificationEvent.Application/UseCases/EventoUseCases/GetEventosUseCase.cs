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
    public class GetEventosUseCase
    {
        private readonly IEventoRepository _eventoRepository;

        public GetEventosUseCase(IEventoRepository eventoRepository)
        {
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<Evento>>> GetEventos()
        {
            
            var resultado = await _eventoRepository.GetEventos();
            return Resultado<List<Evento>>.Ok(resultado);

        }
    }
}
