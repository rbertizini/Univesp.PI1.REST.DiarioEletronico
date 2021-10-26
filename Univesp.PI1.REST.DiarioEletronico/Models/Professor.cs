using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Univesp.PI1.REST.DiarioEletronico.Models
{
    public class Professor
    {
        public int IdCadProfessor { get; set; }
        public string NomeProfessor { get; set; }
        public string Disciplina { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}