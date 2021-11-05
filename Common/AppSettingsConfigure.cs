using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Common
{

    /// <summary>
    /// appsseting.json 文件配置  多环境下使用不同的配置文件
    /// </summary>
    public static class AppSettingsConfigure
    {
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _cacheDict;
        static AppSettingsConfigure()
        {
            // 缓存字典
            _cacheDict = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        /// <summary>
        /// 设置多环境下使用那个appsetting的配置环境
        /// </summary>
        /// <param name="jsonDir"></param>
        /// <param name="environmentName"></param>
        /// <returns></returns>
        public static IConfiguration SetConfigure(string jsonDir, string environmentName = null)
        {
            // 设置缓存的 KEY
            var cacheKey = $"{jsonDir}#{environmentName}";
            var configurationBuild = new ConfigurationBuilder().SetBasePath(jsonDir).AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("Config/kkk.json",true,true);
            if (!string.IsNullOrEmpty(environmentName))
            {
                configurationBuild = configurationBuild.AddJsonFile($"appsettings.{environmentName}.json", false, true);
            }
            var build = configurationBuild.Build();
            //加入缓存
            _cacheDict.TryAdd(cacheKey, build);
            return build;
        }
    }
}
