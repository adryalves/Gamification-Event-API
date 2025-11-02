using GamificationEvent.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace GamificationEvent.API.DTOs.Participante
{
    public class ParticipanteRequestDTO
    {
        [Required]
        public Guid IdEvento { get; set; }

        [Required]
        public Guid IdUsuario { get; set; }

        [Required]
        public Cargo Cargo { get; set; }

       
        public int Pontuacao { get; set; }

        public bool? PrimeiroParticipante { get; set; }

        public List<ParticipanteInteresseDTO> ParticipanteInteresses { get; set; } = new();
    }

    public class ParticipanteInteresseDTO
    {
        [Required]
        public Guid IdInteresse { get; set; }

    }
}
