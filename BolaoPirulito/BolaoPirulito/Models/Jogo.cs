using System;
using System.ComponentModel;
using BolaoPirulito.Annotations;
using Xamarin.Forms;

namespace BolaoPirulito.Models
{
    public class Jogo 
    {
        public int id { get; set; }
        public DateTime data_realizacao { get; set; }
        [CanBeNull] public Time hora_realizacao { get; set; }
        public int? placar_oficial_visitante { get; set; }
        public int? placar_oficial_mandante { get; set; }
        public int? placar_penaltis_visitante { get; set; }
        public int? placar_penaltis_mandante { get; set; }
        public Equipes equipes { get; set; }
        public Sede sede { get; set; }
        public string transmissao { get; set; }

        //public string ApostaGolsTimeA { get; set; } = "0";
        //public string ApostaGolsTimeB { get; set; } = "0";
        //public Color ResultadoColor => ApostaGolsTimeA.Equals(placar_oficial_mandante.ToString()) && ApostaGolsTimeB.Equals(placar_oficial_visitante.ToString())
        //    ? Color.FromHex("2eb82e")
        //    : (TimeAGanhou && ApostouTimeA) || (TimeBGanhou && ApostouTimeB) || (Empate && ApostouEmpate) ? Color.FromHex("ffcc00") : Color.FromHex("ff1a1a");

        //public int Pontos => ApostaGolsTimeA.Equals(placar_oficial_mandante.ToString()) && ApostaGolsTimeB.Equals(placar_oficial_visitante.ToString())
        //    ? 10
        //    : (TimeAGanhou && ApostouTimeA) || (TimeBGanhou && ApostouTimeB) || (Empate && ApostouEmpate) ? 5 : 0;

        //public bool TimeAGanhou => placar_oficial_mandante > placar_oficial_visitante;
        //public bool TimeBGanhou => placar_oficial_mandante < placar_oficial_visitante;
        //public bool Empate => placar_oficial_mandante == placar_oficial_visitante;
        //public bool ApostouTimeA =>  Convert.ToInt16(ApostaGolsTimeA) > Convert.ToInt16(ApostaGolsTimeB);
        //public bool ApostouTimeB =>  Convert.ToInt16(ApostaGolsTimeA) < Convert.ToInt16(ApostaGolsTimeB);
        //public bool ApostouEmpate =>  Convert.ToInt16(ApostaGolsTimeA) == Convert.ToInt16(ApostaGolsTimeB);
        //public bool JogoComecou => data_realizacao > DateTime.Now;
    }
}