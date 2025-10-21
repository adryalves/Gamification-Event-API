using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InscritoUseCases
{
    public class GetInscritosUseCase
    {
        private readonly IInscritoRepository _inscritoRepository;

        public GetInscritosUseCase(IInscritoRepository inscritoRepository)
        {
            _inscritoRepository = inscritoRepository;
        }

        public async Task<Resultado<List<Inscrito>>> GetInscritos()
        {

            var resultado = await _inscritoRepository.GetInscritos();
            return Resultado<List<Inscrito>>.Ok(resultado);

        }
    }
}
