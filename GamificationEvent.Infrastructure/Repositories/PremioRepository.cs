using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorePremio = GamificationEvent.Core.Entidades.Premio;
using InfraPremio = GamificationEvent.Infrastructure.Data.Persistence.Premio;


namespace GamificationEvent.Infrastructure.Repositories
{
    public class PremioRepository : IPremioRepository
    {
        private readonly AppDbContext _context;

        public PremioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarPremio(CorePremio premio)
        {
            var infraPremio = new InfraPremio
            {
                Id = Guid.NewGuid(),
                IdEvento = premio.IdEvento,
                // aqui teria idPatrocinador quando for implementado
                Nome = premio.Nome,
                Descricao = premio.Descricao,
                Tipo = premio.Tipo,
                InfoResgate = premio.InfoResgate,
            };

            _context.Premios.Add(infraPremio);
            await _context.SaveChangesAsync();

            return infraPremio.Id;
        }
        public async Task<List<CorePremio>> GetPremiosPorIdEvento(Guid idEvento)
        {
            var premiosInfra = await _context.Premios.Where(p => p.IdEvento == idEvento && !p.Deletado).ToListAsync();

            var premiosCore = premiosInfra.Select(
                x => new CorePremio
                {
                    Id = x.Id,
                    IdEvento = x.IdEvento,
                    IdPatrocinador = x.IdPatrocinador,
                    Nome = x.Nome,
                    Descricao = x.Descricao,
                    Tipo = x.Tipo,
                    InfoResgate = x.InfoResgate,
                    Deletado = x.Deletado,
                }).ToList();

            return premiosCore;
        }

        public async Task<CorePremio> GetPremioPorid(Guid id)
        {
            var premioInfra = await _context.Premios.FirstOrDefaultAsync(p => p.Id == id && !p.Deletado);

            if (premioInfra != null)
            {
                var premioCore = new CorePremio
                {
                    Id = premioInfra.Id,
                    IdEvento = premioInfra.IdEvento,
                    IdPatrocinador = premioInfra.IdPatrocinador,
                    Nome = premioInfra.Nome,
                    Descricao = premioInfra.Descricao,
                    Tipo = premioInfra.Tipo,
                    InfoResgate = premioInfra.InfoResgate,
                    Deletado = premioInfra.Deletado

                };
                return premioCore;
            }
            return null;
        }
        public async Task<bool> AtualizarPremio(CorePremio premio)
        {
            var premioInfra = await _context.Premios.FirstOrDefaultAsync(p => p.Id == premio.Id && !p.Deletado);

            premioInfra.IdPatrocinador = premio.IdPatrocinador;
            premioInfra.Nome = premio.Nome;
            premioInfra.Descricao = premio.Descricao;
            premioInfra.Tipo = premio.Tipo;
            premioInfra.InfoResgate = premio.InfoResgate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarPremio(Guid id)
        {
            var premioInfra = await _context.Premios.FirstOrDefaultAsync(p => p.Id == id && !p.Deletado);

            if (premioInfra == null) return false;

            premioInfra.Deletado = true;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}


