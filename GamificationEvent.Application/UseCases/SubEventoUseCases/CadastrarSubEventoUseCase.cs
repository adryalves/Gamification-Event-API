using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.SubEventoUseCases
{
    public class CadastrarSubEventoUseCase
    {
        private readonly IEventoRepository _eventoRepository;
        private readonly ISubEventoRepository _subEventoRepository;
        private readonly IPalestranteRepository _palestranteRepository;

        public CadastrarSubEventoUseCase(IEventoRepository eventoRepository, ISubEventoRepository subEventoRepository, IPalestranteRepository palestranteRepository)
        {
            _eventoRepository = eventoRepository;
            _subEventoRepository = subEventoRepository;
            _palestranteRepository = palestranteRepository;
        }

        public async Task<Resultado<Guid>> CadastrarSubEvento(SubEvento subEvento)
        {
            var evento = await _eventoRepository.GetEventoPorId(subEvento.IdEvento);
            if (evento == null) return Resultado<Guid>.Falha($"O id {subEvento.IdEvento} não corresponde a um evento existente");
            //quando tiver ponto mapa adicionar essa validação

            var palestrantesValidos = new List<PalestrantesSubEvento>();

            foreach (var palestranteSubEvento in subEvento.Palestrantes)
            {
                var palestranteValido = await _palestranteRepository.GetPalestrantePorId(palestranteSubEvento.IdPalestrante);
               // var palestranteCadastrado = await _subEventoRepository.PalestranteJaEstaNesseSubEvento(subEvento.Id, palestranteSubEvento.IdPalestrante);
                //tirar duvida com Malu

                if(palestranteValido == null || palestranteValido.IdEvento != subEvento.IdEvento)
                {
                    continue;
                }
                palestrantesValidos.Add(palestranteSubEvento);

            }
            subEvento.Palestrantes = palestrantesValidos;

            // criação automatica do código

            subEvento.CodigoCheckin = $"EVT{subEvento.IdEvento.ToString().Substring(0, 3).ToUpper()}-CHK-{Guid.NewGuid().ToString().Substring(0, 10).ToUpper()}";


            var resultado = await _subEventoRepository.AdicionarSubEvento(subEvento);
            return Resultado<Guid>.Ok(resultado);
            
        }
    }
}
