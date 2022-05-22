using System;

namespace Objeto
{
    public class VencimentoCNHObj
    {
        public int Id { get; set; }
        public MotoristaObj Motorista { get; set; }
        public DateTime Data { get; set; }
        public char Status { get; set; }

    }
}
