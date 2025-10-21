using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InteresseUseCases
{
    public class GetInteressesPorIdEventoUseCase
    {
        private readonly IInteresseRepository _interesseRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetInteressesPorIdEventoUseCase(IInteresseRepository interesseRepository, IEventoRepository eventoRepository)
        {
            _interesseRepository = interesseRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<Interesse>>> GetInteressesPorIdEvento(Guid idEvento)
        {
            var eventoExistente = await _eventoRepository.GetEventoPorId(idEvento);
            if (eventoExistente == null)
                return Resultado<List<Interesse>>.Falha($"Evento com id: {idEvento} não encontrado");

            var resultado = await _interesseRepository.GetInteressesPorIdEvento(idEvento);
            return Resultado<List<Interesse>>.Ok(resultado);
        }
    }
    
}
