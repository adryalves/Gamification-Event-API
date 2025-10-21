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
    public class AtualizarUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AtualizarUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Resultado<bool>> AtualizarUsuario(Usuario usuario)
        {
            var usuarioExistente = await _usuarioRepository.GetUsuarioPorId(usuario.Id);

            if(usuarioExistente == null) return Resultado<bool>.Falha($"Uusario de id {usuario.Id} não encontrado.");


            var emailExiste = await _usuarioRepository.EmailExiste(usuario.Email);
            var cpfExiste = await _usuarioRepository.CpfExiste(usuario.Cpf);

            if(emailExiste && (usuarioExistente.Email != usuario.Email))
                return Resultado<bool>.Falha("Esse email já existe para outro usuário");            

            if(cpfExiste && usuarioExistente.Cpf != usuario.Cpf)
                return Resultado<bool>.Falha("Esse cpf já existe para outro usuário");

            var resultado = await _usuarioRepository.AtualizarUsuario(usuario);
            return Resultado<bool>.Ok(resultado);

        }
    }
}
