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
        Task<Desafio> DesafioJaCadastradoNesseEvento(Guid idEvento, Tipo_Desafio tipoDesafio, int quantidade);

    }
}
