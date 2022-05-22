using System;

namespace Objeto
{
    public class ControleVeiculoObj
    {
        public int Id { get; set; }
        public VeiculoObj Veiculo { get; set; }
        public MotoristaObj Motorista { get; set; }
        public DateTime? DataHoraSaida { get; set; }
        public DateTime? DataHoraChegada { get; set; }
        public string Descricao { get; set; }
        public string KmInicial { get; set; }
        public string KmFinal { get; set; }
        public char Status { get; set; }



    }
}
