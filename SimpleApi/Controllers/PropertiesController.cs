using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers
{
    [Route("api/[controller]")]
    public class PropertiesController : Controller
    {

        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> keys = new List<string>();
            if (null != Program.SimpleApiConfig){
                Program.SimpleApiConfig.GetChildren().ToList().ForEach(x => keys.Add(x.Key));
            }
            return keys;
        }

        [HttpGet("{key}")]
        public string Get(string key)
        {
            var val = Program.SimpleApiConfig[key];
            return (val != null) ? String.Format("{0} = {1}{2}", key, val, Environment.NewLine) : String.Format("<no such key>{0}", Environment.NewLine);
        }
        
    }
}