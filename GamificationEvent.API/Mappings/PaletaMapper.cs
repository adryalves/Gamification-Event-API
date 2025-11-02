using GamificationEvent.API.DTOs.PaletaCor;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class PaletaMapper
    {
        public static PaletaCor ConverterPaletaCore(this PaletaCorRequestDTO paletaRequestDTO)
        {
            return new PaletaCor
            {
                Nome = paletaRequestDTO.Nome,
                IdCor1 = paletaRequestDTO.IdCor1,
                IdCor2 = paletaRequestDTO.IdCor2,
                IdCor3 = paletaRequestDTO.IdCor3,
                IdCor4 = paletaRequestDTO.IdCor4,
            };
        }

        public static PaletaCorResponseDTO ConverterPaletaResponse(this PaletaCor paletaCor)
        {
            return new PaletaCorResponseDTO
            {
                Id = paletaCor.Id,
                Nome = paletaCor.Nome,
                IdCor1 = paletaCor.IdCor1,
                IdCor2 = paletaCor.IdCor2,
                IdCor3 = paletaCor.IdCor3,
                IdCor4 = paletaCor.IdCor4,
                Deletado = paletaCor.Deletado,
            };
        }
    }
}

 