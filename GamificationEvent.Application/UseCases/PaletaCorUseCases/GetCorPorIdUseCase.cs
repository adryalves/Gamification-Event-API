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
    public class GetCorPorIdUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public GetCorPorIdUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<Cor>> GetCorPorId(Guid id)
        {
            var resultado = await _paletaCorRepository.GetCorPorid(id);
            return Resultado<Cor>.Ok(resultado);

        }
    }
}
