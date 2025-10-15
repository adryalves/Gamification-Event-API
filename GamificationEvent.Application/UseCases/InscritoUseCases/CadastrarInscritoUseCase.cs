using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.InscritoUseCases
{
    public class CadastrarInscritoUseCase
    {
        private readonly IInscritoRepository _inscritoRepository;
        private readonly IEventoRepository _eventoRepository;

        public CadastrarInscritoUseCase(IInscritoRepository inscritoRepository, IEventoRepository eventoRepository)
        {
            _inscritoRepository = inscritoRepository;
            _eventoRepository = eventoRepository;
        }
        public async Task<Inscrito> CadastrarInscrito(Inscrito inscrito)
        {
            var evento = await _eventoRepository.GetEventoPorId(inscrito.IdEvento);
            if (evento == null)
                throw new Exception("Esse evento não existe");
            
  
            var inscritoExiste = await _inscritoRepository.JaExisteEsseInscrito(inscrito.Cpf, inscrito.IdEvento);

            if (inscritoExiste != null)
                throw new Exception("Esse inscrito já existe para esse evento");

            inscrito.Id = Guid.NewGuid();
            return await _inscritoRepository.AdicionarInscrito(inscrito);

        }
        }
    
}

