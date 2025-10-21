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
    public class GetPaletasUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public GetPaletasUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<List<PaletaCor>>> GetPaletas()
        {
            var resultado = await _paletaCorRepository.GetPaletas();
            return Resultado<List<PaletaCor>>.Ok(resultado);

        }
    }
}
