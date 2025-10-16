using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InteresseUseCases
{
    public class GetInteressePorIdUseCase
    {
        private readonly IInteresseRepository _interesseRepository;

        public GetInteressePorIdUseCase(IInteresseRepository interesseRepository)
        {
            _interesseRepository = interesseRepository;
        }

        public async Task<Interesse> GerInteressePorId(Guid id)
        {
            return await _interesseRepository.GetInteressePorId(id);
        }
    }
}
