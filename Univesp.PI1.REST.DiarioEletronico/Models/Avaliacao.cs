using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Univesp.PI1.REST.DiarioEletronico.Models
{
    public class Avaliacao
    {
        public int IdMovAvaliacao { get; set; }
        public int IdCadTurma { get; set; }
        public int IdCadAluno { get; set; }
        public string Data { get; set; }
        public decimal Nota { get; set; }
    }
}