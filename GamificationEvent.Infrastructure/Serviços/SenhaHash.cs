using BCrypt.Net;
using GamificationEvent.Core.Interfaces;

namespace GamificationEvent.Infrastructure.Serviços
{
    public class SenhaHash : ISenhaHash
    {
        private const int WorkFactor = 12;


        public string CriptografarSenha(string senha)
        {

            string senha_hash = BCrypt.Net.BCrypt.HashPassword(
                senha,
                WorkFactor
            );

            return senha_hash;
        }


        public bool VerificarSenha(string senhaDigitada, string senhaCadastrada)
        {

            bool senhaValida = BCrypt.Net.BCrypt.Verify(senhaDigitada, senhaCadastrada);

            return senhaValida;
        }

    }
}
