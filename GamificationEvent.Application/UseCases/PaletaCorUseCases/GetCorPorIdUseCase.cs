using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PaletaCorUseCases
{
    public class GetCorPorIdUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public GetCorPorIdUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Cor> GetCorPorId(Guid id)
        {
            return await _paletaCorRepository.GetCorPorid(id);
        }
    }
}
