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
    public class AtualizarEventoUseCase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IPaletaCorRepository _paletaCorRepository;

        public AtualizarEventoUseCase(IEventoRepository eventoRepository, IPaletaCorRepository paletaCorRepository)
        {
            _eventoRepository = eventoRepository;
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<bool>> AtualizarEvento(Evento evento)
        {
            var eventoExistente = await _eventoRepository.GetEventoPorId(evento.Id);
            if(eventoExistente == null)
                return Resultado<bool>.Falha($"Evento com id: {evento.Id} não encontrado");

            var paleta = await _paletaCorRepository.GetPaletaPorId(evento.IdPaleta);
            if(paleta == null)
            return Resultado<bool>.Falha($"Paleta com id: {evento.IdPaleta} não encontrado");


            var resultado = await _eventoRepository.AtualizarEvento(evento);
            return Resultado<bool>.Ok(resultado);

        }
    }
}
