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
    public class GetCheckInsSubEventoPorIdSubEventoUseCase
    {
        private readonly ISubEventoRepository _subEventoRepository;
        private readonly ICheckInSubEventoRepository _checkInSubEventoRepository;

        public GetCheckInsSubEventoPorIdSubEventoUseCase(ISubEventoRepository subEventoRepository, ICheckInSubEventoRepository checkInSubEventoRepository)
        {
            _subEventoRepository = subEventoRepository;
            _checkInSubEventoRepository = checkInSubEventoRepository;
        }

        public async Task<Resultado<List<CheckInSubEvento>>> GetCheckInsSubEventoPorIdSubEvento(Guid idSubEvento)
        {
            var subEvento = await _subEventoRepository.GetSubEventoPorId(idSubEvento);
            if (subEvento == null) return Resultado<List<CheckInSubEvento>>.Falha("Não existe um subEvento Válido com esse Id");

            var resultado = await _checkInSubEventoRepository.GetCheckInFeitosPorIdSubEvento(idSubEvento);
            return Resultado<List<CheckInSubEvento>>.Ok(resultado);

        }
    }
}
