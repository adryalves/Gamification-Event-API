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
using GamificationEvent.Core.Entidades;
using SkiaSharp;


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

        var participanteEstaNesseQuiz = await _context.QuizParticipantes.FirstOrDefaultAsync(x => x.IdParticipante == idParticipante
            && x.IdQuiz == idQuiz && !x.IdQuizNavigation.Deletado && !x.IdQuizNavigation.IdEventoNavigation.Deletado &&
            (x.IdQuizNavigation.IdSubEvento == null || !x.IdQuizNavigation.IdSubEventoNavigation.Deletado) && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado);

            if (participanteEstaNesseQuiz == null) return null;


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
          .FirstOrDefaultAsync(q => q.Id == idQuiz && !q.Deletado && !q.IdEventoNavigation.Deletado);

            if (quizValido == null)
                return null;

            
            var participantes = _context.QuizParticipantes
                .Include(qp => qp.IdParticipanteNavigation)
                    .ThenInclude(p => p.IdUsuarioNavigation)
                .Where(qp =>
                    qp.IdQuiz == idQuiz &&
                    !qp.IdParticipanteNavigation.IdUsuarioNavigation.Deletado)
                .Select(qp => new
                {
                    qp.IdParticipante,
                    Nome = qp.IdParticipanteNavigation.IdUsuarioNavigation.Nome
                });

            
            var respostas = await _context.ParticipanteQuizResposta
                .Include(r => r.IdQuizPerguntaNavigation)
                .Include(r => r.IdQuizAlternativaNavigation)
                .Where(r =>
                    r.IdQuizPerguntaNavigation.IdQuiz == idQuiz &&
                    !r.IdQuizPerguntaNavigation.Deletado &&
                    !r.IdQuizAlternativaNavigation.Deletado)
                .Select(r => new
                {
                    r.IdParticipante,
                    r.IdQuizPergunta,
                    r.HoraResposta,
                    r.IdQuizAlternativaNavigation.ECorreta
                })
                .ToListAsync();



            var rankingBase = participantes
                .AsEnumerable()
                .Select(p =>
                {
                    var respostasDoParticipante = respostas
                        .Where(r => r.IdParticipante == p.IdParticipante)
                        .ToList();

                    double pontuacao = 0.000;
                    int acertos = 0;

                    foreach (var r in respostasDoParticipante)
                    {
                        double segundos = (r.HoraResposta.Hour * 3600 + r.HoraResposta.Minute * 60 + r.HoraResposta.Second);

                        if (segundos == 0)
                            segundos = 1; 

                        
                        if (r.ECorreta)
                        {
                            pontuacao += 100000.0 / segundos; 
                            acertos++;
                        }
                        else
                        {
                            pontuacao += 10.0 / segundos;

                        }
                    }

                    return new QuizRankingParticipanteModel
                    {
                        IdParticipante = p.IdParticipante,
                        Nome = p.Nome,
                        QuantidadeAcertos = acertos,
                        Pontuacao = Math.Round(pontuacao * 100, 2)
                    };
                })
                .ToList();

            
            var rankingOrdenado = rankingBase
                .OrderByDescending(r => r.Pontuacao)
                .ThenByDescending(r => r.QuantidadeAcertos)
                .ThenBy(r => r.Nome)
                .ToList();

           
            for (int i = 0; i < rankingOrdenado.Count; i++)
                rankingOrdenado[i].Posicao = i + 1;

            
            var top = rankingOrdenado.Take(take).ToList();

            
            if (idParticipante.HasValue)
            {
                var participante = rankingOrdenado.FirstOrDefault(p => p.IdParticipante == idParticipante.Value);
                if (participante != null && !top.Any(p => p.IdParticipante == participante.IdParticipante))
                    top.Add(participante);
            }

            return new QuizRankingModel
            {
                IdQuiz = idQuiz,
                Participantes = top
            };
        }

        public async Task<bool> ParticipanteEstaNesseQuiz (Guid idParticipante, Guid idQuiz)
        {
            var existe = await _context.QuizParticipantes.FirstOrDefaultAsync(x => x.IdParticipante == idParticipante
            && x.IdQuiz == idQuiz && !x.IdQuizNavigation.Deletado && !x.IdQuizNavigation.IdEventoNavigation.Deletado &&
            (x.IdQuizNavigation.IdSubEvento == null || !x.IdQuizNavigation.IdSubEventoNavigation.Deletado) && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado);

            return existe != null;
        }

        public async Task<bool> ParticipanteJaRespondeuEssaPergunta(Guid idQuizPergunta, Guid idParticipante)
        {
            var existe = await _context.ParticipanteQuizResposta.FirstOrDefaultAsync(x => x.IdQuizPergunta == idQuizPergunta
            && x.IdParticipante == idParticipante);

            return existe != null;
        }
        public async Task<bool> DeletarTodasAsRespostasDoQuiz(Guid idQuiz)
        {

            var respostas = await _context.ParticipanteQuizResposta
        .Where(r => r.IdQuizPerguntaNavigation.IdQuiz == idQuiz && !r.IdQuizPerguntaNavigation.IdQuizNavigation.Deletado)
        .ToListAsync();

            if (!respostas.Any())
                return false;

            _context.ParticipanteQuizResposta.RemoveRange(respostas);
            await _context.SaveChangesAsync();
            return true;

        }
    }
}
