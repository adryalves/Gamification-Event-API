using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreParticipante = GamificationEvent.Core.Entidades.Participante;
using InfraParticipante = GamificationEvent.Infrastructure.Data.Persistence.Participante;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class ParticipanteRepository : IParticipanteRepository
    {
        private readonly AppDbContext _context;

        public ParticipanteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarParticipante(CoreParticipante participanteCore)
        {
            var participanteDB = new InfraParticipante
            {
                Id = participanteCore.Id,
                IdEvento = participanteCore.IdEvento,
                IdUsuario = participanteCore.IdUsuario,
                Cargo = participanteCore.Cargo,
                Pontuacao = participanteCore.Pontuacao,
                PrimeiroParticipante = participanteCore.PrimeiroParticipante,
                DataHoraCriacao = DateTime.UtcNow,
                ParticipanteInteresses = participanteCore.ParticipanteInteresses.Select(x => new ParticipanteInteresse
                {
                    Id = x.Id,
                    IdInteresse = x.IdInteresse,
                    IdParticipante = x.IdParticipante,
                }).ToList()
            };
            _context.Participantes.Add(participanteDB);
            await _context.SaveChangesAsync();

            return participanteDB.Id;
        }
        public async Task<bool> ParticipanteJaExiste(Guid idEvento, Guid idUsuario)
        {
            var usuario = await _context.Participantes.FirstOrDefaultAsync(x => x.IdEvento == idEvento && x.IdUsuario == idUsuario);

            return usuario != null;
        }

        public async Task<CoreParticipante> GetParticipantePorId(Guid id)
        {
            var participante = await _context.Participantes.Include(p => p.ParticipanteInteresses).FirstOrDefaultAsync(x => x.Id == id && !x.IdUsuarioNavigation.Deletado &&
                      !x.IdEventoNavigation.Deletado);

            if (participante != null)
            {
                var participanteCore = new CoreParticipante
                {
                    Id = participante.Id,
                    IdEvento = participante.IdEvento,
                    IdUsuario = participante.IdUsuario,
                    Cargo = participante.Cargo,
                    Pontuacao = participante.Pontuacao,
                    PrimeiroParticipante = participante.PrimeiroParticipante,
                    DataHoraCriacao = participante.DataHoraCriacao,
                    ParticipanteInteresses = participante.ParticipanteInteresses
                    .Select(x => new Core.Entidades.ParticipanteInteresse
                    {
                        Id = x.Id,
                        IdInteresse = x.IdInteresse,
                        IdParticipante = x.IdParticipante,
                    }).ToList()
                };

                return participanteCore;
            }
            return null;
        }

        public async Task<List<CoreParticipante>> GetParticipantesPorIdEvento(Guid idEvento)
        {
            var participantes = await _context.Participantes.Include(p => p.ParticipanteInteresses).Where(x => x.IdEvento == idEvento && !x.IdUsuarioNavigation.Deletado &&
                      !x.IdEventoNavigation.Deletado).ToListAsync();

            var participantesCore = new List<CoreParticipante>();

            foreach (var participante in participantes)
            {
                var participanteCore = new CoreParticipante
                {
                    Id = participante.Id,
                    IdEvento = participante.IdEvento,
                    IdUsuario = participante.IdUsuario,
                    Cargo = participante.Cargo,
                    Pontuacao = participante.Pontuacao,
                    PrimeiroParticipante = participante.PrimeiroParticipante,
                    DataHoraCriacao = participante.DataHoraCriacao,
                    ParticipanteInteresses = participante.ParticipanteInteresses
                    .Select(x => new Core.Entidades.ParticipanteInteresse
                    {
                        Id = x.Id,
                        IdInteresse = x.IdInteresse,
                        IdParticipante = x.IdParticipante,
                    }).ToList()
                };
                participantesCore.Add(participanteCore);
            }
            return participantesCore;
            }

        public async Task<bool> AtualizarParticipante(CoreParticipante participante)
        {
            var participanteEF = await _context.Participantes.Include(p => p.ParticipanteInteresses).FirstOrDefaultAsync(x => x.Id == participante.Id);
           

            participanteEF.Cargo = participante.Cargo;
            participanteEF.Pontuacao = participante.Pontuacao;

            _context.ParticipanteInteresses.RemoveRange(participanteEF.ParticipanteInteresses);

            participanteEF.ParticipanteInteresses = participante.ParticipanteInteresses.Select(
                pi => new ParticipanteInteresse
                {
                    Id = pi.Id,
                    IdInteresse = pi.IdInteresse,
                    IdParticipante = pi.IdParticipante,
                }).ToList();
 
            var linhasAfetadas = await _context.SaveChangesAsync();

            if (linhasAfetadas > 0) return true;

            return false;
        }

    }

    }



