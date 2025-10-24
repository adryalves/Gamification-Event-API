using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorePalestrante = GamificationEvent.Core.Entidades.Palestrante;
using InfraPalestrante = GamificationEvent.Infrastructure.Data.Persistence.Palestrante;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class PalestranteRepository : IPalestranteRepository
    {
        private readonly AppDbContext _context;

        public PalestranteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarPalestrante(CorePalestrante palestrante)
        {
            var palestranteInfra = new InfraPalestrante
            {
                Id = Guid.NewGuid(),
                IdEvento = palestrante.IdEvento,
                Nome = palestrante.Nome,
                Email = palestrante.Email,
                Telefone = palestrante.Telefone,
                Profissao = palestrante.Profissao,
                DataNascimento = palestrante.DataNascimento,
                Linkedin = palestrante.Linkedin,
            };

            _context.Palestrantes.Add(palestranteInfra);
            await _context.SaveChangesAsync();
            return palestranteInfra.Id;
        }

        public async Task<bool> AtualizarPalestrante(CorePalestrante palestrante)
        {
            var palestranteInfra = await _context.Palestrantes.FirstOrDefaultAsync(x => x.Id == palestrante.Id && !x.Deletado);

            if (palestranteInfra != null)
            {

                palestranteInfra.Nome = palestrante.Nome;
                palestranteInfra.Email = palestrante.Email;
                palestranteInfra.Telefone = palestrante.Telefone;
                palestranteInfra.Profissao = palestrante.Profissao;
                palestranteInfra.DataNascimento = palestrante.DataNascimento;
                palestranteInfra.Linkedin = palestrante.Linkedin;

                await _context.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<bool> DeletarPalestrante(Guid id)
        {
            var palestrante = await _context.Palestrantes.FirstOrDefaultAsync(x => x.Id == id && !x.Deletado);
            palestrante.Deletado = true;

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<List<CorePalestrante>> GetPalestrantesPorIdEvento(Guid idEvento)
        {
            var palestrantes = await _context.Palestrantes.Where(x => x.IdEvento == idEvento && !x.Deletado).ToListAsync();

            return palestrantes.Select(x => new CorePalestrante
            {
                Id = x.Id,
                IdEvento = x.IdEvento,
                Nome = x.Nome,
                Email = x.Email,
                Telefone = x.Telefone,
                Profissao = x.Profissao,
                DataNascimento = x.DataNascimento,
                Linkedin = x.Linkedin,
                Deletado = x.Deletado,
            }).ToList();
        }


        public async Task<CorePalestrante> GetPalestrantePorId(Guid id)
        {
            var palestrante = await _context.Palestrantes.FirstOrDefaultAsync(x => x.Id == id && !x.Deletado);

            if (palestrante != null)
            {

                return new CorePalestrante
                {
                    Id = palestrante.Id,
                    IdEvento = palestrante.IdEvento,
                    Nome = palestrante.Nome,
                    Email = palestrante.Email,
                    Telefone = palestrante.Telefone,
                    Profissao = palestrante.Profissao,
                    DataNascimento = palestrante.DataNascimento,
                    Linkedin = palestrante.Linkedin,
                    Deletado = palestrante.Deletado,
                };
            }
            return null;
        }

        public async Task<List<CorePalestrante>> GetPalestrantesPorIdSubEvento(Guid idSubEvento)
        {
            var palestrantes = await _context.PalestranteSubEventos
            .Where(ps => ps.IdSubEvento == idSubEvento && !ps.IdPalestranteNavigation.Deletado)
            .Select(ps => ps.IdPalestranteNavigation)
            .ToListAsync();

            return palestrantes.Select(x => new CorePalestrante
            {
                Id = x.Id,
                IdEvento = x.IdEvento,
                Nome = x.Nome,
                Email = x.Email,
                Telefone = x.Telefone,
                Profissao = x.Profissao,
                DataNascimento = x.DataNascimento,
                Linkedin = x.Linkedin,
                Deletado = x.Deletado,
            }).ToList();
        }

    }
}