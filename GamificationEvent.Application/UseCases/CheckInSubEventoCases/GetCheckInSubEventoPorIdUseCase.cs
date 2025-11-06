using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.CheckInSubEventoCases
{
    public class GetCheckInSubEventoPorIdUseCase
    {
        private readonly ICheckInSubEventoRepository _checkInSubEventoRepository;

        public GetCheckInSubEventoPorIdUseCase(ICheckInSubEventoRepository checkInSubEventoRepository)
        {
            _checkInSubEventoRepository = checkInSubEventoRepository;
        }

        public async Task<Resultado<CheckInSubEvento>> GetCheckInSubEventoPorId(Guid id)
        {
            var resultado = await _checkInSubEventoRepository.GetCheckInPorId(id);
            return Resultado<CheckInSubEvento>.Ok(resultado);
        }
    }
}
