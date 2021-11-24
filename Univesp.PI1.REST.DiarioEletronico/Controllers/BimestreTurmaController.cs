using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/[controller]")]
    public class BimestreTurmaController : Controller
    {
        BimestreData _BimestreData = new BimestreData();

        [HttpGet]
        [Route("get-bismestre-turma")]
        public IEnumerable<Bimestre> Get(int bimestre, int id)
        {
            return _BimestreData.GetTurmaBimestre(bimestre, id);
        }

        [HttpPost]
        [Route("post-bismestre-turma")]
        public string PostTurma(int bimestre, int id)
        {
            return _BimestreData.DeleteTurmaBimestre(bimestre, id);
        }

        [HttpDelete]
        [Route("delete-bimestre-turma")]
        public string DeleteTurma(int bimestre, int id)
        {
            //deleta tumar bimestre
            return _BimestreData.DeleteTurmaBimestre(bimestre, id);
        }
    }
}
