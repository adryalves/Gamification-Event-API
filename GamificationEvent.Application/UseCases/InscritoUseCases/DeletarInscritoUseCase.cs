using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
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

        public async Task<bool> DeletarInscrito(String cpf, Guid idEvento)
        {
            var inscrito = _inscritoRepository.JaExisteEsseInscrito(cpf, idEvento);
            if (inscrito == null) throw new Exception("Inscrito não encontrado.");

            return await _inscritoRepository.DeletarInscrito(cpf, idEvento);
        }
       
    }
}
