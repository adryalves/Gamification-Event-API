using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.ParticipanteUseCases
{
    public class CadastrarParticipanteUseCase
    {
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IEventoRepository _eventoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IInscritoRepository _inscritoRepository;
        private readonly IInteresseRepository _interesseRepository;

        public CadastrarParticipanteUseCase(IParticipanteRepository participanteRepository, IEventoRepository eventoRepository, IUsuarioRepository usuarioRepository, IInscritoRepository inscritoRepository, IInteresseRepository interesseRepository)
        {
            _participanteRepository = participanteRepository;
            _eventoRepository = eventoRepository;
            _usuarioRepository = usuarioRepository;
            _inscritoRepository = inscritoRepository;
            _interesseRepository = interesseRepository;
        }

        public async Task<Guid> CadastrarParticipante(Participante participante)
        {
            var evento = await _eventoRepository.GetEventoPorId(participante.IdEvento);
            if (evento == null) throw new Exception($"O id {participante.IdEvento} não corresponde a um evento existente");

            var usuario = await _usuarioRepository.GetUsuarioPorId(participante.IdUsuario);
            if (usuario == null) throw new Exception($"O id {participante.IdUsuario} não corresponde a um usuário válido");

            var participanteExiste = await _participanteRepository.ParticipanteJaExiste(participante.IdEvento, participante.IdUsuario);
            if (participanteExiste) throw new Exception($"Esse usuário já em um participante desse evento");

            // lógica para checar se ele esta inscrito

            if (participante.PrimeiroParticipante == null)
            {
                participante.PrimeiroParticipante = false;
            }

                if (!(bool)participante.PrimeiroParticipante)
                {
                    var inscrição = await _inscritoRepository.JaExisteEsseInscrito(usuario.Cpf, participante.IdEvento);

                    if (inscrição == null) throw new Exception("Esse participante não está inscrito");

                    participante.Cargo = inscrição.Cargo;
                }
                else
                {
                    participante.Cargo = Core.Enums.Cargo.Admin;
                }

            participante.Id = Guid.NewGuid();
            var participanteInteresseValidos = new List<ParticipanteInteresse>();
            foreach (var interesse in participante.ParticipanteInteresses)
            {
                var interesseExiste = await _interesseRepository.GetInteressePorId(interesse.IdInteresse);
                if (interesseExiste != null)
                {
                    interesse.IdParticipante = participante.Id;
                    participanteInteresseValidos.Add(interesse);
                }
            }
            participante.ParticipanteInteresses = participanteInteresseValidos;

            return await _participanteRepository.AdicionarParticipante(participante);

        }
    }
}
