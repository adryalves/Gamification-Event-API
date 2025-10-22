using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorePartPremio = GamificationEvent.Core.Entidades.ParticipantePremio;
using InfraPartPremio = GamificationEvent.Infrastructure.Data.Persistence.ParticipantePremio;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class ParticipantePremioRepository : IParticipantePremioRepository
    {
        private readonly AppDbContext _context;

        public ParticipantePremioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarParticipantePremio(CorePartPremio participantePremiumCore)
        {
            var infraPartPremium = new InfraPartPremio
            {
                Id = Guid.NewGuid(),
                IdParticipante = participantePremiumCore.IdParticipante,
                IdPremio = participantePremiumCore.IdPremio,
                Motivo = participantePremiumCore.Motivo,
                DataConcessao = participantePremiumCore.DataConcessao,
            };

            _context.ParticipantePremios.Add(infraPartPremium);
            await _context.SaveChangesAsync();
            return infraPartPremium.Id;
        }

        public async Task<List<CorePartPremio>> GetParticipantePremiosPorIdEvento(Guid idEvento)
        {
            var premio = await _context.ParticipantePremios
           .Include(x => x.IdPremioNavigation)
           .ThenInclude(p => p.IdEventoNavigation)
            .Include(x => x.IdParticipanteNavigation)
           .ThenInclude(p => p.IdUsuarioNavigation).Where(x => x.IdPremioNavigation.IdEventoNavigation.Id == idEvento && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado &&
          !x.IdPremioNavigation.Deletado).ToListAsync();

            var premiosCore = premio.Select(x => new CorePartPremio
            {
                Id = x.Id,
                IdParticipante = x.IdParticipante,
                IdPremio = x.IdPremio,
                Motivo = x.Motivo,
                DataConcessao = x.DataConcessao,
            }).ToList();

            return premiosCore;
        }
        public async Task<List<CorePartPremio>> GetParticipantePremiosPorIdParticipante(Guid idParticipante)
        {
            var premios = await _context.ParticipantePremios.Where(x => x.IdParticipante == idParticipante && !x.IdPremioNavigation.Deletado).ToListAsync();

            var premiosCore = premios.Select(x => new CorePartPremio
            {
                Id = x.Id,
                IdParticipante = x.IdParticipante,
                IdPremio = x.IdPremio,
                Motivo = x.Motivo,
                DataConcessao = x.DataConcessao,
            }).ToList();

            return premiosCore;
    }

        public async Task<List<CorePartPremio>> GetParticipantesPremioPorIdPremio(Guid idPremio)
        {
            var premios = await _context.ParticipantePremios
            .Include(x => x.IdPremioNavigation)
            .ThenInclude(p => p.IdEventoNavigation)
             .Include(x => x.IdParticipanteNavigation)
            .ThenInclude(p => p.IdUsuarioNavigation)
             .Where(x => x.IdPremio == idPremio && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado &&
           !x.IdPremioNavigation.Deletado && !x.IdPremioNavigation.IdEventoNavigation.Deletado).ToListAsync();

            var premiosCore = premios.Select(x => new CorePartPremio
            {
                Id = x.Id,
                IdParticipante = x.IdParticipante,
                IdPremio = x.IdPremio,
                Motivo = x.Motivo,
                DataConcessao = x.DataConcessao,
            }).ToList();

            return premiosCore;

        }

        public async Task<CorePartPremio> GetParticipantePremioPorId(Guid id)
        {
            var premio = await _context.ParticipantePremios
             .Include(x => x.IdPremioNavigation)
             .ThenInclude(p => p.IdEventoNavigation)
              .Include(x => x.IdParticipanteNavigation)
             .ThenInclude(p => p.IdUsuarioNavigation)
              .FirstOrDefaultAsync(x => x.Id == id && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado &&
            !x.IdPremioNavigation.Deletado && !x.IdPremioNavigation.IdEventoNavigation.Deletado);

            if (premio != null) {

                return new CorePartPremio
                {
                    Id = premio.Id,
                    IdParticipante = premio.IdParticipante,
                    IdPremio = premio.IdPremio,
                    Motivo = premio.Motivo,
                    DataConcessao = premio.DataConcessao,

                };
            }
            return null;
        }
        public async Task<bool> AtualizarParticipantePremio(CorePartPremio premio)
        {
            var premioInfra = await _context.ParticipantePremios
             .Include(x => x.IdPremioNavigation)
             .ThenInclude(p => p.IdEventoNavigation)
              .Include(x => x.IdParticipanteNavigation)
             .ThenInclude(p => p.IdUsuarioNavigation)
              .FirstOrDefaultAsync(x => x.Id == premio.Id && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado &&
            !x.IdPremioNavigation.Deletado && !x.IdPremioNavigation.IdEventoNavigation.Deletado);

            premioInfra.Motivo = premio.Motivo;
            premioInfra.DataConcessao = premio.DataConcessao;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}

