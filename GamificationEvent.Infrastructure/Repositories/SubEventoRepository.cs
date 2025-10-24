using GamificationEvent.Core.Entidades;
using GamificationEvent.Core.Enums;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreSubEvento = GamificationEvent.Core.Entidades.SubEvento;
using InfraSubEvento = GamificationEvent.Infrastructure.Data.Persistence.SubEvento;
using CorePerguntas = GamificationEvent.Core.Entidades.PerguntasSubEvento;
using InfraPerguntas = GamificationEvent.Infrastructure.Data.Persistence.PerguntasSubEvento;
using GamificationEvent.Core.Interfaces;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class SubEventoRepository : ISubEventoRepository
    {
        private readonly AppDbContext _context;

        public SubEventoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> AdicionarSubEvento(CoreSubEvento subEvento)
        {
            var idSubEvento = Guid.NewGuid();
            var infraSubEvento = new InfraSubEvento
            {
                Id = idSubEvento,
                IdEvento = subEvento.IdEvento,
                IdPontoMapa = subEvento.IdPontoMapa,
                Nome = subEvento.Nome,
                LocalSubEvento = subEvento.LocalSubEvento,
                Assunto = subEvento.Assunto,
                Tipo = subEvento.Tipo,
                Categoria = subEvento.Categoria,
                Modalidade = subEvento.Modalidade,
                DataSubEvento = subEvento.DataSubEvento,
                HorarioInicio = subEvento.HorarioInicio,
                HorarioFim = subEvento.HorarioFim,
                CodigoCheckin = subEvento.CodigoCheckin,
                PalestranteSubEventos = subEvento.Palestrantes.Select(p => new PalestranteSubEvento
                {
                    Id = Guid.NewGuid(),
                    IdSubEvento = idSubEvento,
                    IdPalestrante = p.IdPalestrante,

                }).ToList()
            };

            _context.SubEventos.Add(infraSubEvento);
            await _context.SaveChangesAsync();
            return idSubEvento;
        }

        public async Task<bool> AtualizarSubEvento(CoreSubEvento subEvento)
        {
            var subEventoEF = await _context.SubEventos.Include(s => s.PalestranteSubEventos).FirstOrDefaultAsync(s => s.Id == subEvento.Id && !s.Deletado && !s.IdEventoNavigation.Deletado);

            // id ponto Mapa futuramente poderá ser atualizado
            subEventoEF.Nome = subEvento.Nome;
            subEventoEF.LocalSubEvento = subEvento.LocalSubEvento;
            subEventoEF.Assunto = subEvento.Assunto;
            subEventoEF.Tipo = subEvento.Tipo;
            subEventoEF.Categoria = subEvento.Categoria;
            subEventoEF.Modalidade = subEvento.Modalidade;
            subEventoEF.DataSubEvento = subEvento.DataSubEvento;
            subEventoEF.HorarioInicio = subEvento.HorarioInicio;
            subEventoEF.HorarioFim = subEvento.HorarioFim;
            subEventoEF.CodigoCheckin = subEventoEF.CodigoCheckin;

            _context.PalestranteSubEventos.RemoveRange(subEventoEF.PalestranteSubEventos);
            subEventoEF.PalestranteSubEventos = subEvento.Palestrantes.Select(p => new PalestranteSubEvento
            {
                IdSubEvento = subEvento.Id,
                IdPalestrante = p.IdPalestrante,
            }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarSubEvento(Guid id)
        {
            var subEventoEF = await _context.SubEventos.Include(s => s.PalestranteSubEventos).FirstOrDefaultAsync(s => s.Id == id && !s.Deletado && !s.IdEventoNavigation.Deletado);

            if (subEventoEF == null) return false;

            subEventoEF.Deletado = true;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Guid> AdicionarPerguntaProSubEvento(CorePerguntas perguntaSubEvento)
        {
            var perguntaSubEventoEF = new InfraPerguntas
            {
                Id = Guid.NewGuid(),
                IdParticipante = perguntaSubEvento.IdParticipante,
                IdSubEvento = perguntaSubEvento.IdSubEvento,
                Assunto = perguntaSubEvento.Assunto,
                Pergunta = perguntaSubEvento.Pergunta,
                DataHora = perguntaSubEvento.DataHora,
            };

            _context.PerguntasSubEventos.Add(perguntaSubEventoEF);
            await _context.SaveChangesAsync();

            return perguntaSubEventoEF.Id;
        }

        public async Task<List<CoreSubEvento>> GetSubEventosPorIdEvento(Guid idEvento)
        {
            var subEventosEF = await _context.SubEventos.Include(p => p.PalestranteSubEventos).Where(s => s.IdEvento == idEvento && !s.Deletado && !s.IdEventoNavigation.Deletado).ToListAsync();
            var subEventos = subEventosEF.Select(x => new CoreSubEvento
            {

                Id = x.Id,
                IdEvento = x.IdEvento,
                IdPontoMapa = x.IdPontoMapa,
                Nome = x.Nome,
                LocalSubEvento = x.LocalSubEvento,
                Assunto = x.Assunto,
                Tipo = x.Tipo,
                Categoria = x.Categoria,
                Modalidade = x.Modalidade,
                DataSubEvento = x.DataSubEvento,
                HorarioInicio = x.HorarioInicio,
                HorarioFim = x.HorarioFim,
                CodigoCheckin = x.CodigoCheckin,
                Palestrantes = x.PalestranteSubEventos.Select(p => new PalestrantesSubEvento
                {
                    Id = p.Id,
                    IdPalestrante = p.IdPalestrante,

                }).ToList()
            }).ToList();

            return subEventos;
            }

        public async Task<CoreSubEvento> GetSubEventoPorId(Guid id)
        {
            var subEventoEF = await _context.SubEventos.Include(p => p.PalestranteSubEventos).FirstOrDefaultAsync(s => s.Id == id && !s.Deletado && !s.IdEventoNavigation.Deletado);

            if (subEventoEF == null) return null;

            return new CoreSubEvento
            {

                Id = subEventoEF.Id,
                IdEvento = subEventoEF.IdEvento,
                IdPontoMapa = subEventoEF.IdPontoMapa,
                Nome = subEventoEF.Nome,
                LocalSubEvento = subEventoEF.LocalSubEvento,
                Assunto = subEventoEF.Assunto,
                Tipo = subEventoEF.Tipo,
                Categoria = subEventoEF.Categoria,
                Modalidade = subEventoEF.Modalidade,
                DataSubEvento = subEventoEF.DataSubEvento,
                HorarioInicio = subEventoEF.HorarioInicio,
                HorarioFim = subEventoEF.HorarioFim,
                CodigoCheckin = subEventoEF.CodigoCheckin,
                Palestrantes = subEventoEF.PalestranteSubEventos.Select(p => new PalestrantesSubEvento
                {
                    Id = p.Id,
                    IdPalestrante = p.IdPalestrante,

                }).ToList()
            };
        }
        public async Task<List<CorePerguntas>> GetPerguntasPorIdSubEvento(Guid idSubEvento)
        {
        var perguntas = await _context.PerguntasSubEventos.Where(x => x.IdSubEvento == idSubEvento && !x.IdSubEventoNavigation.Deletado && !x.IdParticipanteNavigation.IdUsuarioNavigation.Deletado)
                 .Include(x => x.IdSubEventoNavigation)
                 .Include(x => x.IdParticipanteNavigation) 
        .ThenInclude(p => p.IdUsuarioNavigation).ToListAsync();

            return perguntas.Select(x => new CorePerguntas
            {
                Id = x.Id,
                IdParticipante = x.IdParticipante,
                IdSubEvento = x.IdSubEvento,
                Assunto = x.Assunto,
                Pergunta = x.Pergunta,
                DataHora = x.DataHora,
            }).ToList();

        }

        public async Task<bool> PalestranteJaEstaNesseSubEvento(Guid idSubEvento, Guid idPalestrante)
        {
            var existe = await _context.PalestranteSubEventos.FirstOrDefaultAsync(x => x.IdSubEvento == idSubEvento && x.IdPalestrante ==  idPalestrante);

            return existe != null;
        }

    }
    }



