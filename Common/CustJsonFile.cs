using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Common
{
    public  class CustConfigFile
    {
        public  IConfiguration AddCustJsonFile(IConfiguration _configuration)
        {
            dddModel model = new dddModel();
            _configuration.Bind(model);
            return _configuration;
        }   
    }
}
