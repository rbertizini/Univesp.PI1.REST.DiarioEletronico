using System.Collections.Generic;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/avaliacao")]
    public class AvaliacaoController : ApiController
    {
        AvaliacaoData avalData = new AvaliacaoData();

        // GET api/avaliacao/5
        [HttpGet]
        [Route("{id:int}")]
        public IEnumerable<Avaliacao> Get(int id)
        {
            //Obter lista de professores
            List<Avaliacao> _Avaliacoes = new List<Avaliacao>();
            _Avaliacoes = avalData.ObterListaAvalTurma(id);

            //Retorno
            return _Avaliacoes;
        }

        // POST api/avaliacao
        [HttpPost]
        [Route("")]
        public MensProc Post([FromBody] Avaliacao avalIns)
        {
            //Adicionar registro de professos
            string retProc = avalData.AdicionarAval(avalIns);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // DELETE api/avaliacao/5
        [HttpDelete]
        [Route("{id:int}")]
        public MensProc Delete(int id)
        {
            //Adicionar registro de professos
            string retProc = avalData.ExcluirAval(id);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }
    }
}