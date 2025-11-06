namespace GamificationEvent.API.DTOs.PaletaCor
{
    public class PaletaCorResponseDTO
    {
        public Guid Id { get; set; }

        public string Nome { get; set; } = null!;

        public Guid IdCor1 { get; set; }

        public Guid IdCor2 { get; set; }

        public Guid IdCor3 { get; set; }

        public Guid IdCor4 { get; set; }

        public bool Deletado { get; set; }

    }
}
