using System.Collections.Generic;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/aluno")]
    public class AlunoController : ApiController
    {
        AlunoData alunoData = new AlunoData();

        // GET api/aluno
        [HttpGet]
        [Route("")]
        public IEnumerable<Aluno> Get()
        {
            //Obter lista de professores
            List<Aluno> _alunos = new List<Aluno>();
            _alunos = alunoData.ObterListaAluno();

            //Retorno
            return _alunos;
        }

        // GET api/aluno/5
        [HttpGet]
        [Route("{id:int}")]
        public Aluno Get(int id)
        {
            //Obter lista de professores
            Aluno _aluno = new Aluno();
            _aluno = alunoData.ObterAluno(id);

            //Retorno
            return _aluno;
        }

        // POST api/aluno
        [HttpPost]
        [Route("")]
        public MensProc Post([FromBody] Aluno alunoIns)
        {
            //Adicionar registro de professos
            string retProc = alunoData.AdicionarAluno(alunoIns);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // PUT api/aluno/5
        [HttpPut]
        [Route("{id:int}")]
        public MensProc Put(int id, [FromBody] Aluno alunoEdt)
        {
            //Adicionar registro de professos
            string retProc = alunoData.EditarAluno(id, alunoEdt);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // DELETE api/aluno/5
        [HttpDelete]
        [Route("{id:int}")]
        public MensProc Delete(int id)
        {
            //Adicionar registro de professos
            string retProc = alunoData.ExcluirAluno(id);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }
    }
}