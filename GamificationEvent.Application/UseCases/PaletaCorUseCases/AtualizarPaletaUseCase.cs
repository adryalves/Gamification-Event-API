using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PaletaCorUseCases
{
    public class AtualizarPaletaUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public AtualizarPaletaUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<bool>> AtualizarPaleta(PaletaCor paleta)
        {
            var paletaExistente = await _paletaCorRepository.GetPaletaPorId(paleta.Id);
            if (paletaExistente == null) return Resultado<bool>.Falha($"Paleta com id: {paleta.Id} não encontrado");

            var resultado = await _paletaCorRepository.AtualizarPaleta(paleta);
            return Resultado<bool>.Ok(resultado);

        }
    }
}
