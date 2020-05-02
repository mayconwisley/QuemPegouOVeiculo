using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objeto
{
    public class MotoristaObj
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CNH { get; set; }
        public DateTime VencimentoCNH { get; set; }
        public string CategoriaCNH { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public char Ativo { get; set; }

    }
}
