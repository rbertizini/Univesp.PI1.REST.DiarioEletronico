using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/turmaaluno")]
    public class TurmaAlunoController : ApiController
    {
        TurmaAlunoData turmaAlunoData = new TurmaAlunoData();

        // GET api/turmaaluno
        [HttpGet]
        [Route("")]
        public IEnumerable<TurmaAluno> Get()
        {
            //Obter lista de professores
            List<TurmaAluno> _TurmaAlunos = new List<TurmaAluno>();
            _TurmaAlunos = turmaAlunoData.ObterListaTurmaAluno();

            //Retorno
            return _TurmaAlunos;
        }

        // GET api/turmaaluno/5
        [HttpGet]
        [Route("{id:int}")]
        public TurmaAluno Get(int id)
        {
            //Obter lista de professores
            TurmaAluno _TurmaAluno = new TurmaAluno();
            _TurmaAluno = turmaAlunoData.ObterTurmaAluno(id);

            //Retorno
            return _TurmaAluno;
        }

        // POST api/turmaaluno
        [HttpPost]
        [Route("")]
        public MensProc Post([FromBody] TurmaAluno turmaAlunoIns)
        {
            //Adicionar registro de professos
            string retProc = turmaAlunoData.AdicionarTurmaAluno(turmaAlunoIns);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        //PUT desativado - não haverá para esse processo
        //// PUT api/turmaaluno/5
        //[HttpPut]
        //[Route("{id:int}")]
        //public MensProc Put(int id, [FromBody] Turma turmaAlunoEdt)
        //{
        //    //Adicionar registro de professos
        //    string retProc = turmaAlunoData.EditarTurmaAluno(id, turmaAlunoEdt);

        //    //Retorno
        //    MensProc _mens = new MensProc();
        //    _mens.Mensagem = retProc;
        //    return _mens;
        //}

        // DELETE api/turmaaluno/5
        [HttpDelete]
        [Route("{id:int}")]
        public MensProc Delete(int id)
        {
            //Adicionar registro de professos
            string retProc = turmaAlunoData.ExcluirTurmaAluno(id);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }
    }
}