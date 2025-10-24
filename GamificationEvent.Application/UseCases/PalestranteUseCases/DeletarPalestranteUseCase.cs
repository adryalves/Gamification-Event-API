using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PalestranteUseCases
{
    public class DeletarPalestranteUseCase
    {
        private readonly IPalestranteRepository _palestranteRepository;

        public DeletarPalestranteUseCase(IPalestranteRepository palestranteRepository)
        {
            _palestranteRepository = palestranteRepository;
        }

        public async Task<Resultado<bool>> DeletarPalestrante(Guid id)
        {
            var palestranteExistente = await _palestranteRepository.GetPalestrantePorId(id);
            if (palestranteExistente == null) return Resultado<bool>.Falha("Palestrante não encontrado");

            var resultado = await _palestranteRepository.DeletarPalestrante(id);

            if (resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na deleção");
        }
    }
}
