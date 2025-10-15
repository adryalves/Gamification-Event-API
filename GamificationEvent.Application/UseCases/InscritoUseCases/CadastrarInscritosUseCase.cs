using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InscritoUseCases
{
    public class CadastrarInscritosUseCase
    {
        private readonly IInscritoRepository _inscritoRepository;
        private readonly IEventoRepository _eventoRepository;

        public CadastrarInscritosUseCase(IInscritoRepository inscritoRepository, IEventoRepository eventoRepository)
        {
            _inscritoRepository = inscritoRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<int> CadastrarInscritos(Guid idEvento, List<Inscrito> inscritos)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);

            if(evento == null)
            {
                throw new Exception($"O id {idEvento} não corresponde a um evento existente");
            }
            var inscritosValidos = new List<Inscrito>();

            foreach (var inscrito in inscritos)
            {
 
                var inscritoExiste = await _inscritoRepository.JaExisteEsseInscrito(inscrito.Cpf, idEvento);
                if(String.IsNullOrEmpty(inscrito.Nome) || inscrito.Cargo == null || inscritoExiste != null)
                {
                    continue;
                }
                inscrito.Id = Guid.NewGuid();
                inscritosValidos.Add(inscrito);
            }
            return await _inscritoRepository.AdicionarTodosOsInscrito(inscritosValidos);

        }
    }
}
