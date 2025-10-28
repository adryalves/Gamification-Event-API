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
        private readonly GetParticipantePorCpfUseCase _getParticipantePorCpfUseCase;

        public ParticipanteController(AtualizarParticipanteUseCase atualizarParticipanteUseCase, CadastrarParticipanteUseCase cadastrarParticipanteUseCase, GetParticipantePorIdUseCase getParticipantePorIdUseCase, GetParticipantesPorIdEventoUseCase getParticipantesPorIdEventoUseCase, GetParticipantePorCpfUseCase getParticipantePorCpfUseCase)
        {
            _atualizarParticipanteUseCase = atualizarParticipanteUseCase;
            _cadastrarParticipanteUseCase = cadastrarParticipanteUseCase;
            _getParticipantePorIdUseCase = getParticipantePorIdUseCase;
            _getParticipantesPorIdEventoUseCase = getParticipantesPorIdEventoUseCase;
            _getParticipantePorCpfUseCase = getParticipantePorCpfUseCase;
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

                if(participanteId.Sucesso) return Ok(participanteId.Valor);

                if (participanteId.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = participanteId.MensagemDeErro });


                return BadRequest(new { Erro = participanteId.MensagemDeErro });
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

                var resultado = await _atualizarParticipanteUseCase.AtualizarParticipante(participante);
        
               if(resultado.Sucesso) return Ok("Participante atualizada");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });


                return BadRequest(new { Erro = resultado.MensagemDeErro });

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

                if (participantes.Sucesso)
                {
                    var participantesResponse = participantes.Valor.ConverterParaListaResponse();
                    return Ok(participantesResponse);
                }

                if (participantes.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = participantes.MensagemDeErro });


                return BadRequest(new { Erro = participantes.MensagemDeErro });
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

                if (participante.Valor == null) return NotFound();

                if (participante.Sucesso)
                {
                    var participanteResponse = participante.Valor.ConverterParaResponse();
                    return Ok(participanteResponse);
                }

                return BadRequest(new { Erro = participante.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetParticipantePorCpf")]
        public async Task<IActionResult> GetParticipantePorCpf([FromQuery] string cpf)
        {
            try
            {
                if (String.IsNullOrEmpty(cpf)) return BadRequest("Insira um cpf válido");

                var participante = await _getParticipantePorCpfUseCase.GetParticipantePorCpf(cpf);

                if (participante.Valor == null) return NotFound();

                if (participante.Sucesso)
                {
                    var participanteResponse = participante.Valor.ConverterParaResponse();
                    return Ok(participanteResponse);
                }

                return BadRequest(new { Erro = participante.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
