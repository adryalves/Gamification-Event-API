using GamificationEvent.Infrastructure.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreQuiz = GamificationEvent.Core.Entidades.Quiz;
using InfraQuiz = GamificationEvent.Infrastructure.Data.Persistence.Quiz;
using CorePergunta = GamificationEvent.Core.Entidades.QuizPergunta;
using InfraPergunta = GamificationEvent.Infrastructure.Data.Persistence.QuizPergunta;
using CoreAlternativa = GamificationEvent.Core.Entidades.QuizAlternativa;
using InfraAlternativa = GamificationEvent.Infrastructure.Data.Persistence.QuizAlternativa;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Models;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly AppDbContext _context;

        public QuizRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarQuiz(CoreQuiz quiz)
        {
            var quizDB = new InfraQuiz
            {
                Id = Guid.NewGuid(),
                IdEvento = quiz.IdEvento,
                IdSubEvento = quiz.IdSubEvento,
                IdPatrocinador = quiz.IdPatrocinador,
                Nome = quiz.Nome,
                Tema = quiz.Tema,
                DataQuiz = quiz.DataQuiz,
                HoraInicio = quiz.HoraInicio,
                HoraFim = quiz.HoraFim
            };

            _context.Quizzes.Add(quizDB);
            await _context.SaveChangesAsync();
            return quizDB.Id;

        }

        public async Task<Guid> AdicionarPergunta(CorePergunta pergunta)
        {
            var perguntaDB = new InfraPergunta
            {
                Id = Guid.NewGuid(),
                IdQuiz = pergunta.IdQuiz,
                Enunciado = pergunta.Enunciado
            };

            _context.QuizPergunta.Add(perguntaDB);
            await _context.SaveChangesAsync();
            return perguntaDB.Id;
        }

        public async Task<List<CoreAlternativa>> AdicionarAlternativas(List<CoreAlternativa> alternativas)
        {
            var alternativasDB = alternativas.Select(x => new InfraAlternativa
            {
                Id = Guid.NewGuid(),
                IdQuizPergunta = x.IdQuizPergunta,
                Resposta = x.Resposta,
                ECorreta = x.ECorreta
            });

            _context.QuizAlternativas.AddRange(alternativasDB);
            await _context.SaveChangesAsync();

            return alternativasDB.Select(x => new CoreAlternativa
            {
                Id = x.Id,
                IdQuizPergunta = x.IdQuizPergunta,
                Resposta = x.Resposta,
                ECorreta = x.ECorreta
            }).ToList();
        }

        public async Task<List<CoreQuiz>> GetQuizzesPorIdEvento(Guid idEvento) {

            var quizzes = await _context.Quizzes
                .Where(x =>
                    x.IdEvento == idEvento &&
                    !x.Deletado &&
                    !x.IdEventoNavigation.Deletado &&
                    (x.IdSubEvento == null || !x.IdSubEventoNavigation.Deletado))
                .ToListAsync();

            return quizzes.Select(x => new CoreQuiz
            {
                Id = x.Id,
                IdEvento = x.IdEvento,
                IdSubEvento = x.IdSubEvento,
                IdPatrocinador = x.IdPatrocinador,
                Nome = x.Nome,
                Tema = x.Tema,
                DataQuiz = x.DataQuiz,
                HoraInicio = x.HoraInicio,
                HoraFim = x.HoraFim,
                Deletado = x.Deletado
            }).ToList();
        }

    public async Task<QuizPerguntasEAlternativasModel> GetTodasAsPerguntasPorIdQuiz(Guid idQuiz)
    {
        var resultado = await _context.Quizzes
    .Where(q => q.Id == idQuiz
                && !q.Deletado
                && !q.IdEventoNavigation.Deletado
                && (q.IdSubEventoNavigation == null || !q.IdSubEventoNavigation.Deletado))
    .Select(q => new QuizPerguntasEAlternativasModel
    {
        IdQuiz = q.Id,
        Perguntas = q.QuizPergunta
            .Where(p => !p.Deletado)
            .Select(p => new QuizPerguntaCompletaModel
            {
                Id = p.Id,
                Enunciado = p.Enunciado,
                Deletado = p.Deletado,
                PerguntaAlternativas = p.QuizAlternativas
                    .Where(a => !a.Deletado)
                    .Select(a => new QuizAlternativasCompletasModel
                    {
                        Id = a.Id,
                        Resposta = a.Resposta,
                        ECorreta = a.ECorreta,
                        Deletado = a.Deletado
                    })
                    .ToList()
            })
            .ToList()
    })
    .FirstOrDefaultAsync();

        return resultado;
    }

        public async Task<bool> AtualizarQuiz(Guid id, CoreQuiz quiz)
        {
            var quizExistente = await _context.Quizzes.FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.Deletado
            && !x.IdEventoNavigation.Deletado && (x.IdSubEvento == null || !x.IdSubEventoNavigation.Deletado));

            if (quizExistente == null) return false;

            quizExistente.Nome = quiz.Nome;
            quizExistente.Tema = quiz.Tema;
            quizExistente.DataQuiz = quiz.DataQuiz;
            quizExistente.HoraInicio = quiz.HoraInicio;
            quizExistente.HoraFim = quiz.HoraFim;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarQuiz(Guid id)
        {
            var quizExistente = await _context.Quizzes.FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.Deletado
           && !x.IdEventoNavigation.Deletado && (x.IdSubEvento == null || !x.IdSubEventoNavigation.Deletado));

            if (quizExistente == null) return false;

            quizExistente.Deletado = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarPergunta(Guid idPergunta, CorePergunta pergunta)
        {
            var perguntaExistente = await _context.QuizPergunta.FirstOrDefaultAsync(x => x.Id == idPergunta
            && !x.Deletado && !x.IdQuizNavigation.Deletado && !x.IdQuizNavigation.IdEventoNavigation.Deletado
            && (x.IdQuizNavigation.IdSubEvento == null || !x.IdQuizNavigation.IdSubEventoNavigation.Deletado));

            if (perguntaExistente == null) return false;

            perguntaExistente.Enunciado = pergunta.Enunciado;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarPergunta(Guid idPergunta)
        {
            var perguntaExistente = await _context.QuizPergunta.FirstOrDefaultAsync(x => x.Id == idPergunta
           && !x.Deletado && !x.IdQuizNavigation.Deletado && !x.IdQuizNavigation.IdEventoNavigation.Deletado
           && (x.IdQuizNavigation.IdSubEvento == null || !x.IdQuizNavigation.IdSubEventoNavigation.Deletado));

            if (perguntaExistente == null) return false;

            perguntaExistente.Deletado = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarAlternativa(Guid idAlternativa)
        {
            var alternativaExistente = await _context.QuizAlternativas.FirstOrDefaultAsync(x => x.Id == idAlternativa
            && !x.Deletado && !x.IdQuizPerguntaNavigation.Deletado && !x.IdQuizPerguntaNavigation.IdQuizNavigation.Deletado
            && !x.IdQuizPerguntaNavigation.IdQuizNavigation.IdEventoNavigation.Deletado &&
            (x.IdQuizPerguntaNavigation.IdQuizNavigation.IdSubEvento == null || !x.IdQuizPerguntaNavigation.IdQuizNavigation.IdSubEventoNavigation.Deletado));

            if (alternativaExistente == null) return false;

            alternativaExistente.Deletado = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CoreQuiz> GetQuizPorId(Guid id)
        {
            var quizExistente = await _context.Quizzes.FirstOrDefaultAsync(x => x.Id.Equals(id) && !x.Deletado
          && !x.IdEventoNavigation.Deletado && (x.IdSubEvento == null || !x.IdSubEventoNavigation.Deletado));

            if (quizExistente == null) return null;

            return new CoreQuiz
            {
                Id = quizExistente.Id,
                IdEvento = quizExistente.IdEvento,
                IdSubEvento = quizExistente.IdSubEvento,
                IdPatrocinador = quizExistente.IdPatrocinador,
                Nome = quizExistente.Nome,
                Tema = quizExistente.Tema,
                DataQuiz = quizExistente.DataQuiz,
                HoraInicio = quizExistente.HoraInicio,
                HoraFim = quizExistente.HoraFim,
                Deletado = quizExistente.Deletado
            };
        }

        public async Task<CorePergunta> GetPerguntaPorId(Guid idPergunta)
        {
            var pergunta = await _context.QuizPergunta.FirstOrDefaultAsync(x => x.Id == idPergunta
           && !x.Deletado && !x.IdQuizNavigation.Deletado && !x.IdQuizNavigation.IdEventoNavigation.Deletado
           && (x.IdQuizNavigation.IdSubEvento == null || !x.IdQuizNavigation.IdSubEventoNavigation.Deletado));

            if (pergunta == null) return null;

            return new CorePergunta
            {
                Id = pergunta.Id,
                IdQuiz = pergunta.IdQuiz,
                Enunciado = pergunta.Enunciado,
                Deletado = pergunta.Deletado
            };
        }

        public async Task<CoreAlternativa> GetAlternativaPoId(Guid idAlternativa, Guid idQuizPergunta)
        {
            var alternativaExistente = await _context.QuizAlternativas.FirstOrDefaultAsync(x => x.Id == idAlternativa && x.IdQuizPergunta == idQuizPergunta
            && !x.Deletado && !x.IdQuizPerguntaNavigation.Deletado && !x.IdQuizPerguntaNavigation.IdQuizNavigation.Deletado
            && !x.IdQuizPerguntaNavigation.IdQuizNavigation.IdEventoNavigation.Deletado &&
            (x.IdQuizPerguntaNavigation.IdQuizNavigation.IdSubEvento == null || !x.IdQuizPerguntaNavigation.IdQuizNavigation.IdSubEventoNavigation.Deletado));

            if (alternativaExistente == null) return null;

            return new CoreAlternativa
            {
                Id = alternativaExistente.Id,
                IdQuizPergunta = alternativaExistente.IdQuizPergunta,
                Resposta = alternativaExistente.Resposta,
                ECorreta = alternativaExistente.ECorreta,
                Deletado = alternativaExistente.Deletado
            };
        }

    }
}
