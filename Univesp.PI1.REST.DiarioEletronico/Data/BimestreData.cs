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

        internal string Save(int bimestre, int id)
        {        
            List<Avaliacao> _Avaliacao = new List<Avaliacao>();
            List<Diario> _Diario = new List<Diario>();
            try
            {
                _Diario = GetDiario(id);
                _Avaliacao = GetAvaliacao(id);

                return "";
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaAvalTurma", innerException: ex.InnerException);
            }
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

        private List<Diario> GetDiario(int id)
        {
            string strQueryInsDiario = string.Empty;
            strQueryInsDiario += "Select * ";
            strQueryInsDiario += "From MovDiario ";
            strQueryInsDiario += "Where IdCadTurma = @id";
            strQueryInsDiario += "Order by Data";

            //Parâmetros
            var parQueryInsDiario = new Dictionary<string, object>
            {
                {"@id", id},
            };

            //Executando            
            List<Diario> _Diario = new List<Diario>();
            try
            {
                List<Dictionary<string, string>> listRowsDiario = new List<Dictionary<string, string>>();
                listRowsDiario = procDb.SelecionarLista(strQueryInsDiario, parQueryInsDiario);

                foreach (var row in listRowsDiario)
                {
                    Diario bim = new Diario();
                    bim.IdMovDiario = int.Parse(row["IdMovDiario"]);
                    bim.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    bim.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    bim.Data = row["Data"];
                    bim.Presenca = row["Presenca"];
                    _Diario.Add(bim);
                }

                return _Diario;
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaAvalTurma", innerException: ex.InnerException);
            }
        }

        private List<Avaliacao> GetAvaliacao(int id) 
        {
            string strQueryInsAvaliacao = string.Empty;
            strQueryInsAvaliacao += "Select * ";
            strQueryInsAvaliacao += "From MovAvaliacao ";
            strQueryInsAvaliacao += "Where IdCadTurma = @id";
            strQueryInsAvaliacao += "Order by Data";

            //Parâmetros
            var parQueryInsAvaliacao = new Dictionary<string, object>
            {
                {"@id", id},
            };

            //Executando            
            List<Avaliacao> _Avaliacao = new List<Avaliacao>();
            try
            {
                List<Dictionary<string, string>> listRowsAvaliacao = new List<Dictionary<string, string>>();
                listRowsAvaliacao = procDb.SelecionarLista(strQueryInsAvaliacao, parQueryInsAvaliacao);

                foreach (var row in listRowsAvaliacao)
                {
                    Avaliacao bim = new Avaliacao();
                    bim.IdMovAvaliacao = int.Parse(row["IdMovAvaliacao"]);
                    bim.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    bim.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    bim.Data = row["Data"];
                    _Avaliacao.Add(bim);
                }

                return _Avaliacao;
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método GetAvaliacao", innerException: ex.InnerException);
            }
        }
    }
}