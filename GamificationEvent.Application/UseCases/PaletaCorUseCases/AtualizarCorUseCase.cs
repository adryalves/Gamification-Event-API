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
    public class AtualizarCorUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public AtualizarCorUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<bool>> AtualizarCor(Cor cor)
        {
            var corExistente = await _paletaCorRepository.GetCorPorid(cor.Id);

            if (corExistente == null) return Resultado<bool>.Falha($"Cor com id: {cor.Id} não encontrado");
 
            var hexCodJaExiste = await _paletaCorRepository.CorJaExiste(cor.HexCodigo);

            if(hexCodJaExiste && corExistente.HexCodigo != cor.HexCodigo)
                return Resultado<bool>.Falha("Esse código de cor já esta cadastrado.");

            var resultado = await _paletaCorRepository.AtualizarCor(cor);
            return Resultado<bool>.Ok(resultado);
        }
    }
}
