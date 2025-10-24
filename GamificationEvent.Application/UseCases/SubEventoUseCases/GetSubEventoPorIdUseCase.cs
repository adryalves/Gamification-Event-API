using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.SubEventoUseCases
{
    public class GetSubEventoPorIdUseCase
    {
        private readonly ISubEventoRepository _subEventoRepository;

        public GetSubEventoPorIdUseCase(ISubEventoRepository subEventoRepository)
        {
            _subEventoRepository = subEventoRepository;
        }

        public async Task<Resultado<SubEvento>> GetSubEventoPorId(Guid id)
        {
            var subEvento = await _subEventoRepository.GetSubEventoPorId(id);
            return Resultado<SubEvento>.Ok(subEvento);
        }
    }
}
