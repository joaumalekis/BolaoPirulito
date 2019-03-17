using System;
using System.Collections.Generic;

namespace BolaoPirulito.Models
{
    public class Rodada
    {
        public string Id { get; set; }
        public int NumeroRodada { get; set; }
        public Jogo[] Jogos { get; set; }
    }
}