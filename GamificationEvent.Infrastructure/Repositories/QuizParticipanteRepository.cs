using GamificationEvent.Infrastructure.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreQuizParticipante = GamificationEvent.Core.Entidades.QuizParticipante;
using InfraQuizParticipante = GamificationEvent.Infrastructure.Data.Persistence.QuizParticipante;
using CoreParticipanteQuizResposta = GamificationEvent.Core.Entidades.ParticipanteQuizResposta;
using InfraParticipanteQuizResposta = GamificationEvent.Infrastructure.Data.Persistence.ParticipanteQuizResposta;
using Microsoft.EntityFrameworkCore;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Models;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class QuizParticipanteRepository : IQuizParticipanteRepository
    {
        private readonly AppDbContext _context;

        public QuizParticipanteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarQuizParticipante(CoreQuizParticipante participanteQuiz)
        {
            var participanteQuizDB = new InfraQuizParticipante
            {
                Id = Guid.NewGuid(),
                IdParticipante = participanteQuiz.IdParticipante,
                IdQuiz = participanteQuiz.IdQuiz
            };

            _context.QuizParticipantes.Add(participanteQuizDB);
            await _context.SaveChangesAsync();
            return participanteQuizDB.Id;
        }

        public async Task<Guid> AdicionarParticipanteQuizResposta(CoreParticipanteQuizResposta participanteQuizResposta)
        {
            var participanteQuizRespostaDB = new InfraParticipanteQuizResposta
            {
                Id = Guid.NewGuid(),
                IdParticipante = participanteQuizResposta.IdParticipante,
                IdQuizPergunta = participanteQuizResposta.IdQuizPergunta,
                IdQuizAlternativa = participanteQuizResposta.IdQuizAlternativa,
                HoraResposta = participanteQuizResposta.HoraResposta
            };

            _context.ParticipanteQuizResposta.Add(participanteQuizRespostaDB);
            await _context.SaveChangesAsync();
            return participanteQuizRespostaDB.Id;
        }

        public async Task<List<CoreQuizParticipante>> GetQuizzesPorIdParticipante(Guid idParticipante)
        {
            var quizzes = await _context.QuizParticipantes.Where(x => x.IdParticipante == idParticipante
            && !x.IdQuizNavigation.Deletado && !x.IdQuizNavigation.IdEventoNavigation.Deletado &&
            (x.IdQuizNavigation.IdSubEvento == null || !x.IdQuizNavigation.IdSubEventoNavigation.Deletado)).ToListAsync();

            return quizzes.Select(x => new CoreQuizParticipante
            {
                Id = x.Id,
                IdParticipante = x.IdParticipante,
                IdQuiz = x.IdQuiz,
            }).ToList();

        }

        public async Task<List<CoreQuizParticipante>> GetParticipantesQuizPorIdQuiz(Guid idQuiz)
        {
            var participantes = await _context.QuizParticipantes.Where(x => x.IdQuiz == idQuiz &&
            !x.IdQuizNavigation.Deletado && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado).ToListAsync();

            return participantes.Select(x => new CoreQuizParticipante
            {

                Id = x.Id,
                IdParticipante = x.IdParticipante,
                IdQuiz = x.IdQuiz,
            }).ToList();
        }

        public async Task<QuizParticipanteResultadoModel> GetResultadoParticipanteQuiz(Guid idQuiz, Guid idParticipante)
        {
            var quiz = await _context.Quizzes
        .Include(q => q.QuizPergunta)
            .ThenInclude(p => p.QuizAlternativas)
        .Include(q => q.QuizPergunta)
            .ThenInclude(p => p.ParticipanteQuizResposta)
        .AsNoTracking()
        .FirstOrDefaultAsync(q => q.Id == idQuiz && !q.Deletado);

            if (quiz == null)
                return null;


            var perguntasAtivas = quiz.QuizPergunta
                .Where(p => !p.Deletado)
                .Select(p =>
                {
                    var resposta = p.ParticipanteQuizResposta
                        .FirstOrDefault(r => r.IdParticipante == idParticipante);

                    if (resposta == null)
                        return new PerguntaRespondidaModel
                        {
                            IdPergunta = p.Id,
                            Enunciado = p.Enunciado,
                            RespostaEscolhida = "Não respondida",
                            EstaCorreta = false
                        };

                    var alternativa = p.QuizAlternativas
                        .FirstOrDefault(a => a.Id == resposta.IdQuizAlternativa && !a.Deletado);

                    return new PerguntaRespondidaModel
                    {
                        IdPergunta = p.Id,
                        Enunciado = p.Enunciado,
                        RespostaEscolhida = alternativa?.Resposta ?? "",
                        EstaCorreta = alternativa?.ECorreta ?? false
                    };
                }).ToList();

            var quantidadeCorretas = perguntasAtivas.Count(p => p.EstaCorreta);

            return new QuizParticipanteResultadoModel
            {
                IdQuiz = quiz.Id,
                IdParticipante = idParticipante,
                NomeQuiz = quiz.Nome,
                Perguntas = perguntasAtivas,
                QuantidadeAcertos = quantidadeCorretas,
                QuantidadePerguntas = perguntasAtivas.Count
            };
        }

        public async Task<QuizRankingModel> GetQuizRanking(Guid idQuiz, Guid? idParticipante = null, int take = 10)
        {
            var quizValido = await _context.Quizzes
        .Include(q => q.IdEventoNavigation)
        .FirstOrDefaultAsync(q => q.Id == idQuiz && q.Deletado == false && q.IdEventoNavigation.Deletado == false);

            if (quizValido == null)
                throw new Exception("Quiz não encontrado ou inválido.");

            var participantes = _context.QuizParticipantes
                .Include(qp => qp.IdParticipanteNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                .Include(qp => qp.IdParticipanteNavigation)
                    .ThenInclude(p => p.IdEventoNavigation)
                .Where(qp =>
                    qp.IdQuiz == idQuiz &&
                    qp.IdParticipanteNavigation.IdUsuarioNavigation.Deletado == false &&
                    qp.IdParticipanteNavigation.IdEventoNavigation.Deletado == false)
                .Select(qp => new
                {
                    qp.IdParticipante,
                    Nome = qp.IdParticipanteNavigation.IdUsuarioNavigation.Nome
                });

            var respostas = _context.ParticipanteQuizResposta
                .Include(r => r.IdQuizPerguntaNavigation)
                .Include(r => r.IdQuizAlternativaNavigation)
                .Where(r =>
                    r.IdQuizPerguntaNavigation.IdQuiz == idQuiz &&
                    r.IdQuizPerguntaNavigation.Deletado == false &&
                    r.IdQuizAlternativaNavigation.Deletado == false)
                .Select(r => new
                {
                    r.IdParticipante,
                    r.HoraResposta,
                    r.IdQuizAlternativaNavigation.ECorreta
                });

            var rankingBase = await participantes
                .GroupJoin(
                    respostas,
                    p => p.IdParticipante,
                    r => r.IdParticipante,
                    (p, respostas) => new QuizRankingParticipanteModel
                    {
                        IdParticipante = p.IdParticipante,
                        Nome = p.Nome,
                        QuantidadeAcertos = respostas.Count(x => x.ECorreta),
                        TempoTotalRespostas = respostas.Any()
    ? (
        respostas.Count() == 1
            ? respostas.Min(x => x.HoraResposta.Hour * 3600 + x.HoraResposta.Minute * 60 + x.HoraResposta.Second)
            : (respostas.Max(x => x.HoraResposta.Hour * 3600 + x.HoraResposta.Minute * 60 + x.HoraResposta.Second)
                - respostas.Min(x => x.HoraResposta.Hour * 3600 + x.HoraResposta.Minute * 60 + x.HoraResposta.Second))
      )
    : 0
                    })
                .ToListAsync();

            var rankingOrdenado = rankingBase
                .OrderByDescending(r => r.QuantidadeAcertos)
                .ThenBy(r => r.TempoTotalRespostas)
                .ThenBy(r =>
                    r.QuantidadeAcertos == 0 && r.TempoTotalRespostas == 0
                        ? r.Nome
                        : null)
                .ToList();

            for (int i = 0; i < rankingOrdenado.Count; i++)
            {
                rankingOrdenado[i].Posicao = i + 1;
            }

            var top = rankingOrdenado.Take(take).ToList();

            if (idParticipante.HasValue)
            {
                var participante = rankingOrdenado.FirstOrDefault(p => p.IdParticipante == idParticipante.Value);
                if (participante != null && !top.Any(p => p.IdParticipante == participante.IdParticipante))
                {
                    top.Add(participante);
                }
            }

            return new QuizRankingModel
            {
                IdQuiz = idQuiz,
                Participantes = top
            };
        }
    }
}
