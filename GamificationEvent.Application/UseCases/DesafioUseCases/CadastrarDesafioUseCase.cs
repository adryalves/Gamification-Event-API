using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.DesafioUseCases
{
    public class CadastrarDesafioUseCase
    {
        private readonly IDesafioRepository _desafioRepository;
        private readonly IEventoRepository _eventoRepository;

        public CadastrarDesafioUseCase(IDesafioRepository desafioRepository, IEventoRepository eventoRepository)
        {
            _desafioRepository = desafioRepository;
            _eventoRepository = eventoRepository;
        }

        public async Task<Resultado<Guid>> CadastrarDesafio(Desafio desafio)
        {
            var evento = await _eventoRepository.GetEventoPorId(desafio.IdEvento);
            if (evento == null) return Resultado<Guid>.Falha($"O id {desafio.IdEvento} não corresponde a um evento existente");

            var desafioExiste = await _desafioRepository.DesafioJaCadastradoNesseEvento(desafio.IdEvento, desafio.TipoDesafio);
            if (desafioExiste != null) return Resultado<Guid>.Falha($"Um desafio igual a esse já esta cadastrados. O id dele é: {desafio.Id}");

            var resultado = await _desafioRepository.AdicionarDesafio(desafio);
            return Resultado<Guid>.Ok(resultado);

        }
    }
}
