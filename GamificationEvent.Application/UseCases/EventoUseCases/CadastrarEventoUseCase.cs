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
    public class CadastrarEventoUseCase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly IPaletaCorRepository _paletaCorRepository;

        public CadastrarEventoUseCase(IEventoRepository eventoRepository, IPaletaCorRepository paletaCorRepository)
        {
            _eventoRepository = eventoRepository;
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<Evento>> CadastrarEvento(Evento evento)
        {
            var paleta = await _paletaCorRepository.GetPaletaPorId(evento.IdPaleta);
            if(paleta == null) return Resultado<Evento>.Falha($"Paleta com id: {evento.IdPaleta} não encontrado");

            evento.Id = Guid.NewGuid();
            evento.Deletado = false;

            var resultado = await _eventoRepository.AdicionarEvento(evento);
            return Resultado<Evento>.Ok(resultado);
        }
    }
}
