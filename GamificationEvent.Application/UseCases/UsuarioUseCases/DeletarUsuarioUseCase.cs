using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Resultados;
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

        public async Task<Resultado<bool>> DeletarUsuario(Guid id)
        {
            var usuarioExistente = await _usuarioRepository.GetUsuarioPorId(id);

            if (usuarioExistente == null) return Resultado<bool>.Falha($"Usuário de id {id} não encontrado.");

            var resultado = await _usuarioRepository.DeletarUsuario(id);
            return Resultado<bool>.Ok(resultado);

        }
    }
}
