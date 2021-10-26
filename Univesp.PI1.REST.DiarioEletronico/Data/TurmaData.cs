﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Univesp.PI1.Database;
using Univesp.PI1.REST.DiarioEletronico.Models;
using static Univesp.PI1.REST.DiarioEletronico.Except.ExceptionDb;

namespace Univesp.PI1.REST.DiarioEletronico.Data
{
    public class TurmaData
    {
        //Obter lista de Turma
        internal List<Turma> ObterListaTurma()
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdCadTurma, IdCadProfessor, NomeTurma, ";
            strQueryIns += "B1Inicial, B1Final, B2Inicial, B2Final, ";
            strQueryIns += "B3Inicial, B3Final, B4Inicial, B4Final ";
            strQueryIns += "From CadTurma as tur ";

            //Executando            
            List<Turma> Turmas = new List<Turma>();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns);

                foreach (var row in listRows)
                {
                    Turma _turma = new Turma();
                    _turma.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    _turma.IdCadProfessor = int.Parse(row["IdCadProfessor"]);
                    _turma.NomeTurma = row["NomeTurma"];
                    _turma.B1Inicial = DbToRest(row["B1Inicial"]);
                    _turma.B1Final = DbToRest(row["B1Final"]);
                    _turma.B2Inicial = DbToRest(row["B2Inicial"]);
                    _turma.B2Final = DbToRest(row["B2Final"]);
                    _turma.B3Inicial = DbToRest(row["B3Inicial"]);
                    _turma.B3Final = DbToRest(row["B3Final"]);
                    _turma.B4Inicial = DbToRest(row["B4Inicial"]);
                    _turma.B4Final = DbToRest(row["B4Final"]);
                    Turmas.Add(_turma);
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterListaTurma", innerException: ex.InnerException);
            }

