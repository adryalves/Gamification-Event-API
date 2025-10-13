using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.UsuarioUseCases
{
    public class DeletarUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public DeletarUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> DeletarUsuario(Guid id)
        {
            return await _usuarioRepository.DeletarUsuario(id);
        }
    }
}
