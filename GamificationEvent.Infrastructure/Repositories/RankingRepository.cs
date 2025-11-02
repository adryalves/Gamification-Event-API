using GamificationEvent.Infrastructure.Data.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Models;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class RankingRepository : IRankingRepository
    {
        private readonly AppDbContext _context;

        public RankingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RankingModel>> GetRankingGeralPorIdEvento(Guid idEvento, int quantidade)
        {
            var ranking = await _context.Participantes
                .Where(p => p.IdEvento == idEvento &&
                       p.Cargo == Core.Enums.Cargo.Membro &&
                      !p.IdUsuarioNavigation.Deletado &&
                      !p.IdEventoNavigation.Deletado)
                .OrderByDescending(p => p.Pontuacao)
                .Select(p => new RankingModel
                {
                    IdParticipante = p.Id,
                    Foto = p.IdUsuarioNavigation.Foto,
                    Nome = p.IdUsuarioNavigation.Nome,
                    Pontuacao = p.Pontuacao,
                    Email = p.IdUsuarioNavigation.Email,
                }).Take(quantidade)
                .ToListAsync();

            int posicao = 1;
            foreach (var item in ranking)
            {
                item.Posicao = posicao++;
            }

            return ranking;
        }

        public async Task<List<RankingModel>> GetRankingPersonalizado(Guid idEvento, Guid idParticipante, int quantidade)
        {
            var ranking = await _context.Participantes
           .Where(p => p.IdEvento == idEvento &&
                  p.Cargo == Core.Enums.Cargo.Membro &&
                  !p.IdUsuarioNavigation.Deletado &&
                  !p.IdEventoNavigation.Deletado)
           .OrderByDescending(p => p.Pontuacao)
           .Select(p => new RankingModel
           {
               IdParticipante = p.Id,
               Nome = p.IdUsuarioNavigation.Nome,
               Email = p.IdUsuarioNavigation.Email,
               Foto = p.IdUsuarioNavigation.Foto,
               Pontuacao = p.Pontuacao
           })
           .Take(quantidade)
           .ToListAsync();

            int posicao = 1;
            foreach (var item in ranking)
            {
                item.Posicao = posicao++;
            }

            if (!ranking.Any(r => r.IdParticipante == idParticipante))
            {
                var TodosOsParticipantes = await _context.Participantes
                    .Where(p => p.IdEvento == idEvento &&
                     p.Cargo == Core.Enums.Cargo.Membro &&
                    !p.IdUsuarioNavigation.Deletado &&
                    !p.IdEventoNavigation.Deletado)
                    .OrderByDescending(p => p.Pontuacao)
                    .Select(p => new RankingModel
                    {
                        IdParticipante = p.Id,
                        Nome = p.IdUsuarioNavigation.Nome,
                        Email = p.IdUsuarioNavigation.Email,
                        Foto = p.IdUsuarioNavigation.Foto,
                        Pontuacao = p.Pontuacao
                    })
                    .ToListAsync();

                var participante = TodosOsParticipantes
                    .Select((r, index) => new RankingModel
                    {
                        IdParticipante = r.IdParticipante,
                        Nome = r.Nome,
                        Email = r.Email,
                        Foto = r.Foto,
                        Pontuacao = r.Pontuacao,
                        Posicao = index + 1
                    })
                    .FirstOrDefault(r => r.IdParticipante == idParticipante);

                if (participante != null)
                    ranking.Add(participante);
            }
            return ranking.OrderBy(r => r.Posicao).ToList();

        }
    }
}
