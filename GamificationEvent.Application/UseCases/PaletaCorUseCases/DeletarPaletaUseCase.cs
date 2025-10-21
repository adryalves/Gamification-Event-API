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
    public class DeletarPaletaUseCase
    {
        private readonly IPaletaCorRepository _paletaCorRepository;

        public DeletarPaletaUseCase(IPaletaCorRepository paletaCorRepository)
        {
            _paletaCorRepository = paletaCorRepository;
        }

        public async Task<Resultado<bool>> DeletarPaleta(Guid id)
        {
            var paletaExistente = await _paletaCorRepository.GetPaletaPorId(id);

            if (paletaExistente == null) return Resultado<bool>.Falha($"Paleta de id {id} não encontrado.");

            if (await _paletaCorRepository.PaletaPertenceAEvento(id))
                return Resultado<bool>.Falha("Essa paleta não pode ser apagada pois está sendo usada por algum evento");

            var resultado = await _paletaCorRepository.DeletarPaleta(id);
            return Resultado<bool>.Ok(resultado);

        }
    }
}
