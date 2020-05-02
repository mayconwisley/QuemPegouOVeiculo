using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Objeto
{
    public class StatusVeiculoObj
    {
        public int Id { get; set; }
        public VeiculoObj Veiculo { get; set; }

        public DateTime? DataHoraInicial { get; set; }

        public DateTime? DataHoraFinal { get; set; }

        public string Descricao { get; set; }

    }
}
