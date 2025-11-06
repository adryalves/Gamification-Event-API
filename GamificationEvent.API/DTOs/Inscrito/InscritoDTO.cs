using GamificationEvent.Core.Enums;
using GamificationEvent.Core.Validações;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Inscrito
{
    public class InscritoDTO
    {

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [ValidaçãoCPF(ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; } = null!;

        [Required]
        public Guid IdEvento { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;

        [Required]
        public Cargo Cargo { get; set; }
    }
}
