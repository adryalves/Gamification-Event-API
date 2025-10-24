using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PalestranteUseCases
{
    public class GetPalestrantePorIdUseCase
    {
        private readonly IPalestranteRepository _palestranteRepository;

        public GetPalestrantePorIdUseCase(IPalestranteRepository palestranteRepository)
        {
            _palestranteRepository = palestranteRepository;
        }

        public async Task<Resultado<Palestrante>> GetPalestrantePorId(Guid id)
        {
            var palestrante = await _palestranteRepository.GetPalestrantePorId(id);
            return Resultado<Palestrante>.Ok(palestrante);
        }
    }
}
