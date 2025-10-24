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
    public class AdicionarPerguntaProSubEventoUseCase
    {
        private readonly ISubEventoRepository _subEventoRepository;
        private readonly IParticipanteRepository _participanteRepository;

        public AdicionarPerguntaProSubEventoUseCase(ISubEventoRepository subEventoRepository, IParticipanteRepository participanteRepository)
        {
            _subEventoRepository = subEventoRepository;
            _participanteRepository = participanteRepository;
        }

        public async Task<Resultado<Guid>> AdicionarPerguntaProSubEvento(PerguntasSubEvento perguntaSubEvento)
        {
            var subEvento = await _subEventoRepository.GetSubEventoPorId(perguntaSubEvento.IdSubEvento);
            if (subEvento == null) return Resultado<Guid>.Falha("Não existe um subEvento Válido com esse Id");

            var participante = await _participanteRepository.GetParticipantePorId(perguntaSubEvento.IdParticipante);
            if (participante == null) return Resultado<Guid>.Falha($"Esse id de participante não corresponde a nenhum participante válido");

            if (subEvento.IdEvento != participante.IdEvento) return Resultado<Guid>.Falha("O participante e o subEvento não pertence ao mesmo Evento");

            var resultado = await _subEventoRepository.AdicionarPerguntaProSubEvento(perguntaSubEvento);
            return Resultado<Guid>.Ok(resultado);
            
        }
    }
}
