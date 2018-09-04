using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace BolaoPirulito.Models
{
    public class Jogo 
    {
        public Guid Id { get; set; }
        public Time TimeA { get; set; }
        public Time TimeB { get; set; }
        public DateTime DataJogo { get; set; }
        public int GolsTimeA { get; set; }
        public int GolsTimeB { get; set; }
        public string ApostaGolsTimeA { get; set; } = "0";
        public string ApostaGolsTimeB { get; set; } = "0";
        public Color ResultadoColor => ApostaGolsTimeA.Equals(GolsTimeA.ToString()) && ApostaGolsTimeB.Equals(GolsTimeB.ToString())
            ? Color.FromHex("2eb82e")
            : (TimeAGanhou && ApostouTimeA) || (TimeBGanhou && ApostouTimeB) || (Empate && ApostouEmpate) ? Color.FromHex("ffcc00") : Color.FromHex("ff1a1a");

        public int Pontos => ApostaGolsTimeA.Equals(GolsTimeA.ToString()) && ApostaGolsTimeB.Equals(GolsTimeB.ToString())
            ? 10
            : (TimeAGanhou && ApostouTimeA) || (TimeBGanhou && ApostouTimeB) || (Empate && ApostouEmpate) ? 5 : 0;

        public bool TimeAGanhou => GolsTimeA > GolsTimeB;
        public bool TimeBGanhou => GolsTimeA < GolsTimeB;
        public bool Empate => GolsTimeA == GolsTimeB;
        public bool ApostouTimeA =>  Convert.ToInt16(ApostaGolsTimeA) > Convert.ToInt16(ApostaGolsTimeB);
        public bool ApostouTimeB =>  Convert.ToInt16(ApostaGolsTimeA) < Convert.ToInt16(ApostaGolsTimeB);
        public bool ApostouEmpate =>  Convert.ToInt16(ApostaGolsTimeA) == Convert.ToInt16(ApostaGolsTimeB);
        public bool JogoComecou => DataJogo > DateTime.Now;
    }
}