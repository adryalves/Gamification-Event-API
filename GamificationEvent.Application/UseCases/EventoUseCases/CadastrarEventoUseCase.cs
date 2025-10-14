using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
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

        public async Task<Evento> CadastrarEvento(Evento evento)
        {
            var paleta = await _paletaCorRepository.GetPaletaPorId(evento.IdPaleta);
            if(paleta == null) throw new Exception("O id da paleta não corresponde a nenhuma existente");

            evento.Id = Guid.NewGuid();
            evento.Deletado = false;

            return await _eventoRepository.AdicionarEvento(evento);
        }
    }
}