            //Retorno 
            return Turmas;
        }

        //Obter registro de Turma
        internal Turma ObterTurma(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Select IdCadTurma, IdCadProfessor, NomeTurma, ";
            strQueryIns += "B1Inicial, B1Final, B2Inicial, B2Final, ";
            strQueryIns += "B3Inicial, B3Final, B4Inicial, B4Final ";
            strQueryIns += "From CadTurma as tur ";
            strQueryIns += "Where IdCadTurma = @id";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@id", id}
            };

            //Executando            
            Turma Turma = new Turma();
            try
            {
                List<Dictionary<string, string>> listRows = new List<Dictionary<string, string>>();
                listRows = procDb.SelecionarLista(strQueryIns, parQueryIns);

                foreach (var row in listRows)
                {
                    Turma _turma = new Turma();
                    _turma.IdCadTurma = int.Parse(row["IdCadTurma"]);
                    _turma.IdCadProfessor = int.Parse(row["IdCadProfessor"]);
                    _turma.NomeTurma = row["NomeTurma"];
                    _turma.B1Inicial = DbToRest(row["B1Inicial"]);
                    _turma.B1Final = DbToRest(row["B1Final"]);
                    _turma.B2Inicial = DbToRest(row["B2Inicial"]);
                    _turma.B2Final = DbToRest(row["B2Final"]);
                    _turma.B3Inicial = DbToRest(row["B3Inicial"]);
                    _turma.B3Final = DbToRest(row["B3Final"]);
                    _turma.B4Inicial = DbToRest(row["B4Inicial"]);
                    _turma.B4Final = DbToRest(row["B4Final"]);
                    Turma = _turma;
                }
            }
            catch (Exception ex)
            {
                throw new DbInicProcException(message: "Erro no método ObterTurma", innerException: ex.InnerException);
            }

            //Retorno 
            return Turma;
        }

        //Adicionar registro de Turma
        internal string AdicionarTurma(Turma turmaIns)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Insert Into CadTurma ";
            strQueryIns += "(IdCadProfessor, NomeTurma, B1Inicial, ";
            strQueryIns += "B1Final, B2Inicial, B2Final, B3Inicial, ";
            strQueryIns += "B3Final, B4Inicial, B4Final) ";
            strQueryIns += "values ";
            strQueryIns += "(@IdCadProfessor, @NomeTurma, @B1Inicial, ";
            strQueryIns += "@B1Final, @B2Inicial, @B2Final, @B3Inicial, ";
            strQueryIns += "@B3Final, @B4Inicial, @B4Final) ";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadProfessor", turmaIns.IdCadProfessor},
                {"@NomeTurma", turmaIns.NomeTurma},
                {"@B1Inicial", RestToDb(turmaIns.B1Inicial)},
                {"@B1Final", RestToDb(turmaIns.B1Final)},
                {"@B2Inicial", RestToDb(turmaIns.B2Inicial)},
                {"@B2Final", RestToDb(turmaIns.B2Final)},
                {"@B3Inicial", RestToDb(turmaIns.B3Inicial)},
                {"@B3Final", RestToDb(turmaIns.B3Final)},
                {"@B4Inicial", RestToDb(turmaIns.B4Inicial)},
                {"@B4Final", RestToDb(turmaIns.B4Final)}
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
                throw new DbInicProcException(message: "Erro no método AdicionarTurma", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Alterar registro de Turma
        internal string EditarTurma(int id, Turma turmaEdt)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Update CadTurma ";
            strQueryIns += "Set IdCadProfessor = @IdCadProfessor, ";
            strQueryIns += "NomeTurma = @NomeTurma, ";
            strQueryIns += "B1Inicial = @B1Inicial, ";
            strQueryIns += "B1Final = @B1Final, ";
            strQueryIns += "B2Inicial = @B2Inicial, ";
            strQueryIns += "B2Final = @B2Final, ";
            strQueryIns += "B3Inicial = @B3Inicial, ";
            strQueryIns += "B3Final = @B3Final, ";
            strQueryIns += "B4Inicial = @B4Inicial, ";
            strQueryIns += "B4Final = @B4Final ";
            strQueryIns += "Where IdCadTurma = @IdCadTurma";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadProfessor", turmaEdt.IdCadProfessor},
                {"@NomeTurma", turmaEdt.NomeTurma},
                {"@B1Inicial", RestToDb(turmaEdt.B1Inicial)},
                {"@B1Final", RestToDb(turmaEdt.B1Final)},
                {"@B2Inicial", RestToDb(turmaEdt.B2Inicial)},
                {"@B2Final", RestToDb(turmaEdt.B2Final)},
                {"@B3Inicial", RestToDb(turmaEdt.B3Inicial)},
                {"@B3Final", RestToDb(turmaEdt.B3Final)},
                {"@B4Inicial", RestToDb(turmaEdt.B4Inicial)},
                {"@B4Final", RestToDb(turmaEdt.B4Final)},
                {"@IdCadTurma", turmaEdt.IdCadTurma}
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
                throw new DbInicProcException(message: "Erro no método EditarTurma", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Excluir registro de Turma
        internal string ExcluirTurma(int id)
        {
            //Criando instrução
            ProcessoDb procDb = new ProcessoDb();
            string strQueryIns = string.Empty;
            strQueryIns += "Delete From CadTurma ";
            strQueryIns += "Where IdCadTurma = @IdCadTurma";

            //Parâmetros
            var parQueryIns = new Dictionary<string, object>
            {
                {"@IdCadTurma", id}
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
                throw new DbInicProcException(message: "Erro no método ExcluirTurma", innerException: ex.InnerException);
            }

            //Retorno 
            return retProc;
        }

        //Conversão de Data - Db para Rest
        private string DbToRest(string DtConv)
        {
            DateTime dtTemp = DateTime.Parse(DtConv);
            
            return dtTemp.ToString("dd/MM/yyyy");
        }

        //Conversão de Data - Rest para Db
        private string RestToDb(string DtConv)
        {
            DateTime dtTemp = DateTime.Parse(DtConv);

            return dtTemp.ToString("yyyy-MM-dd");
        }
    }
}