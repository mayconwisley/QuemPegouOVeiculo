using System;

namespace Objeto
{
    public class ControleMultaObj
    {
        public int Id { get; set; }
        public VeiculoObj Veiculo { get; set; }
        public MotoristaObj Motorista { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public int Pontos { get; set; }
        public string Descricao { get; set; }

    }
}
