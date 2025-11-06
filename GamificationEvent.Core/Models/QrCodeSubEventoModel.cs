using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Models
{
    public class QrCodeSubEventoModel
    {
        public Guid IdSubEvento { get; set; }
        public string CodigoSubEvento { get; set; }
        public string QrCode { get; set; }

        public QrCodeSubEventoModel(Guid idSubEvento, string codigoSubEvento, string qrCode)
        {
            IdSubEvento = idSubEvento;
            CodigoSubEvento = codigoSubEvento;
            QrCode = qrCode;
        }
    }


}
