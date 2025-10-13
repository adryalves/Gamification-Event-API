using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.UsuarioUseCases
{

    public class CadastrarUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaHash _senhaHash;

        public CadastrarUsuarioUseCase(IUsuarioRepository usuarioRepository, ISenhaHash senhaHash)
        {
            _usuarioRepository = usuarioRepository;
            _senhaHash = senhaHash;
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario, string senha)
        {

            var cpfUsuarioExiste = await _usuarioRepository.CpfExiste(usuario.Cpf);
            var emailUsuarioExiste = await _usuarioRepository.CpfExiste(usuario.Email);

            if (cpfUsuarioExiste)
                throw new InvalidOperationException("CPF já cadastrados");

            if (emailUsuarioExiste)
                throw new InvalidOperationException("Email já cadastrado");


            usuario.SenhaHash = _senhaHash.CriptografarSenha(senha);
            usuario.Id = Guid.NewGuid();

            foreach(var redeSocial in usuario.RedesSociais)
            {
                redeSocial.Id = Guid.NewGuid();
                redeSocial.IdUsuario = usuario.Id;
            }

            return await _usuarioRepository.AdicionarUsuario(usuario);

        }
    }
}
