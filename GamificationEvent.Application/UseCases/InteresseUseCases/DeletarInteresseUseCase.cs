using GamificationEvent.Core.Interfaces;
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

        public async Task<bool> DeletarInteresse(Guid id)
        {
            var interesse = await _interesseRepository.GetInteressePorId(id);
            if (interesse == null) throw new Exception("Esse interesse não esta cadastrado");

            return await _interesseRepository.DeletarInteresse(id);
        }
    }
}
