using GamificationEvent.API.DTOs.Participante;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.ParticipanteUseCases;
using GamificationEvent.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ParticipanteController : ControllerBase
    {
        private readonly AtualizarParticipanteUseCase _atualizarParticipanteUseCase;
        private readonly CadastrarParticipanteUseCase _cadastrarParticipanteUseCase;
        private readonly GetParticipantePorIdUseCase _getParticipantePorIdUseCase;
        private readonly GetParticipantesPorIdEventoUseCase _getParticipantesPorIdEventoUseCase;
        private readonly GetParticipantePorCpfUseCase _getParticipantePorCpfUseCase;
        private readonly IValidaçãoPermissões _validaçãoPermissões;

        public ParticipanteController(AtualizarParticipanteUseCase atualizarParticipanteUseCase, CadastrarParticipanteUseCase cadastrarParticipanteUseCase, GetParticipantePorIdUseCase getParticipantePorIdUseCase, GetParticipantesPorIdEventoUseCase getParticipantesPorIdEventoUseCase, GetParticipantePorCpfUseCase getParticipantePorCpfUseCase, IValidaçãoPermissões validaçãoPermissões)
        {
            _atualizarParticipanteUseCase = atualizarParticipanteUseCase;
            _cadastrarParticipanteUseCase = cadastrarParticipanteUseCase;
            _getParticipantePorIdUseCase = getParticipantePorIdUseCase;
            _getParticipantesPorIdEventoUseCase = getParticipantesPorIdEventoUseCase;
            _getParticipantePorCpfUseCase = getParticipantePorCpfUseCase;
            _validaçãoPermissões = validaçãoPermissões;
        }
        [HttpPost("CadastrarParticipante")]
        public async Task<IActionResult> CadastrarParticipante(ParticipanteRequestDTO participanteDTO)
        {
            try
            {
                if (participanteDTO == null) return BadRequest("Insira dados");

                if (participanteDTO.IdEvento == Guid.Empty ||
                    participanteDTO.IdUsuario == Guid.Empty)
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

        [HttpPut("AtualizarParticipante/{id}")]
        public async Task<IActionResult> AtualizarParticipante([FromRoute] Guid id, [FromBody] ParticipanteUpdateDTO participanteDTO)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um id válido");

                var participante = participanteDTO.ConverterUpdateParCore();
                participante.Id = id;

                //Como é possivel que um admin atualize o cargo de outro participante tem-se essa lógica

                var participanteExistente = await _getParticipantePorIdUseCase.GetParticipantePorId(id);
                if (participanteExistente.Valor == null) return NotFound();

                if ((participante.PrimeiroParticipante == true && participanteExistente.Valor.PrimeiroParticipante == false) || (participanteExistente.Valor.Cargo == Core.Enums.Cargo.Membro && participante.Cargo == Core.Enums.Cargo.Admin))
                {
                    // Estamos usando o usuario/participante que fez a requisição e nao o participante que esta sendo atualizado. Apenas estamos usando o memso id evento
                    var permissao = await _validaçãoPermissões.ParticipanteEhAdmin(User, participanteExistente.Valor.IdEvento);
                    if (!permissao) return StatusCode(StatusCodes.Status403Forbidden, new
                    {
                        message = "Essa ação é restrita para admins"
                    });
                }

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
        public async Task<IActionResult> GetParticipantesPorIdEvento([FromQuery] Guid idEvento)
        {
            try
            {
                if (idEvento == Guid.Empty)
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
        public async Task<IActionResult> GetParticipantePorId([FromQuery]Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um id válido");
                 
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
