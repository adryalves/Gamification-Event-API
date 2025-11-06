using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.DTOs.Inscrito
{
    public class InscritosResponseDTO
    {

        public Guid IdEvento { get; set; }
        public List<InscritoResponse> Inscritos { get; set; } = new();
    }

    public class InscritoResponse
    {
        public Guid Id { get; set; }
        public string Cpf { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public Cargo Cargo { get; set; }
    }
}


