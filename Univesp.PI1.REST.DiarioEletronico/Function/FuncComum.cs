using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Univesp.PI1.REST.DiarioEletronico.Function
{
    public class FuncComum
    {
        //Conversão de Data - Db para Rest
        internal string DbToRest(string DtConv)
        {
            DateTime dtTemp = DateTime.Parse(DtConv);

            return dtTemp.ToString("dd/MM/yyyy");
        }

        //Conversão de Data - Rest para Db
        internal string RestToDb(string DtConv)
        {
            DateTime dtTemp = DateTime.Parse(DtConv);

            return dtTemp.ToString("yyyy-MM-dd");
        }
    }
}