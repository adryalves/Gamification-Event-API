using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.DTOs.Inscrito
{
    public class InscritosRequestDTO
    {
        public Guid IdEvento { get; set; }
        public List<InscritoRequest> Inscritos { get; set; } = new();
    }

    public class InscritoRequest
    {
        public string Cpf { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public Cargo Cargo { get; set; }
    }
}
