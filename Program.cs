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
            //��ȡ�����в���
            .ConfigureAppConfiguration(build =>
            {
               

                //����һ�������ļ�
                build.AddJsonFile("Config/kkk.json",false,true);

                build.AddCommandLine(args);
                for (int i = 0; i < args.Length; i++)
                {
                    Console.WriteLine("----����-" + (i + 1) + "---" + args[i] + "-------");
                }
               
            })
            //����Ĭ����������
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //Ӳ����ı�������ַ
                    // webBuilder.UseUrls("http://*:7000");
                }).UseSerilog(dispose:true);

       

       
    }
}
