using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Univesp.PI1.Database;
using Univesp.PI1.REST.DiarioEletronico.Models;
using static Univesp.PI1.REST.DiarioEletronico.Except.ExceptionDb;

namespace Univesp.PI1.REST.DiarioEletronico.Data
{
    public class LoginData
    {
        //Adicionar registro de TurmaAluno
        internal string VerificarLogin(Login loginChk)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select email, senha ";
            strQueryIns += "From CadProfessor as login ";
            strQueryIns += "Where email = @email and senha = @senha ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@email", loginChk.Email},
                {"@senha", loginChk.Senha},
            };

            //Executando            
            string retProc = string.Empty;
            Login Login = new Login();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns, parQueryIns);

                if (listRows.Count == 1)
                {
                    retProc = "Ok";
                }
                else
                {
                    retProc = "Usuário ou senha inválido";
                }                
            }
            catch (Exception ex)
            {
                retProc = "Erro na inserção do registro";
                throw new DbInicProcException(message: "Erro no método Login", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }
    }
}