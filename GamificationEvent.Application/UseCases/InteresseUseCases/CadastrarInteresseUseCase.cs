using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InteresseUseCases
{
    public class CadastrarInteresseUseCase
    {
        private readonly IInteresseRepository _interesseRepository;
        private readonly IEventoRepository _eventoRepository;

        public CadastrarInteresseUseCase(IInteresseRepository interesseRepository, IEventoRepository eventoRepository)
        {
            _interesseRepository = interesseRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<int> CadastrarInteresses(Guid idEvento, List<Interesse> interesses)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);

            if (evento == null)
            {
                throw new Exception($"O id {idEvento} não corresponde a um evento existente");
            }

            var interessesValidos = new List<Interesse>();

            foreach (var interesse in interesses)
            {

                if (String.IsNullOrEmpty(interesse.Nome))
                {
                    continue;
                }
                interesse.Id = Guid.NewGuid();
                interessesValidos.Add(interesse);
            }

            return await _interesseRepository.AdicionarInteresses(interessesValidos);
        }
    }
}
