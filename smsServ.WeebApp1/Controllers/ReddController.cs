using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using smsServ.WeebApp1.Model;

namespace smsServ.WeebApp1.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class ReddController : ControllerBase
    {
        [HttpGet]
        public async Task<repoMdl> Get()
        {
            repoMdl res = new repoMdl();
            res.prodName = "It service";
            res.prodCode = "101009";
            return res;
        }
    }
}