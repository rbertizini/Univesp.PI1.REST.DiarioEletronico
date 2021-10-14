using System;
using System.IO;
using System.Xml;
using static Univesp.PI1.Config.ExcecaoConfig;

namespace Univesp.PI1.Config
{
    public class ParamSolucao
    {
        //Obtendo configurações no XML
        public string ObterConfig(string nomeConfig)
        {
            //Variáveis loais
            string infoConfig = string.Empty;

            //Carregando XML
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\config.xml")))
            {
                //Obtendo parametrização
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\config.xml"));
                XmlNodeList xmlElem = xmlDoc.GetElementsByTagName(nomeConfig);
                if (xmlElem.Count > 0)
                {
                    infoConfig = xmlElem[0].InnerXml;
                }
                else
                {
                    //throw new ParamNaoLocalizadoException(message: "Parametrização não localizada");
                    infoConfig = "";
                }
            }
            else
            {
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml")))
                {
                    //Obtendo parametrização
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml"));
                    XmlNodeList xmlElem = xmlDoc.GetElementsByTagName(nomeConfig);
                    if (xmlElem.Count > 0)
                    {
                        infoConfig = xmlElem[0].InnerXml;
                    }
                    else
                    {
                        //throw new ParamNaoLocalizadoException(message: "Parametrização não localizada");
                        infoConfig = "";
                    }
                }
                else
                {
                    throw new ParamNaoLocalizadoException(message: "Configuração da aplicação não localizada");
                }
            }

            //Rerotno
            return infoConfig;
        }

        //Obtendo configurações no XML
        public string ObterAtrib(string nomeConfig, string nomeAtrib)
        {
            //Variáveis loais
            string infoConfig = string.Empty;

            //Carregando XML
            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\config.xml")))
            {
                //Obtendo parametrização
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\config.xml"));
                XmlNodeList xmlElem = xmlDoc.GetElementsByTagName(nomeConfig);
                if (xmlElem.Count > 0)
                {
                    infoConfig = xmlElem[0].Attributes[nomeAtrib].Value;
                }
                else
                {
                    //throw new ParamNaoLocalizadoException(message: "Parametrização não localizada:" + nomeConfig + "-" + nomeAtrib);
                    infoConfig = "";
                }
            }
            else
            {
                if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml")))
                {
                    //Obtendo parametrização
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml"));
                    XmlNodeList xmlElem = xmlDoc.GetElementsByTagName(nomeConfig);
                    if (xmlElem.Count > 0)
                    {
                        infoConfig = xmlElem[0].Attributes[nomeAtrib].Value;
                    }
                    else
                    {
                        //throw new ParamNaoLocalizadoException(message: "Parametrização não localizada:" + nomeConfig + "-" + nomeAtrib);
                        infoConfig = "";
                    }
                }
                else
                {
                    throw new ParamNaoLocalizadoException(message: "Configuração da aplicação não localizada");
                }
            }

            //Rerotno
            return infoConfig;
        }
    }
}
