using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PalestranteUseCases
{
    public class AtualizarPalestranteUseCase
    {
        private readonly IPalestranteRepository _palestranteRepository;

        public AtualizarPalestranteUseCase(IPalestranteRepository palestranteRepository)
        {
            _palestranteRepository = palestranteRepository;
        }

        public async Task<Resultado<bool>> AtualizarPalestrante(Guid id, Palestrante palestrante)
        {
            var palestranteExistente = await _palestranteRepository.GetPalestrantePorId(id);
            if(palestranteExistente == null) return Resultado<bool>.Falha("Palestrante não encontrado");

            palestrante.Id = id;
            var resultado = await _palestranteRepository.AtualizarPalestrante(palestrante);
            if(resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na atualização");
        }
    }
}
