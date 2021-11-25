using System.Collections.Generic;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/turma")]
    public class TurmaController : ApiController
    {
        TurmaData turmaData = new TurmaData();

        // GET api/turma
        [HttpGet]
        [Route("")]
        public IEnumerable<Turma> Get()
        {
            //Obter lista de professores
            List<Turma> _Turmas = new List<Turma>();
            _Turmas = turmaData.ObterListaTurma();

            //Retorno
            return _Turmas;
        }

        // GET api/turma/5
        [HttpGet]
        [Route("{id:int}")]
        public Turma Get(int id)
        {
            //Obter lista de professores
            Turma _Turma = new Turma();
            _Turma = turmaData.ObterTurma(id);

            //Retorno
            return _Turma;
        }

        // POST api/turma
        [HttpPost]
        [Route("")]
        public MensProc Post([FromBody] Turma turmaIns)
        {
            //Adicionar registro de professos
            string retProc = turmaData.AdicionarTurma(turmaIns);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // PUT api/turma/5
        [HttpPut]
        [Route("{id:int}")]
        public MensProc Put(int id, [FromBody] Turma turmaEdt)
        {
            //Adicionar registro de professos
            string retProc = turmaData.EditarTurma(id, turmaEdt);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // DELETE api/turma/5
        [HttpDelete]
        [Route("{id:int}")]
        public MensProc Delete(int id)
        {
            //Adicionar registro de professos
            string retProc = turmaData.ExcluirTurma(id);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }
    }
}