using GamificationEvent.Core.Enums;
using GamificationEvent.Core.Validações;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Inscrito
{
    public class InscritosRequestDTO
    {
        [Required]
        public Guid IdEvento { get; set; }
        public List<InscritoRequest> Inscritos { get; set; } = new();
    }

    public class InscritoRequest
    {

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [ValidaçãoCPF(ErrorMessage = "CPF inválido.")]
        public string Cpf { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        public Cargo Cargo { get; set; }
    }
}
