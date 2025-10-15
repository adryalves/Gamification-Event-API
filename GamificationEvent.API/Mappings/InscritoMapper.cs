using GamificationEvent.API.DTOs;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.Mappings
{
    public static class InscritoMapper
    {
        public static List<InscritosResponseDTO> ConverterTodosOsInscritosParaResponseDTO(this List<Inscrito> inscritos)
        {
            var agrupadoPorEvento = inscritos
                .GroupBy(i => i.IdEvento)
                .Select(grupo => new InscritosResponseDTO
                {
                    IdEvento = grupo.Key,
                    Inscritos = grupo.Select(i => new InscritoResponse
                    {
                        Id = i.Id,
                        Cpf = i.Cpf,
                        Nome = i.Nome,
                        Cargo = i.Cargo
                    }).ToList()
                })
                .ToList();

            return agrupadoPorEvento;
        }

        public static InscritosResponseDTO ConverteInscritosPorEventoParaResponseDTO(this List<Inscrito> inscritos)
        {
            var incritosDTO = new InscritosResponseDTO
            {
                IdEvento = inscritos.First().IdEvento,
                Inscritos = inscritos.Select(i => new InscritoResponse
                {
                    Id = i.Id,
                    Cpf = i.Cpf,
                    Nome = i.Nome,
                    Cargo = i.Cargo,
                }).ToList()
            };
            return incritosDTO;
        }

        public static List<Inscrito> ConverterListaDeTodosParaCore(this InscritosRequestDTO inscritosDTO)
        {
            var inscritos = inscritosDTO.Inscritos.Select(i => new Inscrito
            {
                IdEvento = inscritosDTO.IdEvento,
                Cpf = i.Cpf,
                Nome = i.Nome,
                Cargo = i.Cargo,
            }).ToList();

            return inscritos;
        }

        public static Inscrito ConverterUmInscritoParaCore(this InscritoDTO inscritoDTO)
        {
            return new Inscrito
            {
                Cpf = inscritoDTO.Cpf,
                IdEvento = inscritoDTO.IdEvento,
                Nome = inscritoDTO.Nome,
                Cargo = inscritoDTO.Cargo,
            };
        }

    }
}
