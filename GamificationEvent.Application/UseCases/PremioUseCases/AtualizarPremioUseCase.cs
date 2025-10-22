using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PremioUseCases
{
    public class AtualizarPremioUseCase
    {
        private readonly IPremioRepository _premioRepository;

        public AtualizarPremioUseCase(IPremioRepository premioRepository)
        {
            _premioRepository = premioRepository;
        }

        public async Task<Resultado<bool>> AtualizarPremio(Guid id, Premio premio)
        {
            var premioExistente = await _premioRepository.GetPremioPorid(id);

            if(premioExistente == null) return Resultado<bool>.Falha($"Premio não encontrado");

            premio.Id = id;
            var resultado = await _premioRepository.AtualizarPremio(premio);

            if (resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na atualização");


            
        }
    }
}
