using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizParticipanteUseCases
{
    public class CadastrarQuizParticipanteUseCase
    {
        private readonly IQuizParticipanteRepository _quizParticipanteRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IParticipanteRepository _participanteRepository;
        private readonly IDesafioRepository _desafioRepository;

        public CadastrarQuizParticipanteUseCase(IQuizParticipanteRepository quizParticipanteRepository, IQuizRepository quizRepository, IParticipanteRepository participanteRepository, IDesafioRepository desafioRepository)
        {
            _quizParticipanteRepository = quizParticipanteRepository;
            _quizRepository = quizRepository;
            _participanteRepository = participanteRepository;
            _desafioRepository = desafioRepository;
        }

        public async Task<Resultado<Guid>> CadastrarQuizParticipante(QuizParticipante quizParticipante)
        {
            var participante = await _participanteRepository.GetParticipantePorId(quizParticipante.IdParticipante);
            if (participante == null) return Resultado<Guid>.Falha($"Esse id de participante não corresponde a nenhum participante válido");

            var quiz = await _quizRepository.GetQuizPorId(quizParticipante.IdQuiz);
            if (quiz == null) return Resultado<Guid>.Falha("Não foi encontrado um Quiz referente a esse Id");

            if (quiz.IdEvento != participante.IdEvento) return Resultado<Guid>.Falha("O participante precisa estar no mesmo evento do quiz");

            if (participante.Cargo == Cargo.Admin) return Resultado<Guid>.Falha("Um Admin não pode participar do quiz");

            var participanteJaNesseQuiz = await _quizParticipanteRepository.ParticipanteEstaNesseQuiz(quizParticipante.IdParticipante, quizParticipante.IdQuiz);
            if (participanteJaNesseQuiz) return Resultado<Guid>.Falha("Esse participante já está nesse Quiz");

            var cadastroQuizParticipante = await _quizParticipanteRepository.AdicionarQuizParticipante(quizParticipante);

            // Foi cadastrado e agr é a questão de gamificação

            var desafioAtivo = await _desafioRepository.DesafioJaCadastradoNesseEvento(quiz.IdEvento, Core.Enums.Tipo_Desafio.Quiz);

            if (desafioAtivo != null)
            {
                var participanteEstaNoDesafio = await _desafioRepository.ParticipanteEstaParticipandoDoDesafio(desafioAtivo.Id, participante.Id);

                if (participanteEstaNoDesafio != null)
                {
                    if (participanteEstaNoDesafio.StatusDesafio == Core.Enums.Status_Desafio.Completo) return Resultado<Guid>.Ok(cadastroQuizParticipante); // se o participante já cumpriu o desafio correspondente não tem pq add mais um valor na qtd de quiz feitos

                    participanteEstaNoDesafio.QuantidadeRealizada += 1;

                    var qtdParticipante = participanteEstaNoDesafio.QuantidadeRealizada;

                    if (qtdParticipante == desafioAtivo.QuantidadeDesafio) // acabou de concluir desafio, preciso marcar como finalizado e somar pontuação
                    {
                        await _desafioRepository.AtualizarDesafioParticipante(participanteEstaNoDesafio.Id, qtdParticipante, Core.Enums.Status_Desafio.Completo);
                        await _participanteRepository.AtualizarPontuacao(participante.Id, desafioAtivo.Pontuacao);
                    }
                    else // não finalizou, então apenas aumento a qtd de quiz feito
                    {
                        await _desafioRepository.AtualizarDesafioParticipante(participanteEstaNoDesafio.Id, qtdParticipante);
                    }

                }
                else
                {
                    var desafioParticipante = new DesafioParticipante
                    {

                        IdParticipante = participante.Id,
                        IdDesafio = desafioAtivo.Id,
                        StatusDesafio = Status_Desafio.Aberto,
                        QuantidadeRealizada = 1
                    };

                    await _desafioRepository.CadastrarDesafioParticipante(desafioParticipante);
                }

            }

            return Resultado<Guid>.Ok(cadastroQuizParticipante);
        }
    }
}
