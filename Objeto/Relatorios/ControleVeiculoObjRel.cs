using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Objeto.Relatorios
{
    public class ControleVeiculoObjRel
    {
        public int Id { get; set; }
        public int Id_Veiculo { get; set; }
        public int Id_Motorista { get; set; }
        public string Modelo { get; set; }
        public string Nome { get; set; }
        public DateTime DataHoraSaida { get; set; }
        public DateTime DataHoraChegada { get; set; }
        public int Dias { get; set; }
        public DateTime Horas { get; set; }
        public string Descricao { get; set; }
        public string KmInicial { get; set; }
        public string KmFinal { get; set; }
        public string KmTotal { get; set; }
        public string Status { get; set; }
    }
}
