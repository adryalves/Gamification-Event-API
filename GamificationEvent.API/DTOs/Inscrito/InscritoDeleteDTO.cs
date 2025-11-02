using GamificationEvent.Core.Validações;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Inscrito
{
    public class InscritoDeleteDTO
    {
        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [ValidaçãoCPF(ErrorMessage = "CPF inválido.")]
        public string Cpf {  get; set; }

        [Required]
        public Guid IdEvento { get; set; }

    }
}
