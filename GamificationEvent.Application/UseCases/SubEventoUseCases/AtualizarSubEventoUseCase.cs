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
    public class AtualizarSubEventoUseCase
    {
        private readonly ISubEventoRepository _subEventoRepository;
        private readonly IPalestranteRepository _palestranteRepository;

        public AtualizarSubEventoUseCase(ISubEventoRepository subEventoRepository, IPalestranteRepository palestranteRepository)
        {
            _subEventoRepository = subEventoRepository;
            _palestranteRepository = palestranteRepository;
        }

        public async Task<Resultado<bool>> AtualizarSubEvento(Guid id, SubEvento subEvento)
        {
            var subEventoExistente = await _subEventoRepository.GetSubEventoPorId(id);
            if (subEventoExistente == null) return Resultado<bool>.Falha("Não foi encontrado um subEvento Válido com esse Id");

            subEvento.Id = id;

            var palestrantesValidos = new List<PalestrantesSubEvento>();

            foreach(var palestrante  in subEvento.Palestrantes) {

                var palestranteValido = await _palestranteRepository.GetPalestrantePorId(palestrante.IdPalestrante);
                // var palestranteCadastrado = await _subEventoRepository.PalestranteJaEstaNesseSubEvento(subEvento.Id, palestranteSubEvento.IdPalestrante);
                //tirar duvida com Malu

                if (palestranteValido == null || palestranteValido.IdEvento != subEvento.IdEvento)
                {
                    continue;
                }
                palestrantesValidos.Add(palestrante);
            }

            subEvento.Palestrantes = palestrantesValidos;   

            var resultado = await _subEventoRepository.AtualizarSubEvento(subEvento);
            if (resultado) return Resultado<bool>.Ok(resultado);

            return Resultado<bool>.Falha("Algo deu errado na atualização");
        }
    }
}
