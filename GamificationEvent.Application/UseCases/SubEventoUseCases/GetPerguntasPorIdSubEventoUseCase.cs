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
    public class GetPerguntasPorIdSubEventoUseCase
    {
        private readonly ISubEventoRepository _subEventoRepository;

        public GetPerguntasPorIdSubEventoUseCase(ISubEventoRepository subEventoRepository)
        {
            _subEventoRepository = subEventoRepository;
        }

        public async Task<Resultado<List<PerguntasSubEvento>>> GetPerguntasPorIdSubEvento(Guid idSubEvento)
        {
            var subEventoExistente = await _subEventoRepository.GetSubEventoPorId(idSubEvento);
            if (subEventoExistente == null) return Resultado<List<PerguntasSubEvento>>.Falha("Não foi encontrado um subEvento Válido com esse Id");

            var perguntas = await _subEventoRepository.GetPerguntasPorIdSubEvento(idSubEvento);
            return Resultado<List<PerguntasSubEvento>>.Ok(perguntas);
        }
    }
}
