﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebApp.Controllers
{

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IOptions<WebApplication1.Models.MM> _option;
        private readonly IOptions<WebApplication1.Models.dddModel> _optiondd;

        public ValuesController(IOptions<WebApplication1.Models.MM> option, IOptions<WebApplication1.Models.dddModel> optiondd)
        {
            _option = option;
            _optiondd = optiondd;
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
        public void Post([FromBody] string value)
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
    }
}