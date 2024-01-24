using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using static miniKifir.Felvetelizo;

namespace miniKifir
{
    public class Felvetelizo : IFelvetelizo
    {
        string omAzon;
        string nev;
        string eretesitesiCime;
        string email;
        DateTime szuletesiDatum;
        int matek;
        int magyar;

        public Felvetelizo()
        {
        }

        public Felvetelizo(String csvString)
        {
            ModositCSVSorral(csvString);
        }

        public Felvetelizo(string omAzon, string nev, string eretesitesiCime, string email, DateTime szuletesiDatum, int matek, int magyar)
        {
            this.omAzon = omAzon;
            this.nev = nev;
            this.eretesitesiCime = eretesitesiCime;
            this.email = email;
            this.szuletesiDatum = szuletesiDatum;
            this.matek = matek;
            this.magyar = magyar;
        }

        public string OM_Azonosito { get => omAzon; set => omAzon = value; }
        
        public string Neve { get => nev; set => nev = value; }

        public string ErtesitesiCime { get => eretesitesiCime; set => eretesitesiCime = value; }

        public string Email { get => email; set => email = value; }

        public DateTime SzuletesiDatum { get => szuletesiDatum; set => this.szuletesiDatum = value; }

        public int Matematika { get => matek; set => matek = value; }

        public int Magyar { get => magyar; set => magyar = value; }

        public string CSVSortAdVissza()
        {
            return $"{this.omAzon};{this.nev};{this.eretesitesiCime};{this.email};{this.szuletesiDatum.ToShortDateString()};{this.matek};{this.magyar}";
        }

        public void ModositCSVSorral(string csvString)
        {
            string[] mezok = csvString.Split(';');
            this.omAzon = mezok[0];
            this.nev = mezok[1];
            this.eretesitesiCime = mezok[2];
            this.email = mezok[3];
            this.szuletesiDatum = Convert.ToDateTime(mezok[4]);
            this.matek = int.Parse(mezok[5]);
            this.magyar = int.Parse(mezok[6]);
        }
    }
}
