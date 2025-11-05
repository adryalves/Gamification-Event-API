using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.ParticipanteUseCases
{
    public class AtualizarParticipanteUseCase
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IInteresseRepository _interesseRepository;

        public AtualizarParticipanteUseCase(IParticipanteRepository participanteRepository, IInteresseRepository interesseRepository)
        {
            _participanteRepository = participanteRepository;
            _interesseRepository = interesseRepository;
        }

        public async Task<Resultado<bool>> AtualizarParticipante(Participante participante)
        {
            var participanteExistente = await _participanteRepository.GetParticipantePorId(participante.Id);
            if (participanteExistente == null) return Resultado<bool>.Falha($"Participante de id {participante.Id} não encontrado.");

            var participanteInteresseValidos = new List<ParticipanteInteresse>();
            foreach (var interesse in participante.ParticipanteInteresses)
            {
                var interesseExiste = await _interesseRepository.GetInteressePorId(interesse.IdInteresse);
                if (interesseExiste != null && !participanteInteresseValidos.Any(i => i.IdInteresse == interesse.IdInteresse))
                {
                    interesse.IdParticipante = participante.Id;
                    participanteInteresseValidos.Add(interesse);
                }
            }
            participante.ParticipanteInteresses = participanteInteresseValidos;
           
            var resultado = await _participanteRepository.AtualizarParticipante(participante);
            return Resultado<bool>.Ok(resultado);

        }
    }
}
