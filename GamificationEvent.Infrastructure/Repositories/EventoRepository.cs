using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreEvento = GamificationEvent.Core.Entidades.Evento;
using InfraEvento = GamificationEvent.Infrastructure.Data.Persistence.Evento;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class EventoRepository : IEventoRepository
    {
        private readonly AppDbContext _context;

        public EventoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CoreEvento> AdicionarEvento(CoreEvento evento)
        {
            var eventoDB = new InfraEvento
            {
                Id = evento.Id,
                IdPaleta = evento.IdPaleta,
                Titulo = evento.Titulo,
                Descricao = evento.Descricao,
                Objetivo = evento.Objetivo,
                Categoria = evento.Categoria,
                PublicoAlvo = evento.PublicoAlvo,
                DataInicio = evento.DataInicio,
                DataFinal = evento.DataFinal,
                Deletado = evento.Deletado
            };
            _context.Eventos.Add(eventoDB);
            await _context.SaveChangesAsync();

            return evento;
        }
        public async Task<List<CoreEvento>> GetEventos()
        {
            var eventosCore = new List<CoreEvento>();
            var eventosInfra = await _context.Eventos.Where(x => !x.Deletado).ToListAsync();

            foreach(var eventoInfra in eventosInfra)
            {
                var evento = new CoreEvento
                {
                    Id = eventoInfra.Id,
                    IdPaleta = eventoInfra.IdPaleta,
                    Titulo = eventoInfra.Titulo,
                    Descricao = eventoInfra.Descricao,
                    Objetivo = eventoInfra.Objetivo,
                    Categoria = eventoInfra.Categoria,
                    PublicoAlvo = eventoInfra.PublicoAlvo,
                    DataInicio = eventoInfra.DataInicio,
                    DataFinal = eventoInfra.DataFinal,
                    Deletado = eventoInfra.Deletado
                };
                eventosCore.Add(evento);
            }
            return eventosCore;
        }

        public async Task<CoreEvento> GetEventoPorId(Guid id)
        {
            var eventoInfra = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id && !e.Deletado);
            if (eventoInfra == null)
                return null;

            var evento = new CoreEvento
            {
                Id = eventoInfra.Id,
                IdPaleta = eventoInfra.IdPaleta,
                Titulo = eventoInfra.Titulo,
                Descricao = eventoInfra.Descricao,
                Objetivo = eventoInfra.Objetivo,
                Categoria = eventoInfra.Categoria,
                PublicoAlvo = eventoInfra.PublicoAlvo,
                DataInicio = eventoInfra.DataInicio,
                DataFinal = eventoInfra.DataFinal,
                Deletado = eventoInfra.Deletado
            };
            return evento;
        }

        public async Task<bool> DeletarEvento(Guid id)
        {
            var evento  = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == id && !e.Deletado);
            if (evento == null) return false;

            evento.Deletado = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarEvento(CoreEvento evento)
        {
            var eventoExistente = await _context.Eventos.FirstOrDefaultAsync(e => e.Id == evento.Id && !e.Deletado);
           
            if(eventoExistente == null) throw new Exception("Evento não encontrado.");

            eventoExistente.IdPaleta = evento.IdPaleta;
            eventoExistente.Titulo = evento.Titulo;
            eventoExistente.Descricao = evento.Descricao;
            eventoExistente.Objetivo = evento.Objetivo;
            eventoExistente.Categoria = evento.Categoria;
            eventoExistente.PublicoAlvo = evento.PublicoAlvo;
            eventoExistente.DataInicio = evento.DataInicio;
            eventoExistente.DataFinal = evento.DataFinal;

            var linhasAfetadas = await _context.SaveChangesAsync();

            if (linhasAfetadas > 0)
                return true;

            return false;
        }
    }
}
