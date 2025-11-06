using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.CheckInSubEventoCases
{
    public class CadastrarCheckInSubEventoUseCase
    {
        private readonly ISubEventoRepository _subEventoRepository;
        private readonly ICheckInSubEventoRepository _checkInSubEventoRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IDesafioRepository _desafioRepository;

        public CadastrarCheckInSubEventoUseCase(ISubEventoRepository subEventoRepository, ICheckInSubEventoRepository checkInSubEventoRepository, IParticipanteRepository participanteRepository, IDesafioRepository desafioRepository)
        {
            _subEventoRepository = subEventoRepository;
            _checkInSubEventoRepository = checkInSubEventoRepository;
            _participanteRepository = participanteRepository;
            _desafioRepository = desafioRepository;
        }

        public async Task<Resultado<Guid>> CadastrarCheckInSubEvento(string codigoCheckIn, Guid idParticipante)
        {
            var subEvento = await _subEventoRepository.CodigoSubEventoValido(codigoCheckIn);

            if (subEvento == null) return Resultado<Guid>.Falha("Esse código de SubEvento não é válido");

            var participante = await _participanteRepository.GetParticipantePorId(idParticipante);
            if(participante == null) return Resultado<Guid>.Falha("Esse id Participante não corresponde a um participante válido");

            if (subEvento.IdEvento != participante.IdEvento) return Resultado<Guid>.Falha("Um checkIn só pode ser feito se o participante for do mesmo evento que o subEvento");

            var checkInJaFeito = await _checkInSubEventoRepository.ParticipanteRealizouCheckIn(subEvento.Id, idParticipante);
            if(checkInJaFeito) return Resultado<Guid>.Falha("CheckIn Nesse evento já realizado por esse participante");

            var checkIn = new CheckInSubEvento(subEvento.Id, idParticipante);

            var cadastrarCheckIn = await _checkInSubEventoRepository.AdicionarCheckIn(checkIn);

            // o checkIn foi feito, porém como o checkIn é uma forma de gamificação e de tipo de desafio, temos que fazer
            // o fluxo disso

            var desafioAtivo = await _desafioRepository.DesafioJaCadastradoNesseEvento(subEvento.IdEvento, Core.Enums.Tipo_Desafio.Checkin_sub_evento);

            if(desafioAtivo != null)
            {
                var participanteEstaNoDesafio = await _desafioRepository.ParticipanteEstaParticipandoDoDesafio(desafioAtivo.Id, idParticipante);

                if(participanteEstaNoDesafio != null)
                {
                    if(participanteEstaNoDesafio.StatusDesafio == Core.Enums.Status_Desafio.Completo) return Resultado<Guid>.Ok(cadastrarCheckIn); // se o participante já cumpriu o desafio correspondente não tem pq add mais um valor na qtd de checkIns realizada

                    participanteEstaNoDesafio.QuantidadeRealizada += 1;

                    var qtdParticipante = participanteEstaNoDesafio.QuantidadeRealizada;

                    if (qtdParticipante == desafioAtivo.QuantidadeDesafio) // acabou de concluir desafio, preciso marcar como finalizado e somar pontuação
                    {
                        await _desafioRepository.AtualizarDesafioParticipante(participanteEstaNoDesafio.Id, qtdParticipante, Core.Enums.Status_Desafio.Completo);
                        await _participanteRepository.AtualizarPontuacao(idParticipante, desafioAtivo.Pontuacao);
                    }
                    else // não finalizou, então apenas aumento a qtd de checkIns feito
                    {
                        await _desafioRepository.AtualizarDesafioParticipante(participanteEstaNoDesafio.Id, qtdParticipante);
                    }

                }
                else
                {
                    var desafioParticipante = new DesafioParticipante { 

                        IdParticipante = participante.Id,
                        IdDesafio = desafioAtivo.Id,
                        StatusDesafio = Status_Desafio.Aberto,
                        QuantidadeRealizada = 1
                        };

                    await _desafioRepository.CadastrarDesafioParticipante(desafioParticipante);
                }

            }

            return Resultado<Guid>.Ok(cadastrarCheckIn);
        }
    }
}
