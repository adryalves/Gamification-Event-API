using GamificationEvent.Core.Interfaces;
using GamificationEvent.Infrastructure.Data.Persistence;
using CoreUsuario = GamificationEvent.Core.Entidades.Usuario;
using InfraUsuario = GamificationEvent.Infrastructure.Data.Persistence.Usuario;
using Microsoft.EntityFrameworkCore;
using GamificationEvent.Core.Entidades;

namespace GamificationEvent.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CoreUsuario> AdicionarUsuario(CoreUsuario usuario)
        {
            var usuarioDB = new InfraUsuario
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Cpf = usuario.Cpf,
                SenhaHash = usuario.SenhaHash,
                Telefone = usuario.Telefone,
                DataDeNascimento = usuario.DataDeNascimento,
                Foto = usuario.Foto,
                DataHoraCriacao = usuario.DataHoraCriacao,
                Deletado = usuario.Deletado,
                UsuarioRedeSocials = usuario.RedesSociais.Select(r => new Data.Persistence.UsuarioRedeSocial
                {
                    Id = r.Id,
                    IdUsuario = r.IdUsuario,
                    Plataforma = r.Plataforma,
                    Url = r.Url
                }).ToList()
            };

            _context.Usuarios.Add(usuarioDB);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<bool> CpfExiste(string cpf)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Cpf == cpf && !x.Deletado);

            return usuario != null;
        }

        public async Task<bool> EmailExiste(string email)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == email && !x.Deletado);
            return usuario != null;
        }

        public async Task<List<CoreUsuario>> GetUsuarios()
        {
            var usuariosCore = new List<CoreUsuario>();

            var usuarios = await _context.Usuarios.Include(u => u.UsuarioRedeSocials).Where(u => !u.Deletado)
          .ToListAsync();

            foreach (var usuario in usuarios)
            {
                var usuarioCore = new CoreUsuario
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Cpf = usuario.Cpf,
                    SenhaHash = usuario.SenhaHash,
                    Telefone = usuario.Telefone,
                    DataDeNascimento = usuario.DataDeNascimento,
                    Foto = usuario.Foto,
                    DataHoraCriacao = usuario.DataHoraCriacao,
                    Deletado = usuario.Deletado,
                    RedesSociais = usuario.UsuarioRedeSocials

                .Select(redeSocialEF => new Core.Entidades.UsuarioRedeSocial
                {
                    Id = redeSocialEF.Id,
                    Plataforma = redeSocialEF.Plataforma,
                    Url = redeSocialEF.Url
                })
                .ToList()
                };

                usuariosCore.Add(usuarioCore);
            }
            return usuariosCore;
        }

        public async Task<CoreUsuario> GetUsuarioPorId(Guid id)
        {
            var usuario = await _context.Usuarios.Include(u => u.UsuarioRedeSocials).FirstOrDefaultAsync(u => u.Id == id && !u.Deletado);

            if (usuario != null)
            {
                var usuarioCore = new CoreUsuario
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email,
                    Cpf = usuario.Cpf,
                    SenhaHash = usuario.SenhaHash,
                    Telefone = usuario.Telefone,
                    DataDeNascimento = usuario.DataDeNascimento,
                    Foto = usuario.Foto,
                    DataHoraCriacao = usuario.DataHoraCriacao,
                    Deletado = usuario.Deletado,
                    RedesSociais = usuario.UsuarioRedeSocials

                   .Select(redeSocialEF => new Core.Entidades.UsuarioRedeSocial
                   {
                       Id = redeSocialEF.Id,
                       Plataforma = redeSocialEF.Plataforma,
                       Url = redeSocialEF.Url
                   })
                   .ToList()
                };
                return usuarioCore;
            }

            return null;
        }

        public async Task<bool> DeletarUsuario(Guid id)
        {
            var usuario = await _context.Usuarios.Include(u => u.UsuarioRedeSocials).FirstOrDefaultAsync(u => u.Id == id && !u.Deletado);

            if (usuario == null)
            {
                return false;
            }

            usuario.Deletado = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AtualizarUsuario(CoreUsuario usuario)
        {
            var usuarioEF = await _context.Usuarios
               .Include(u => u.UsuarioRedeSocials)
               .FirstOrDefaultAsync(u => u.Id == usuario.Id && u.Deletado == false);

            if (usuarioEF == null)
                throw new Exception("Usuário não encontrado.");

           
            usuarioEF.Nome = usuario.Nome;
            usuarioEF.Email = usuario.Email;
            usuarioEF.Cpf = usuario.Cpf;
            usuarioEF.Telefone = usuario.Telefone;
            usuarioEF.DataDeNascimento = usuario.DataDeNascimento;
            usuarioEF.Foto = usuario.Foto;

          
            _context.UsuarioRedeSocials.RemoveRange(usuarioEF.UsuarioRedeSocials);
            usuarioEF.UsuarioRedeSocials = usuario.RedesSociais.Select(rs => new Data.Persistence.UsuarioRedeSocial
            {
                IdUsuario = usuario.Id,
                Plataforma = rs.Plataforma,
                Url = rs.Url
            }).ToList();

         var linhasAfetadas = await _context.SaveChangesAsync();

            if (linhasAfetadas > 0)
                return true;

            return false;
        }
    }
}