using GamificationEvent.API.DTOs.Participante;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;

namespace GamificationEvent.API.Mappings
{
    public static class ParticipanteMapper
    {
        public static Participante ConverterRequestParaCore(this ParticipanteRequestDTO participanteDTO)
        {
            return new Participante
            {
                IdEvento = participanteDTO.IdEvento,
                IdUsuario = participanteDTO.IdUsuario,
                Cargo = participanteDTO.Cargo,
                Pontuacao = participanteDTO.Pontuacao,
                PrimeiroParticipante = participanteDTO.PrimeiroParticipante,
                ParticipanteInteresses = participanteDTO.ParticipanteInteresses.Select(i => new ParticipanteInteresse
                {
                    IdInteresse = i.IdInteresse,
                }).ToList(),
            };
        }

        public static Participante ConverterUpdateParCore(this ParticipanteUpdateDTO participanteDTO)
        {
            return new Participante
            {
               
                Cargo = participanteDTO.Cargo,
                Pontuacao = participanteDTO.Pontuacao,
                ParticipanteInteresses = participanteDTO.ParticipanteInteresses.Select(i => new ParticipanteInteresse
                {
                    IdInteresse = i.IdInteresse,
                }).ToList(),
            };
        }

        public static List<ParticipanteResponseDTO> ConverterParaListaResponse(this List<Participante> participantes)
        {
            var listaParticipantesDTO = new List<ParticipanteResponseDTO>();

            foreach(var participante in participantes)
            {
                var participanteDTO = new ParticipanteResponseDTO
                {
                    Id = participante.Id,
                    IdEvento = participante.IdEvento,
                    IdUsuario = participante.IdUsuario,
                    Cargo = participante.Cargo,
                    Pontuacao = participante.Pontuacao,
                    PrimeiroParticipante = participante.PrimeiroParticipante,
                    DataHoraCriacao = participante.DataHoraCriacao,
                    ParticipanteInteresses = participante.ParticipanteInteresses.Select(p => new ParticipanteInteresseResponseDTO
                    {
                        Id = p.Id,
                        IdInteresse = p.IdInteresse
                    }).ToList()
                };
                listaParticipantesDTO.Add(participanteDTO);
            }
            return listaParticipantesDTO;
        }

        public static ParticipanteResponseDTO ConverterParaResponse(this Participante participante)
        {
            
                var participanteDTO = new ParticipanteResponseDTO
                {
                    Id = participante.Id,
                    IdEvento = participante.IdEvento,
                    IdUsuario = participante.IdUsuario,
                    Cargo = participante.Cargo,
                    Pontuacao = participante.Pontuacao,
                    PrimeiroParticipante = participante.PrimeiroParticipante,
                    DataHoraCriacao = participante.DataHoraCriacao,
                    ParticipanteInteresses = participante.ParticipanteInteresses.Select(p => new ParticipanteInteresseResponseDTO
                    {
                        Id = p.Id,
                        IdInteresse = p.IdInteresse
                    }).ToList()
                };

            return participanteDTO;
        }

        
    }
}


