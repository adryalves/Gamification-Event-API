using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InteresseUseCases
{
    public class GetInteressesPorIdEventoUseCase
    {
        private readonly IInteresseRepository _interesseRepository;

        public GetInteressesPorIdEventoUseCase(IInteresseRepository interesseRepository)
        {
            _interesseRepository = interesseRepository;
        }

        public async Task<List<Interesse>> GetInteressesPorIdEvento(Guid idEvento)
        {
            return await _interesseRepository.GetInteressesPorIdEvento(idEvento);
        }
    }
    
}
