using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.UsuarioUseCases
{
    public class GetUsuariosUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuariosUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
          return await _usuarioRepository.GetUsuarios();
        }
    }
}