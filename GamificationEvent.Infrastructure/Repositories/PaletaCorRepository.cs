using GamificationEvent.Core.Entidades;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using CoreCor = GamificationEvent.Core.Entidades.Cor;
using InfraCore = GamificationEvent.Infrastructure.Data.Persistence.Cor;
using CorePaleta = GamificationEvent.Core.Entidades.PaletaCor;
using InfraPaleta = GamificationEvent.Infrastructure.Data.Persistence.PaletaCor;
using GamificationEvent.Core.Interfaces;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class PaletaCorRepository : IPaletaCorRepository
    {
        private readonly AppDbContext _context;

        public PaletaCorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CoreCor> AdicionarCor(CoreCor cor)
        {
            var corDb = new InfraCore
            {
                Id = cor.Id,
                HexCodigo = cor.HexCodigo,
                Nome = cor.Nome
              };
            _context.Cors.Add(corDb);
            await _context.SaveChangesAsync();
            return cor;
        }

        public async Task<List<CoreCor>> GetCores()
        {
            var coresCore = new List<CoreCor>();

            var cores = await _context.Cors.ToListAsync();

            foreach (var cor in cores)
            {
                var corCore = new CoreCor
                {
                    Id = cor.Id,
                    HexCodigo = cor.HexCodigo,
                    Nome = cor.Nome
                };
                coresCore.Add(corCore);
             }

            return coresCore;
        }

        public async Task<CoreCor> GetCorPorid(Guid id)
        {
            var corDB = await _context.Cors.FirstOrDefaultAsync(c => c.Id == id);

            if (corDB == null)
                return null;

            var corCore = new CoreCor
            {
                Id = corDB.Id,
                HexCodigo = corDB.HexCodigo,
                Nome = corDB.Nome
            };
            return corCore;
        }

        public async Task<bool> AtualizarCor(CoreCor cor)
        {
            var corEF = await _context.Cors
             .FirstOrDefaultAsync(c => c.Id == cor.Id);

            if (corEF == null)
                return false;

            corEF.HexCodigo = cor.HexCodigo;
            corEF.Nome = cor.Nome;

          var linhasAfetadas = await _context.SaveChangesAsync();

            if (linhasAfetadas > 0)
                return true;

            return false;
        }

        public async Task<bool> CorJaExiste(string hexCod)
        {
            var cor = await _context.Cors.FirstOrDefaultAsync(x => x.HexCodigo == hexCod);

            return cor != null;
        }

        public async Task<CorePaleta> AdicionarPaleta(CorePaleta paleta)
        {
            var paletaDB = new InfraPaleta
            {
                 Id = paleta.Id,
                 Nome = paleta.Nome,
                 IdCor1 = paleta.IdCor1,
                 IdCor2 = paleta.IdCor2,
                 IdCor3 = paleta.IdCor3,
                 IdCor4 = paleta.IdCor4,
                 Deletado = paleta.Deletado
            };

            _context.PaletaCors.Add(paletaDB);
            await _context.SaveChangesAsync();
            return paleta;
        }

        public async Task<List<CorePaleta>> GetPaletas()
        {
            var paletasCore = new List<CorePaleta>();

            var paletasInfra = await _context.PaletaCors.Where(p => p.Deletado == false).ToListAsync();

            foreach (var paleta in paletasInfra)
            {
                var paletaCore = new CorePaleta
                {
                    Id = paleta.Id,
                    Nome = paleta.Nome,
                    IdCor1 = paleta.IdCor1,
                    IdCor2 = paleta.IdCor2,
                    IdCor3 = paleta.IdCor3,
                    IdCor4 = paleta.IdCor4,
                    Deletado = paleta.Deletado
                };
                paletasCore.Add(paletaCore);
            }
            return paletasCore;
        }

        public async Task<CorePaleta> GetPaletaPorId(Guid id)
        {
            var paletaInfra = await _context.PaletaCors.FirstOrDefaultAsync(p => p.Id == id && p.Deletado == false);

            if (paletaInfra == null)
                return null;

                var paleta = new CorePaleta
                {
                    Id = paletaInfra.Id,
                    Nome = paletaInfra.Nome,
                    IdCor1 = paletaInfra.IdCor1,
                    IdCor2 = paletaInfra.IdCor2,
                    IdCor3 = paletaInfra.IdCor3,
                    IdCor4 = paletaInfra.IdCor4,
                    Deletado = paletaInfra.Deletado
                };
              
            return paleta;
        }
        public async Task<bool> AtualizarPaleta(CorePaleta paleta)
        {
            var paletaEF = await _context.PaletaCors
              .FirstOrDefaultAsync(p => p.Id == paleta.Id && paleta.Deletado == false);
           

            paletaEF.Nome = paleta.Nome;
            paletaEF.IdCor1 = paleta.IdCor1;
            paletaEF.IdCor2 = paleta.IdCor2;
            paletaEF.IdCor3 = paleta.IdCor3;
            paletaEF.IdCor4 = paleta.IdCor4;

           var linhasAfetadas = await _context.SaveChangesAsync();

            if (linhasAfetadas > 0)
                return true;

            return false;
               
        }

        public async Task<bool> DeletarPaleta(Guid id)
        {
            var paletaEF = await _context.PaletaCors
            .FirstOrDefaultAsync(p => p.Id == id && p.Deletado == false);

            if (paletaEF == null)
                return false;

            paletaEF.Deletado = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PaletaExiste(Guid id)
        {
            var paleta = await _context.PaletaCors.FirstOrDefaultAsync(x => x.Id == id && !x.Deletado);
            return paleta != null;
        }

        public async Task<bool> PaletaPertenceAEvento(Guid idPaleta)
        {
            return await _context.Eventos
            .AnyAsync(e => e.IdPaleta == idPaleta && !e.Deletado);
        }
    }
}
