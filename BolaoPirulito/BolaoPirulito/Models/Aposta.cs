using System;

namespace BolaoPirulito.Models
{
    public class Aposta
    {
        public Guid Id { get; set; }
        public Apostador Apostador { get; set; }
        public Rodada Rodada { get; set; }
    }
}