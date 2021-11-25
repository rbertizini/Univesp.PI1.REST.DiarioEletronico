using System.Collections.Generic;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/diario")]
    public class DiarioController : ApiController
    {
        DiarioData diarioData = new DiarioData();

        // GET api/diario/5
        [HttpGet]
        [Route("{id:int}")]
        public IEnumerable<Diario> Get(int id)
        {
            //Obter lista de professores
            List<Diario> _Diarios = new List<Diario>();
            _Diarios = diarioData.ObterListaDiarioTurma(id);

            //Retorno
            return _Diarios;
        }

        // POST api/diario
        [HttpPost]
        [Route("")]
        public MensProc Post([FromBody] Diario diarioIns)
        {
            //Adicionar registro de professos
            string retProc = diarioData.AdicionarDiario(diarioIns);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // DELETE api/diario/5
        [HttpDelete]
        [Route("{id:int}")]
        public MensProc Delete(int id)
        {
            //Adicionar registro de professos
            string retProc = diarioData.ExcluirDiario(id);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }
    }
}