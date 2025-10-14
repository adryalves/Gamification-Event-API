using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
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

        public async Task<PaletaCor> GetPaletaPorId(Guid id)
        {
            return await _paletaCorRepository.GetPaletaPorId(id);
        }
    }
}
