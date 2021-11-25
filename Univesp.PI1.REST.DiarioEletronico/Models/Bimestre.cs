using System;

namespace Univesp.PI1.REST.DiarioEletronico.Models
{
    public class Bimestre
    {
        public int IdResBimestre { get; set; }
        public int IdCadTurma { get; set; }
        public int IdCadAluno { get; set; }
        public DateTime DataFechamento { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int QtdAusencia { get; set; }
        public int QtdPresenca { get; set; }
        public decimal NotaMedia { get; set; }
        public int IdentBimestre { get; set; }
    }
}