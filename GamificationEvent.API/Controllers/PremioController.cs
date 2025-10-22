using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.PremioUseCases;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PremioController : ControllerBase
    {
        private readonly AtualizarPremioUseCase _atualizarPremioUseCase;
        private readonly CadastrarPremioUseCase _cadastrarPremioUseCase;
        private readonly DeletarPremioUseCase _deletarPremioUseCase;
        private readonly GetPremioPorIdUseCase _getPremioPorIdUseCase;
        private readonly GetPremiosPorIdEventoUseCase _getPremiosPorIdEventoUseCase;

        public PremioController(AtualizarPremioUseCase atualizarPremioUseCase, CadastrarPremioUseCase cadastrarPremioUseCase, DeletarPremioUseCase deletarPremioUseCase, GetPremioPorIdUseCase getPremioPorIdUseCase, GetPremiosPorIdEventoUseCase getPremiosPorIdEventoUseCase)
        {
            _atualizarPremioUseCase = atualizarPremioUseCase;
            _cadastrarPremioUseCase = cadastrarPremioUseCase;
            _deletarPremioUseCase = deletarPremioUseCase;
            _getPremioPorIdUseCase = getPremioPorIdUseCase;
            _getPremiosPorIdEventoUseCase = getPremiosPorIdEventoUseCase;
        }

        [HttpPost("CadastrarPremio")]
        public async Task<IActionResult> CadastrarPremio(PremioRequestDTO premioDTO)
        {
            try
            {
                if (premioDTO.IdEvento == null || premioDTO.IdEvento == Guid.Empty || String.IsNullOrEmpty(premioDTO.Nome)) 
                {
                    return BadRequest("Preencha os campos obrigatórios com valores válidos");
                }

                var premio = premioDTO.ConverterRequestParaCore();

                var resultado = await _cadastrarPremioUseCase.CadastrarPremio(premio);

                if (resultado.Sucesso) return Ok(resultado.Valor);

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarPremio")]
        public async Task<IActionResult> AtualizarPremio(Guid id, PremioUpdateDTO premioDTO)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um id válido para atualização");

                var premio = premioDTO.ConverterUpdateParaCore();

                var resultado = await _atualizarPremioUseCase.AtualizarPremio(id, premio);

                if (resultado.Sucesso) return Ok("Premio Atualizado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetPremiosPorIdEvento")]
        public async Task<IActionResult> GetPremiosPorIdEvento(Guid idEvento)
        {
            try
            {
                if (idEvento == Guid.Empty || idEvento == null) return BadRequest("Insira um id válido para atualização");
          
                var premios = await _getPremiosPorIdEventoUseCase.GetPremiosPorIdEvento(idEvento);

                if (premios.Sucesso)
                {
                    var participantesResponse = premios.Valor.ConverterParaListaResponse();
                    return Ok(participantesResponse);
                }

                return BadRequest(new { Erro = premios.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetPremioPorId")]
        public async Task<IActionResult> GetPremioPorId(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um id válido para atualização");
                var premio = await _getPremioPorIdUseCase.GetPremioPorId(id);

                if (premio.Valor == null) return NotFound();

                if (premio.Sucesso)
                {
                    var premioResponse = premio.Valor.ConverterParaResponse();
                    return Ok(premioResponse);
                }

                return BadRequest(new { Erro = premio.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarPremio")]
        public async Task<IActionResult> DeletarPremioPorId(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um id válido para atualização");

                var resultado = await _deletarPremioUseCase.DeletarPremio(id);

                if (resultado.Sucesso) return Ok("Premio deletado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
