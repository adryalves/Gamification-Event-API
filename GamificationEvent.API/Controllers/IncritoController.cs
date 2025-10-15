using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.InscritoUseCases;
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
                return Ok($"Foram cadastrados {cadastrados} inscritos ");
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }

        }
        [HttpPost("CadastrarUmInscritoPorEvento")]
        public async Task<IActionResult> CadastrarTodosOsInscritos([FromQuery] InscritoDTO inscritoDTO)
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

                return Ok($"Inscrito com o CPF {inscritoCadastrado.Cpf} cadastrado");
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

                if (inscritos == null || inscritos.Count == 0) return NotFound("Não existe inscritos cadastrados");

                var eventosDTO = inscritos.ConverterTodosOsInscritosParaResponseDTO();
                return Ok(eventosDTO);
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

                if (inscritos == null || inscritos.Count == 0) return NotFound("Não existe inscritos cadastrados nesse evento");

                var eventosDTO = inscritos.ConverteInscritosPorEventoParaResponseDTO();
                return Ok(eventosDTO);
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

                var deleção = await _deletarInscritoUseCase.DeletarInscrito(Cpf, idEvento);

                if (!deleção)
                {
                    return NotFound("Cadastro de Inscrito nesse evento não encontrado para ser deletado");
                }

                return Ok("Inscrito deletado");
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}