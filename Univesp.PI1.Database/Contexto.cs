using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Univesp.PI1.Config;

namespace Univesp.PI1.Database
{
    public class Contexto : IDisposable
    {

        private MySqlConnection conexao;

        public Contexto()
        {
            string conexaoString = MontarConexao();
            conexao = new MySqlConnection(conexaoString);
        }

        private string MontarConexao()
        {
            //Obtendo informação do XML de configuração
            ParamSolucao paramSol = new ParamSolucao();
            string retorno = "Server=" + paramSol.ObterConfig("servidor") + ";";
            retorno += "Database=" + paramSol.ObterConfig("banco") + ";";
            retorno += "Uid=" + paramSol.ObterConfig("usuario") + ";";
            retorno += "Pwd=" + paramSol.ObterConfig("senha") + ";";

            return retorno;
        }

        public int ExecutaComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            var resultado = 0;
            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comandoSQL não pode ser nulo ou vazio");
            }
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);
                resultado = cmdComando.ExecuteNonQuery();
            }
            finally
            {
                FecharConexao();
            }

            return resultado;
        }

        public List<Dictionary<string, string>> ExecutaComandoComRetorno(string comandoSQL, Dictionary<string, object> parametros = null)
        {
            List<Dictionary<string, string>> linhas = null;

            if (string.IsNullOrEmpty(comandoSQL))
            {
                throw new ArgumentException("O comandoSQL não pode ser nulo ou vazio");
            }
            try
            {
                AbrirConexao();
                var cmdComando = CriarComando(comandoSQL, parametros);
                using (var reader = cmdComando.ExecuteReader())
                {
                    linhas = new List<Dictionary<string, string>>();
                    while (reader.Read())
                    {
                        var linha = new Dictionary<string, string>();

                        for (var i = 0; i < reader.FieldCount; i++)
                        {
                            var nomeDaColuna = reader.GetName(i);
                            var valorDaColuna = reader.IsDBNull(i) ? null : reader.GetString(i);
                            linha.Add(nomeDaColuna, valorDaColuna);
                        }
                        linhas.Add(linha);
                    }
                }
            }
            finally
            {
                FecharConexao();
            }

            return linhas;
        }

        private MySqlCommand CriarComando(string comandoSQL, Dictionary<string, object> parametros)
        {
            var cmdComando = conexao.CreateCommand();
            cmdComando.CommandText = comandoSQL;
            AdicionarParamatros(cmdComando, parametros);
            return cmdComando;
        }

        private static void AdicionarParamatros(MySqlCommand cmdComando, Dictionary<string, object> parametros)
        {
            if (parametros == null)
                return;

            foreach (var item in parametros)
            {
                var parametro = cmdComando.CreateParameter();
                parametro.ParameterName = item.Key;
                parametro.Value = item.Value ?? DBNull.Value;
                cmdComando.Parameters.Add(parametro);
            }
        }

        private void AbrirConexao()
        {
            if (conexao.State == ConnectionState.Open)
                return;

            conexao.Open();
        }

        private void FecharConexao()
        {
            if (conexao.State == ConnectionState.Open)
            {
                conexao.Close();
                //Dispose();
            }
        }

        public void Dispose()
        {
            if (conexao == null)
                return;

            conexao.Dispose();
            conexao = null;
        }
    }
}
