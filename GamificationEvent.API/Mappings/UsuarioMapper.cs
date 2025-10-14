using GamificationEvent.API.DTOs;
using GamificationEvent.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.Mappings
{
    public static class UsuarioMapper
    {
        public static Usuario ConverterUsuarioCore(this UsuarioRequestDTO usuarioDTO)
        {
            DateOnly dataDeNascimentoConvertida = new();

            if (!string.IsNullOrEmpty(usuarioDTO.DataDeNascimento))
            {
                dataDeNascimentoConvertida = DateOnly.ParseExact(
                    usuarioDTO.DataDeNascimento,
                    "dd/MM/yyyy",
                    CultureInfo.InvariantCulture
                );
            }

            return new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Cpf = usuarioDTO.Cpf,
                Telefone = usuarioDTO.Telefone,
                DataDeNascimento = dataDeNascimentoConvertida,
                Foto = usuarioDTO.Foto,
                DataHoraCriacao = DateTime.UtcNow,
                Deletado = false,
                RedesSociais = usuarioDTO.RedesSociais.Select(r => new UsuarioRedeSocial
                {
                    Plataforma = r.Plataforma,
                    Url = r.Url
                }).ToList()
            };
        }

        public static UsuarioResponseDTO ConverterUsuarioResponse(this Usuario usuario)
        {

            return new UsuarioResponseDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Cpf = usuario.Cpf,
                Telefone = usuario.Telefone,
                DataDeNascimento = usuario.DataDeNascimento?.ToString("dd/MM/yyyy"),
                Foto = usuario.Foto,
                DataHoraCriacao = usuario.DataHoraCriacao,
                Deletado = usuario.Deletado,
                RedesSociais = usuario.RedesSociais.Select(r => new RedeSocialDTO
                {
                    Plataforma = r.Plataforma,
                    Url = r.Url
                }).ToList()
            };
        }


        public static Usuario ConverterUpdateParaCore(this UsuarioUpdateDTO usuarioDTO)
        {
            DateOnly dataDeNascimentoConvertida = new DateOnly();

            if (usuarioDTO.DataDeNascimento != null)
            {
                dataDeNascimentoConvertida = DateOnly.ParseExact(
                usuarioDTO.DataDeNascimento,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture
                            );
            }

            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Cpf = usuarioDTO.Cpf,
                Telefone = usuarioDTO.Telefone,
                DataDeNascimento = dataDeNascimentoConvertida,
                RedesSociais = usuarioDTO.RedesSociais.Select(r => new UsuarioRedeSocial
                {
                    Plataforma = r.Plataforma,
                    Url = r.Url
                }).ToList()
            };
            return usuario;
        }

    }
}

