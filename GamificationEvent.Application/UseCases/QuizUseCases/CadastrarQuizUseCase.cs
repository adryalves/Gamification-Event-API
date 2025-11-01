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
    public class CadastrarQuizUseCase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ISubEventoRepository _subEventoRepository;
        private readonly IQuizRepository _quizRepository;

        public CadastrarQuizUseCase(IEventoRepository eventoRepository, ISubEventoRepository subEventoRepository, IQuizRepository quizRepository)
        {
            _eventoRepository = eventoRepository;
            _subEventoRepository = subEventoRepository;
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<Guid>> CadastrarQuiz(Quiz quiz)
        {
            var evento = await _eventoRepository.GetEventoPorId(quiz.IdEvento);
            if (evento == null) return Resultado<Guid>.Falha($"O id {quiz.IdEvento} não corresponde a um evento existente");

            if (quiz.IdSubEvento != null)
            {
                var subEvento = await _subEventoRepository.GetSubEventoPorId((Guid)quiz.IdSubEvento);
                if (subEvento== null) return Resultado<Guid>.Falha("Não foi encontrado um subEvento Válido com esse Id");

                if (subEvento.IdEvento != quiz.IdEvento) return Resultado<Guid>.Falha("O subEvento não pode ter um id Evento diferente do quiz");
            }

            //checagem de patrocinador quando houver

            var resultado = await _quizRepository.AdicionarQuiz(quiz);
            return Resultado<Guid>.Ok(resultado);

        }
    }
}
