using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
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

        public async Task<bool> AtualizarPaleta(PaletaCor paleta)
        {
            var paletaExistente = await _paletaCorRepository.GetPaletaPorId(paleta.Id);
            if (paletaExistente == null)
            {
                throw new Exception("Paleta não encontrado.");
            }

          return await _paletaCorRepository.AtualizarPaleta(paleta);
        }
    }
}
