using System;
using System.Collections.Generic;
using Univesp.PI1.Database;
using Univesp.PI1.REST.DiarioEletronico.Models;
using static Univesp.PI1.REST.DiarioEletronico.Except.ExceptionDb;

namespace Univesp.PI1.REST.DiarioEletronico.Data
{
    public class TurmaAlunoData
    {
        //Obter lista de TurmaAluno
        internal List<TurmaAluno> ObterListaTurmaAluno()
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdMovTurma, IdCadTurma, IdCadAluno ";
            strQueryIns += "From CadTurmaAluno as turAl ";
            strQueryIns += "Order by IdCadTurma, IdCadAluno ";

            //Executando            
            List<TurmaAluno> TurmaAlunos = new List<TurmaAluno>();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns);

                foreach (var row in listRows)
                {
                    TurmaAluno _turmaAluno = new TurmaAluno();
                    _turmaAluno.IdMovTurma = int.Parse(row["IdMovTurma"]);
                    _turmaAluno.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    _turmaAluno.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    TurmaAlunos.Add(_turmaAluno);
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaTurmaAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return TurmaAlunos;
        }

        //Obter registro de TurmaAluno
        internal TurmaAluno ObterTurmaAluno(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdMovTurma, IdCadTurma, IdCadAluno ";
            strQueryIns += "From CadTurmaAluno as turAl ";
            strQueryIns += "Where IdMovTurma = @id";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@id", id}
            };

            //Executando            
            TurmaAluno TurmaAluno = new TurmaAluno();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns, parQueryIns);

                foreach (var row in listRows)
                {
                    TurmaAluno _turmaAluno = new TurmaAluno();
                    _turmaAluno.IdMovTurma = int.Parse(row["IdMovTurma"]);
                    _turmaAluno.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    _turmaAluno.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    TurmaAluno = _turmaAluno;
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterTurmaAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return TurmaAluno;
        }

        //Adicionar registro de TurmaAluno
        internal string AdicionarTurmaAluno(TurmaAluno turmaAlunoIns)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Insert Into CadTurmaAluno ";
            strQueryIns += "(IdCadTurma, IdCadAluno) ";
            strQueryIns += "values ";
            strQueryIns += "(@IdCadTurma, @IdCadAluno) ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadTurma", turmaAlunoIns.IdCadTurma},
                {"@IdCadAluno", turmaAlunoIns.IdCadAluno}
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
                throw new DbInicProcException(message: "Erro no método AdicionarTurmaAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Alterar registro de TurmaAluno
        internal string EditarTurmaAluno(int id, TurmaAluno turmaAlunoEdt)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Update CadTurmaAluno ";
            strQueryIns += "Set IdCadTurma = @IdCadTurma, ";
            strQueryIns += "IdCadAluno = @IdCadAluno ";
            strQueryIns += "Where IdMovTurma = @IdMovTurma";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadTurma", turmaAlunoEdt.IdCadTurma},
                {"@IdCadAluno", turmaAlunoEdt.IdCadAluno}
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
                throw new DbInicProcException(message: "Erro no método EditarTurmaAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Excluir registro de TurmaAluno
        internal string ExcluirTurmaAluno(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Delete From CadTurmaAluno ";
            strQueryIns += "Where IdMovTurma = @IdMovTurma";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdMovTurma", id}
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
                throw new DbInicProcException(message: "Erro no método ExcluirTurmaAluno", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }
    }
}