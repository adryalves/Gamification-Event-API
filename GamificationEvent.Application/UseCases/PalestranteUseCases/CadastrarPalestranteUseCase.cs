using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PalestranteUseCases
{
    public class CadastrarPalestranteUseCase
    {
        private readonly IPalestranteRepository _palestranteRepository;
        private readonly IEventoRepository _eventoRepository;

        public CadastrarPalestranteUseCase(IPalestranteRepository palestranteRepository, IEventoRepository eventoRepository)
        {
            _palestranteRepository = palestranteRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<Guid>> CadastrarPalestrante(Palestrante palestrante)
        {
            var evento = await _eventoRepository.GetEventoPorId(palestrante.IdEvento);
            if (evento == null) return Resultado<Guid>.Falha($"O id {palestrante.IdEvento} não corresponde a um evento existente");

           
            var resultado = await _palestranteRepository.AdicionarPalestrante(palestrante);
            return Resultado<Guid>.Ok(resultado);

        }
    }
}
