using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Univesp.PI1.Database;
using Univesp.PI1.REST.DiarioEletrônico.Models;
using static Univesp.PI1.REST.DiarioEletrônico.Except.ExceptionDb;

namespace Univesp.PI1.REST.DiarioEletrônico.Data
{
    public class ProfessorData
    {
        //Obter lista de professores
        internal List<Professor> ObterListaProfessor()
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdCadProfessor, NomeProfessor, Disciplina, Email ";
            strQueryIns += "From CadProfessor as prf ";

            //Executando            
            List<Professor> Profs = new List<Professor>();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns);

                foreach (var row in listRows)
                {
                    Professor _prof = new Professor();
                    _prof.IdCadProfessor = int.Parse(row["IdCadProfessor"]);
                    _prof.NomeProfessor = row["NomeProfessor"];
                    _prof.Disciplina = row["Disciplina"];
                    _prof.Email = (row["Email"]);
                    Profs.Add(_prof);
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaProfessor", innerException: ex.InnerException);
            }

            //Retorno 
            return Profs;
        }

        //Obter registro de professor
        internal Professor ObterProfessor(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdCadProfessor, NomeProfessor, Disciplina, Email ";
            strQueryIns += "From CadProfessor as prf ";
            strQueryIns += "Where IdCadProfessor = @id";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@id", id}
            };

            //Executando            
            Professor Prof = new Professor();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns, parQueryIns);

                foreach (var row in listRows)
                {
                    Professor _prof = new Professor();
                    _prof.IdCadProfessor = int.Parse(row["IdCadProfessor"]);
                    _prof.NomeProfessor = row["NomeProfessor"];
                    _prof.Disciplina = row["Disciplina"];
                    _prof.Email = (row["Email"]);
                    Prof = _prof;
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterProfessor", innerException: ex.InnerException);
            }

            //Retorno 
            return Prof;
        }

        //Adicionar registro de professor
        internal string AdicionarProfessor(Professor profIns)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Insert Into CadProfessor ";
            strQueryIns += "(NomeProfessor, Disciplina, Email) ";
            strQueryIns += "values ";
            strQueryIns += "(@NomeProfessor, @Disciplina, @Email) ";
            
            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@NomeProfessor", profIns.NomeProfessor},
                {"@Disciplina", profIns.Disciplina},
                {"@Email", profIns.Email}
            };

            //Executando            
            string retProc = string.Empty;
            try
            {
                procDb.Inserir(strQueryIns, parQueryIns);
                retProc = "Registro incluído";
            }
            catch (Exception ex)
            {
                retProc = "Erro na inserção do registro";
                throw new DbInicProcException(message: "Erro no método AdicionarProfessor", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Alterar registro de professor
        internal string EditarProfessor(int id, Professor profEdt)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Update CadProfessor ";
            strQueryIns += "Set NomeProfessor = @NomeProfessor, ";
            strQueryIns += "Disciplina = @Disciplina, ";
            strQueryIns += "Email = @Email ";
            if (!string.IsNullOrEmpty(profEdt.Senha))
                strQueryIns += ", Senha = @Senha ";
            strQueryIns += "Where IdCadProfessor = @IdCadProfessor";
            
            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@NomeProfessor", profEdt.NomeProfessor},
                {"@Disciplina", profEdt.Disciplina},
                {"@Email", profEdt.Email},
                {"@Senha", profEdt.Senha},
                {"@IdCadProfessor", id}
            };

            //Executando            
            string retProc = string.Empty;
            try
            {
                procDb.Atualizar(strQueryIns, parQueryIns);
                retProc = "Registro alterado";
            }
            catch (Exception ex)
            {
                retProc = "Erro na atualização do registro";
                throw new DbInicProcException(message: "Erro no método EditarProfessor", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Excluir registro de professor
        internal string ExcluirProfessor(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Delete From CadProfessor ";
            strQueryIns += "Where IdCadProfessor = @IdCadProfessor";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadProfessor", id}
            };

            //Executando            
            string retProc = string.Empty;
            try
            {
                procDb.Excluir(strQueryIns, parQueryIns);
                retProc = "Registro excluído";
            }
            catch (Exception ex)
            {
                retProc = "Erro na exclusão do registro";
                throw new DbInicProcException(message: "Erro no método ExcluirProfessor", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }
    }
}