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
    public class CadastrarInteresseUseCase
    {
        private readonly IInteresseRepository _interesseRepository;
        private readonly IEventoRepository _eventoRepository;

        public CadastrarInteresseUseCase(IInteresseRepository interesseRepository, IEventoRepository eventoRepository)
        {
            _interesseRepository = interesseRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<int>> CadastrarInteresses(Guid idEvento, List<Interesse> interesses)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);

            if (evento == null) return Resultado<int>.Falha($"Evento com id: {idEvento} não encontrado");

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

            var resultado = await _interesseRepository.AdicionarInteresses(interessesValidos);
            return Resultado<int>.Ok(resultado);

        }
    }
}
