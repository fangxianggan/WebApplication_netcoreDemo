using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            //获取命令行参数
            .ConfigureAppConfiguration(build =>
            {
               

                //新增一个配置文件
                build.AddJsonFile("Config/kkk.json",false,true);

                build.AddCommandLine(args);
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine("----参数-" + (i + 1) + "---" + args[i] + "-------");
                }
               
            })
            //设置默认主机配置
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //硬编码改变启动地址
                    // webBuilder.UseUrls("http://*:7000");
                }).UseSerilog(dispose:true);

       

       
    }
}
