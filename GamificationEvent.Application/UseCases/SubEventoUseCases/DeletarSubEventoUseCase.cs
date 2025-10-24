using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.SubEventoUseCases
{
    public class DeletarSubEventoUseCase
    {
        private readonly ISubEventoRepository _subEventoRepository;

        public DeletarSubEventoUseCase(ISubEventoRepository subEventoRepository)
        {
            _subEventoRepository = subEventoRepository;
        }

        public async Task<Resultado<bool>> DeletarSubEvento(Guid id)
        {
            var subEventoExistente = await _subEventoRepository.GetSubEventoPorId(id);
            if (subEventoExistente == null) return Resultado<bool>.Falha("Não foi encontrado um subEvento Válido com esse Id");

            var resultado = await _subEventoRepository.DeletarSubEvento(id);

            if (resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na deleção");
        }
    }
}
