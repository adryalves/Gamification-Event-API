using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PaletaCorUseCases
{
    public class DeletarPaletaUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public DeletarPaletaUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<bool> DeletarPaleta(Guid id)
        {
            var paletaExistente = await _paletaCorRepository.GetPaletaPorId(id);

            if (paletaExistente == null)
            {
                throw new Exception("Paleta não encontrado.");
            }

            return await _paletaCorRepository.DeletarPaleta(id);
        }
    }
}
