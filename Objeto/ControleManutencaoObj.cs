using System;

namespace Objeto
{
    public class ControleManutencaoObj
    {
        public int Id { get; set; }
        public VeiculoObj Veiculo { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }

    }
}
