using GamificationEvent.Core.Interfaces;
using GamificationEvent.Core.Models;
using GamificationEvent.Core.Resultados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Application.UseCases.CheckInSubEventoCases
{
    public class GerarQrCodeUseCaseSubEvento
    {
        private readonly ISubEventoRepository _subEventoRepository;
        private readonly IQrCode _qrCode;

        public GerarQrCodeUseCaseSubEvento(ISubEventoRepository subEventoRepository, IQrCode qrCode)
        {
            _subEventoRepository = subEventoRepository;
            _qrCode = qrCode;
        }

        public async Task<Resultado<QrCodeSubEventoModel>> GerarQrCodeDoSubEvento(Guid idSubEvento)
        {
            var subEvento = await _subEventoRepository.GetSubEventoPorId(idSubEvento);

            if (subEvento == null) return Resultado<QrCodeSubEventoModel>.Falha("O Id não corresponde a um Id SubEvento válido");

            var qrCode = _qrCode.GerarQRCode(subEvento.CodigoCheckin);

            if (String.IsNullOrEmpty(qrCode)) return Resultado<QrCodeSubEventoModel>.Falha("Ocorreu algum erro inesperado");

            var resultado = new QrCodeSubEventoModel(idSubEvento,subEvento.CodigoCheckin,qrCode);

            return Resultado<QrCodeSubEventoModel>.Ok(resultado);

            }
        }
    }

