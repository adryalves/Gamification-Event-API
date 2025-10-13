using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface ISenhaHash
    {
        public string CriptografarSenha(string senha);
        public bool VerificarSenha(string senhaDigitada, string senhaCadastrada);
    }
}
