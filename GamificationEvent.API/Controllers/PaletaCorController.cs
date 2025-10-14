using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.PaletaCorUseCases;
using GamificationEvent.Application.UseCases.UsuarioUseCases;
using GamificationEvent.Core.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
                if (corDTO.HexCodigo.Length != 7)
                {
                    return BadRequest("A cor deve ter um valor válido, ou seja com 7 caracteres");
                }

                var cor = corDTO.ConverterCorCore();
                var corCadastrada = await _cadastrarCorUseCase.CadastrarCor(cor);

                return Ok(corCadastrada.Id);

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

                if (cores == null || cores.Count == 0)
                {
                    return NotFound("Não há cores cadastradas");
                }

                List<CorResponseDTO> coresResponse = new();

                foreach (var cor in cores)
                {
                    var corResponse = cor.ConverterCorResponse();

                    coresResponse.Add(corResponse);

                }
                return Ok(coresResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetCorPorId")]
        public async Task<IActionResult> GetCorPorId(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null)
                    return BadRequest("O id deve ser um válor válido");

                var cor = await _getCorPorIdUseCase.GetCorPorId(id);

                if (cor == null)
                    return NotFound("Não há uma cor com esse Id");

                var corResponse = cor.ConverterCorResponse();
                return Ok(corResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarCor")]
        public async Task<IActionResult> AtualizarCor(Guid id, CorRequestDTO corDTO)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                    return BadRequest("Insira uma valor válido");

                var cor = corDTO.ConverterCorCore();
                cor.Id = id;
                await _atualizarCorUseCase.AtualizarCor(cor);

                return Ok("Cor atualizada");
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
                if (paletaDTO.IdCor1 == null || paletaDTO.IdCor2 == null
                    || paletaDTO.IdCor3 == null || paletaDTO.IdCor4 == null)
                {
                    return BadRequest("Os ids precisam conter um valor válido");
                }
                var paleta = paletaDTO.ConverterPaletaCore();

                var paletaCadastrada = await _cadastrarPaletaUseCase.CadastrarPaletaCor(paleta);
                return Ok(paletaCadastrada.Id);

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

                if (paletas == null || paletas.Count == 0)
                    return NotFound("Não há paletas cadastradas");

                var paletasResponse = new List<PaletaCorResponseDTO>();

                foreach (var paleta in paletas)
                {
                    var paletaResponse = paleta.ConverterPaletaResponse();

                    paletasResponse.Add(paletaResponse);

                }
                return Ok(paletasResponse);
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetPaletaPorId")]
        public async Task<IActionResult> GetPaletaPorId(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                    return BadRequest("Insira um id válido");

                var paleta = await _getPaletaPorIdUseCase.GetPaletaPorId(id);

                if (paleta == null)
                    return NotFound("Não existe uma paleta com esse Id");

                var paletaResponse = paleta.ConverterPaletaResponse();

                return Ok(paletaResponse);

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarPaleta")]
        public async Task<IActionResult> AtualizarPaleta(Guid id, [FromBody] PaletaCorRequestDTO paletaDTO)
        {
            try
            {

                if (id == null || id == Guid.Empty)
                    return BadRequest("Insira um valor válido");

                var paleta = paletaDTO.ConverterPaletaCore();
                paleta.Id = id;

                await _atualizarPaletaUseCase.AtualizarPaleta(paleta);
                return Ok("Paleta atualizada");

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }

        [HttpDelete("DeletarPaleta")]
        public async Task<IActionResult> DeletarPaleta(Guid id)
        {
            if (id == null || id == Guid.Empty)
                return BadRequest("Insira um id válido");

            var deleção = await _deletarPaletaUseCase.DeletarPaleta(id);

            if (!deleção)
            {
                return NotFound("Paleta não encontrado para ser deletado");
            }

            return Ok("Paleta deletada");
        }
    }
}
