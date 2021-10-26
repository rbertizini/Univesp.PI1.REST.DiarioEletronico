using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Univesp.PI1.REST.DiarioEletronico.Models
{
    public class Turma
    {
        public int IdCadTurma { get; set; }
        public int IdCadProfessor { get; set; }
        public string NomeTurma { get; set; }
        public string B1Inicial { get; set; }
        public string B1Final { get; set; }
        public string B2Inicial { get; set; }
        public string B2Final { get; set; }
        public string B3Inicial { get; set; }
        public string B3Final { get; set; }
        public string B4Inicial { get; set; }
        public string B4Final { get; set; }
    }
}