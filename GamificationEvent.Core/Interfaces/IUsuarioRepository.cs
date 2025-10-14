using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario> AdicionarUsuario(Usuario usuario);
        Task<bool> EmailExiste(string email);
        Task<bool> CpfExiste(string cpf);
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuarioPorId(Guid id);
        Task<bool> DeletarUsuario(Guid id);
        Task<bool> AtualizarUsuario(Usuario usuario);
    }
}
