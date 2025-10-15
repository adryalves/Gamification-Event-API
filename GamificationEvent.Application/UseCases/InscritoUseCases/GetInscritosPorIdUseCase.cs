using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InscritoUseCases
{
    public class GetInscritosPorIdUseCase
    {
        private readonly IInscritoRepository _inscritoRepository;

        public GetInscritosPorIdUseCase(IInscritoRepository inscritoRepository)
        {
            _inscritoRepository = inscritoRepository;
        }

        public async Task<List<Inscrito>> GetInscritosPorIdEvento(Guid idEvento)
        {
            return await _inscritoRepository.GetInscritosPorIdEvento(idEvento);
        }
    }
}
