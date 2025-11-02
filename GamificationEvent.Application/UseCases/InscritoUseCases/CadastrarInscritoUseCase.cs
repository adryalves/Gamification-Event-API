using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        public async Task<Resultado<Inscrito>> CadastrarInscrito(Inscrito inscrito)
        {
            var evento = await _eventoRepository.GetEventoPorId(inscrito.IdEvento);
            if (evento == null)
                return Resultado<Inscrito>.Falha($"Evento com id: {inscrito.IdEvento} não encontrado");

            string padraoRegex = @"\D";
            string cpfValido = Regex.Replace(inscrito.Cpf, padraoRegex, "");

            var inscritoExiste = await _inscritoRepository.JaExisteEsseInscrito(cpfValido, inscrito.IdEvento);

            if (inscritoExiste != null)
                return Resultado<Inscrito>.Falha("Esse inscrito já existe para esse evento");
                

            inscrito.Id = Guid.NewGuid();
            inscrito.Cpf = cpfValido;

            var resultado = await _inscritoRepository.AdicionarInscrito(inscrito);

            return Resultado<Inscrito>.Ok(resultado);

        }
        }
    
}

