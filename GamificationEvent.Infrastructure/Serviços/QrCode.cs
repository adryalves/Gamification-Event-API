
using System;
using System.IO;
using QRCoder;
using SkiaSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GamificationEvent.Core.Interfaces;


namespace GamificationEvent.Infrastructure.Serviços
{
    public class QrCode : IQrCode
    {
        public string GerarQRCode(string codigoCheckIn)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                var qrCodeData = qrGenerator.CreateQrCode(codigoCheckIn, QRCodeGenerator.ECCLevel.Q);

                int pixelsPorModulo = 10;
                int tamanho = qrCodeData.ModuleMatrix.Count * pixelsPorModulo;

                using (var surface = SKSurface.Create(new SKImageInfo(tamanho, tamanho)))
                {
                    var canvas = surface.Canvas;
                    canvas.Clear(SKColors.White);

                    using (var paint = new SKPaint { Color = SKColors.Black })
                    {
                        for (int y = 0; y < qrCodeData.ModuleMatrix.Count; y++)
                        {
                            for (int x = 0; x < qrCodeData.ModuleMatrix.Count; x++)
                            {
                                if (qrCodeData.ModuleMatrix[y][x])
                                {
                                    canvas.DrawRect(x * pixelsPorModulo, y * pixelsPorModulo,
                                                    pixelsPorModulo, pixelsPorModulo, paint);
                                }
                            }
                        }
                    }

                    using (var image = surface.Snapshot())
                    using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                    using (var ms = new MemoryStream())
                    {
                        data.SaveTo(ms);
                        string base64 = Convert.ToBase64String(ms.ToArray());
                        return $"data:image/png;base64,{base64}";
                    }
                }
            }
        }
    }
}
