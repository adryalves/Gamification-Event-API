using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InteresseUseCases
{
    public class DeletarInteresseUseCase
    {
        private readonly IInteresseRepository _interesseRepository;

        public DeletarInteresseUseCase(IInteresseRepository interesseRepository)
        {
            _interesseRepository = interesseRepository;
        }

        public async Task<Resultado<bool>> DeletarInteresse(Guid id)
        {
            var interesse = await _interesseRepository.GetInteressePorId(id);
            if (interesse == null) return Resultado<bool>.Falha($"Interesse com id: {id} não encontrado para ser deletado");

           var resultado = await _interesseRepository.DeletarInteresse(id);
            return Resultado<bool>.Ok(resultado);
        }
    }
}
