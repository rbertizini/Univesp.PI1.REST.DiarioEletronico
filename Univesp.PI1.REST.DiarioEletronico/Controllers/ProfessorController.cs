using System.Collections.Generic;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/professor")]
    public class ProfessorController : ApiController
    {

        ProfessorData profData = new ProfessorData();

        // GET api/professor
        [HttpGet]
        [Route("")]
        public IEnumerable<Professor> Get()
        {
            //Obter lista de professores
            List<Professor> _profs = new List<Professor>();
            _profs = profData.ObterListaProfessor();

            //Retorno
            return _profs;
        }

        // GET api/professor/5
        [HttpGet]
        [Route("{id:int}")]
        public Professor Get(int id)
        {
            //Obter lista de professores
            Professor _prof = new Professor();
            _prof = profData.ObterProfessor(id);

            //Retorno
            return _prof;
        }

        // POST api/professor
        [HttpPost]
        [Route("")]
        public MensProc Post([FromBody] Professor profIns)
        {
            //Adicionar registro de professos
            string retProc = profData.AdicionarProfessor(profIns);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // PUT api/professor/5
        [HttpPut]
        [Route("{id:int}")]
        public MensProc Put(int id, [FromBody] Professor profEdt)
        {
            //Adicionar registro de professos
            string retProc = profData.EditarProfessor(id, profEdt);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // DELETE api/professor/5
        [HttpDelete]
        [Route("{id:int}")]
        public MensProc Delete(int id)
        {
            //Adicionar registro de professos
            string retProc = profData.ExcluirProfessor(id);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }
    }
}
