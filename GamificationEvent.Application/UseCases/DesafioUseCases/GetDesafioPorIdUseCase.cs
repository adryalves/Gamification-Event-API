using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.DesafioUseCases
{
    public class GetDesafioPorIdUseCase
    {
        private readonly IDesafioRepository _desafioRepository;

        public GetDesafioPorIdUseCase(IDesafioRepository desafioRepository)
        {
            _desafioRepository = desafioRepository;
        }

        public async Task<Resultado<Desafio>> GetDesafioPorId(Guid id)
        {
            var desafio = await _desafioRepository.GetDesafioPorId(id);
            return Resultado<Desafio>.Ok(desafio);
        }
    }
}
