using GamificationEvent.API.DTOs.Palestrante;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.API.Mappings
{
    public static class PalestranteMapper
    {
        public static Palestrante ConverterRequestParaCore(this PalestranteRequestDTO palestrante)
        {
            return new Palestrante
            {
                IdEvento = palestrante.IdEvento,
                Nome = palestrante.Nome,
                Email = palestrante.Email,
                Telefone = palestrante.Telefone,
                Profissao = palestrante.Profissao,
                DataNascimento = palestrante.DataNascimento,
                Linkedin = palestrante.Linkedin,
            };
        }

   
        public static Palestrante ConverterUpdateParaCore(this PalestranteUpdateDTO palestrante)
        {         

            return new Palestrante
            {
                Nome = palestrante.Nome,
                Email = palestrante.Email,
                Telefone = palestrante.Telefone,
                Profissao = palestrante.Profissao,
                DataNascimento = palestrante.DataNascimento,
                Linkedin = palestrante.Linkedin
            };
        }

       
        public static PalestranteResponseDTO ConverterCoreParaResponse(this Palestrante palestrante)
        {

            return new PalestranteResponseDTO
            {
                Id = palestrante.Id,
                IdEvento = palestrante.IdEvento,
                Nome = palestrante.Nome,
                Email = palestrante.Email,
                Telefone = palestrante.Telefone,
                Profissao = palestrante.Profissao,
                DataNascimento = palestrante.DataNascimento,
                Linkedin = palestrante.Linkedin,
                Deletado = palestrante.Deletado
            };
        }

     
        public static List<PalestranteResponseDTO> ConverterListaCoreParaListaResponse(this List<Palestrante> palestrantes)
        {
            if (palestrantes == null || !palestrantes.Any())
                return new List<PalestranteResponseDTO>();

            return palestrantes.Select(p => p.ConverterCoreParaResponse()).ToList();
        }
    }
}
