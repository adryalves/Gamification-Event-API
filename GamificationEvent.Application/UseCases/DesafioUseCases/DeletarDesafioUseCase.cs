using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.DesafioUseCases
{
    public class DeletarDesafioUseCase
    {
        private readonly IDesafioRepository _desafioRepository;

        public DeletarDesafioUseCase(IDesafioRepository desafioRepository)
        {
            _desafioRepository = desafioRepository;
        }

        public async Task<Resultado<bool>> DeletarDesafio(Guid id)
        {
            var desafioExistente = await _desafioRepository.GetDesafioPorId(id);
            if (desafioExistente == null) return Resultado<bool>.Falha("Não foi encontrado um desafio válido com esse Id");

            var resultado = await _desafioRepository.DeletarDesafio(id);

            if (resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na deleção");

        }
    }
}
