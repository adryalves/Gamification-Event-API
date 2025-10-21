using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PaletaCorUseCases
{
    public class CadastrarPaletaUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public CadastrarPaletaUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<PaletaCor>> CadastrarPaletaCor(PaletaCor paleta)
        {
            if(await _paletaCorRepository.GetCorPorid(paleta.IdCor1) == null)
                return Resultado<PaletaCor>.Falha($"Cor de id {paleta.IdCor1} não encontrado.");

            if (await _paletaCorRepository.GetCorPorid(paleta.IdCor2) == null)
                return Resultado<PaletaCor>.Falha($"Cor de id {paleta.IdCor2} não encontrado.");

            if (await _paletaCorRepository.GetCorPorid(paleta.IdCor3) == null)
                return Resultado<PaletaCor>.Falha($"Cor de id {paleta.IdCor3} não encontrado.");

            if (await _paletaCorRepository.GetCorPorid(paleta.IdCor4) == null)
                return Resultado<PaletaCor>.Falha($"Cor de id {paleta.IdCor4} não encontrado.");


            paleta.Id = Guid.NewGuid();
            paleta.Deletado = false;

            var resultado = await _paletaCorRepository.AdicionarPaleta(paleta);
            return Resultado<PaletaCor>.Ok(resultado);

        }
    }
}
