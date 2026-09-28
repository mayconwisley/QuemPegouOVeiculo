using System;

namespace QuemPegouOVeiculo.Desktop.Models
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
