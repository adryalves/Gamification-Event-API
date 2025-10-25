using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreDesafio = GamificationEvent.Core.Entidades.Desafio;
using InfraDesafio = GamificationEvent.Infrastructure.Data.Persistence.Desafio;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class DesafioRepository : IDesafioRepository
    {
        private readonly AppDbContext _context;

        public DesafioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarDesafio(CoreDesafio desafio)
        {
            var desafioDB = new InfraDesafio
            {
                Id = Guid.NewGuid(),
                IdEvento = desafio.IdEvento,
                Nome = desafio.Nome,
                Descricao = desafio.Descricao,
                Regra = desafio.Regra,
                Pontuacao = desafio.Pontuacao,
                TipoDesafio = desafio.TipoDesafio,
                QuantidadeDesafio = desafio.QuantidadeDesafio,
                DataHoraInicio = desafio.DataHoraInicio,
                DataHoraFim = desafio.DataHoraFim,
            };

            _context.Desafios.Add(desafioDB);
            await _context.SaveChangesAsync();
            return desafioDB.Id;
        }

        public async Task<bool> AtualizarDesafio(CoreDesafio desafio)
        {
            var desafioExistente = await _context.Desafios.FirstOrDefaultAsync(x => x.Id == desafio.Id && !x.Deletado && !x.IdEventoNavigation.Deletado);

            if(desafioExistente != null)
            {
                desafioExistente.Nome = desafio.Nome;
                desafioExistente.Descricao = desafio.Descricao;
                desafioExistente.Regra = desafio.Regra;
                desafioExistente.Pontuacao = desafio.Pontuacao;
                desafioExistente.TipoDesafio = desafio.TipoDesafio;
                desafioExistente.QuantidadeDesafio = desafio.QuantidadeDesafio;
                desafioExistente.DataHoraInicio = desafio.DataHoraInicio;
                desafioExistente.DataHoraFim = desafio.DataHoraFim;

                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeletarDesafio(Guid id)
        {
            var desafioExistente = await _context.Desafios.FirstOrDefaultAsync(x => x.Id == id && !x.Deletado && !x.IdEventoNavigation.Deletado);

            if (desafioExistente == null) return false;

            desafioExistente.Deletado = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CoreDesafio> GetDesafioPorId(Guid id)
        {
            var desafio = await _context.Desafios.FirstOrDefaultAsync(x => x.Id == id && !x.Deletado && !x.IdEventoNavigation.Deletado);

            if (desafio == null) return null;

            return new CoreDesafio
            {
                Id = desafio.Id,
                IdEvento = desafio.IdEvento,
                Nome = desafio.Nome,
                Descricao = desafio.Descricao,
                Regra = desafio.Regra,
                Pontuacao = desafio.Pontuacao,
                TipoDesafio = desafio.TipoDesafio,
                QuantidadeDesafio = desafio.QuantidadeDesafio,
                DataHoraInicio = desafio.DataHoraInicio,
                DataHoraFim = desafio.DataHoraFim,
                Deletado = desafio.Deletado,
            };
        }

        public async Task<List<CoreDesafio>> GetDesafiosPorIdEvento(Guid idEvento)
        {
            var desafios = await _context.Desafios.Where(x => x.IdEvento == idEvento && !x.Deletado && !x.IdEventoNavigation.Deletado).ToListAsync();

            return desafios.Select(x => new CoreDesafio
            {
                Id = x.Id,
                IdEvento = x.IdEvento,
                Nome = x.Nome,
                Descricao = x.Descricao,
                Regra = x.Regra,
                Pontuacao = x.Pontuacao,
                TipoDesafio = x.TipoDesafio,
                QuantidadeDesafio = x.QuantidadeDesafio,
                DataHoraInicio = x.DataHoraInicio,
                DataHoraFim = x.DataHoraFim,
                Deletado = x.Deletado,
            }).ToList();

        }

        public async Task<CoreDesafio> DesafioJaCadastradoNesseEvento(Guid idEvento, Tipo_Desafio tipoDesafio, int quantidade)
        {
            var desafio = await _context.Desafios.FirstOrDefaultAsync(x => x.IdEvento == idEvento && !x.Deletado && !x.IdEventoNavigation.Deletado && x.TipoDesafio == tipoDesafio && x.QuantidadeDesafio == quantidade);

            if (desafio == null) return null;

            return new CoreDesafio
            {
                Id = desafio.Id,
                IdEvento = desafio.IdEvento,
                Nome = desafio.Nome,
                Descricao = desafio.Descricao,
                Regra = desafio.Regra,
                Pontuacao = desafio.Pontuacao,
                TipoDesafio = desafio.TipoDesafio,
                QuantidadeDesafio = desafio.QuantidadeDesafio,
                DataHoraInicio = desafio.DataHoraInicio,
                DataHoraFim = desafio.DataHoraFim,
                Deletado = desafio.Deletado,
            };

        }

    }
}



