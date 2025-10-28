using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IDesafioRepository
    {
        Task<Guid> AdicionarDesafio(Desafio desafio);
        Task<bool> AtualizarDesafio(Desafio desafio);
        Task<bool> DeletarDesafio(Guid id);
        Task<Desafio> GetDesafioPorId(Guid id);
        Task<List<Desafio>> GetDesafiosPorIdEvento(Guid idEvento);
        Task<Desafio> DesafioJaCadastradoNesseEvento(Guid idEvento, Tipo_Desafio tipoDesafio);
        Task<Guid> CadastrarDesafioParticipante(DesafioParticipante desafioPart);
        Task<DesafioParticipante> ParticipanteEstaParticipandoDoDesafio(Guid idDesafio, Guid idParticipante);
        Task<bool> AtualizarDesafioParticipante(Guid id, int quantidade, Status_Desafio? status_Desafio = null);
        Task<List<DesafioParticipante>> GetDesafiosParticipantePorIdParticipante(Guid idParticipante);




    }
}
