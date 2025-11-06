using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamificationEvent.Core.Resultados;
using System.Text.RegularExpressions;
using GamificationEvent.Core.Models;

namespace GamificationEvent.Application.UseCases.UsuarioUseCases
{

    public class CadastrarUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ISenhaHash _senhaHash;
        private readonly IAuthenticate _authenticate;

        public CadastrarUsuarioUseCase(IUsuarioRepository usuarioRepository, ISenhaHash senhaHash, IAuthenticate authenticate)
        {
            _usuarioRepository = usuarioRepository;
            _senhaHash = senhaHash;
            _authenticate = authenticate;
        }

        public async Task<Resultado<UsuarioTokenModel>> CadastrarUsuario(Usuario usuario, string senha)
        {

            string padraoRegex = @"\D"; 

            string cpfValido = Regex.Replace(usuario.Cpf, padraoRegex, "");

            var cpfUsuarioExiste = await _usuarioRepository.CpfExiste(cpfValido);
            var emailUsuarioExiste = await _usuarioRepository.EmailExiste(usuario.Email);

            var usuarioExistenteEDeletado = await _usuarioRepository.CpfJaFoiCadastradoEDeletado(cpfValido);

            usuario.Cpf = cpfValido;

            if (cpfUsuarioExiste)
                return Resultado<UsuarioTokenModel>.Falha("CPF já cadastrado");

            if (emailUsuarioExiste)
                return Resultado<UsuarioTokenModel>.Falha("Email já cadastrado");


            usuario.SenhaHash = _senhaHash.CriptografarSenha(senha);

            string telefoneValido = Regex.Replace(usuario.Telefone ?? string.Empty, @"[\s\-\(\)]", "");
            usuario.Telefone = telefoneValido;

            if (usuarioExistenteEDeletado != null)
            {
                usuarioExistenteEDeletado.Deletado = false;
                usuarioExistenteEDeletado.Nome = usuario.Nome;
                usuarioExistenteEDeletado.Email = usuario.Email;
                usuarioExistenteEDeletado.Telefone = usuario.Telefone;
                usuarioExistenteEDeletado.DataDeNascimento = usuario.DataDeNascimento;
                usuarioExistenteEDeletado.Foto = usuario.Foto;
                usuarioExistenteEDeletado.SenhaHash = usuario.SenhaHash;
                usuarioExistenteEDeletado.RedesSociais = usuario.RedesSociais;

                var atualizacao = await _usuarioRepository.AtualizarUsuario(usuarioExistenteEDeletado);
                if(atualizacao)
                {
                    var token = _authenticate.GenerateToken(usuario.Id, usuario.Email);
                    return Resultado<UsuarioTokenModel>.Ok(new UsuarioTokenModel { Token = token});
                }
                else
                {
                    return Resultado<UsuarioTokenModel>.Falha("Algo deu errado no cadastro de usuário");
                }
              
            }

            usuario.Id = Guid.NewGuid();

            foreach(var redeSocial in usuario.RedesSociais)
            {
                redeSocial.Id = Guid.NewGuid();
                redeSocial.IdUsuario = usuario.Id;
            }

            var resultado = await _usuarioRepository.AdicionarUsuario(usuario);
            if (resultado != null)
            {
                var token = _authenticate.GenerateToken(usuario.Id, usuario.Email);
                return Resultado<UsuarioTokenModel>.Ok(new UsuarioTokenModel { Token = token });
            }

            return Resultado<UsuarioTokenModel>.Falha("Algo deu errado no cadastro de usuário");

        }
    }
}
