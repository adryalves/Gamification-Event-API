using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.DesafioUseCases
{
    public class AtualizarDesafioUseCase
    {
        private readonly IDesafioRepository _desafioRepository;

        public AtualizarDesafioUseCase(IDesafioRepository desafioRepository)
        {
            _desafioRepository = desafioRepository;
        }

        public async Task<Resultado<bool>> AtualizarDesafio(Guid id, Desafio desafio)
        {
            var desafioExistente = await _desafioRepository.GetDesafioPorId(id);

            if(desafioExistente == null) return Resultado<bool>.Falha("Não foi encontrado um desafio válido com esse Id");

            desafio.Id = desafioExistente.Id;

            var resultado = await _desafioRepository.AtualizarDesafio(desafio);

            if (resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na atualização");

        }
    }
}
