using System;
using System.Collections.Generic;

namespace BolaoPirulito.Models
{
    public class Rodada
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int NumeroRodada { get; set; }
        public List<Jogo> Jogos { get; set; }
    }
}