using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Badge> Badges { get; set; }

    public virtual DbSet<BadgeParticipante> BadgeParticipantes { get; set; }

    public virtual DbSet<CheckinEstande> CheckinEstandes { get; set; }

    public virtual DbSet<CheckinSubEvento> CheckinSubEventos { get; set; }

    public virtual DbSet<Conexao> Conexaos { get; set; }

    public virtual DbSet<Conversa> Conversas { get; set; }

    public virtual DbSet<Cor> Cors { get; set; }

    public virtual DbSet<Desafio> Desafios { get; set; }

    public virtual DbSet<DesafioParticipante> DesafioParticipantes { get; set; }

    public virtual DbSet<Estande> Estandes { get; set; }

    public virtual DbSet<Evento> Eventos { get; set; }

    public virtual DbSet<Inscrito> Inscritos { get; set; }

    public virtual DbSet<InteracaoPublicacao> InteracaoPublicacaos { get; set; }

    public virtual DbSet<Interesse> Interesses { get; set; }

    public virtual DbSet<Mapa> Mapas { get; set; }

    public virtual DbSet<Mensagem> Mensagems { get; set; }

    public virtual DbSet<Notificacao> Notificacaos { get; set; }

    public virtual DbSet<OpcaoPerguntaPesquisa> OpcaoPerguntaPesquisas { get; set; }

    public virtual DbSet<Palestrante> Palestrantes { get; set; }

    public virtual DbSet<PalestranteSubEvento> PalestranteSubEventos { get; set; }

    public virtual DbSet<PaletaCor> PaletaCors { get; set; }

    public virtual DbSet<Participante> Participantes { get; set; }

    public virtual DbSet<ParticipanteInteresse> ParticipanteInteresses { get; set; }

    public virtual DbSet<ParticipanteNotificacao> ParticipanteNotificacaos { get; set; }

    public virtual DbSet<ParticipantePremio> ParticipantePremios { get; set; }

    public virtual DbSet<ParticipanteQuizResposta> ParticipanteQuizResposta { get; set; }

    public virtual DbSet<Passaporte> Passaportes { get; set; }

    public virtual DbSet<PassaporteItem> PassaporteItems { get; set; }

    public virtual DbSet<PassaporteParticipante> PassaporteParticipantes { get; set; }

    public virtual DbSet<PassaporteParticipanteItem> PassaporteParticipanteItems { get; set; }

    public virtual DbSet<Patrocinador> Patrocinadors { get; set; }

    public virtual DbSet<PatrocinadorSubEvento> PatrocinadorSubEventos { get; set; }

    public virtual DbSet<PerguntaPesquisa> PerguntaPesquisas { get; set; }

    public virtual DbSet<PerguntasSubEvento> PerguntasSubEventos { get; set; }

    public virtual DbSet<Pesquisa> Pesquisas { get; set; }

    public virtual DbSet<PontoMapa> PontoMapas { get; set; }

    public virtual DbSet<Premio> Premios { get; set; }

    public virtual DbSet<Publicacao> Publicacaos { get; set; }

    public virtual DbSet<PublicacaoImagem> PublicacaoImagems { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<QuizAlternativa> QuizAlternativas { get; set; }

    public virtual DbSet<QuizParticipante> QuizParticipantes { get; set; }

    public virtual DbSet<QuizPergunta> QuizPergunta { get; set; }

    public virtual DbSet<RespostaParticipantePerguntum> RespostaParticipantePergunta { get; set; }

    public virtual DbSet<SubEvento> SubEventos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRedeSocial> UsuarioRedeSocials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=gamification_events_DB;uid=root;pwd=alves0919", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.ClrType.GetProperties())
            {
                if (property.PropertyType.IsEnum)
                {
                    modelBuilder.Entity(entityType.Name)
                        .Property(property.Name)
                        .HasConversion<string>();
                }
            }
        }

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Badge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("badge");

            entity.HasIndex(e => e.IdEvento, "FK_badge_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.Icone)
                .HasMaxLength(255)
                .HasColumnName("icone");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Regra)
                .HasColumnType("text")
                .HasColumnName("regra");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Badges)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_badge_evento");
        });

        modelBuilder.Entity<BadgeParticipante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("badge_participante");

            entity.HasIndex(e => e.IdBadge, "FK_badge_participante_badge");

            entity.HasIndex(e => e.IdParticipante, "FK_badge_participante_participante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraConquista)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_conquista");
            entity.Property(e => e.IdBadge).HasColumnName("id_badge");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");

            entity.HasOne(d => d.IdBadgeNavigation).WithMany(p => p.BadgeParticipantes)
                .HasForeignKey(d => d.IdBadge)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_badge_participante_badge");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.BadgeParticipantes)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_badge_participante_participante");
        });

        modelBuilder.Entity<CheckinEstande>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("checkin_estande");

            entity.HasIndex(e => e.IdEstande, "FK_checkin_estande");

            entity.HasIndex(e => e.IdParticipante, "FK_checkin_participante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.IdEstande).HasColumnName("id_estande");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");

            entity.HasOne(d => d.IdEstandeNavigation).WithMany(p => p.CheckinEstandes)
                .HasForeignKey(d => d.IdEstande)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_checkin_estande");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.CheckinEstandes)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_checkin_participante");
        });

        modelBuilder.Entity<CheckinSubEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("checkin_sub_evento");

            entity.HasIndex(e => e.IdParticipante, "FK_checkin_sub_evento_participante");

            entity.HasIndex(e => e.IdSubEvento, "FK_checkin_sub_evento_sub_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdSubEvento).HasColumnName("id_sub_evento");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.CheckinSubEventos)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_checkin_sub_evento_participante");

            entity.HasOne(d => d.IdSubEventoNavigation).WithMany(p => p.CheckinSubEventos)
                .HasForeignKey(d => d.IdSubEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_checkin_sub_evento_sub_evento");
        });

        modelBuilder.Entity<Conexao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("conexao");

            entity.HasIndex(e => e.IdParticipante1, "FK_conexao_participante_1");

            entity.HasIndex(e => e.IdParticipante2, "FK_conexao_participante_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.IdParticipante1).HasColumnName("id_participante_1");
            entity.Property(e => e.IdParticipante2).HasColumnName("id_participante_2");
            entity.Property(e => e.StatusConexao)
                .HasDefaultValueSql("'Pendente'")
                .HasColumnType("enum('Pendente','Aceita','Rejeitada')")
                .HasColumnName("status_conexao");

            entity.HasOne(d => d.IdParticipante1Navigation).WithMany(p => p.ConexaoIdParticipante1Navigations)
                .HasForeignKey(d => d.IdParticipante1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_conexao_participante_1");

            entity.HasOne(d => d.IdParticipante2Navigation).WithMany(p => p.ConexaoIdParticipante2Navigations)
                .HasForeignKey(d => d.IdParticipante2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_conexao_participante_2");
        });

        modelBuilder.Entity<Conversa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("conversa");

            entity.HasIndex(e => e.IdParticipante1, "FK_conversa_participante_1");

            entity.HasIndex(e => e.IdParticipante2, "FK_conversa_participante_2");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_inicio");
            entity.Property(e => e.IdParticipante1).HasColumnName("id_participante_1");
            entity.Property(e => e.IdParticipante2).HasColumnName("id_participante_2");

            entity.HasOne(d => d.IdParticipante1Navigation).WithMany(p => p.ConversaIdParticipante1Navigations)
                .HasForeignKey(d => d.IdParticipante1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_conversa_participante_1");

            entity.HasOne(d => d.IdParticipante2Navigation).WithMany(p => p.ConversaIdParticipante2Navigations)
                .HasForeignKey(d => d.IdParticipante2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_conversa_participante_2");
        });

        modelBuilder.Entity<Cor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cor");

            entity.HasIndex(e => e.HexCodigo, "hex_codigo").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HexCodigo)
                .HasMaxLength(7)
                .HasColumnName("hex_codigo");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
        });

        modelBuilder.Entity<Desafio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("desafio");

            entity.HasIndex(e => e.IdEvento, "FK_desafio_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraFim)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_fim");
            entity.Property(e => e.DataHoraInicio)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_inicio");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Pontuacao).HasColumnName("pontuacao");
            entity.Property(e => e.QuantidadeDesafio)
                .HasDefaultValueSql("'1'")
                .HasColumnName("quantidade_desafio");
            entity.Property(e => e.Regra)
                .HasColumnType("text")
                .HasColumnName("regra");
            entity.Property(e => e.TipoDesafio)
                .HasColumnType("enum('Networking','Checkin_estande','Checkin_sub_evento','Pesquisa','Quiz')")
                .HasColumnName("tipo_desafio");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Desafios)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_desafio_evento");
        });

        modelBuilder.Entity<DesafioParticipante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("desafio_participante");

            entity.HasIndex(e => e.IdDesafio, "FK_desafio_participante_desafio");

            entity.HasIndex(e => e.IdParticipante, "FK_desafio_participante_participante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraConclusao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_conclusao");
            entity.Property(e => e.IdDesafio).HasColumnName("id_desafio");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.QuantidadeRealizada).HasColumnName("quantidade_realizada");
            entity.Property(e => e.StatusDesafio)
                .HasDefaultValueSql("'Aberto'")
                .HasColumnType("enum('Aberto','Completo')")
                .HasColumnName("status_desafio");

            entity.HasOne(d => d.IdDesafioNavigation).WithMany(p => p.DesafioParticipantes)
                .HasForeignKey(d => d.IdDesafio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_desafio_participante_desafio");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.DesafioParticipantes)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_desafio_participante_participante");
        });

        modelBuilder.Entity<Estande>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estande");

            entity.HasIndex(e => e.IdEvento, "FK_estande_evento");

            entity.HasIndex(e => e.IdPatrocinador, "FK_estande_patrocinador");

            entity.HasIndex(e => e.IdPontoMapa, "FK_estande_ponto_mapa");

            entity.HasIndex(e => e.CodigoCheckin, "codigo_checkin").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Brindes)
                .HasColumnType("text")
                .HasColumnName("brindes");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.CodigoCheckin).HasColumnName("codigo_checkin");
            entity.Property(e => e.DataFim)
                .HasColumnType("datetime")
                .HasColumnName("data_fim");
            entity.Property(e => e.DataInicio)
                .HasColumnType("datetime")
                .HasColumnName("data_inicio");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.HorarioFim)
                .HasColumnType("time")
                .HasColumnName("horario_fim");
            entity.Property(e => e.HorarioInicio)
                .HasColumnType("time")
                .HasColumnName("horario_inicio");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdPatrocinador).HasColumnName("id_patrocinador");
            entity.Property(e => e.IdPontoMapa).HasColumnName("id_ponto_mapa");
            entity.Property(e => e.LocalEstande)
                .HasMaxLength(255)
                .HasColumnName("local_estande");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Produto)
                .HasColumnType("text")
                .HasColumnName("produto");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Estandes)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_estande_evento");

            entity.HasOne(d => d.IdPatrocinadorNavigation).WithMany(p => p.Estandes)
                .HasForeignKey(d => d.IdPatrocinador)
                .HasConstraintName("FK_estande_patrocinador");

            entity.HasOne(d => d.IdPontoMapaNavigation).WithMany(p => p.Estandes)
                .HasForeignKey(d => d.IdPontoMapa)
                .HasConstraintName("FK_estande_ponto_mapa");
        });

        modelBuilder.Entity<Evento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("evento");

            entity.HasIndex(e => e.IdPaleta, "FK_evento_paleta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.DataFinal)
                .HasColumnType("datetime")
                .HasColumnName("data_final");
            entity.Property(e => e.DataInicio)
                .HasColumnType("datetime")
                .HasColumnName("data_inicio");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.IdPaleta).HasColumnName("id_paleta");
            entity.Property(e => e.Objetivo)
                .HasMaxLength(255)
                .HasColumnName("objetivo");
            entity.Property(e => e.PublicoAlvo)
                .HasMaxLength(255)
                .HasColumnName("publico_alvo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdPaletaNavigation).WithMany(p => p.Eventos)
                .HasForeignKey(d => d.IdPaleta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_evento_paleta");
        });

        modelBuilder.Entity<Inscrito>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("inscrito");

            entity.HasIndex(e => e.IdEvento, "FK_inscrito_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo)
                .HasColumnType("enum('Admin','Membro')")
                .HasColumnName("cargo");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("cpf");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Inscritos)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_inscrito_evento");
        });

        modelBuilder.Entity<InteracaoPublicacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("interacao_publicacao");

            entity.HasIndex(e => e.IdParticipante, "FK_interacao_publicacao_participante");

            entity.HasIndex(e => e.IdPublicacao, "FK_interacao_publicacao_publicacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdPublicacao).HasColumnName("id_publicacao");
            entity.Property(e => e.TextoComentario)
                .HasColumnType("text")
                .HasColumnName("texto_comentario");
            entity.Property(e => e.TipoInteracao)
                .HasColumnType("enum('Like','Comentario')")
                .HasColumnName("tipo_interacao");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.InteracaoPublicacaos)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_interacao_publicacao_participante");

            entity.HasOne(d => d.IdPublicacaoNavigation).WithMany(p => p.InteracaoPublicacaos)
                .HasForeignKey(d => d.IdPublicacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_interacao_publicacao_publicacao");
        });

        modelBuilder.Entity<Interesse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("interesse");

            entity.HasIndex(e => e.IdEvento, "FK_interesses_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Interesses)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_interesses_evento");
        });

        modelBuilder.Entity<Mapa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mapa");

            entity.HasIndex(e => e.IdEvento, "FK_mapa_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.ImagemUrl)
                .HasMaxLength(255)
                .HasColumnName("imagem_url");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Mapas)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mapa_evento");
        });

        modelBuilder.Entity<Mensagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("mensagem");

            entity.HasIndex(e => e.IdConversa, "FK_mensagem_conversa");

            entity.HasIndex(e => e.IdParticipanteRemetente, "FK_mensagem_participante_remetente");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.IdConversa).HasColumnName("id_conversa");
            entity.Property(e => e.IdParticipanteRemetente).HasColumnName("id_participante_remetente");
            entity.Property(e => e.StatusMensagem)
                .HasDefaultValueSql("'Enviada'")
                .HasColumnType("enum('Enviada','Lida')")
                .HasColumnName("status_mensagem");
            entity.Property(e => e.Texto)
                .HasColumnType("text")
                .HasColumnName("texto");

            entity.HasOne(d => d.IdConversaNavigation).WithMany(p => p.Mensagems)
                .HasForeignKey(d => d.IdConversa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mensagem_conversa");

            entity.HasOne(d => d.IdParticipanteRemetenteNavigation).WithMany(p => p.Mensagems)
                .HasForeignKey(d => d.IdParticipanteRemetente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mensagem_participante_remetente");
        });

        modelBuilder.Entity<Notificacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notificacao");

            entity.HasIndex(e => e.IdEvento, "FK_notificacao_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraEnvio)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_envio");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Mensagem)
                .HasColumnType("text")
                .HasColumnName("mensagem");
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .HasColumnName("tipo");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Notificacaos)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_notificacao_evento");
        });

        modelBuilder.Entity<OpcaoPerguntaPesquisa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("opcao_pergunta_pesquisa");

            entity.HasIndex(e => e.IdPerguntaPesquisa, "FK_opcao_pergunta_pesquisa_pergunta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdPerguntaPesquisa).HasColumnName("id_pergunta_pesquisa");
            entity.Property(e => e.TextoOpcaoResposta)
                .HasColumnType("text")
                .HasColumnName("texto_opcao_resposta");

            entity.HasOne(d => d.IdPerguntaPesquisaNavigation).WithMany(p => p.OpcaoPerguntaPesquisas)
                .HasForeignKey(d => d.IdPerguntaPesquisa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_opcao_pergunta_pesquisa_pergunta");
        });

        modelBuilder.Entity<Palestrante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("palestrante");

            entity.HasIndex(e => e.IdEvento, "FK_palestrante_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataNascimento).HasColumnName("data_nascimento");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Linkedin)
                .HasMaxLength(255)
                .HasColumnName("linkedin");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Profissao)
                .HasMaxLength(100)
                .HasColumnName("profissao");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Palestrantes)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_palestrante_evento");
        });

        modelBuilder.Entity<PalestranteSubEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("palestrante_sub_evento");

            entity.HasIndex(e => e.IdPalestrante, "FK_palestrante_sub_evento_palestrante");

            entity.HasIndex(e => e.IdSubEvento, "FK_palestrante_sub_evento_sub_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPalestrante).HasColumnName("id_palestrante");
            entity.Property(e => e.IdSubEvento).HasColumnName("id_sub_evento");

            entity.HasOne(d => d.IdPalestranteNavigation).WithMany(p => p.PalestranteSubEventos)
                .HasForeignKey(d => d.IdPalestrante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_palestrante_sub_evento_palestrante");

            entity.HasOne(d => d.IdSubEventoNavigation).WithMany(p => p.PalestranteSubEventos)
                .HasForeignKey(d => d.IdSubEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_palestrante_sub_evento_sub_evento");
        });

        modelBuilder.Entity<PaletaCor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("paleta_cor");

            entity.HasIndex(e => e.IdCor1, "FK_paleta_cor_1");

            entity.HasIndex(e => e.IdCor2, "FK_paleta_cor_2");

            entity.HasIndex(e => e.IdCor3, "FK_paleta_cor_3");

            entity.HasIndex(e => e.IdCor4, "FK_paleta_cor_4");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdCor1).HasColumnName("id_cor_1");
            entity.Property(e => e.IdCor2).HasColumnName("id_cor_2");
            entity.Property(e => e.IdCor3).HasColumnName("id_cor_3");
            entity.Property(e => e.IdCor4).HasColumnName("id_cor_4");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");

            entity.HasOne(d => d.IdCor1Navigation).WithMany(p => p.PaletaCorIdCor1Navigations)
                .HasForeignKey(d => d.IdCor1)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_paleta_cor_1");

            entity.HasOne(d => d.IdCor2Navigation).WithMany(p => p.PaletaCorIdCor2Navigations)
                .HasForeignKey(d => d.IdCor2)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_paleta_cor_2");

            entity.HasOne(d => d.IdCor3Navigation).WithMany(p => p.PaletaCorIdCor3Navigations)
                .HasForeignKey(d => d.IdCor3)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_paleta_cor_3");

            entity.HasOne(d => d.IdCor4Navigation).WithMany(p => p.PaletaCorIdCor4Navigations)
                .HasForeignKey(d => d.IdCor4)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_paleta_cor_4");
        });

        modelBuilder.Entity<Participante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("participante");

            entity.HasIndex(e => e.IdEvento, "FK_participante_evento");

            entity.HasIndex(e => e.IdUsuario, "FK_participante_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cargo)
                .HasColumnType("enum('Admin','Membro')")
                .HasColumnName("cargo");
            entity.Property(e => e.DataHoraCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_criacao");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Pontuacao).HasColumnName("pontuacao");
            entity.Property(e => e.PrimeiroParticipante).HasColumnName("primeiro_participante");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Participantes)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_participante_evento");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Participantes)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_participante_usuario");
        });

        modelBuilder.Entity<ParticipanteInteresse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("participante_interesse");

            entity.HasIndex(e => e.IdInteresse, "FK_participante_interesse_interesse");

            entity.HasIndex(e => e.IdParticipante, "FK_participante_interesse_participante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdInteresse).HasColumnName("id_interesse");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");

            entity.HasOne(d => d.IdInteresseNavigation).WithMany(p => p.ParticipanteInteresses)
                .HasForeignKey(d => d.IdInteresse)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_participante_interesse_interesse");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.ParticipanteInteresses)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_participante_interesse_participante");
        });

        modelBuilder.Entity<ParticipanteNotificacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("participante_notificacao");

            entity.HasIndex(e => e.IdNotificacao, "FK_p_notificacao_notificacao");

            entity.HasIndex(e => e.IdParticipante, "FK_p_notificacao_participante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraVisualizacao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_visualizacao");
            entity.Property(e => e.IdNotificacao).HasColumnName("id_notificacao");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.StatusNotificacao)
                .HasDefaultValueSql("'Nao_lida'")
                .HasColumnType("enum('Nao_lida','Lida')")
                .HasColumnName("status_notificacao");

            entity.HasOne(d => d.IdNotificacaoNavigation).WithMany(p => p.ParticipanteNotificacaos)
                .HasForeignKey(d => d.IdNotificacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_p_notificacao_notificacao");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.ParticipanteNotificacaos)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_p_notificacao_participante");
        });

        modelBuilder.Entity<ParticipantePremio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("participante_premio");

            entity.HasIndex(e => e.IdParticipante, "FK_participante_premio_participante");

            entity.HasIndex(e => e.IdPremio, "FK_participante_premio_premio");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataConcessao)
                .HasColumnType("datetime")
                .HasColumnName("data_concessao");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdPremio).HasColumnName("id_premio");
            entity.Property(e => e.Motivo)
                .HasMaxLength(255)
                .HasColumnName("motivo");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.ParticipantePremios)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_participante_premio_participante");

            entity.HasOne(d => d.IdPremioNavigation).WithMany(p => p.ParticipantePremios)
                .HasForeignKey(d => d.IdPremio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_participante_premio_premio");
        });

        modelBuilder.Entity<ParticipanteQuizResposta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("participante_quiz_resposta");

            entity.HasIndex(e => e.IdParticipante, "FK_pqr_participante");

            entity.HasIndex(e => e.IdQuizAlternativa, "FK_pqr_quiz_alternativa");

            entity.HasIndex(e => e.IdQuizPergunta, "FK_pqr_quiz_pergunta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HoraResposta)
                .HasColumnType("time")
                .HasColumnName("hora_resposta");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdQuizAlternativa).HasColumnName("id_quiz_alternativa");
            entity.Property(e => e.IdQuizPergunta).HasColumnName("id_quiz_pergunta");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.ParticipanteQuizResposta)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pqr_participante");

            entity.HasOne(d => d.IdQuizAlternativaNavigation).WithMany(p => p.ParticipanteQuizResposta)
                .HasForeignKey(d => d.IdQuizAlternativa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pqr_quiz_alternativa");

            entity.HasOne(d => d.IdQuizPerguntaNavigation).WithMany(p => p.ParticipanteQuizResposta)
                .HasForeignKey(d => d.IdQuizPergunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pqr_quiz_pergunta");
        });

        modelBuilder.Entity<Passaporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("passaporte");

            entity.HasIndex(e => e.IdEvento, "FK_passaporte_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ativo)
                .IsRequired()
                .HasDefaultValueSql("'1'")
                .HasColumnName("ativo");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.Pontuacao).HasColumnName("pontuacao");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Passaportes)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_passaporte_evento");
        });

        modelBuilder.Entity<PassaporteItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("passaporte_item");

            entity.HasIndex(e => e.IdEstande, "FK_passaporte_item_estande");

            entity.HasIndex(e => e.IdPassaporte, "FK_passaporte_item_passaporte");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdEstande).HasColumnName("id_estande");
            entity.Property(e => e.IdPassaporte).HasColumnName("id_passaporte");

            entity.HasOne(d => d.IdEstandeNavigation).WithMany(p => p.PassaporteItems)
                .HasForeignKey(d => d.IdEstande)
                .HasConstraintName("FK_passaporte_item_estande");

            entity.HasOne(d => d.IdPassaporteNavigation).WithMany(p => p.PassaporteItems)
                .HasForeignKey(d => d.IdPassaporte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_passaporte_item_passaporte");
        });

        modelBuilder.Entity<PassaporteParticipante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("passaporte_participante");

            entity.HasIndex(e => e.IdParticipante, "FK_passaporte_participante_participante");

            entity.HasIndex(e => e.IdPassaporte, "FK_passaporte_participante_passaporte");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraConclusao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_conclusao");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdPassaporte).HasColumnName("id_passaporte");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'Incompleto'")
                .HasColumnType("enum('Incompleto','Completo')")
                .HasColumnName("status");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.PassaporteParticipantes)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_passaporte_participante_participante");

            entity.HasOne(d => d.IdPassaporteNavigation).WithMany(p => p.PassaporteParticipantes)
                .HasForeignKey(d => d.IdPassaporte)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_passaporte_participante_passaporte");
        });

        modelBuilder.Entity<PassaporteParticipanteItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("passaporte_participante_item");

            entity.HasIndex(e => e.IdParticipante, "FK_passaporte_participante_item_participante");

            entity.HasIndex(e => e.IdPassaporteItem, "FK_passaporte_participante_item_passaporte_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHoraConclusao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_conclusao");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdPassaporteItem).HasColumnName("id_passaporte_item");
            entity.Property(e => e.StatusItem)
                .HasDefaultValueSql("'Nao_concluido'")
                .HasColumnType("enum('Nao_concluido','Concluido')")
                .HasColumnName("status_item");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.PassaporteParticipanteItems)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_passaporte_participante_item_participante");

            entity.HasOne(d => d.IdPassaporteItemNavigation).WithMany(p => p.PassaporteParticipanteItems)
                .HasForeignKey(d => d.IdPassaporteItem)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_passaporte_participante_item_passaporte_item");
        });

        modelBuilder.Entity<Patrocinador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("patrocinador");

            entity.HasIndex(e => e.IdEvento, "FK_patrocinador_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.Contato)
                .HasMaxLength(255)
                .HasColumnName("contato");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.EnderecoLoja)
                .HasMaxLength(255)
                .HasColumnName("endereco_loja");
            entity.Property(e => e.EnderecoOnline)
                .HasMaxLength(255)
                .HasColumnName("endereco_online");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.InfoExtra)
                .HasColumnType("text")
                .HasColumnName("info_extra");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Produtos)
                .HasColumnType("text")
                .HasColumnName("produtos");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Patrocinadors)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patrocinador_evento");
        });

        modelBuilder.Entity<PatrocinadorSubEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("patrocinador_sub_evento");

            entity.HasIndex(e => e.IdPatrocinador, "FK_patrocinador_sub_evento_patrocinador");

            entity.HasIndex(e => e.IdSubEvento, "FK_patrocinador_sub_evento_sub_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPatrocinador).HasColumnName("id_patrocinador");
            entity.Property(e => e.IdSubEvento).HasColumnName("id_sub_evento");

            entity.HasOne(d => d.IdPatrocinadorNavigation).WithMany(p => p.PatrocinadorSubEventos)
                .HasForeignKey(d => d.IdPatrocinador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patrocinador_sub_evento_patrocinador");

            entity.HasOne(d => d.IdSubEventoNavigation).WithMany(p => p.PatrocinadorSubEventos)
                .HasForeignKey(d => d.IdSubEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_patrocinador_sub_evento_sub_evento");
        });

        modelBuilder.Entity<PerguntaPesquisa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pergunta_pesquisa");

            entity.HasIndex(e => e.IdPesquisa, "FK_pergunta_pesquisa_pesquisa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdPesquisa).HasColumnName("id_pesquisa");
            entity.Property(e => e.TextoPergunta)
                .HasColumnType("text")
                .HasColumnName("texto_pergunta");
            entity.Property(e => e.TipoPergunta)
                .HasColumnType("enum('Multipla_escolha','Dissertativa')")
                .HasColumnName("tipo_pergunta");

            entity.HasOne(d => d.IdPesquisaNavigation).WithMany(p => p.PerguntaPesquisas)
                .HasForeignKey(d => d.IdPesquisa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pergunta_pesquisa_pesquisa");
        });

        modelBuilder.Entity<PerguntasSubEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("perguntas_sub_evento");

            entity.HasIndex(e => e.IdParticipante, "FK_perguntas_sub_evento_participante");

            entity.HasIndex(e => e.IdSubEvento, "FK_perguntas_sub_evento_sub_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Assunto)
                .HasMaxLength(255)
                .HasColumnName("assunto");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdSubEvento).HasColumnName("id_sub_evento");
            entity.Property(e => e.Pergunta)
                .HasColumnType("text")
                .HasColumnName("pergunta");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.PerguntasSubEventos)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_perguntas_sub_evento_participante");

            entity.HasOne(d => d.IdSubEventoNavigation).WithMany(p => p.PerguntasSubEventos)
                .HasForeignKey(d => d.IdSubEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_perguntas_sub_evento_sub_evento");
        });

        modelBuilder.Entity<Pesquisa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pesquisa");

            entity.HasIndex(e => e.IdEvento, "FK_pesquisa_evento");

            entity.HasIndex(e => e.IdPatrocinador, "FK_pesquisa_patrocinador");

            entity.HasIndex(e => e.IdSubEvento, "FK_pesquisa_sub_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdPatrocinador).HasColumnName("id_patrocinador");
            entity.Property(e => e.IdSubEvento).HasColumnName("id_sub_evento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Pontuacao).HasColumnName("pontuacao");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Pesquisas)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pesquisa_evento");

            entity.HasOne(d => d.IdPatrocinadorNavigation).WithMany(p => p.Pesquisas)
                .HasForeignKey(d => d.IdPatrocinador)
                .HasConstraintName("FK_pesquisa_patrocinador");

            entity.HasOne(d => d.IdSubEventoNavigation).WithMany(p => p.Pesquisas)
                .HasForeignKey(d => d.IdSubEvento)
                .HasConstraintName("FK_pesquisa_sub_evento");
        });

        modelBuilder.Entity<PontoMapa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("ponto_mapa");

            entity.HasIndex(e => e.IdMapa, "FK_ponto_mapa_mapa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.CoordenadaX).HasColumnName("coordenada_x");
            entity.Property(e => e.CoordenadaY).HasColumnName("coordenada_y");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.DetalhesLocalizacao)
                .HasColumnType("text")
                .HasColumnName("detalhes_localizacao");
            entity.Property(e => e.IdMapa).HasColumnName("id_mapa");
            entity.Property(e => e.Latitude)
                .HasPrecision(10, 8)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasPrecision(11, 8)
                .HasColumnName("longitude");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");

            entity.HasOne(d => d.IdMapaNavigation).WithMany(p => p.PontoMapas)
                .HasForeignKey(d => d.IdMapa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ponto_mapa_mapa");
        });

        modelBuilder.Entity<Premio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("premio");

            entity.HasIndex(e => e.IdEvento, "FK_premio_evento");

            entity.HasIndex(e => e.IdPatrocinador, "FK_premio_patrocinador");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Descricao)
                .HasColumnType("text")
                .HasColumnName("descricao");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdPatrocinador).HasColumnName("id_patrocinador");
            entity.Property(e => e.InfoResgate)
                .HasColumnType("text")
                .HasColumnName("info_resgate");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Premios)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_premio_evento");

            entity.HasOne(d => d.IdPatrocinadorNavigation).WithMany(p => p.Premios)
                .HasForeignKey(d => d.IdPatrocinador)
                .HasConstraintName("FK_premio_patrocinador");
        });

        modelBuilder.Entity<Publicacao>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("publicacao");

            entity.HasIndex(e => e.IdEvento, "FK_publicacao_evento");

            entity.HasIndex(e => e.IdParticipante, "FK_publicacao_participante");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataHora)
                .HasColumnType("datetime")
                .HasColumnName("data_hora");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.Texto)
                .HasColumnType("text")
                .HasColumnName("texto");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Publicacaos)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_publicacao_evento");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.Publicacaos)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_publicacao_participante");
        });

        modelBuilder.Entity<PublicacaoImagem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("publicacao_imagem");

            entity.HasIndex(e => e.IdPublicacao, "FK_publicacao_imagem_publicacao");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPublicacao).HasColumnName("id_publicacao");
            entity.Property(e => e.Imagem)
                .HasMaxLength(255)
                .HasColumnName("imagem");

            entity.HasOne(d => d.IdPublicacaoNavigation).WithMany(p => p.PublicacaoImagems)
                .HasForeignKey(d => d.IdPublicacao)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_publicacao_imagem_publicacao");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("quiz");

            entity.HasIndex(e => e.IdEvento, "FK_quiz_evento");

            entity.HasIndex(e => e.IdPatrocinador, "FK_quiz_patrocinador");

            entity.HasIndex(e => e.IdSubEvento, "FK_quiz_sub_evento");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataQuiz)
                .HasColumnType("datetime")
                .HasColumnName("data_quiz");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.HoraFim)
                .HasColumnType("time")
                .HasColumnName("hora_fim");
            entity.Property(e => e.HoraInicio)
                .HasColumnType("time")
                .HasColumnName("hora_inicio");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdPatrocinador).HasColumnName("id_patrocinador");
            entity.Property(e => e.IdSubEvento).HasColumnName("id_sub_evento");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Tema)
                .HasMaxLength(255)
                .HasColumnName("tema");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quiz_evento");

            entity.HasOne(d => d.IdPatrocinadorNavigation).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.IdPatrocinador)
                .HasConstraintName("FK_quiz_patrocinador");

            entity.HasOne(d => d.IdSubEventoNavigation).WithMany(p => p.Quizzes)
                .HasForeignKey(d => d.IdSubEvento)
                .HasConstraintName("FK_quiz_sub_evento");
        });

        modelBuilder.Entity<QuizAlternativa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("quiz_alternativa");

            entity.HasIndex(e => e.IdQuizPergunta, "FK_quiz_alternativa_quiz_pergunta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.ECorreta).HasColumnName("e_correta");
            entity.Property(e => e.IdQuizPergunta).HasColumnName("id_quiz_pergunta");
            entity.Property(e => e.Resposta)
                .HasColumnType("text")
                .HasColumnName("resposta");

            entity.HasOne(d => d.IdQuizPerguntaNavigation).WithMany(p => p.QuizAlternativas)
                .HasForeignKey(d => d.IdQuizPergunta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quiz_alternativa_quiz_pergunta");
        });

        modelBuilder.Entity<QuizParticipante>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("quiz_participante");

            entity.HasIndex(e => e.IdParticipante, "FK_quiz_participante_participante");

            entity.HasIndex(e => e.IdQuiz, "FK_quiz_participante_quiz");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdQuiz).HasColumnName("id_quiz");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.QuizParticipantes)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quiz_participante_participante");

            entity.HasOne(d => d.IdQuizNavigation).WithMany(p => p.QuizParticipantes)
                .HasForeignKey(d => d.IdQuiz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quiz_participante_quiz");
        });

        modelBuilder.Entity<QuizPergunta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("quiz_pergunta");

            entity.HasIndex(e => e.IdQuiz, "FK_quiz_pergunta_quiz");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Enunciado)
                .HasColumnType("text")
                .HasColumnName("enunciado");
            entity.Property(e => e.IdQuiz).HasColumnName("id_quiz");

            entity.HasOne(d => d.IdQuizNavigation).WithMany(p => p.QuizPergunta)
                .HasForeignKey(d => d.IdQuiz)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_quiz_pergunta_quiz");
        });

        modelBuilder.Entity<RespostaParticipantePerguntum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("resposta_participante_pergunta");

            entity.HasIndex(e => e.IdOpcaoPerguntaPesquisa, "FK_resposta_participante_pergunta_opcao");

            entity.HasIndex(e => e.IdParticipante, "FK_resposta_participante_pergunta_participante");

            entity.HasIndex(e => e.IdPerguntaPesquisa, "FK_resposta_participante_pergunta_pergunta");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DataResposta)
                .HasColumnType("datetime")
                .HasColumnName("data_resposta");
            entity.Property(e => e.IdOpcaoPerguntaPesquisa).HasColumnName("id_opcao_pergunta_pesquisa");
            entity.Property(e => e.IdParticipante).HasColumnName("id_participante");
            entity.Property(e => e.IdPerguntaPesquisa).HasColumnName("id_pergunta_pesquisa");
            entity.Property(e => e.TextoResposta)
                .HasColumnType("text")
                .HasColumnName("texto_resposta");

            entity.HasOne(d => d.IdOpcaoPerguntaPesquisaNavigation).WithMany(p => p.RespostaParticipantePergunta)
                .HasForeignKey(d => d.IdOpcaoPerguntaPesquisa)
                .HasConstraintName("FK_resposta_participante_pergunta_opcao");

            entity.HasOne(d => d.IdParticipanteNavigation).WithMany(p => p.RespostaParticipantePergunta)
                .HasForeignKey(d => d.IdParticipante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_resposta_participante_pergunta_participante");

            entity.HasOne(d => d.IdPerguntaPesquisaNavigation).WithMany(p => p.RespostaParticipantePergunta)
                .HasForeignKey(d => d.IdPerguntaPesquisa)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_resposta_participante_pergunta_pergunta");
        });

        modelBuilder.Entity<SubEvento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sub_evento");

            entity.HasIndex(e => e.IdEvento, "FK_sub_evento_evento");

            entity.HasIndex(e => e.IdPontoMapa, "FK_sub_evento_ponto_mapa");

            entity.HasIndex(e => e.CodigoCheckin, "codigo_checkin").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Assunto)
                .HasColumnType("text")
                .HasColumnName("assunto");
            entity.Property(e => e.Categoria)
                .HasMaxLength(255)
                .HasColumnName("categoria");
            entity.Property(e => e.CodigoCheckin).HasColumnName("codigo_checkin");
            entity.Property(e => e.DataSubEvento)
                .HasColumnType("datetime")
                .HasColumnName("data_sub_evento");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.HorarioFim)
                .HasColumnType("time")
                .HasColumnName("horario_fim");
            entity.Property(e => e.HorarioInicio)
                .HasColumnType("time")
                .HasColumnName("horario_inicio");
            entity.Property(e => e.IdEvento).HasColumnName("id_evento");
            entity.Property(e => e.IdPontoMapa).HasColumnName("id_ponto_mapa");
            entity.Property(e => e.LocalSubEvento)
                .HasMaxLength(255)
                .HasColumnName("local_sub_evento");
            entity.Property(e => e.Modalidade)
                .HasColumnType("enum('Online','Presencial')")
                .HasColumnName("modalidade");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.Tipo)
                .HasMaxLength(255)
                .HasColumnName("tipo");

            entity.HasOne(d => d.IdEventoNavigation).WithMany(p => p.SubEventos)
                .HasForeignKey(d => d.IdEvento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_sub_evento_evento");

            entity.HasOne(d => d.IdPontoMapaNavigation).WithMany(p => p.SubEventos)
                .HasForeignKey(d => d.IdPontoMapa)
                .HasConstraintName("FK_sub_evento_ponto_mapa");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.Cpf, "cpf").IsUnique();

            entity.HasIndex(e => e.Email, "email").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cpf)
                .HasMaxLength(11)
                .HasColumnName("cpf");
            entity.Property(e => e.DataDeNascimento).HasColumnName("data_de_nascimento");
            entity.Property(e => e.DataHoraCriacao)
                .HasColumnType("datetime")
                .HasColumnName("data_hora_criacao");
            entity.Property(e => e.Deletado).HasColumnName("deletado");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Foto)
                .HasMaxLength(255)
                .HasColumnName("foto");
            entity.Property(e => e.Nome)
                .HasMaxLength(255)
                .HasColumnName("nome");
            entity.Property(e => e.SenhaHash)
                .HasMaxLength(255)
                .HasColumnName("senha_hash");
            entity.Property(e => e.Telefone)
                .HasMaxLength(20)
                .HasColumnName("telefone");
        });

        modelBuilder.Entity<UsuarioRedeSocial>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario_rede_social");

            entity.HasIndex(e => e.IdUsuario, "FK_usuario_rede_social_usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Plataforma)
                .HasMaxLength(50)
                .HasColumnName("plataforma");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasColumnName("url");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioRedeSocials)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_usuario_rede_social_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
