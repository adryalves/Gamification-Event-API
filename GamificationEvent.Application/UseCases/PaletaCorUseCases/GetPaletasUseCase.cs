using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PaletaCorUseCases
{
    public class GetPaletasUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public GetPaletasUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<List<PaletaCor>> GetPaletas()
        {
            return await _paletaCorRepository.GetPaletas();
        }
    }
}
