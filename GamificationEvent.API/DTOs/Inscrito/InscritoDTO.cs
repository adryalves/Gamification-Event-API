using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.DTOs.Inscrito
{
    public class InscritoDTO
    {
        public string Cpf { get; set; } = null!;
        public Guid IdEvento { get; set; }
        public string Nome { get; set; } = null!;
        public Cargo Cargo { get; set; }
    }
}
