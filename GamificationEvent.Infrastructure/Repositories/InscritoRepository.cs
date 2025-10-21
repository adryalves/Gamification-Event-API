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
using CoreInscrito = GamificationEvent.Core.Entidades.Inscrito;
using InfraInscrito = GamificationEvent.Infrastructure.Data.Persistence.Inscrito;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class InscritoRepository : IInscritoRepository
    {
        private readonly AppDbContext _context;

        public InscritoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AdicionarTodosOsInscrito(List<CoreInscrito> inscritosCore)
        {
            var inscritosInfra = new List<InfraInscrito>();
            foreach(var inscritoCore in inscritosCore) {

                var inscritoInfra = new InfraInscrito
                {
                    Id = inscritoCore.Id,
                    Cpf = inscritoCore.Cpf,
                    IdEvento = inscritoCore.IdEvento,
                    Nome = inscritoCore.Nome,
                    Cargo = inscritoCore.Cargo,
                };

                inscritosInfra.Add(inscritoInfra);
            }

            _context.Inscritos.AddRange(inscritosInfra);
            var linhasAfetadas = await _context.SaveChangesAsync();

            return linhasAfetadas;
        }

        public async Task<CoreInscrito> AdicionarInscrito(CoreInscrito inscritoCore)
        {
            var inscritoInfra = new InfraInscrito
            {   Id = inscritoCore.Id,
                Cpf = inscritoCore.Cpf,
                IdEvento = inscritoCore.IdEvento,
                Nome = inscritoCore.Nome,
                Cargo = inscritoCore.Cargo,
            };

            _context.Inscritos.Add(inscritoInfra);
              await _context.SaveChangesAsync();

            return inscritoCore;
        }

        public async Task<bool> DeletarInscrito(string cpf, Guid idEvento)
        {
            var inscrito = await _context.Inscritos.FirstOrDefaultAsync(i => i.Cpf == cpf && i.IdEvento == idEvento);

            if (inscrito == null) return false;

            _context.Inscritos.Remove(inscrito);

           int linhasAfetadas =  await _context.SaveChangesAsync();

            if (linhasAfetadas > 0)
                return true;

            return false;
        }
        public async Task<List<CoreInscrito>> GetInscritos()
        {
            var inscritosCore = new List<CoreInscrito>();
            var inscritosInfra = await _context.Inscritos.ToListAsync();

            foreach (var inscritoInfra in inscritosInfra)
            {
                var inscritoCore = new CoreInscrito
                {
                    Id = inscritoInfra.Id,
                    Cpf = inscritoInfra.Cpf,
                    IdEvento = inscritoInfra.IdEvento,
                    Nome = inscritoInfra.Nome,
                    Cargo = inscritoInfra.Cargo
                };
                inscritosCore.Add(inscritoCore);
            }
            return inscritosCore;
        }

        public async Task<List<CoreInscrito>> GetInscritosPorIdEvento(Guid idEvento)
        {
            var inscritosCore = new List<CoreInscrito>();
            var inscritosInfra = await _context.Inscritos.Where(i => i.IdEvento == idEvento).ToListAsync();

            foreach (var inscritoInfra in inscritosInfra)
            {
                var inscritoCore = new CoreInscrito
                {
                    Id = inscritoInfra.Id,
                    Cpf = inscritoInfra.Cpf,
                    IdEvento = inscritoInfra.IdEvento,
                    Nome = inscritoInfra.Nome,
                    Cargo = inscritoInfra.Cargo
                };
                inscritosCore.Add(inscritoCore);
            }
            return inscritosCore;
        }

        public async Task<CoreInscrito> JaExisteEsseInscrito(string cpf, Guid idEvento)
        {
           var inscrito =  await _context.Inscritos.FirstOrDefaultAsync(i => i.IdEvento.Equals(idEvento) && i.Cpf == cpf);

          if (inscrito == null)
                return null;

            var inscritoCore = new CoreInscrito
            {
                Id = inscrito.Id,
                Cpf = inscrito.Cpf,
                IdEvento = inscrito.IdEvento,
                Nome = inscrito.Nome,
                Cargo = inscrito.Cargo
            };
            return inscritoCore; 
        }
     
    }
}
