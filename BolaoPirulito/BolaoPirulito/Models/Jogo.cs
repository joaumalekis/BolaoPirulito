using System;
using System.ComponentModel;

namespace BolaoPirulito.Models
{
    public class Jogo 
    {
        public Guid Id { get; set; }
        public Time TimeA { get; set; }
        public Time TimeB { get; set; }
        public int GolsTimeA { get; set; }
        public int GolsTimeB { get; set; }
        public string ApostaGolsTimeA { get; set; }
        public string ApostaGolsTimeB { get; set; }

    }
}