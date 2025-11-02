using GamificationEvent.API.DTOs.Quiz;
using GamificationEvent.Core.Entidades;


namespace GamificationEvent.API.Mappings
{
    public static class QuizMapper
    {
        public static Quiz ConverterQuizParaCore(this QuizRequestDTO quizRequestDTO)
        {
            return new Quiz
            {
                IdEvento = quizRequestDTO.IdEvento,
                IdSubEvento = quizRequestDTO.IdSubEvento,
                IdPatrocinador = quizRequestDTO.IdPatrocinador,
                Nome = quizRequestDTO.Nome,
                Tema = quizRequestDTO.Tema,
                DataQuiz = quizRequestDTO.DataQuiz,
                HoraInicio = quizRequestDTO.HoraInicio,
                HoraFim = quizRequestDTO.HoraFim
            };
        }

        public static QuizPergunta ConverterQuizPerguntaParaCore(this QuizPerguntaRequestDTO quizPerguntaDTO)
        {
            return new QuizPergunta
            {
                IdQuiz = quizPerguntaDTO.IdQuiz,
                Enunciado = quizPerguntaDTO.Enunciado,
            };
        }

        public static List<QuizAlternativa> ConverterAlternativasParaCore(this AlternativasPerguntasQuizDTO alternativasDTO)
        {
            return alternativasDTO.AlternativaQuizDTOs.Select(x => new QuizAlternativa
            {
                IdQuizPergunta = alternativasDTO.IdPerguntaQuiz,
                Resposta = x.Resposta,
                ECorreta = x.ECorreta,

            }).ToList();
        }

        public static Quiz ConverterQuizUpdateParaCore(this QuizUpdateDTO quizUpdate)
        {
            return new Quiz
            {
                IdPatrocinador = quizUpdate.IdPatrocinador,
                Nome = quizUpdate.Nome,
                Tema = quizUpdate.Tema,
                DataQuiz = quizUpdate.DataQuiz,
                HoraInicio = quizUpdate.HoraInicio,
                HoraFim = quizUpdate.HoraFim
            };


        }
        public static QuizPergunta ConverterQuizPerguntaUpdateParaCore(this QuizPerguntaUpdateDTO perguntaUpdate)
        {
            return new QuizPergunta
            {
                Enunciado = perguntaUpdate.Enunciado,
            };
        }

        public static QuizResponseDTO ConverterQuizParaResponse(this Quiz quiz)
        {
            return new QuizResponseDTO
            {
                Id = quiz.Id,
                IdEvento = quiz.IdEvento,
                IdSubEvento = quiz.IdSubEvento,
                IdPatrocinador = quiz.IdPatrocinador,
                Nome = quiz.Nome,
                Tema = quiz.Tema,
                DataQuiz = quiz.DataQuiz,
                HoraInicio = quiz.HoraInicio,
                HoraFim = quiz.HoraFim,
                Deletado = quiz.Deletado,
            };
        }

        public static List<QuizResponseDTO> ConverterListaQuizParaResponse(this List<Quiz> quizzes)
        {
            return quizzes.Select(d => d.ConverterQuizParaResponse()).ToList();
        }

        public static QuizPerguntasEAlternativasResponseDTO ConverterListaPerguntasEAlternativasComRespostaResponse(this QuizPerguntasEAlternativas perguntas)
        {
            return new QuizPerguntasEAlternativasResponseDTO
            {
                IdQuiz = perguntas.IdQuiz,
                Perguntas = perguntas.Perguntas.Select(p => new QuizPerguntaCompletaResponseDTO
                {
                    Id = p.Id,
                    Enunciado = p.Enunciado,
                    Deletado = p.Deletado,
                    PerguntaAlternativas = p.PerguntaAlternativas.Select(a =>
                        new QuizAlternativasCompletasResponseDTO
                        {
                            Id = a.Id,
                            Resposta = a.Resposta,
                            ECorreta = a.ECorreta,
                            Deletado = a.Deletado
                        }).ToList()
                }).ToList()
            };
        }

        public static QuizPerguntasEAlternativasResponseDTO ConverterListaPerguntasEAlternativasResponse(this QuizPerguntasEAlternativas perguntas)
        {
            return new QuizPerguntasEAlternativasResponseDTO
            {
                IdQuiz = perguntas.IdQuiz,
                Perguntas = perguntas.Perguntas.Select(p => new QuizPerguntaCompletaResponseDTO
                {
                    Id = p.Id,
                    Enunciado = p.Enunciado,
                    Deletado = p.Deletado,
                    PerguntaAlternativas = p.PerguntaAlternativas.Select(a =>
                        new QuizAlternativasCompletasResponseDTO
                        {
                            Id = a.Id,
                            Resposta = a.Resposta,
                            Deletado = a.Deletado
                        }).ToList()
                }).ToList()
            };
        }

    }
}

