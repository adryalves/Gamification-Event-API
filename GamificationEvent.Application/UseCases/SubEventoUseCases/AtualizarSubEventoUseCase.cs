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
    public class AtualizarSubEventoUseCase
    {
        private readonly ISubEventoRepository _subEventoRepository;

        public AtualizarSubEventoUseCase(ISubEventoRepository subEventoRepository)
        {
            _subEventoRepository = subEventoRepository;
        }

        public async Task<Resultado<bool>> AtualizarSubEvento(Guid id, SubEvento subEvento)
        {
            var subEventoExistente = await _subEventoRepository.GetSubEventoPorId(id);
            if (subEventoExistente == null) return Resultado<bool>.Falha("Não foi encontrado um subEvento Válido com esse Id");

            subEvento.Id = id;

            var resultado = await _subEventoRepository.AtualizarSubEvento(subEvento);
            if (resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na atualização");
        }
    }
}
