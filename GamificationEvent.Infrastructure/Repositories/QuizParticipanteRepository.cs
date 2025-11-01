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

namespace GamificationEvent.Infrastructure.Repositories
{
    public class QuizParticipanteRepository
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

     
    }
}
