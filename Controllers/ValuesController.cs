using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Profiling;
using WebApplication1.Models;

namespace WebApp.Controllers
{
    /// <summary>
    /// 一日一日游刃有余
    /// </summary>
    [Route("api/[controller]")]

    public class ValuesController : Controller
    {
        private readonly IOptions<WebApplication1.Models.MM> _option;
        private readonly IOptions<WebApplication1.Models.dddModel> _optiondd;

        private readonly ILogger<ValuesController> _logger;

        /// <summary>
        /// jgjgjgaaa
        /// </summary>
        /// <param name="logger">ww</param>
        /// <param name="option">33</param>
        /// <param name="optiondd">444rr</param>
        public ValuesController(ILogger<ValuesController> logger, IOptions<WebApplication1.Models.MM> option, IOptions<WebApplication1.Models.dddModel> optiondd)
        {
            _option = option;
            _optiondd = optiondd;
            _logger = logger;
        }
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            var dd = _optiondd.Value.uuu;
            var aa = new string[] { _option.Value.aa, _option.Value.bb };

            var a = aa.ToList();
            var b = dd.ToList();
            var cc = a.Concat(b);

            //默认的日志输出

            _logger.LogTrace("version" + Guid.NewGuid().ToString("N"));
            _logger.LogDebug("LogDebug" + Guid.NewGuid().ToString("N"));
            _logger.LogInformation("LogInformation" + Guid.NewGuid().ToString("N"));
            _logger.LogWarning("LogWarning" + Guid.NewGuid().ToString("N"));
            _logger.LogError("LogError" + Guid.NewGuid().ToString("N"));
            _logger.LogCritical("LogError" + Guid.NewGuid().ToString("N"));
            return cc;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var text = "";
            var file = new FileInfo("Config/aaa.err");
            using (FileStream str = file.OpenRead())
            {
                using (StreamReader stream = new StreamReader(str, Encoding.GetEncoding("GB2312")))
                {
                    text = stream.ReadToEnd();
                }
            }
            return text;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] MM value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



        /// <summary>
        /// 获取html片段
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [Route("GetHtml")]
        public IActionResult GetHtml()
        {

            var html = MiniProfiler.Current.RenderIncludes(HttpContext);
            return Ok(html.Value);


        }
    }

}
