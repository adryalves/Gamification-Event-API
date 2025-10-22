using GamificationEvent.API.DTOs;
using GamificationEvent.Application.Mappings;
using GamificationEvent.Application.UseCases.UsuarioUseCases;
using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Resultados;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace GamificationEvent.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly CadastrarUsuarioUseCase _cadastrarUsuarioUseCase;
        private readonly GetUsuariosUseCase _getUsuariosUseCase;
        private readonly GetUsuarioPorIdUseCase _getUsuarioPorIdUseCase;
        private readonly DeletarUsuarioUseCase _deletarUsuarioUseCase;
        private readonly AtualizarUsuarioUseCase _atualizarUsuarioUseCase;

        public UsuarioController(CadastrarUsuarioUseCase cadastrarUsuarioUseCase, GetUsuariosUseCase getUsuariosUseCase, GetUsuarioPorIdUseCase getUsuarioPorIdUseCase, DeletarUsuarioUseCase deletarUsuarioUseCase, AtualizarUsuarioUseCase atualizarUsuarioUseCase)
        {
            _cadastrarUsuarioUseCase = cadastrarUsuarioUseCase;
            _getUsuariosUseCase = getUsuariosUseCase;
            _getUsuarioPorIdUseCase = getUsuarioPorIdUseCase;
            _deletarUsuarioUseCase = deletarUsuarioUseCase;
            _atualizarUsuarioUseCase = atualizarUsuarioUseCase;
        }

        [HttpPost("CadastrarUsuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] UsuarioRequestDTO usuarioDTO)
        {
            try
            {
                var usuario = usuarioDTO.ConverterUsuarioCore();
                var novoUsuario = await _cadastrarUsuarioUseCase.CadastrarUsuario(usuario, usuarioDTO.Senha);

                if(novoUsuario.Sucesso) return Ok(novoUsuario.Valor.Id);

                return BadRequest(new { Erro = novoUsuario.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {

                var usuarios = await _getUsuariosUseCase.GetUsuarios();


                List<UsuarioResponseDTO> usuariosResponse = new();

                foreach (var usuario in usuarios.Valor)
                {
                    var usuarioResponse = usuario.ConverterUsuarioResponse();  
                    usuariosResponse.Add(usuarioResponse);

                }

                if(usuarios.Sucesso) return Ok(usuariosResponse);

                return BadRequest(new { Erro = usuarios.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("GetUsuarioPorId")]
        public async Task<IActionResult> GetUsuarioPorId(Guid id)
        {
            try
            {
                var usuario = await _getUsuarioPorIdUseCase.GetUsuario(id);
                if (usuario.Valor == null) return NotFound();


                if (usuario.Sucesso)
                {
                    var usuarioResponse = usuario.Valor.ConverterUsuarioResponse();
                    return Ok(usuarioResponse);
                }

                return BadRequest(new { Erro = usuario.MensagemDeErro });
            }

            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpDelete("DeletarUsuario")]
        public async Task<IActionResult> DeletarUsuarioPorId(Guid id)
        {
            try
            {
                if (id == null || id == Guid.Empty)
                    return BadRequest("Insira um id válido");

                var deleção = await _deletarUsuarioUseCase.DeletarUsuario(id);

               if(deleção.Sucesso) return Ok("Usuário deletado");

                if (deleção.MensagemDeErro!.Contains("não encontrado"))
                    return NotFound(new { Erro = deleção.MensagemDeErro });
                

                return BadRequest(new { Erro = deleção.MensagemDeErro });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPut("AtualizarUsuario")]
        public async Task<IActionResult> AtualizarUsuarioPorId(Guid id, [FromBody] UsuarioUpdateDTO usuarioDTO)
        {
            try
            {

               var usuario = usuarioDTO.ConverterUpdateParaCore();
               usuario.Id = id;

               var resultado = await _atualizarUsuarioUseCase.AtualizarUsuario(usuario);

               if(resultado.Sucesso) return Ok("Usuario atualizado");

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

