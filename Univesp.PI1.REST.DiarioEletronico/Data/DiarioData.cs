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
    public class DiarioData
    {
        //Função comum
        FuncComum funcComum = new FuncComum();

        //Obter registro de Diario por Turma
        internal List<Diario> ObterListaDiarioTurma(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdMovDiario, IdCadTurma, IdCadAluno, ";
            strQueryIns += "Data, Presenca ";
            strQueryIns += "From MovDiario as dir ";
            strQueryIns += "Where IdCadTurma = @id ";
            strQueryIns += "Order by IdCadTurma, Data, IdCadAluno ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@id", id}
            };

            //Executando            
            List<Diario> Diarios = new List<Diario>();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns, parQueryIns);

                foreach (var row in listRows)
                {
                    Diario _diario = new Diario();
                    _diario.IdMovDiario = int.Parse(row["IdMovDiario"]);
                    _diario.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    _diario.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    _diario.Data = funcComum.DbToRest(row["Data"]);
                    _diario.Presenca = row["Presenca"];
                    Diarios.Add(_diario);
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaDiarioTurma", innerException: ex.InnerException);
            }

            //Retorno 
            return Diarios;
        }

        //Adicionar registro de Diário
        internal string AdicionarDiario(Diario diarioIns)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Insert Into MovDiario ";
            strQueryIns += "(IdCadTurma, IdCadAluno, Data, Presenca) ";
            strQueryIns += "values ";
            strQueryIns += "(@IdCadTurma, @IdCadAluno, @Data, @Presenca) ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadTurma", diarioIns.IdCadTurma},
                {"@IdCadAluno", diarioIns.IdCadAluno},
                {"@Data", funcComum.RestToDb(diarioIns.Data)},
                {"@Presenca", diarioIns.Presenca}
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
                throw new DbInicProcException(message: "Erro no método AdicionarDiario", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Excluir registro de Diário
        internal string ExcluirDiario(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Delete From MovDiario ";
            strQueryIns += "Where IdMovDiario = @IdMovDiario";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdMovDiario", id}
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
                throw new DbInicProcException(message: "Erro no método ExcluirDiario", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }
    }
}