using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Models;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.UsuarioUseCases
{
    public class UsuarioLoginUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaHash _senhaHash;
        private readonly IAuthenticate _authenticate;

        public UsuarioLoginUseCase(IUsuarioRepository usuarioRepository, ISenhaHash senhaHash, IAuthenticate authenticate)
        {
            _usuarioRepository = usuarioRepository;
            _senhaHash = senhaHash;
            _authenticate = authenticate;
        }

        public async Task<Resultado<UsuarioTokenModel>> LoginUsario(UsuarioLoginModel login)
        {
            var usuario = await _usuarioRepository.GetUsuarioPorEmail(login.Email);

            if (usuario == null) return Resultado<UsuarioTokenModel>.Falha("Email ou senha inválido");

            var autenticacao = _senhaHash.VerificarSenha(login.Senha, usuario.SenhaHash);

            if(!autenticacao) return Resultado<UsuarioTokenModel>.Falha("Email ou senha inválido");

            var token = _authenticate.GenerateToken(usuario.Id, login.Email);
            return Resultado<UsuarioTokenModel>.Ok(new UsuarioTokenModel { Token = token });


        }
    }
}
