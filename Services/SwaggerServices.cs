using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Services
{
    public static class SwaggerServices
    {

        public static void AddSwaggerServiceSetup(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                //配置文档
                options.SwaggerDoc("V1", new Microsoft.OpenApi.Models.OpenApiInfo()
                {
                    Version = "V1",
                    Description = "vvvvv",
                    Title = "aaaaaa",

                });
                options.OrderActionsBy(o => o.RelativePath);



                //文档注释

                var xmlPath = Path.Combine(AppContext.BaseDirectory,"WebApplication1.xml");
                options.IncludeXmlComments(xmlPath,true); //默认的第二个参数是false，这个是controller的注释，记得修改


            });

        }
    }
}
