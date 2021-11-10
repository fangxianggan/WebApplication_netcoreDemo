using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Profiling.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using WebApplication1.Common;
using WebApplication1.Extendsion;
using WebApplication1.Middleware;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        //配置文件获取的
        private IConfiguration _configuration;

        public Startup(IHostEnvironment env)
        {
            _configuration = AppSettingsConfigure.SetConfigure(env.ContentRootPath, env.EnvironmentName);
            //设置日志
            _configuration.SetSerilogConfig();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            // services.AddCustEncodingService();

            // services.AddCustJsonFileService(_configuration);

            //swagger注入
            services.AddSwaggerServiceSetup();


            //注册到服务里

            //1.注册配置选项的服务
            services.Configure<MM>(_configuration.GetSection("MM"));
            services.Configure<dddModel>(_configuration);


            //2.通过绑定配置对象模型方式  用法是  IOptions<T> 获取 
            //全部绑定
            dddModel ddd = new dddModel();
            _configuration.Bind(ddd);

            //部分类绑定
            MM mM = new MM();
            _configuration.GetSection("MM").Bind(mM);


            //3.直接读取
            var aaa = _configuration["dddModel:uuu"];
            var aa = _configuration["MM:aa"];
            var bb = _configuration["MM:bb"];

            Console.WriteLine(aaa);
            Console.WriteLine(aa);
            Console.WriteLine(bb);

            //使用自定义的服务
            services.AddCustomHttpContextAccessor();


            services.AddMiniProfiler(options =>
            {
                //设置访问地址的根目录路径
                options.RouteBasePath = "/profiler";
                //数据缓存时间
                (options.Storage as MemoryCacheStorage).CacheDuration = TimeSpan.FromMinutes(60);
                //sql 格式的设置
                options.SqlFormatter =new StackExchange.Profiling.SqlFormatters.InlineFormatter();
                //跟踪连接打开关闭
                options.TrackConnectionOpenClose = true;
                //界面主题颜色方案;默认浅色
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Dark;
                //.net core 3.0以上：对MVC过滤器进行分析
                options.EnableMvcFilterProfiling = true;
                //对视图进行分析
                options.EnableMvcViewProfiling = true;
            });

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

            //启动swagger 
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
               

                //自定义swaager显示的首页 为了集成miniprofiler 性能检测
                c.IndexStream = (Func<Stream>)(
                () => Assembly.GetExecutingAssembly().GetManifestResourceStream("WebApplication1.wwwroot.index.html")
                );

                //  设置首页为Swagger
                c.RoutePrefix = string.Empty;

                //
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "webapi v1");

                //
            });

            app.UseMiniProfiler();

            //加入到管道
            app.UseStaticHttpContext();

            //自定义中间键
            app.UseRequestMiddleware();


            Console.WriteLine("哈哈哈凤凰凤凰试试看");

            //终点中间键
            //app.Run(async (cotenxt) => {
            //    await cotenxt.Response.WriteAsync("终点中间键");
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

                //    //获取命令行输入的参数IP和端口
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
