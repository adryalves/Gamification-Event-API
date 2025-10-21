using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
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
        private readonly IEventoRepository _eventoRepository;

        public GetInscritosPorIdUseCase(IInscritoRepository inscritoRepository, IEventoRepository eventoRepository)
        {
            _inscritoRepository = inscritoRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<Inscrito>>> GetInscritosPorIdEvento(Guid idEvento)
        {
            var eventoExistente = await _eventoRepository.GetEventoPorId(idEvento);
            if (eventoExistente == null)
                return Resultado<List<Inscrito>>.Falha($"Evento com id: {idEvento} não encontrado");

            var resultado = await _inscritoRepository.GetInscritosPorIdEvento(idEvento);
            return Resultado<List<Inscrito>>.Ok(resultado);
        }
    }
}
