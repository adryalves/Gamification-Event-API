using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.ParticipanteUseCases;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipanteController : ControllerBase
    {
        private readonly AtualizarParticipanteUseCase _atualizarParticipanteUseCase;
        private readonly CadastrarParticipanteUseCase _cadastrarParticipanteUseCase;
        private readonly GetParticipantePorIdUseCase _getParticipantePorIdUseCase;
        private readonly GetParticipantesPorIdEventoUseCase _getParticipantesPorIdEventoUseCase;

        public ParticipanteController(AtualizarParticipanteUseCase atualizarParticipanteUseCase, CadastrarParticipanteUseCase cadastrarParticipanteUseCase, GetParticipantePorIdUseCase getParticipantePorIdUseCase, GetParticipantesPorIdEventoUseCase getParticipantesPorIdEventoUseCase)
        {
            _atualizarParticipanteUseCase = atualizarParticipanteUseCase;
            _cadastrarParticipanteUseCase = cadastrarParticipanteUseCase;
            _getParticipantePorIdUseCase = getParticipantePorIdUseCase;
            _getParticipantesPorIdEventoUseCase = getParticipantesPorIdEventoUseCase;
        }
        [HttpPost("CadastrarParticipante")]
        public async Task<IActionResult> CadastrarParticipante(ParticipanteRequestDTO participanteDTO)
        {
            try
            {
                if (participanteDTO == null) return BadRequest("Insira dados");

                if (participanteDTO.IdEvento == Guid.Empty || participanteDTO.IdEvento == null ||
                    participanteDTO.IdUsuario == Guid.Empty || participanteDTO.IdUsuario == null)
                    return BadRequest("Insira Ids válidos");

                var participante = participanteDTO.ConverterRequestParaCore();

                var participanteId = await _cadastrarParticipanteUseCase.CadastrarParticipante(participante);
                return Ok(participanteId);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarParticipante")]
        public async Task<IActionResult> AtualizarParticipante(Guid id, [FromBody] ParticipanteUpdateDTO participanteDTO)
        {
            try
            {
                if (id == null || id == Guid.Empty) return BadRequest("Insira um id válido");

                var participante = participanteDTO.ConverterUpdateParCore();
                participante.Id = id;

                var sucesso = await _atualizarParticipanteUseCase.AtualizarParticipante(participante);

                if (!sucesso)
                    return BadRequest("Algo deu errado na atualização");

                return Ok("Participante atualizada");

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetParticipantesPorIdEvento")]
        public async Task<IActionResult> GetParticipantesPorIdEvento(Guid idEvento)
        {
            try
            {
                if (idEvento == Guid.Empty || idEvento == null)
                    return BadRequest("Insira um id válido");

                var participantes = await _getParticipantesPorIdEventoUseCase.GetParticipantesPorIdEvento(idEvento);
                var participantesResponse = participantes.ConverterParaListaResponse();

                if (participantesResponse == null || participantesResponse.Count == 0) return NotFound("Não há participantes cadastrados");
                return Ok(participantesResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }

        [HttpGet("GetParticipantePorId")]
        public async Task<IActionResult> GetParticipantePorId(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty) return BadRequest("Insira um id válido");
                 
                var participante = await _getParticipantePorIdUseCase.GetParticipantePorId(id);
                var participanteResponse = participante.ConverterParaResponse();

                if (participanteResponse == null) return NotFound("Não existe participante cadastrado");
                return Ok(participanteResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
