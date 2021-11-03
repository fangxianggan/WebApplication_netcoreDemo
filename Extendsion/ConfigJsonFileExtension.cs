using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Common;
using WebApplication1.Models;

namespace WebApplication1.Extendsion
{
    public  static class ConfigJsonFileExtension
    {
       
        public static void AddCustJsonFileService(this IServiceCollection services ,IConfiguration configuration)
        {
            CustConfigFile configFile = new CustConfigFile();
            services.Configure<dddModel>(configFile.AddCustJsonFile(configuration));
        }
    }
}
