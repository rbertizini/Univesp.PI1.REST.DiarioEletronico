using System;
using System.Collections.Generic;
using Univesp.PI1.Database;
using Univesp.PI1.REST.DiarioEletronico.Function;
using Univesp.PI1.REST.DiarioEletronico.Models;
using static Univesp.PI1.REST.DiarioEletronico.Except.ExceptionDb;

namespace Univesp.PI1.REST.DiarioEletronico.Data
{
    public class BimestreData
    {
        //Função comum
        FuncComum funcComum = new FuncComum();
        ProcessoDb procDb = new ProcessoDb();

        //Método retorna a tabela ResBimestre buscando pelo semestre e id
        internal IEnumerable<Bimestre> GetTurmaBimestre(int bimestre, int id)
        {
            //Criando instrução

            string strQueryIns = string.Empty;
            strQueryIns += "Select * ";
            strQueryIns += "From ResBimestre ";
            strQueryIns += "Where IdResBimestre = @id AND IdentBimestre = @bimestre";
            strQueryIns += "Order by DataInicio";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@id", id},
                {"@bimestre", bimestre }
            };

            //Executando            
            List<Bimestre> Bimestre = new List<Bimestre>();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns, parQueryIns);

                foreach (var row in listRows)
                {
                    Bimestre bim = new Bimestre();
                    bim.IdResBimestre = int.Parse(row["IdResBimestre"]);
                    bim.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    bim.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    bim.DataFechamento = Convert.ToDateTime(row["DataFechamento"]);
                    bim.DataInicio = Convert.ToDateTime(row["DataInicio"]);
                    bim.DataFim = Convert.ToDateTime(row["DataFim"]);
                    bim.QtdAusencia = int.Parse(row["QtdAusencia"]);
                    bim.QtdPresenca = int.Parse(row["QtdPresenca"]);
                    bim.NotaMedia = Decimal.Parse(row["NotaMedia"]);
                    bim.IdentBimestre = int.Parse(row["IdentBimestre"]);
                    Bimestre.Add(bim);
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaAvalTurma", innerException: ex.InnerException);
            }

            //Retorno 
            return Bimestre;
        }

        //Exclui os dados da tabela ResBimestre pelo id da turma e bimestre
        internal string DeleteTurmaBimestre(int bimestre, int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Delete From ResBimestre ";
            strQueryIns += "Where IdResBimestre = @id AND IdentBimestre = @bimestre";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                { "@id", id },
                { "@bimestre", bimestre }
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