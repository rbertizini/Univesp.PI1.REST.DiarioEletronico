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

        //Atualizar informações - update
        public int Atualizar(string strQuery, Dictionary<string, object> parQuery)
        {
            //Executando comando
            return contexto.ExecutaComando(strQuery, parQuery);
        }

        //Excluir informações - delete
        public int Excluir(string strQuery, Dictionary<string, object> parQuery)
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
        public List<Dictionary<string, string>> SelecionarLista(string strQuery, Dictionary<string, object> parQuery = null)
        {
            //Executando comando
            return contexto.ExecutaComandoComRetorno(strQuery, parQuery);
        }

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
    }
}
