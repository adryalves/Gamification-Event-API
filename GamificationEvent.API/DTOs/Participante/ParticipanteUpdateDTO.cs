using GamificationEvent.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Participante
{
    public class ParticipanteUpdateDTO
    {
        [Required]
        public Cargo Cargo { get; set; }
        [Required]
        public int Pontuacao { get; set; }
        public bool? PrimeiroParticipante { get; set; }
        public List<ParticipanteInteresseDTO> ParticipanteInteresses { get; set; } = new();
    }
}
