using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Extendsion
{
    public static class SerilogExtension
    {
        public static void SetSerilogConfig(this IConfiguration configration)
        {
            //系统自带注入的日志级别 是
            // 优先等级从上到下递减Trace < Debug < Information < Warning < Error < Critical < None



            //serilog 日志级别
            //Verbose >Debug > Information> Warning  >Error > Fatal
            var elasticUri = configration["ElasticConfiguration:Uri"];
            var appKey = configration["ElasticConfiguration:AppKey"];

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configration)
                  //  .MinimumLevel.Information() //最小日志级别
                  //   .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                  .Enrich.FromLogContext()//使用Serilog.Context.LogContext中的属性丰富日志事件。
                                          // .WriteTo.Console(new RenderedCompactJsonFormatter())//输出到控制台
                                          //.WriteTo.Console(
                                          //  outputTemplate: "发生时间:{Timestamp: HH:mm:ss.fff} 事件级别:{Level} 详细信息:{Message}{NewLine}{Exception}")
                                          //  .WriteTo.File(formatter: new CompactJsonFormatter(), "logs\\test.txt", rollingInterval: RollingInterval.Day)//输出到文件

               .WriteTo.Logger(lc =>
               {
                   lc.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Information)
                   .WriteTo.File("logs/informations/log.txt",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {SourceContext} {NewLine}{Message}{NewLine}{Exception}",
                       rollingInterval: RollingInterval.Day,
                       rollOnFileSizeLimit: true);
               })
         .WriteTo.Logger(lc =>
         {
             lc.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
             .WriteTo.File("logs/errors/log.txt",
              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {SourceContext} {NewLine}{Message}{NewLine}{Exception}",
                 rollingInterval: RollingInterval.Day,
                 rollOnFileSizeLimit: true);
         })
          .WriteTo.Logger(lc =>
          {
              lc.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
              .WriteTo.File("logs/debugs/log.txt",
               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {SourceContext} {NewLine}{Message}{NewLine}{Exception}",
                  rollingInterval: RollingInterval.Day,
                  rollOnFileSizeLimit: true);
          }).WriteTo.Logger(lc =>
          {
              lc.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Warning)
              .WriteTo.File("logs/warings/log.txt",
               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {SourceContext} {NewLine}{Message}{NewLine}{Exception}",
                  rollingInterval: RollingInterval.Day,
                  rollOnFileSizeLimit: true);
          }).WriteTo.Logger(lc =>
          {
              lc.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Verbose)
              .WriteTo.File("logs/versions/log.txt",
               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {SourceContext} {NewLine}{Message}{NewLine}{Exception}",
                  rollingInterval: RollingInterval.Day,
                  rollOnFileSizeLimit: true);
          }).WriteTo.Logger(lc =>
          {
              lc.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Fatal)
              .WriteTo.File("logs/fatals/log.txt",
               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {SourceContext} {NewLine}{Message}{NewLine}{Exception}",
                  rollingInterval: RollingInterval.Day,
                  rollOnFileSizeLimit: true);
          }).WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
          {
              AutoRegisterTemplate = true,//自动注册模版
              EmitEventFailure = EmitEventFailureHandling.RaiseCallback,
              //如果数据存储报错 回调的方法
              FailureCallback = e => Console.WriteLine("Unable to submit event " + e.MessageTemplate),
              //开启用户名和密码 认证
              ModifyConnectionSettings = conn =>
            {
                conn.ServerCertificateValidationCallback((source, certificate, chain, sslPolicyErrors) => true);
                conn.BasicAuthentication("elastic", "fxg123456");
                return conn;
            }
          }).Enrich.WithProperty("AppKey", appKey)  //增加自定义索引属性  应用的标识作用
           .CreateLogger();//清除内置日志框架
        }
    }
}
