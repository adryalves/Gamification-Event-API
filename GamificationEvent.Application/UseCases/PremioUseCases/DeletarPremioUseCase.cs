using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PremioUseCases
{
    public class DeletarPremioUseCase
    {
        private readonly IPremioRepository _premioRepository;

        public DeletarPremioUseCase(IPremioRepository premioRepository)
        {
            _premioRepository = premioRepository;
        }

        public async Task<Resultado<bool>> DeletarPremio(Guid id)
        {
            var premio = await _premioRepository.GetPremioPorid(id);
            if (premio == null) return Resultado<bool>.Falha("Premio não encontrado para ser deletado");

            var resultado = await _premioRepository.DeletarPremio(id);

            if(resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na deleção");
        }
    }
}
