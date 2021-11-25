using System.Collections.Generic;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/bimestreturma")]
    public class BimestreTurmaController : ApiController
    {
        BimestreData _BimestreData = new BimestreData();

        [HttpGet]
        [Route("{bimestre:int}/{id:int}")]
        public IEnumerable<Bimestre> Get(int bimestre, int id)
        {
            return _BimestreData.GetTurmaBimestre(bimestre, id);
        }

        [HttpPost]
        [Route("{bimestre:int}/{id:int}")]
        public MensProc Post(int bimestre, int id)
        {
            MensProc _mens = new MensProc();
            _mens.Mensagem = _BimestreData.Save(bimestre, id);
            return _mens;
        }

        [HttpDelete]
        [Route("{bimestre:int}/{id:int}")]
        public MensProc Delete(int bimestre, int id)
        {
            //deleta tumar bimestre
            MensProc _mens = new MensProc();
            _mens.Mensagem = _BimestreData.DeleteTurmaBimestre(bimestre, id); ;
            return _mens;
        }
    }
}
