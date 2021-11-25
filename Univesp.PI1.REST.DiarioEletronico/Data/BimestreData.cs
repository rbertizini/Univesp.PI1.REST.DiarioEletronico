using System;
using System.Collections.Generic;
using System.Linq;
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

        //Método retorna a tabela ResBimestre buscando pelo semestre e id
        internal List<Bimestre> GetTurmaBimestre(int bimestre, int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select * ";
            strQueryIns += "From ResBimestre ";
            strQueryIns += "Where IdCadTurma = @id AND IdentBimestre = @bimestre ";
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
                throw new DbInicProcException(message: "Erro no método GetTurmaBimestre", innerException: ex.InnerException);
            }

            //Retorno 
            return Bimestre;
        }

        internal string Save(int identBim, int id)
        {
            List<Avaliacao> _Avaliacao = new List<Avaliacao>();
            List<Diario> _Diario = new List<Diario>();
            Turma _Turma = new Turma();
            List<TurmaAluno> _TurmaAluno = new List<TurmaAluno>();

            List<Bimestre> _Bimestre = new List<Bimestre>();

            try
            {
                _Diario = GetDiario(id);
                _Avaliacao = GetAvaliacao(id);
                _Turma = GetTurma(id);
                _TurmaAluno = GetTurmaAluno(id);

                //Verificar quantidade de presenças x dias previstos
                int qtdDias = 0;
                DateTime dtInicio = DateTime.Now;
                DateTime dtFinal = DateTime.Now;
                if (identBim == 1)
                {
                    dtInicio = funcComum.DtDbToC(_Turma.B1Inicial);
                    dtFinal = funcComum.DtDbToC(_Turma.B1Final);
                }
                if (identBim == 2)
                {
                    dtInicio = funcComum.DtDbToC(_Turma.B2Inicial);
                    dtFinal = funcComum.DtDbToC(_Turma.B2Final);
                }
                if (identBim == 3)
                {
                    dtInicio = funcComum.DtDbToC(_Turma.B3Inicial);
                    dtFinal = funcComum.DtDbToC(_Turma.B3Final);
                }
                if (identBim == 4)
                {
                    dtInicio = funcComum.DtDbToC(_Turma.B4Inicial);
                    dtFinal = funcComum.DtDbToC(_Turma.B4Final);
                }
                qtdDias = funcComum.GetDifDias(dtInicio, dtFinal);

                //Organização de informações
                _TurmaAluno = _TurmaAluno.OrderBy(a => a.IdCadAluno).ToList(); ;
                _Diario = _Diario.OrderBy(a => a.IdCadAluno).ThenBy(a => a.Data).ToList();
                _Avaliacao = _Avaliacao.OrderBy(a => a.IdCadAluno).ThenBy(a => a.Data).ToList();

                //Loop de dados
                foreach (TurmaAluno turmaAluno in _TurmaAluno)
                {
                    //Bimestre
                    Bimestre bimestre = new Bimestre();

                    bimestre.IdCadTurma = turmaAluno.IdCadTurma;
                    bimestre.IdCadAluno = turmaAluno.IdCadAluno;
                    bimestre.DataFechamento = DateTime.Now;
                    bimestre.DataInicio = dtInicio;
                    bimestre.DataFim = dtFinal;
                    bimestre.IdentBimestre = identBim;

                    //Diário
                    bimestre.QtdPresenca = 0;
                    bimestre.QtdAusencia = 0;
                    foreach (Diario diario in _Diario)
                    {
                        if ((diario.IdCadAluno == turmaAluno.IdCadAluno) &&
                            ((funcComum.DtDbToC(diario.Data) >= bimestre.DataInicio) &&
                            (funcComum.DtDbToC(diario.Data) <= bimestre.DataFim)))
                        {
                            if (diario.Presenca == "P")
                                bimestre.QtdPresenca++;
                        }
                    }

                    if (bimestre.QtdPresenca > 0)
                        bimestre.QtdAusencia = qtdDias - bimestre.QtdPresenca;
                    else
                        bimestre.QtdAusencia = qtdDias;

                    //Avaliação
                    bimestre.NotaMedia = 0;
                    foreach (Avaliacao avaliacao in _Avaliacao)
                    {
                        if ((avaliacao.IdCadAluno == turmaAluno.IdCadAluno) &&
                            ((funcComum.DtDbToC(avaliacao.Data) >= bimestre.DataInicio) &&
                            (funcComum.DtDbToC(avaliacao.Data) <= bimestre.DataFim)))
                            bimestre.NotaMedia = bimestre.NotaMedia + avaliacao.Nota;
                    }

                    bimestre.NotaMedia = bimestre.NotaMedia / 2;

                    _Bimestre.Add(bimestre);
                }

                //Loop para adicionar registros
                string retProc = string.Empty;
                foreach (Bimestre bimSave in _Bimestre)
                {
                    //Criando instrução
                    ProcessoDb procDb = new ProcessoDb();
                    string strQueryIns = string.Empty;
                    strQueryIns += "Insert Into ResBimestre ";
                    strQueryIns += "(IdCadTurma, IdCadAluno, IdentBimestre, ";
                    strQueryIns += "DataFechamento, DataInicio, DataFim, ";
                    strQueryIns += "QtdAusencia, QtdPresenca, NotaMedia) ";
                    strQueryIns += "values ";
                    strQueryIns += "(@IdCadTurma, @IdCadAluno, @IdentBimestre, ";
                    strQueryIns += "@DataFechamento, @DataInicio, @DataFim, ";
                    strQueryIns += "@QtdAusencia, @QtdPresenca, @NotaMedia) ";

                    //Parâmetros
                    var parQueryIns = new Dictionary<string, object>
                    {
                        {"@IdCadTurma", bimSave.IdCadTurma},
                        {"@IdCadAluno", bimSave.IdCadAluno},
                        {"@IdentBimestre", bimSave.IdentBimestre},
                        {"@DataFechamento", funcComum.DtRestToDb(bimSave.DataFechamento.ToString())},
                        {"@DataInicio", funcComum.DtRestToDb(bimSave.DataInicio.ToString())},
                        {"@DataFim", funcComum.DtRestToDb(bimSave.DataFim.ToString())},
                        {"@QtdAusencia", bimSave.QtdAusencia},
                        {"@QtdPresenca", bimSave.QtdPresenca},
                        {"@NotaMedia", bimSave.NotaMedia}
                    };

                    //Executando            
                    try
                    {
                        procDb.Inserir(strQueryIns, parQueryIns);
                        retProc = "Registro incluído";
                    }
                    catch (Exception ex)
                    {
                        retProc = "Erro na inserção do registro";
                        throw new DbInicProcException(message: "Erro no método AdicionarTurma", innerException: ex.InnerException);
                    }
                }

                //Retorno 
                return retProc;
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método FinalizarBimestre", innerException: ex.InnerException);
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
            ProcessoDb procDb = new ProcessoDb();
            string strQueryInsDiario = string.Empty;
            strQueryInsDiario += "Select * ";
            strQueryInsDiario += "From MovDiario ";
            strQueryInsDiario += "Where IdCadTurma = @id ";
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
            ProcessoDb procDb = new ProcessoDb();
            string strQueryInsAvaliacao = string.Empty;
            strQueryInsAvaliacao += "Select * ";
            strQueryInsAvaliacao += "From MovAvaliacao ";
            strQueryInsAvaliacao += "Where IdCadTurma = @id ";
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

        private Turma GetTurma(int id)
        {
            ProcessoDb procDb = new ProcessoDb();
            string strQueryInsAvaliacao = string.Empty;
            strQueryInsAvaliacao += "Select * ";
            strQueryInsAvaliacao += "From CadTurma ";
            strQueryInsAvaliacao += "Where IdCadTurma = @id ";

            //Parâmetros
            var parQueryInsAvaliacao = new Dictionary<string, object>
            {
                {"@id", id},
            };

            //Executando            
            Turma Turma = new Turma();
            try
            {
                List<Dictionary<string, string>> listRowsTurma = new List<Dictionary<string, string>>();
                listRowsTurma = procDb.SelecionarLista(strQueryInsAvaliacao, parQueryInsAvaliacao);

                foreach (var row in listRowsTurma)
                {
                    Turma _turma = new Turma();
                    _turma.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    _turma.IdCadProfessor = int.Parse(row["IdCadProfessor"]);
                    _turma.NomeTurma = row["NomeTurma"];
                    _turma.B1Inicial = funcComum.DtDbToRest(row["B1Inicial"]);
                    _turma.B1Final = funcComum.DtDbToRest(row["B1Final"]);
                    _turma.B2Inicial = funcComum.DtDbToRest(row["B2Inicial"]);
                    _turma.B2Final = funcComum.DtDbToRest(row["B2Final"]);
                    _turma.B3Inicial = funcComum.DtDbToRest(row["B3Inicial"]);
                    _turma.B3Final = funcComum.DtDbToRest(row["B3Final"]);
                    _turma.B4Inicial = funcComum.DtDbToRest(row["B4Inicial"]);
                    _turma.B4Final = funcComum.DtDbToRest(row["B4Final"]);
                    Turma = _turma;
                }

                return Turma;
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método GetAvaliacao", innerException: ex.InnerException);
            }
        }

        private List<TurmaAluno> GetTurmaAluno(int id)
        {
            ProcessoDb procDb = new ProcessoDb();
            string strQueryInsAvaliacao = string.Empty;
            strQueryInsAvaliacao += "Select * ";
            strQueryInsAvaliacao += "From CadTurmaAluno ";
            strQueryInsAvaliacao += "Where IdCadTurma = @id ";

            //Parâmetros
            var parQueryInsAvaliacao = new Dictionary<string, object>
            {
                {"@id", id},
            };

            //Executando            
            List<TurmaAluno> TurmaAluno = new List<TurmaAluno>();
            try
            {
                List<Dictionary<string, string>> listRowsTurmaAluno = new List<Dictionary<string, string>>();
                listRowsTurmaAluno = procDb.SelecionarLista(strQueryInsAvaliacao, parQueryInsAvaliacao);

                foreach (var row in listRowsTurmaAluno)
                {
                    TurmaAluno _turmaAluno = new TurmaAluno();
                    _turmaAluno.IdMovTurma = int.Parse(row["IdMovTurma"]);
                    _turmaAluno.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    _turmaAluno.IdCadAluno = int.Parse(row["IdCadAluno"]);
                    TurmaAluno.Add(_turmaAluno);
                }

                return TurmaAluno;
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método GetAvaliacao", innerException: ex.InnerException);
            }
        }
    }
}