using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.DesafioUseCases;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

        [HttpPut("AtualizarDesafio")]
        public async Task<IActionResult> AtualizarDesafio(Guid id, DesafioUpdateDTO desafioDTO)
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

        [HttpDelete("DeletarDesafio")]
        public async Task<IActionResult> DeletarDesafio(Guid id)
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
        public async Task<IActionResult> GetDesafioPorId(Guid id)
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
        public async Task<IActionResult> GetDesafiosPorIdEvento(Guid idEvento)
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
        public async Task<IActionResult> GetDesafiosParticipantePorIdParticipante(Guid idParticipante)
        {
            try
            {
                if (idParticipante == null || idParticipante == Guid.Empty) return BadRequest("Insira um id válido");

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
        
    
        
        
    

