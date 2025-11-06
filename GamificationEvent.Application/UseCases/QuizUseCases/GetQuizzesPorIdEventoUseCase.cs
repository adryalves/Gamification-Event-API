using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizUseCases
{
    public class GetQuizzesPorIdEventoUseCase
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IEventoRepository _eventoRepository;

        public GetQuizzesPorIdEventoUseCase(IQuizRepository quizRepository, IEventoRepository eventoRepository)
        {
            _quizRepository = quizRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<List<Quiz>>> GetQuizzesPorIdEvento(Guid idEvento)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);
            if (evento == null) return Resultado<List<Quiz>>.Falha($"O id {idEvento} não corresponde a um evento existente");

            var resultado = await _quizRepository.GetQuizzesPorIdEvento(idEvento);
            return Resultado<List<Quiz>>.Ok(resultado);

        }
    }
}
