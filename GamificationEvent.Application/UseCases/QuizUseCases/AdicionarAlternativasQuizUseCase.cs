using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.QuizUseCases
{
    public class AdicionarAlternativasQuizUseCase
    {
        private readonly IQuizRepository _quizRepository;

        public AdicionarAlternativasQuizUseCase(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Resultado<List<QuizAlternativa>>> AdicionarAlternativasQuiz(Guid idPergunta, List<QuizAlternativa> quizAlternativas)
        {
            var pergunta = await _quizRepository.GetPerguntaPorId(idPergunta);
            if (pergunta == null) return Resultado<List<QuizAlternativa>>.Falha("Não foi encontrado uma pergunta com esse Id");


            List<QuizAlternativa> alternativasValidas = new List<QuizAlternativa>();

            var alternativasVerdadeiras = 0;
            var jaExisteUmaAlternativaCorreta = await _quizRepository.JaExisteUmaAlternativaCorretaParaEssaQuestao(idPergunta);
            if (jaExisteUmaAlternativaCorreta) alternativasVerdadeiras = 1;
      

            foreach (var alternativa in quizAlternativas)
            {
                if (String.IsNullOrEmpty(alternativa.Resposta))
                    continue;

                if (alternativa.ECorreta) alternativasVerdadeiras++;
                alternativasValidas.Add(alternativa);
            }
            if (alternativasVerdadeiras > 1) return Resultado<List<QuizAlternativa>>.Falha("Uma pergunta só pode ter uma alternativa Correta");

            var resultado = await _quizRepository.AdicionarAlternativas(alternativasValidas);
            return Resultado<List<QuizAlternativa>>.Ok(resultado);
        }
    }
}
