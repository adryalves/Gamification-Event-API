using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.UsuarioUseCases
{
    public class AtualizarUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AtualizarUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task AtualizarUsuario(Usuario usuario)
        {
            var usuarioExistente = await _usuarioRepository.GetUsuarioPorId(usuario.Id);

            if(usuarioExistente == null)
            {
                throw new Exception("Usuário não encontrado.");
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Cpf = usuario.Cpf;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.DataDeNascimento = usuario.DataDeNascimento;
            usuarioExistente.Telefone = usuario.Telefone;
            // ainda não decidi a lógica da foto então se fosse aq teria que add

            usuarioExistente.RedesSociais = usuario.RedesSociais.Select(r => new UsuarioRedeSocial
            {
                Plataforma = r.Plataforma,
                Url = r.Url
            }).ToList();

            await _usuarioRepository.AtualizarUsuario(usuarioExistente);
        }
    }
}
