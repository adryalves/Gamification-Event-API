using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Resultados
{
    
    public record Resultado<T>
    {
       
        public T? Valor { get; init; }
        public bool Sucesso { get; init; }

       
        public string? MensagemDeErro { get; init; }

        
        private Resultado(T? valor, bool sucesso, string? mensagemDeErro)
        {
            Valor = valor;
            Sucesso = sucesso;
            MensagemDeErro = mensagemDeErro;
        }

       
        public static Resultado<T> Ok(T valor) =>
            new(valor, true, null);

        
        public static Resultado<T> Falha(string mensagemDeErro) =>
            
            new(default, false, mensagemDeErro);
    }
}
