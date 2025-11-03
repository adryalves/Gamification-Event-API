using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamificationEvent.Core.Resultados;
using System.Text.RegularExpressions;

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

        public async Task<Resultado<Usuario>> CadastrarUsuario(Usuario usuario, string senha)
        {

            string padraoRegex = @"\D"; 

            string cpfValido = Regex.Replace(usuario.Cpf, padraoRegex, "");

            var cpfUsuarioExiste = await _usuarioRepository.CpfExiste(cpfValido);
            var emailUsuarioExiste = await _usuarioRepository.EmailExiste(usuario.Email);

            var usuarioExistenteEDeletado = await _usuarioRepository.CpfJaFoiCadastradoEDeletado(cpfValido);

            usuario.Cpf = cpfValido;

            if (cpfUsuarioExiste)
                return Resultado<Usuario>.Falha("CPF já cadastrados");

            if (emailUsuarioExiste)
                return Resultado<Usuario>.Falha("Email já cadastrado");


            usuario.SenhaHash = _senhaHash.CriptografarSenha(senha);

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

                await _usuarioRepository.AtualizarUsuario(usuarioExistenteEDeletado);
                return Resultado<Usuario>.Ok(usuarioExistenteEDeletado);
            }


            usuario.Id = Guid.NewGuid();

            foreach(var redeSocial in usuario.RedesSociais)
            {
                redeSocial.Id = Guid.NewGuid();
                redeSocial.IdUsuario = usuario.Id;
            }

            var resultado = await _usuarioRepository.AdicionarUsuario(usuario);
            return Resultado<Usuario>.Ok(resultado);


        }
    }
}
