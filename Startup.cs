using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using WebApplication1.Common;
using WebApplication1.Extendsion;
using WebApplication1.Middleware;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Startup
    {
        //�����ļ���ȡ��
        private IConfiguration _configuration;

        public Startup(IHostEnvironment env)
        {
            _configuration = AppSettingsConfigure.SetConfigure(env.ContentRootPath, env.EnvironmentName);
            //������־
            _configuration.SetSerilogConfig();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // services.AddCustEncodingService();

            // services.AddCustJsonFileService(_configuration);

            //ע�ᵽ������

            //1.ע������ѡ��ķ���
            services.Configure<MM>(_configuration.GetSection("MM"));
            services.Configure<dddModel>(_configuration);


            //2.ͨ�������ö���ģ�ͷ�ʽ  �÷���  IOptions<T> ��ȡ 
            //ȫ����
            dddModel ddd = new dddModel();
            _configuration.Bind(ddd);

            //�������
            MM mM = new MM();
            _configuration.GetSection("MM").Bind(mM);


            //3.ֱ�Ӷ�ȡ
            var aaa = _configuration["dddModel:uuu"];
            var aa = _configuration["MM:aa"];
            var bb = _configuration["MM:bb"];

            Console.WriteLine(aaa);
            Console.WriteLine(aa);
            Console.WriteLine(bb);

            //ʹ���Զ���ķ���
            services.AddCustomHttpContextAccessor();

            //
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHttpContextAccessor httpContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //���뵽�ܵ�
            app.UseStaticHttpContext();

            //�Զ����м��
            app.UseRequestMiddleware();


            Console.WriteLine("��������˷�����Կ�");

            //�յ��м��
            //app.Run(async (cotenxt) => {
            //    await cotenxt.Response.WriteAsync("�յ��м��");
            //});

            app.UseRouting();

            //
            app.Use(async (context, next) =>
            {
                var dd = context.Request.RouteValues;
                foreach (var item in dd)
                {
                    Console.WriteLine(item.Value.ToString());
                }
                
                await next();
            });


            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    var aa = httpContext.HttpContext.User.Identity.Name;
                //    await context.Response.WriteAsync("aa=" + aa);

                //    //��ȡ����������Ĳ���IP�Ͷ˿�
                //    var ip = context.Request.Host.Host;
                //    var port = context.Request.Host.Port;
                //    await context.Response.WriteAsync("ip=" + ip + ";port=" + port + "");
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapControllers();
            });
        }
    }
}
