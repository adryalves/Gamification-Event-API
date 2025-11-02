using GamificationEvent.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Desafio
{
    public class DesafioUpdateDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; } = null!;

        public string? Descricao { get; set; }

        public string? Regra { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "A pontuação não pode ter valor 0.")]
        public int Pontuacao { get; set; }

        public Tipo_Desafio TipoDesafio { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "O Quantidade Desafio não pode ter valor 0.")]
        public int QuantidadeDesafio { get; set; }

        public DateTime? DataHoraInicio { get; set; }

        public DateTime? DataHoraFim { get; set; }
    }
}
