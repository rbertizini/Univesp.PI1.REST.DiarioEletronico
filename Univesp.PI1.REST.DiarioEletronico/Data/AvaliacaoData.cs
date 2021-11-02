using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Univesp.PI1.Database;
using Univesp.PI1.REST.DiarioEletronico.Function;
using Univesp.PI1.REST.DiarioEletronico.Models;
using static Univesp.PI1.REST.DiarioEletronico.Except.ExceptionDb;

namespace Univesp.PI1.REST.DiarioEletronico.Data
{
    public class AvaliacaoData
    {
        //Função comum
        FuncComum funcComum = new FuncComum();

        //Obter registro de Avaliação por Turma
        internal List<Avaliacao> ObterListaAvalTurma(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdMovAvaliacao, IdCadTurma, IdCadAluno, ";
            strQueryIns += "Data, Nota ";
            strQueryIns += "From MovAvaliacao as aval ";
            strQueryIns += "Where IdCadTurma = @id ";
            strQueryIns += "Order by IdCadTurma, Data, IdCadAluno ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@id", id}
            };

            //Executando            
            List<Avaliacao> Avaliacaoes = new List<Avaliacao>();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns, parQueryIns);

                foreach (var row in listRows)
                {
                    Avaliacao _aval = new Avaliacao();
                    _aval.IdMovAvaliacao = int.Parse(row["IdMovAvaliacao"]);
                    _aval.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    _aval.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    _aval.Data = funcComum.DbToRest(row["Data"]);
                    _aval.Nota = Decimal.Parse(row["Nota"]);
                    Avaliacaoes.Add(_aval);
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaAvalTurma", innerException: ex.InnerException);
            }

            //Retorno 
            return Avaliacaoes;
        }

        //Adicionar registro de Avaliação
        internal string AdicionarAval(Avaliacao avalIns)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Insert Into MovAvaliacao ";
            strQueryIns += "(IdCadTurma, IdCadAluno, Data, Nota) ";
            strQueryIns += "values ";
            strQueryIns += "(@IdCadTurma, @IdCadAluno, @Data, @Nota) ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadTurma", avalIns.IdCadTurma},
                {"@IdCadAluno", avalIns.IdCadAluno},
                {"@Data", funcComum.RestToDb(avalIns.Data)},
                {"@Nota", avalIns.Nota}
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
                throw new DbInicProcException(message: "Erro no método AdicionarAval", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Excluir registro de Avaliação
        internal string ExcluirAval(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Delete From MovAvaliacao ";
            strQueryIns += "Where IdMovAvaliacao = @IdMovAvaliacao";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdMovAvaliacao", id}
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
                throw new DbInicProcException(message: "Erro no método ExcluirAval", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }
    }
}