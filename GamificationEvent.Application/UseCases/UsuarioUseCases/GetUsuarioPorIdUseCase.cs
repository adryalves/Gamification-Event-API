using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.UsuarioUseCases
{
    public class GetUsuarioPorIdUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuarioPorIdUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> GetUsuario(Guid id)
        {
            return await _usuarioRepository.GetUsuarioPorId(id);
        }
    }
}
