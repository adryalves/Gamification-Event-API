using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IParticipanteRepository
    {
        Task<Guid> AdicionarParticipante(Participante participanteCore);
        Task<bool> ParticipanteJaExiste(Guid idEvento, Guid idUsuario);
        Task<Participante> GetParticipantePorId(Guid id);
        Task<List<Participante>> GetParticipantesPorIdEvento(Guid idEvento);
        Task<bool> AtualizarParticipante(Participante participante);
        Task<Participante> GetParticipantePorCpf(string cpf);
        Task<bool> AtualizarPontuacao(Guid idParticipante, int pontuacao);

    }
}
