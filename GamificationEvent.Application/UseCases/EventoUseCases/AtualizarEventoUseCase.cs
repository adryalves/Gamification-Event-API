using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.EventoUseCases
{
    public class AtualizarEventoUseCase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IPaletaCorRepository _paletaCorRepository;

        public AtualizarEventoUseCase(IEventoRepository eventoRepository, IPaletaCorRepository paletaCorRepository)
        {
            _eventoRepository = eventoRepository;
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<bool> AtualizarEvento(Evento evento)
        {
            var eventoExistente = await _eventoRepository.GetEventoPorId(evento.Id);
            if(eventoExistente == null)
                throw new Exception("Evento não encontrado.");

            var paleta = await _paletaCorRepository.GetPaletaPorId(evento.IdPaleta);
            if (paleta == null) throw new Exception("O id da paleta não corresponde a nenhuma existente");


            return await _eventoRepository.AtualizarEvento(evento);

        }
    }
}
