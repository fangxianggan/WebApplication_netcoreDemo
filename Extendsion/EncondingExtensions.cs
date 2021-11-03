using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication1.Extendsion
{
    public static class EncondingExtensions
    {

        public static void AddCustEncodingService(this IServiceCollection service)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding.GetEncoding("GB2312");

          //  service.AddSingleton<Encoding, Encoding>();
        }
    }
}
