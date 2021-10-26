using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Univesp.PI1.Database;
using Univesp.PI1.REST.DiarioEletronico.Models;
using static Univesp.PI1.REST.DiarioEletronico.Except.ExceptionDb;

namespace Univesp.PI1.REST.DiarioEletronico.Data
{
    public class AlunoData
    {
        //Obter lista de Alunoes
        internal List<Aluno> ObterListaAluno()
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdCadAluno, NomeAluno ";
            strQueryIns += "From CadAluno as alu ";

            //Executando            
            List<Aluno> Alunos = new List<Aluno>();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns);

                foreach (var row in listRows)
                {
                    Aluno _aluno = new Aluno();
                    _aluno.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    _aluno.NomeAluno = row["NomeAluno"];
                    Alunos.Add(_aluno);
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return Alunos;
        }

        //Obter registro de Aluno
        internal Aluno ObterAluno(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdCadAluno, NomeAluno ";
            strQueryIns += "From CadAluno as alu ";
            strQueryIns += "Where IdCadAluno = @id";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@id", id}
            };

            //Executando            
            Aluno Aluno = new Aluno();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns, parQueryIns);

                foreach (var row in listRows)
                {
                    Aluno _aluno = new Aluno();
                    _aluno.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    _aluno.NomeAluno = row["NomeAluno"];
                    Aluno = _aluno;
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return Aluno;
        }

        //Adicionar registro de Aluno
        internal string AdicionarAluno(Aluno alunoIns)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Insert Into CadAluno ";
            strQueryIns += "(NomeAluno) ";
            strQueryIns += "values ";
            strQueryIns += "(@NomeAluno) ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@NomeAluno", alunoIns.NomeAluno}
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
                throw new DbInicProcException(message: "Erro no método AdicionarAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Alterar registro de Aluno
        internal string EditarAluno(int id, Aluno alunoEdt)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Update CadAluno ";
            strQueryIns += "Set NomeAluno = @NomeAluno ";
            strQueryIns += "Where IdCadAluno = @IdCadAluno ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@NomeAluno", alunoEdt.NomeAluno},
                {"@IdCadAluno", id}
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
                throw new DbInicProcException(message: "Erro no método EditarAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Excluir registro de Aluno
        internal string ExcluirAluno(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Delete From CadAluno ";
            strQueryIns += "Where IdCadAluno = @IdCadAluno";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadAluno", id}
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
                throw new DbInicProcException(message: "Erro no método ExcluirAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }
    }
}