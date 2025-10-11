using GamificationEvent.Core.Enums;
using System;
using System.Collections.Generic;

namespace GamificationEvent.Infrastructure.Data.Persistence;

public partial class Participante
{
    public Guid Id { get; set; }

    public Guid IdEvento { get; set; }

    public Guid IdUsuario { get; set; }

    public Cargo Cargo { get; set; } 

    public int Pontuacao { get; set; }

    public DateTime DataHoraCriacao { get; set; }

    public virtual ICollection<BadgeParticipante> BadgeParticipantes { get; set; } = new List<BadgeParticipante>();

    public virtual ICollection<CheckinEstande> CheckinEstandes { get; set; } = new List<CheckinEstande>();

    public virtual ICollection<CheckinSubEvento> CheckinSubEventos { get; set; } = new List<CheckinSubEvento>();

    public virtual ICollection<Conexao> ConexaoIdParticipante1Navigations { get; set; } = new List<Conexao>();

    public virtual ICollection<Conexao> ConexaoIdParticipante2Navigations { get; set; } = new List<Conexao>();

    public virtual ICollection<Conversa> ConversaIdParticipante1Navigations { get; set; } = new List<Conversa>();

    public virtual ICollection<Conversa> ConversaIdParticipante2Navigations { get; set; } = new List<Conversa>();

    public virtual ICollection<DesafioParticipante> DesafioParticipantes { get; set; } = new List<DesafioParticipante>();

    public virtual Evento IdEventoNavigation { get; set; } = null!;

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<InteracaoPublicacao> InteracaoPublicacaos { get; set; } = new List<InteracaoPublicacao>();

    public virtual ICollection<Mensagem> Mensagems { get; set; } = new List<Mensagem>();

    public virtual ICollection<ParticipanteInteresse> ParticipanteInteresses { get; set; } = new List<ParticipanteInteresse>();

    public virtual ICollection<ParticipanteNotificacao> ParticipanteNotificacaos { get; set; } = new List<ParticipanteNotificacao>();

    public virtual ICollection<ParticipantePremio> ParticipantePremios { get; set; } = new List<ParticipantePremio>();

    public virtual ICollection<ParticipanteQuizRespostum> ParticipanteQuizResposta { get; set; } = new List<ParticipanteQuizRespostum>();

    public virtual ICollection<PassaporteParticipanteItem> PassaporteParticipanteItems { get; set; } = new List<PassaporteParticipanteItem>();

    public virtual ICollection<PassaporteParticipante> PassaporteParticipantes { get; set; } = new List<PassaporteParticipante>();

    public virtual ICollection<PerguntasSubEvento> PerguntasSubEventos { get; set; } = new List<PerguntasSubEvento>();

    public virtual ICollection<Publicacao> Publicacaos { get; set; } = new List<Publicacao>();

    public virtual ICollection<QuizParticipante> QuizParticipantes { get; set; } = new List<QuizParticipante>();

    public virtual ICollection<RespostaParticipantePerguntum> RespostaParticipantePergunta { get; set; } = new List<RespostaParticipantePerguntum>();
}
