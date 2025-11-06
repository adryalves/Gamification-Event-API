using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamificationEvent.Core.Interfaces
{
    public interface IQrCode
    {
        public string GerarQRCode(string codigoCheckIn);
    }
}
