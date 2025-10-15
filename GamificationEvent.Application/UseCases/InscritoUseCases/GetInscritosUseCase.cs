using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InscritoUseCases
{
    public class GetInscritosUseCase
    {
        private readonly IInscritoRepository _inscritoRepository;

        public GetInscritosUseCase(IInscritoRepository inscritoRepository)
        {
            _inscritoRepository = inscritoRepository;
        }

        public async Task<List<Inscrito>> GetInscritos()
        {
            return await _inscritoRepository.GetInscritos();
        }
    }
}
