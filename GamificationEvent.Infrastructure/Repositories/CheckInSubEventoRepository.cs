using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using CoreCheckIn = GamificationEvent.Core.Entidades.CheckInSubEvento;
using InfraCheckIn = GamificationEvent.Infrastructure.Data.Persistence.CheckinSubEvento;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class CheckInSubEventoRepository : ICheckInSubEventoRepository
    {
        private readonly AppDbContext _context;

        public CheckInSubEventoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarCheckIn(CoreCheckIn checkIn)
        {
            var checkInDB = new InfraCheckIn
            {
                Id = Guid.NewGuid(),
                IdSubEvento = checkIn.IdSubEvento,
                IdParticipante = checkIn.IdParticipante,
                DataHora = DateTime.UtcNow,
            };
            _context.CheckinSubEventos.Add(checkInDB);
            await _context.SaveChangesAsync();
            return checkInDB.Id;
        }

        public async Task<List<CoreCheckIn>> GetCheckInFeitosPorIdParticipante(Guid idParticipante)
        {
            var checkIns = await _context.CheckinSubEventos
        .Include(x => x.IdSubEventoNavigation)
            .ThenInclude(s => s.IdEventoNavigation)
        .Where(x =>
            x.IdParticipante == idParticipante &&
            !x.IdSubEventoNavigation.Deletado &&
            !x.IdSubEventoNavigation.IdEventoNavigation.Deletado
        )
        .ToListAsync();

            return checkIns.Select(x => new CoreCheckIn
            {
                Id = x.Id,
                IdSubEvento = x.IdSubEvento,
                IdParticipante = x.IdParticipante,
                DataHora = x.DataHora,
            }).ToList();
        }

        public async Task<List<CoreCheckIn>> GetCheckInFeitosPorIdSubEvento(Guid idSubEvento)
        {
            var checkIns = await _context.CheckinSubEventos
        .Include(x => x.IdParticipanteNavigation)
        .ThenInclude(p => p.IdUsuarioNavigation)
        .Where(x =>
            x.IdSubEvento == idSubEvento &&
            !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado

        )
        .ToListAsync();

            return checkIns.Select(x => new CoreCheckIn
            {
                Id = x.Id,
                IdSubEvento = x.IdSubEvento,
                IdParticipante = x.IdParticipante,
                DataHora = x.DataHora,
            }).ToList();
        }

        public async Task<CoreCheckIn> GetCheckInPorId(Guid id)
        {
            var checkIn = await _context.CheckinSubEventos
                .Include(x => x.IdSubEventoNavigation)
            .ThenInclude(s => s.IdEventoNavigation)
            .Include(i => i.IdParticipanteNavigation)
        .ThenInclude(p => p.IdUsuarioNavigation).FirstOrDefaultAsync(x => x.Id == id && !x.IdSubEventoNavigation.Deletado
        && !x.IdSubEventoNavigation.IdEventoNavigation.Deletado && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado);

            if (checkIn == null) return null;

            return new CoreCheckIn
            {
                Id = checkIn.Id,
                IdSubEvento = checkIn.IdSubEvento,
                IdParticipante = checkIn.IdParticipante,
                DataHora = checkIn.DataHora,
            };
        }

        public async Task<bool> ParticipanteRealizouCheckIn(Guid idSubEvento, Guid idParticipante)
        {
            var checkIn = await _context.CheckinSubEventos.FirstOrDefaultAsync(x => x.IdSubEvento == idSubEvento &&
            x.IdParticipante == idParticipante);

            if(checkIn == null) return false;

            return true;
        }
    }

}