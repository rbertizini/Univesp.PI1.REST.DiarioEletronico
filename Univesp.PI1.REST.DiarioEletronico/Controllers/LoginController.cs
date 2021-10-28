using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Univesp.PI1.REST.DiarioEletronico.Data;
using Univesp.PI1.REST.DiarioEletronico.Models;

namespace Univesp.PI1.REST.DiarioEletronico.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        LoginData loginData = new LoginData();

        // POST api/login
        [HttpPost]
        [Route("")]
        public MensProc Post([FromBody] Login loginChk)
        {
            //Adicionar registro de professos
            string retProc = loginData.VerificarLogin(loginChk);

            //Retorno
            MensProc _mens = new MensProc();
            _mens.Mensagem = retProc;
            return _mens;
        }
    }
}