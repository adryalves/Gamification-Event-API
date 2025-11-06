using GamificationEvent.API.DTOs.Desafio;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.DesafioUseCases;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DesafioController : ControllerBase
    {
        private readonly AtualizarDesafioUseCase _atualizarDesafioUseCase;
        private readonly CadastrarDesafioUseCase _cadastrarDesafioUseCase;
        private readonly DeletarDesafioUseCase _deletarDesafioUseCase;
        private readonly GetDesafioPorIdUseCase _getDesafioPorIdUseCase;
        private readonly GetDesafiosPorIdEventoUseCase _getDesafiosPorIdEventoUseCase;
        private readonly GetDesafiosParticipantePorIdParticipanteUseCase _getDesafiosParticipantePorIdParticipanteUseCase;

        public DesafioController(AtualizarDesafioUseCase atualizarDesafioUseCase, CadastrarDesafioUseCase cadastrarDesafioUseCase, DeletarDesafioUseCase deletarDesafioUseCase, GetDesafioPorIdUseCase getDesafioPorIdUseCase, GetDesafiosPorIdEventoUseCase getDesafiosPorIdEventoUseCase, GetDesafiosParticipantePorIdParticipanteUseCase getDesafiosParticipantePorIdParticipanteUseCase )
        {
            _atualizarDesafioUseCase = atualizarDesafioUseCase;
            _cadastrarDesafioUseCase = cadastrarDesafioUseCase;
            _deletarDesafioUseCase = deletarDesafioUseCase;
            _getDesafioPorIdUseCase = getDesafioPorIdUseCase;
            _getDesafiosPorIdEventoUseCase = getDesafiosPorIdEventoUseCase;
            _getDesafiosParticipantePorIdParticipanteUseCase = getDesafiosParticipantePorIdParticipanteUseCase;
        }

        [HttpPost("CadastrarDesafio")]
        public async Task<IActionResult> CadastrarDesafio(DesafioRequestDTO desafioDTO)
        {
            try { 

            if(desafioDTO.IdEvento == Guid.Empty) return BadRequest("Insira um Id válido");

            var desafio = desafioDTO.ConverterRequestParaCore();

            var resultado = await _cadastrarDesafioUseCase.CadastrarDesafio(desafio);
            if (resultado.Sucesso) return Ok(resultado.Valor);
            return BadRequest(new { Erro = resultado.MensagemDeErro });

        }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message});
            }
        }

        [HttpPut("AtualizarDesafio/{id}")]
        public async Task<IActionResult> AtualizarDesafio([FromRoute] Guid id, DesafioUpdateDTO desafioDTO)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um id válido");

                var desafio = desafioDTO.ConverterUpdateParaCore();
                var resultado = await _atualizarDesafioUseCase.AtualizarDesafio(id, desafio);

                if (resultado.Sucesso) return Ok("Desafio Atualizado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarDesafio/{id}")]
        public async Task<IActionResult> DeletarDesafio([FromRoute] Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira uma id válido");
                var resultado = await _deletarDesafioUseCase.DeletarDesafio(id);

                if (resultado.Sucesso) return Ok("Desafio deletado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetDesafioPorId")]
        public async Task<ActionResult<DesafioResponseDTO>> GetDesafioPorId([FromQuery]Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um id válido");

                var desafio = await _getDesafioPorIdUseCase.GetDesafioPorId(id);

                if (desafio.Valor == null) return NotFound("Desafio não encontrado");

                if (desafio.Sucesso)
                {
                    var desafioResponse = desafio.Valor.ConverterDesafioParaResponse();
                    return Ok(desafioResponse);
                }
                return BadRequest(new { Erro = desafio.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetDesafiosPorIdEvento")]
        public async Task<ActionResult<List<DesafioResponseDTO>>> GetDesafiosPorIdEvento([FromQuery] Guid idEvento)
        {
            try
            {
                if (idEvento == Guid.Empty) return BadRequest("Insira um id válido");

                var desafios = await _getDesafiosPorIdEventoUseCase.GetDesafiosPorIdEvento(idEvento);

                if (desafios.Sucesso)
                {
                    var desafioResponse = desafios.Valor.ConverterListaParaResponse();
                    return Ok(desafioResponse);
                }
                return BadRequest(new { Erro = desafios.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetDesafiosParticipantePorIdParticipante")]
        public async Task<ActionResult<List<DesafioParticipanteResponseDTO>>> GetDesafiosParticipantePorIdParticipante([FromQuery] Guid idParticipante)
        {
            try
            {
                if (idParticipante == Guid.Empty) return BadRequest("Insira um id válido");

                var desafioPart = await _getDesafiosParticipantePorIdParticipanteUseCase.GetDesafiosPaticipantePorIdParticipante(idParticipante);

                if (desafioPart.Sucesso)
                {
                   var desafioPartDTO = desafioPart.Valor.ConverterDesafioParticipanteListaParaResponse();
                    return Ok(desafioPartDTO);
                }
                return BadRequest(new { Erro = desafioPart.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
        }
        }
        
    
        
        
    

