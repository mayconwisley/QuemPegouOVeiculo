using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objeto.Relatorios
{
    public class ControleAbastecimentoObjRel
    {
        public int Id { get; set; }
        public int Id_Veiculo { get; set; }
        public string Modelo { get; set; }
        public int Id_Motorista { get; set; }
        public string Nome { get; set; }
        public string KmInicial { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }
}
