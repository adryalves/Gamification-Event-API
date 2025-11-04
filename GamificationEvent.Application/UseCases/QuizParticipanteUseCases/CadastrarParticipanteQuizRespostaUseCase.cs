using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizParticipanteUseCases
{
    public class CadastrarParticipanteQuizRespostaUseCase
    {
        private readonly IQuizParticipanteRepository _quizParticipanteRepository;
        private readonly IQuizRepository _quizRepository;
        private readonly IParticipanteRepository _participanteRepository;

        public CadastrarParticipanteQuizRespostaUseCase(IQuizParticipanteRepository quizParticipanteRepository, IQuizRepository quizRepository, IParticipanteRepository participanteRepository)
        {
            _quizParticipanteRepository = quizParticipanteRepository;
            _quizRepository = quizRepository;
            _participanteRepository = participanteRepository;
        }

        public async Task<Resultado<Guid>> CadastrarParticipanteQuizResposta(ParticipanteQuizResposta participanteResposta)
        {
            var participante = await _participanteRepository.GetParticipantePorId(participanteResposta.IdParticipante);
            if (participante == null) return Resultado<Guid>.Falha($"Esse id de participante não corresponde a nenhum participante válido");

            var quizPergunta = await _quizRepository.GetPerguntaPorId(participanteResposta.IdQuizPergunta);
            if (quizPergunta == null) return Resultado<Guid>.Falha($"Esse id pergunta não corresponde a uma pergunta válida");

            var quizAlternativa = await _quizRepository.GetAlternativaPoId(participanteResposta.IdQuizAlternativa,participanteResposta.IdQuizPergunta);
            if (quizAlternativa == null) return Resultado<Guid>.Falha($"Esse id alternativa não corresponde a uma alternativa válida dessa pergunta");

            var participanteEstaNoQuiz = await _quizParticipanteRepository.ParticipanteEstaNesseQuiz(participanteResposta.IdParticipante, quizPergunta.IdQuiz);
            if (!participanteEstaNoQuiz) return Resultado<Guid>.Falha("O participante precisa fazer parte do quiz para responder");


            var resultado = await _quizParticipanteRepository.AdicionarParticipanteQuizResposta(participanteResposta);
            return Resultado<Guid>.Ok(resultado);
        }
    }
}
