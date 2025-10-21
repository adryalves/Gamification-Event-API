using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
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

        public async Task<Resultado<bool>> DeletarEvento(Guid id)
        {
            var evento = await _eventoRepository.GetEventoPorId(id);
            if(evento == null) return Resultado<bool>.Falha($"Evento com id: {id} não encontrado");


            var resultado =  await _eventoRepository.DeletarEvento(id);

           return Resultado<bool>.Ok(resultado);
        }
    }
}
