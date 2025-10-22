using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PremioUseCases
{
    public class GetPremioPorIdUseCase
    {
        private readonly IPremioRepository _premioRepository;

        public GetPremioPorIdUseCase(IPremioRepository premioRepository)
        {
            _premioRepository = premioRepository;
        }

        public async Task<Resultado<Premio>> GetPremioPorId(Guid id)
        {
            var premio = await _premioRepository.GetPremioPorid(id);
            return Resultado<Premio>.Ok(premio);
        }
    }
}
