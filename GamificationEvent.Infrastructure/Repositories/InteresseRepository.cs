using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreInteresse = GamificationEvent.Core.Entidades.Interesse;
using InfraInteresse = GamificationEvent.Infrastructure.Data.Persistence.Interesse;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class InteresseRepository : IInteresseRepository
    {
        private readonly AppDbContext _context;

        public InteresseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AdicionarInteresses(List<CoreInteresse> interesses)
        {
            var infraInteresses = new List<InfraInteresse>();

            foreach (var interesse in interesses)
            {
                var infraInteresse = new InfraInteresse
                {
                    Id = Guid.NewGuid(),
                    IdEvento = interesse.IdEvento,
                    Nome = interesse.Nome,
                    Deletado = interesse.Deletado,
                };

                infraInteresses.Add(infraInteresse);
            }

            _context.Interesses.AddRange(infraInteresses);

            var linhasAfetadas = await _context.SaveChangesAsync();

            return linhasAfetadas;
        }

        public async Task<bool> DeletarInteresse(Guid id)
        {
            var interesse = _context.Interesses.FirstOrDefault(i => i.Id == id && !i.Deletado);

            if (interesse == null) return false;

            interesse.Deletado = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<CoreInteresse>> GetInteressesPorIdEvento(Guid idEvento)
        {
            var CoreInteresses = new List<CoreInteresse>();
            var InfraInteresses = await _context.Interesses.Where(i => i.IdEvento == idEvento && !i.Deletado).ToListAsync();

            foreach (var interesse in InfraInteresses)
            {
                var CoreInteresse  = new CoreInteresse
                {
                    Id = interesse.Id,
                    IdEvento = interesse.IdEvento,
                    Nome = interesse.Nome,
                    Deletado = interesse.Deletado,
                };

                CoreInteresses.Add(CoreInteresse);
            }
            return CoreInteresses;
        }

        public async Task<CoreInteresse> GetInteressePorId(Guid id)
        {
            var interesse = await _context.Interesses.FirstOrDefaultAsync(i => i.Id == id && !i.Deletado);

            if (interesse == null) return null;

            return new CoreInteresse
            {
                Id = interesse.Id,
                IdEvento = interesse.IdEvento,
                Nome = interesse.Nome,
                Deletado = interesse.Deletado
            };
        }

    }
}

