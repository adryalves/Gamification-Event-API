using GamificationEvent.API.DTOs.Palestrante;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.PalestranteUseCases;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Resultados;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PalestranteController : ControllerBase
    {
        private readonly AtualizarPalestranteUseCase _atualizarPalestranteUseCase;
        private readonly CadastrarPalestranteUseCase _cadastrarPalestranteUseCase;
        private readonly GetPalestrantePorIdUseCase _getPalestrantePorIdUseCase;
        private readonly GetPalestrantesPorIdEventoUseCase _getPalestrantesPorIdEventoUseCase;
        private readonly GetPalestrantesPorIdSubEventoUseCase _getPalestrantesPorIdSubEventoUseCase;
        private readonly DeletarPalestranteUseCase _deletarPalestranteUseCase;

        public PalestranteController(AtualizarPalestranteUseCase atualizarPalestranteUseCase, CadastrarPalestranteUseCase cadastrarPalestranteUseCase, GetPalestrantePorIdUseCase getPalestrantePorIdUseCase, GetPalestrantesPorIdEventoUseCase getPalestrantesPorIdEventoUseCase, GetPalestrantesPorIdSubEventoUseCase getPalestrantesPorIdSubEventoUseCase, DeletarPalestranteUseCase deletarPalestranteUseCase)
        {
            _atualizarPalestranteUseCase = atualizarPalestranteUseCase;
            _cadastrarPalestranteUseCase = cadastrarPalestranteUseCase;
            _getPalestrantePorIdUseCase = getPalestrantePorIdUseCase;
            _getPalestrantesPorIdEventoUseCase = getPalestrantesPorIdEventoUseCase;
            _getPalestrantesPorIdSubEventoUseCase = getPalestrantesPorIdSubEventoUseCase;
            _deletarPalestranteUseCase = deletarPalestranteUseCase;
        }

        [HttpPost("CadastrarPalestrante")]
        public async Task<IActionResult> CadastrarPalestrante(PalestranteRequestDTO palestranteDTO)
        {
            try
            {
                if (palestranteDTO.IdEvento == Guid.Empty || String.IsNullOrEmpty(palestranteDTO.Nome))
                    return BadRequest("Insira dados válidos");

                var palestrante = palestranteDTO.ConverterRequestParaCore();

                var resultado = await _cadastrarPalestranteUseCase.CadastrarPalestrante(palestrante);

                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });

            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarPalestrante/{idPalestrante}")]
        public async Task<IActionResult> AtualizarPalestrante([FromRoute]Guid idPalestrante, PalestranteUpdateDTO palestranteDTO )
        {
            try
            {
                if (idPalestrante == Guid.Empty) return BadRequest("Insira dados válidos para a atualização");

                var palestrante = palestranteDTO.ConverterUpdateParaCore();
                var resultado = await _atualizarPalestranteUseCase.AtualizarPalestrante(idPalestrante, palestrante);
             
                if (resultado.Sucesso) return Ok("Palestrante Atualizado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarPalestrante/{id}")]
        public async Task<IActionResult> DeletarPalestrante([FromRoute]Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira dados válidos para a deleção");

                var resultado = await _deletarPalestranteUseCase.DeletarPalestrante(id);

                if (resultado.Sucesso) return Ok("Palestrante deletado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetPalestrantePorId")]
        public async Task<IActionResult> GetPalestrantePorId([FromQuery] Guid id)
        {
            try
            {
                if (id == Guid.Empty) return BadRequest("Insira um Id válido");

                var palestrante = await _getPalestrantePorIdUseCase.GetPalestrantePorId(id);
                if(palestrante.Valor == null) return NotFound("Palestrante não encontrado");

                if (palestrante.Sucesso)
                {
                    var palestranteDTO = palestrante.Valor.ConverterCoreParaResponse();
                    return Ok(palestranteDTO);
                }

                return BadRequest(new { Erro = palestrante.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetPalestrantesPorIdEvento")]
        public async Task<IActionResult> GetPalestrantesPorIdEvento([FromQuery]Guid idEvento)
        {
            try
            {
                if (idEvento == Guid.Empty) return BadRequest("Insira um Id válido");

                var palestrantes = await _getPalestrantesPorIdEventoUseCase.GetPalestrantesPorIdEvento(idEvento);

                if (palestrantes.Sucesso)
                {
                    var palestrantesDTO = palestrantes.Valor.ConverterListaCoreParaListaResponse();
                    return Ok(palestrantesDTO);
                }
                return BadRequest(new { Erro = palestrantes.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }

        [HttpGet("GetPalestrantesPorIdSubEvento")]
        public async Task<IActionResult> GetPalestrantesPorIdSubEvento([FromQuery]Guid idSubEvento)
        {
            try
            {
                if (idSubEvento == Guid.Empty) return BadRequest("Insira um Id válido");

                var palestrantes = await _getPalestrantesPorIdSubEventoUseCase.GetPalestrantesPorIdSubEvento(idSubEvento);

                if (palestrantes.Sucesso)
                {
                    var palestrantesDTO = palestrantes.Valor.ConverterListaCoreParaListaResponse();
                    return Ok(palestrantesDTO);
                }
                return BadRequest(new { Erro = palestrantes.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }
    }

    }

