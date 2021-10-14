using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Univesp.PI1.Database.Model;

namespace Univesp.PI1.Database
{
    public class ProcessoDb
    {
        private readonly Contexto contexto;

        //Instancia base
        public ProcessoDb()
        {
            contexto = new Contexto();
        }

        //Inserir informações - insert
        public int Inserir(string strQuery, Dictionary<string, object> parQuery)
        {
            //Executando comando
            return contexto.ExecutaComando(strQuery, parQuery);
        }

        //Inserir informações - insert + retorno do LAST_ID
        public int InserirRetorno(string strQuery, Dictionary<string, object> parQuery)
        {
            //Específico para obter último ID (AI)
            var rows = contexto.ExecutaComandoComRetorno(strQuery, parQuery);
            int ultId = 0;
            foreach (var row in rows)
            {
                ultId = int.Parse(!string.IsNullOrEmpty(row["LAST_INSERT_ID()"]) ? row["LAST_INSERT_ID()"] : "0");
            }

            //Retorno
            return ultId;
        }

        //Atuaizar informações - update
        public int Atualizar(string strQuery, Dictionary<string, object> parQuery)
        {
            //Executando comando
            return contexto.ExecutaComando(strQuery, parQuery);
        }

        //Obtendo último id criado por auto-incremment
        public int SelecionarUltId(string strQuery)
        {
            //Criando parâmetros vazios
            var parQuery = new Dictionary<string, object>
            {

            };

            //Específico para obter último ID (AI)
            var rows = contexto.ExecutaComandoComRetorno(strQuery, parQuery);
            int ultId = 0;
            foreach (var row in rows)
            {
                ultId = int.Parse(!string.IsNullOrEmpty(row["LAST_INSERT_ID()"]) ? row["LAST_INSERT_ID()"] : "0");
            }

            //Retorno
            return ultId;
        }

        //Selecionar informações e retornar lista
        public List<TClass> SelecionarLista<TClass>(TClass classRef, List<InfoDePara> linfoConv, string strQuery,
                                                    Dictionary<string, object> parQuery)
        {
            //Executando comando
            var rows = contexto.ExecutaComandoComRetorno(strQuery, parQuery);

            //Obtendo conteúdo
            List<TClass> listClass = new List<TClass>();

            foreach (var row in rows)
            {
                //listClass.Add(ConvDbClass<TClass>(row));

                listClass.Add(classRef);
            }

            //Retorno
            return listClass;
        }

        //Selecionar informações e retornar lista
        public List<Dictionary<string, string>> SelecionarLista(string strQuery, Dictionary<string, object> parQuery)
        {
            //Executando comando
            return contexto.ExecutaComandoComRetorno(strQuery, parQuery);
        }

        //private TClass ConvDbClass<TClass>(Dictionary<string, string> row)
        //{
        //    TClass teste = new TClass();
        //    foreach (InfoDePara infoConv in linfoConv)
        //    {
        //        classRef.GetType().GetProperty(infoConv.cmpPara).SetValue(classRef, row[infoConv.cmpDe]);
        //    }
        //}

        //Criando listagem de De x Para (Banco x Classe)
        public InfoDePara ConvDePara(string cmpDe, string cmpPara)
        {
            //Criando item
            InfoDePara infoConv = new InfoDePara
            {
                CmpDe = cmpDe,
                CmpPara = cmpPara
            };

            //Retorno
            return infoConv;
        }

        //public Pessoa ListarPorId(int id)
        //{
        //    var pessoas = new List<Pessoa>();
        //    const string strQuery = "SELECT Id, Nome FROM Pessoa WHERE Id = @Id";
        //    var parametros = new Dictionary<string, object>
        //    {
        //        {"Id", id}
        //    };
        //    var rows = contexto.ExecutaComandoComRetorno(strQuery, parametros);
        //    foreach (var row in rows)
        //    {
        //        var tempPessoa = new Pessoa
        //        {
        //            Id = int.Parse(!string.IsNullOrEmpty(row["Id"]) ? row["Id"] : "0"),
        //            Nome = row["Nome"]
        //        };
        //        pessoas.Add(tempPessoa);
        //    }

        //    return pessoas.FirstOrDefault();
        //}

        //public List<EnviarArquivo> ListarTodos()
        //{
        //    var pessoas = new List<Pessoa>();
        //    const string strQuery = "SELECT Id, Nome FROM Pessoa";

        //    var rows = contexto.ExecutaComandoComRetorno(strQuery);
        //    foreach (var row in rows)
        //    {
        //        var tempPessoa = new Pessoa
        //        {
        //            Id = int.Parse(!string.IsNullOrEmpty(row["Id"]) ? row["Id"] : "0"),
        //            Nome = row["Nome"]
        //        };
        //        pessoas.Add(tempPessoa);
        //    }

        //    return pessoas;
        //}

        //private int Inserir(string strQuery)
        //{
        //    const string commandText = " INSERT INTO Pessoa (Nome) VALUES (@Nome) ";

        //    var parameters = new Dictionary<string, object>
        //    {
        //        {"Nome", pessoa.Nome}
        //    };

        //    return contexto.ExecutaComando(commandText, parameters);
        //}

        //private int Alterar(Pessoa pessoa)
        //{
        //    var commandText = " UPDATE Pessoa SET ";
        //    commandText += " Nome = @Nome ";
        //    commandText += " WHERE Id = @Id ";

        //    var parameters = new Dictionary<string, object>
        //    {
        //        {"Id", pessoa.Id},
        //        {"Nome", pessoa.Nome}
        //    };

        //    return contexto.ExecutaComando(commandText, parameters);
        //}

        //public void Salvar(Pessoa pessoa)
        //{
        //    if (pessoa.Id > 0)
        //        Alterar(pessoa);
        //    else
        //        Inserir(pessoa);
        //}

        //public int Excluir(int id)
        //{
        //    const string strQuery = "DELETE FROM Pessoa WHERE Id = @Id";
        //    var parametros = new Dictionary<string, object>
        //    {
        //        {"Id", id}
        //    };

        //    return contexto.ExecutaComando(strQuery, parametros);
        //}

        //public Pessoa ListarPorId(int id)
        //{
        //    var pessoas = new List<Pessoa>();
        //    const string strQuery = "SELECT Id, Nome FROM Pessoa WHERE Id = @Id";
        //    var parametros = new Dictionary<string, object>
        //    {
        //        {"Id", id}
        //    };
        //    var rows = contexto.ExecutaComandoComRetorno(strQuery, parametros);
        //    foreach (var row in rows)
        //    {
        //        var tempPessoa = new Pessoa
        //        {
        //            Id = int.Parse(!string.IsNullOrEmpty(row["Id"]) ? row["Id"] : "0"),
        //            Nome = row["Nome"]
        //        };
        //        pessoas.Add(tempPessoa);
        //    }

        //    return pessoas.FirstOrDefault();
        //}
    }
}
