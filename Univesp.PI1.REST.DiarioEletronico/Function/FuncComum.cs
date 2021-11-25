using System;

namespace Univesp.PI1.REST.DiarioEletronico.Function
{
    public class FuncComum
    {
        //Conversão de Data - Db para Rest
        internal string DtDbToRest(string DtConv)
        {
            DateTime dtTemp = DateTime.Parse(DtConv);

            return dtTemp.ToString("dd/MM/yyyy");
        }

        //Conversão de Data - Rest para Db
        internal string DtRestToDb(string DtConv)
        {
            DateTime dtTemp = DateTime.Parse(DtConv);

            return dtTemp.ToString("yyyy-MM-dd");
        }

        //Conversão de Data - string para c
        internal DateTime DtDbToC(string DtConv)
        {
            DateTime dtTemp = DateTime.Parse(DtConv);

            return dtTemp;
        }

        //Cálculo de dias úteis
        internal int GetDifDias(DateTime initialDate, DateTime finalDate)
        {
            var days = 0;
            var daysCount = 0;
            days = initialDate.Subtract(finalDate).Days;

            if (days < 0)
                days = days * -1;

            for (int i = 1; i <= days; i++)
            {
                initialDate = initialDate.AddDays(1);

                if (initialDate.DayOfWeek != DayOfWeek.Sunday &&
                    initialDate.DayOfWeek != DayOfWeek.Saturday)
                    daysCount++;
            }
            return daysCount;
        }

        //
    }
}