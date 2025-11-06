using GamificationEvent.API.DTOs.QuizParticipante;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Models;


namespace GamificationEvent.API.Mappings
{
    public static class QuizParticipanteMapper
    {
        public static ParticipanteQuizResposta ConverterParticipanteQuizRespostaParaCore(this ParticipanteQuizRespostaRequestDTO participanteQuizResposta)
        {
            return new ParticipanteQuizResposta
            {
                IdParticipante = participanteQuizResposta.IdParticipante,
                IdQuizPergunta = participanteQuizResposta.IdQuizPergunta,
                IdQuizAlternativa = participanteQuizResposta.IdQuizAlternativa,
                HoraResposta = participanteQuizResposta.HoraResposta
            };
        }
        public static QuizParticipante ConveterQuizParticipante(this QuizParticipanteRequestDTO quizParticipante)
        {
            return new QuizParticipante
            {
                IdParticipante = quizParticipante.IdParticipante,
                IdQuiz = quizParticipante.IdQuiz
            };
        }

        public static QuizParticipanteResponseDTO ConverterQuizParticipanteParaResponse(this QuizParticipante quizParticipante)
        {
            return new QuizParticipanteResponseDTO
            {
                Id = quizParticipante.Id,
                IdParticipante = quizParticipante.IdParticipante,
                IdQuiz = quizParticipante.IdQuiz
            };
        }

        public static List<QuizParticipanteResponseDTO> ConverterQuizParticipanteListaParaResponse(this List<QuizParticipante> quizParticipantes)
        {
            return quizParticipantes.Select(p => p.ConverterQuizParticipanteParaResponse()).ToList();
        }

        public static QuizParticipanteResultadoResponseDTO ConverterQuizParticipanteResultadoParaResponse(this QuizParticipanteResultadoModel model)
        {
            return new QuizParticipanteResultadoResponseDTO
            {
                IdQuiz = model.IdQuiz,
                IdParticipante = model.IdParticipante,
                NomeQuiz = model.NomeQuiz,
                QuantidadeAcertos = model.QuantidadeAcertos,
                QuantidadePerguntas = model.QuantidadePerguntas,
                Perguntas = model.Perguntas.Select(p => new PerguntaRespondidaResponseDTO
                {
                    IdPergunta = p.IdPergunta,
                    Enunciado = p.Enunciado,
                    RespostaEscolhida = p.RespostaEscolhida,
                    EstaCorreta = p.EstaCorreta
                }).ToList()
            };
        }
        public static QuizRankingResponseDTO QuizRankingParaResponse(this QuizRankingModel model)
        {
            return new QuizRankingResponseDTO
            {
                IdQuiz = model.IdQuiz,
                Participantes = model.Participantes.Select(p => new QuizRankingParticipanteResponseDTO
                {
                    Posicao = p.Posicao,
                    IdParticipante = p.IdParticipante,
                    Nome = p.Nome,
                    QuantidadeAcertos = p.QuantidadeAcertos,
                    Pontuacao = p.Pontuacao
                }).ToList()
            };
        }

    }
}

