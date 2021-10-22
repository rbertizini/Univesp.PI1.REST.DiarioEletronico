using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletrônico.Data;
using Univesp.PI1.REST.DiarioEletrônico.Models;

namespace Univesp.PI1.REST.DiarioEletrônico.Controllers
{
    public class ProfessorController : ApiController
    {
        ProfessorData profData = new ProfessorData();

        // GET api/values
        public IEnumerable<Professor> Get()
        {
            //Obter lista de professores
            List<Professor> _profs = new List<Professor>();
            _profs = profData.ObterListaProfessor();
            
            //Retorno
            return _profs;
        }

        // GET api/values/5
        public Professor Get(int id)
        {
            //Obter lista de professores
            Professor _prof = new Professor();
            _prof = profData.ObterProfessor(id);
           
            //Retorno
            return _prof;
        }

        // POST api/values
        public MensProc Post([FromBody] Professor profIns)
        {
            //Adicionar registro de professos
            string retProc = profData.AdicionarProfessor(profIns);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // PUT api/values/5
        public MensProc Put(int id, [FromBody] Professor profEdt)
        {
            //Adicionar registro de professos
            string retProc = profData.EditarProfessor(id, profEdt);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }

        // DELETE api/values/5
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
