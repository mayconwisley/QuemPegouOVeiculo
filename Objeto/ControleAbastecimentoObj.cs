using System;

namespace Objeto
{
    public class ControleAbastecimentoObj
    {
        public int Id { get; set; }
        public VeiculoObj Veiculo { get; set; }
        public MotoristaObj Motorista { get; set; }
        public string KmInicio { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal Litros { get; set; }
        public string Descricao { get; set; }

    }
}
