using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PaletaCorUseCases
{
    public class AtualizarCorUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public AtualizarCorUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<bool> AtualizarCor(Cor cor)
        {
            var corExistente = await _paletaCorRepository.GetCorPorid(cor.Id);

            if (corExistente == null)
                throw new Exception("Cor não encontrado.");

            var hexCodJaExiste = await _paletaCorRepository.CorJaExiste(cor.HexCodigo);


            if(hexCodJaExiste && corExistente.HexCodigo != cor.HexCodigo)
                throw new Exception("Esse código de cor já esta cadastrado.");

         return await _paletaCorRepository.AtualizarCor(cor);
        }
    }
}
