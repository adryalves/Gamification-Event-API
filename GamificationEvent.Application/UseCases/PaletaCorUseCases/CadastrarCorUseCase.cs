using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
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

        public async Task<Cor> CadastrarCor(Cor cor)
        {
          if(await _paletaCorRepository.CorJaExiste(cor.HexCodigo))
            {
                throw new InvalidOperationException("Cor já existe");
            }
            cor.Id = Guid.NewGuid();

          return  await _paletaCorRepository.AdicionarCor(cor);
        }
    }
}
  