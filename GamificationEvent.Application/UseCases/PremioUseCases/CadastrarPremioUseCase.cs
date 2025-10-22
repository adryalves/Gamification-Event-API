using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.PremioUseCases
{
    public class CadastrarPremioUseCase
    {
        private readonly IPremioRepository _premioRepository;
        private readonly IEventoRepository _eventoRepository;

        public CadastrarPremioUseCase(IPremioRepository premioRepository, IEventoRepository eventoRepository)
        {
            _premioRepository = premioRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<Guid>> CadastrarPremio(Premio premio)
        {
            var evento = await _eventoRepository.GetEventoPorId(premio.IdEvento);
            if (evento == null) return Resultado<Guid>.Falha($"O id {premio.IdEvento} não corresponde a um evento existente");

            //quando tiver patrocinado adicionar essa validação

            var resultado = await _premioRepository.AdicionarPremio(premio);

            return Resultado<Guid>.Ok(resultado);
        }
    }
}
