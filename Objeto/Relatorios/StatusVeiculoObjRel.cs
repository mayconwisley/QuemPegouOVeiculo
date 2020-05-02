using System;

namespace Objeto.Relatorios
{
    public class StatusVeiculoObjRel
    {

        public int Id { get; set; }
        public int Id_Veiculo { get; set; }
        public string Modelo { get; set; }
        public DateTime? DataHoraInicio { get; set; }
        public DateTime? DataHoraFinal { get; set; }
        public string Descricao { get; set; }
    }
}
