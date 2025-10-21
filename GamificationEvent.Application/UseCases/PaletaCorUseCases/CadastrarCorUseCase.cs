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
    public class CadastrarCorUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public CadastrarCorUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<Cor>> CadastrarCor(Cor cor)
        {
          if(await _paletaCorRepository.CorJaExiste(cor.HexCodigo)) return Resultado<Cor>.Falha($"Cor com esse código: {cor.HexCodigo} já existe");

            cor.Id = Guid.NewGuid();

             var resultado = await _paletaCorRepository.AdicionarCor(cor);
            return Resultado<Cor>.Ok(resultado);

        }
    }
}
  