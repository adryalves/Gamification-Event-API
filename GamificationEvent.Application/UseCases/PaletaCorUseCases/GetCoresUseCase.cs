using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PaletaCorUseCases
{
    public class GetCoresUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public GetCoresUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<List<Cor>> GetCores()
        {
            return await _paletaCorRepository.GetCores();
        }
    }
}
