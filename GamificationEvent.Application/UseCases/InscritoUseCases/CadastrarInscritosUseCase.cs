using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public async Task<Resultado<List<Inscrito>>> CadastrarInscritos(Guid idEvento, List<Inscrito> inscritos)
        {
            var evento = await _eventoRepository.GetEventoPorId(idEvento);

            if(evento == null) return Resultado<List<Inscrito>>.Falha($"Evento com id: {idEvento} não encontrado");

          
            var inscritosValidos = new List<Inscrito>();

            string padraoRegex = @"\D";
            foreach (var inscrito in inscritos)
            {
                string cpfValido = Regex.Replace(inscrito.Cpf, padraoRegex, "");


                var inscritoExiste = await _inscritoRepository.JaExisteEsseInscrito(cpfValido, idEvento);
                if(String.IsNullOrEmpty(inscrito.Nome) || inscrito.Cargo == null || inscritoExiste != null)
                {
                    continue;
                }
                inscrito.Id = Guid.NewGuid();
                inscrito.Cpf = cpfValido;

                inscritosValidos.Add(inscrito);
            }
            var resultado = await _inscritoRepository.AdicionarTodosOsInscrito(inscritosValidos);

            return Resultado<List<Inscrito>>.Ok(resultado);

        }
    }
}
