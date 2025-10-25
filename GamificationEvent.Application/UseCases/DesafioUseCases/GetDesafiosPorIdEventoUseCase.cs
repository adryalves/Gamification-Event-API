using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.DesafioUseCases
{
    public class GetDesafiosPorIdEventoUseCase
    {
        private readonly IDesafioRepository _desafioRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetDesafiosPorIdEventoUseCase(IDesafioRepository desafioRepository, IEventoRepository eventoRepository)
        {
            _desafioRepository = desafioRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<Desafio>>> GetDesafiosPorIdEvento(Guid idEvento)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);
            if (evento == null) return Resultado<List<Desafio>>.Falha($"O id {idEvento} não corresponde a um evento existente");

            var desafios = await _desafioRepository.GetDesafiosPorIdEvento(idEvento);
            return Resultado<List<Desafio>>.Ok(desafios);

        }
    }
}
