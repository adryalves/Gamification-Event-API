﻿using GamificationEvent.API.DTOs;
using GamificationEvent.API.Mappings;
using GamificationEvent.Application.UseCases.InteresseUseCases;
using GamificationEvent.Core.Resultados;
using Microsoft.AspNetCore.Mvc;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InteresseController : ControllerBase
    {
        private readonly CadastrarInteresseUseCase _cadastrarInteresseUseCase;
        private readonly DeletarInteresseUseCase _deletarInteresseUseCase;
        private readonly GetInteressePorIdUseCase _getInteressePorIdUseCase;
        private readonly GetInteressesPorIdEventoUseCase _getInteressesPorIdEventoUseCase;

        public InteresseController(CadastrarInteresseUseCase cadastrarInteresseUseCase, DeletarInteresseUseCase deletarInteresseUseCase, GetInteressePorIdUseCase getInteressePorIdUseCase, GetInteressesPorIdEventoUseCase getInteressesPorIdEventoUseCase)
        {
            _cadastrarInteresseUseCase = cadastrarInteresseUseCase;
            _deletarInteresseUseCase = deletarInteresseUseCase;
            _getInteressePorIdUseCase = getInteressePorIdUseCase;
            _getInteressesPorIdEventoUseCase = getInteressesPorIdEventoUseCase;
        }

        [HttpPost("CadastrarInteresses")]
        public async Task<IActionResult> CadastrarInteresses(ListaInteresseRequestDTO interessesRequest)
        {
            try
            {
                if (interessesRequest == null || interessesRequest.InteressesDTO == null) return BadRequest("Insira valores válidos");
                if (interessesRequest.IdEvento == null || interessesRequest.IdEvento == Guid.Empty) return BadRequest("Insira uma Id evento válido");

                var interesses = interessesRequest.ConverterListaParaCore();

                var cadastrados = await _cadastrarInteresseUseCase.CadastrarInteresses(interessesRequest.IdEvento, interesses);

                if(cadastrados.Sucesso) return Ok($"Foram cadastrados {cadastrados.Valor} interesses");

                if (cadastrados.MensagemDeErro!.Contains("não encontrado"))
                {
                    return NotFound(new { Erro = cadastrados.MensagemDeErro });
                }

                return BadRequest(new { Erro = cadastrados.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarInteresse")]
        public async Task<IActionResult> DeletarInteresse(Guid id)
        {
            try
            {
                if (id == Guid.Empty || id == null) return BadRequest("Insira um id  válido");

                var resultado = await _deletarInteresseUseCase.DeletarInteresse(id);

               if(resultado.Sucesso) return Ok("Interesse deletado");

                if (resultado.MensagemDeErro!.Contains("não encontrado"))
                {
                    return NotFound(new { Erro = resultado.MensagemDeErro });
                }

                return BadRequest(new { Erro = resultado.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetInteressesPorIdEvento")]
        public async Task<IActionResult> GetInteressesPorIdEvento(Guid idEvento)
        {
            try
            {
                var interesses = await _getInteressesPorIdEventoUseCase.GetInteressesPorIdEvento(idEvento);

                if (interesses.Sucesso)
                {
                    var interessesDTO = interesses.Valor.ConverterListaParaResponse();
                    return Ok(interessesDTO);
                }

                if (interesses.MensagemDeErro!.Contains("não encontrado"))
                {
                    return NotFound(new { Erro = interesses.MensagemDeErro });
                }

                return BadRequest(new { Erro = interesses.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetInteressePorId")]
        public async Task<IActionResult> GetInteressePorId(Guid id)
        {
            try
            {
                var interesse = await _getInteressePorIdUseCase.GerInteressePorId(id);

                if (interesse.Valor == null) return NotFound();

                if (interesse.Sucesso)
                {
                    var interesseDTO = interesse.Valor.ConverterInteresseParaResponse();
                    return Ok(interesseDTO);
                }

                return BadRequest(new { Erro = interesse.MensagemDeErro });

            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}
