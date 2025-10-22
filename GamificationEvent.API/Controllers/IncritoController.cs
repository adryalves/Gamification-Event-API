using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.InscritoUseCases;
using GamificationEvent.Core.Resultados;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncritoController : ControllerBase
    {
        private readonly CadastrarInscritosUseCase _cadastrarInscritosUseCase;
        private readonly CadastrarInscritoUseCase _cadastrarInscritoUseCase;
        private readonly DeletarInscritoUseCase _deletarInscritoUseCase;
        private readonly GetInscritosUseCase _getInscritosUseCase;
        private readonly GetInscritosPorIdUseCase _getInscritosPorIdUseCase;

        public IncritoController(CadastrarInscritosUseCase cadastrarInscritosUseCase, CadastrarInscritoUseCase cadastrarInscritoUseCase, DeletarInscritoUseCase deletarInscritoUseCase, GetInscritosUseCase getInscritosUseCase, GetInscritosPorIdUseCase getInscritosPorIdUseCase)
        {
            _cadastrarInscritosUseCase = cadastrarInscritosUseCase;
            _cadastrarInscritoUseCase = cadastrarInscritoUseCase;
            _deletarInscritoUseCase = deletarInscritoUseCase;
            _getInscritosUseCase = getInscritosUseCase;
            _getInscritosPorIdUseCase = getInscritosPorIdUseCase;
        }

        [HttpPost("CadastrarTodosOsInscritosPorEvento")]
        public async Task<IActionResult> CadastrarTodosOsInscritos(InscritosRequestDTO inscritosDTO)
        {
            try
            {

                if (inscritosDTO == null) { return BadRequest("Valores não podem ser nulos"); }

                if (inscritosDTO.IdEvento == null || inscritosDTO.IdEvento == Guid.Empty)
                    return BadRequest("É preciso inserir um id Evento válido");

                
                var inscritos = inscritosDTO.ConverterListaDeTodosParaCore();

                var cadastrados = await _cadastrarInscritosUseCase.CadastrarInscritos(inscritosDTO.IdEvento, inscritos);

                if (cadastrados.Sucesso)
                {
                    var inscritosCadastrados = cadastrados.Valor.ConverterTodosOsInscritosParaResponseDTO();
                    return Ok(inscritosCadastrados);
                }

                if (cadastrados.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = cadastrados.MensagemDeErro });
                              
                return BadRequest(new { Erro = cadastrados.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }
        [HttpPost("CadastrarUmInscritoPorEvento")]
        public async Task<IActionResult> CadastrarUmInscritoPorEvento([FromQuery] InscritoDTO inscritoDTO)
        {
            try
            {

                if (inscritoDTO == null) return BadRequest("Insira valores válidos");

                if (inscritoDTO.IdEvento == Guid.Empty || inscritoDTO.IdEvento == null) return BadRequest("Insira um id Evento válido");

                if (String.IsNullOrEmpty(inscritoDTO.Cpf) || String.IsNullOrEmpty(inscritoDTO.Nome) || inscritoDTO.Cargo == null)
                {
                    return BadRequest("Todos os campos precisa serem preenchidos");
                }
                var inscrito = inscritoDTO.ConverterUmInscritoParaCore();
                var inscritoCadastrado = await _cadastrarInscritoUseCase.CadastrarInscrito(inscrito);

  

                if (inscritoCadastrado.Sucesso) return Ok($"Inscrito com o CPF {inscritoCadastrado.Valor.Cpf} cadastrado");

                if (inscritoCadastrado.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = inscritoCadastrado.MensagemDeErro });
                

                return BadRequest(new { Erro = inscritoCadastrado.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetInscritos")]
        public async Task<IActionResult> GetTodosOsInscritos()
        {
            try
            {
                var inscritos = await _getInscritosUseCase.GetInscritos();
              
                if (inscritos.Sucesso)
                {
                    var inscritosDTO = inscritos.Valor.ConverterTodosOsInscritosParaResponseDTO();
                    return Ok(inscritosDTO);
                }

                return BadRequest(new { Erro = inscritos.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetInscritosPorIdEvento")]
        public async Task<IActionResult> GetInscritosPorEvento(Guid idEvento)
        {
            try
            {

                if (idEvento == Guid.Empty || idEvento == null) return BadRequest("Insira um id evento válido");

                var inscritos = await _getInscritosPorIdUseCase.GetInscritosPorIdEvento(idEvento);

                if (inscritos.Sucesso)
                {
                    var inscritosDTO = inscritos.Valor.ConverteInscritosPorEventoParaResponseDTO(idEvento);
                    return Ok(inscritosDTO);
                }


                return BadRequest(new { Erro = inscritos.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarInscrito")]
        public async Task<IActionResult> DeletarInscrito(String Cpf, Guid idEvento)
        {

            try {

                if (idEvento == Guid.Empty || idEvento == null) return BadRequest("Insira um id evento válido");
                if (String.IsNullOrEmpty(Cpf)) return BadRequest("Insira um cpf válido");

                var resultado = await _deletarInscritoUseCase.DeletarInscrito(Cpf, idEvento);

                if(resultado.Sucesso) return Ok("Inscrito deletado");

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