using GamificationEvent.API.DTOs.PaletaCor;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.PaletaCorUseCases;
using GamificationEvent.Application.UseCases.UsuarioUseCases;
using GamificationEvent.Core.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PaletaCorController : ControllerBase
    {
        private readonly AtualizarPaletaUseCase _atualizarPaletaUseCase;
        private readonly CadastrarCorUseCase _cadastrarCorUseCase;
        private readonly AtualizarCorUseCase _atualizarCorUseCase;
        private readonly CadastrarPaletaUseCase _cadastrarPaletaUseCase;
        private readonly DeletarPaletaUseCase _deletarPaletaUseCase;
        private readonly GetCoresUseCase _getCoresUseCase;
        private readonly GetCorPorIdUseCase _getCorPorIdUseCase;
        private readonly GetPaletaPorIdUseCase _getPaletaPorIdUseCase;
        private readonly GetPaletasUseCase _getPaletasUseCase;

        public PaletaCorController(AtualizarPaletaUseCase atualizarPaletaUseCase, CadastrarCorUseCase cadastrarCorUseCase, CadastrarPaletaUseCase cadastrarPaletaUseCase, DeletarPaletaUseCase deletarPaletaUseCase, GetCoresUseCase getCoresUseCase, GetCorPorIdUseCase getCorPorIdUseCase, GetPaletaPorIdUseCase getPaletaPorIdUseCase, GetPaletasUseCase getPaletasUseCase, AtualizarCorUseCase atualizarCorUseCase)
        {
            _atualizarPaletaUseCase = atualizarPaletaUseCase;
            _cadastrarCorUseCase = cadastrarCorUseCase;
            _cadastrarPaletaUseCase = cadastrarPaletaUseCase;
            _deletarPaletaUseCase = deletarPaletaUseCase;
            _getCoresUseCase = getCoresUseCase;
            _getCorPorIdUseCase = getCorPorIdUseCase;
            _getPaletaPorIdUseCase = getPaletaPorIdUseCase;
            _getPaletasUseCase = getPaletasUseCase;
            _atualizarCorUseCase = atualizarCorUseCase;
        }

        [HttpPost("CadastrarCor")]
        public async Task<IActionResult> CadastrarCor([FromBody] CorRequestDTO corDTO)
        {
            try
            {
                if (corDTO.HexCodigo == null)
                {
                    return BadRequest("A cor deve ter um valor válido");
                }

                var cor = corDTO.ConverterCorCore();
                var corCadastrada = await _cadastrarCorUseCase.CadastrarCor(cor);

                if(corCadastrada.Sucesso) return Ok(corCadastrada.Valor.Id);


                return BadRequest(new { Erro = corCadastrada.MensagemDeErro });


            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetCores")]
        public async Task<IActionResult> GetCores()
        {
            try
            {
                var cores = await _getCoresUseCase.GetCores();      

                List<CorResponseDTO> coresResponse = new();

                if (cores.Sucesso)
                {
                    foreach (var cor in cores.Valor)
                    {
                        var corResponse = cor.ConverterCorResponse();
                        coresResponse.Add(corResponse);
                    }
                    return Ok(coresResponse);
                }

                return BadRequest(new { Erro = cores.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetCorPorId")]
        public async Task<IActionResult> GetCorPorId([FromQuery] Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("O id deve ser um válor válido");

                var cor = await _getCorPorIdUseCase.GetCorPorId(id);

                if (cor.Valor == null) return NotFound();

                if (cor.Sucesso)
                {
                    var corResponse = cor.Valor.ConverterCorResponse();
                    return Ok(corResponse);
                }

                return BadRequest(new { Erro = cor.MensagemDeErro });

            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarCor/{id}")]
        public async Task<IActionResult> AtualizarCor([FromRoute]Guid id, CorUpdateDTO corDTO)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Insira uma valor válido");

                var cor = corDTO.ConverterUpdateCorCore();
                cor.Id = id;

               var resultado = await _atualizarCorUseCase.AtualizarCor(cor);
               
                if(resultado.Sucesso) return Ok("Cor atualizada");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }

        [HttpPost("CadastrarPaleta")]
        public async Task<IActionResult> CadastrarPaleta(PaletaCorRequestDTO paletaDTO)
        {
            try
            {
                if (paletaDTO.IdCor1 == Guid.Empty || paletaDTO.IdCor2 == Guid.Empty
                    || paletaDTO.IdCor3 == Guid.Empty || paletaDTO.IdCor4 == Guid.Empty)
                {
                    return BadRequest("Os ids precisam conter um valor válido");
                }
                var paleta = paletaDTO.ConverterPaletaCore();

                var paletaCadastrada = await _cadastrarPaletaUseCase.CadastrarPaletaCor(paleta);

                if(paletaCadastrada.Sucesso) return Ok(paletaCadastrada.Valor.Id);

                if (paletaCadastrada.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = paletaCadastrada.MensagemDeErro });

                return BadRequest(new { Erro = paletaCadastrada.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }

        [HttpGet("GetPaletas")]
        public async Task<IActionResult> GetPaletas()
        {
            try
            {
                var paletas = await _getPaletasUseCase.GetPaletas();

                var paletasResponse = new List<PaletaCorResponseDTO>();

                if (paletas.Sucesso)
                {
                    foreach (var paleta in paletas.Valor)
                    {
                        var paletaResponse = paleta.ConverterPaletaResponse();
                        paletasResponse.Add(paletaResponse);

                    }
                    return Ok(paletasResponse);
                }

                return BadRequest(new { Erro = paletas.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetPaletaPorId")]
        public async Task<IActionResult> GetPaletaPorId([FromQuery]Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Insira um id válido");

                var paleta = await _getPaletaPorIdUseCase.GetPaletaPorId(id);

                if (paleta.Valor == null) return NotFound();

                if (paleta.Sucesso)
                {
                    var paletaResponse = paleta.Valor.ConverterPaletaResponse();
                    return Ok(paletaResponse);
                }

                return BadRequest(new { Erro = paleta.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarPaleta/{id}")]
        public async Task<IActionResult> AtualizarPaleta([FromRoute]Guid id, [FromBody] PaletaCorUpdateDTO paletaDTO)
        {
            try
            {
                if (id == Guid.Empty)
                    return BadRequest("Insira um valor válido");

                var paleta = paletaDTO.ConverterUpdatePaletaCore();
                paleta.Id = id;
                paleta.Deletado = false;

               var resultado = await _atualizarPaletaUseCase.AtualizarPaleta(paleta);

               if(resultado.Sucesso) return Ok("Paleta atualizada");

               if (resultado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = resultado.MensagemDeErro });

                return BadRequest(new { Erro = resultado.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }

        [HttpDelete("DeletarPaleta/{id}")]
        public async Task<IActionResult> DeletarPaleta([FromRoute]Guid id)
        {
            try {

                if (id == Guid.Empty)
                    return BadRequest("Insira um id válido");

                var deleção = await _deletarPaletaUseCase.DeletarPaleta(id);

                if (deleção.Sucesso) return Ok("Paleta deletada");

                if (deleção.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = deleção.MensagemDeErro });


                return BadRequest(new { Erro = deleção.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

    }
}
