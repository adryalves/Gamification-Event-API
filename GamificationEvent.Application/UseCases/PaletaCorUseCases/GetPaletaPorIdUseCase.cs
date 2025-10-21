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
    public class GetPaletaPorIdUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public GetPaletaPorIdUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<PaletaCor>> GetPaletaPorId(Guid id)
        {
            var resultado = await _paletaCorRepository.GetPaletaPorId(id);
            return Resultado<PaletaCor>.Ok(resultado);

        }
    }
}
