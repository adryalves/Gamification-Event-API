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
    public class DeletarInscritoUseCase
    {
        private readonly IInscritoRepository _inscritoRepository;

        public DeletarInscritoUseCase(IInscritoRepository inscritoRepository)
        {
            _inscritoRepository = inscritoRepository;
        }

        public async Task<Resultado<bool>> DeletarInscrito(String cpf, Guid idEvento)
        {
            var inscrito = _inscritoRepository.JaExisteEsseInscrito(cpf, idEvento);
            if (inscrito == null) return Resultado<bool>.Falha("Cadastro de Inscrito nesse evento não encontrado para ser deletado.");

            var resultado = await _inscritoRepository.DeletarInscrito(cpf, idEvento);

            return Resultado<bool>.Ok(resultado);
        }
       
    }
}
