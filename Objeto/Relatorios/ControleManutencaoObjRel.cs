using System;

namespace Objeto.Relatorios
{
    public class ControleManutencaoObjRel
    {
        public int Id { get; set; }
        public int Id_Veiculo { get; set; }
        public string Modelo { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
    }
}
