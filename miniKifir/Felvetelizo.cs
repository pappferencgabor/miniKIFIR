using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public string OM_Azonosito 
        { 
            get => this.omAzon; 
            set
            {
                if (value.ToString().Length != 11)
                {
                    throw new ArgumentException("Az OM azonosító hossza nem helyes!");
                }
                else
                {
                    try
                    {
                        int.Parse(value);
                        this.omAzon = value;
                    }
                    catch (Exception)
                    {
                        throw new ArgumentException("Az OM azonosító csak számokat tartalmazhat!");
                    }
                }
            }
        }
        public string Neve { get => this.nev; set => this.nev = value; }
        public string ErtesitesiCime { get => eretesitesiCime; set => eretesitesiCime = value; }
        public string Email { get => email; set => email = value; }
        public DateTime SzuletesiDatum { get => szuletesiDatum; set => szuletesiDatum = value; }
        public int Matematika
        {
            get => this.matek;
            set
            {
                if (value < 0 || value > 50)
                {
                    throw new ArgumentException("Nem létező pontérték!");
                }
                else
                {
                    this.matek = value;
                }
            }
        }
        public int Magyar
        {
            get => magyar;
            set
            {
                if (value < 0 || value > 50)
                {
                    throw new ArgumentException("Nem létező pontérték!");
                }
                else 
                { 
                    this.magyar = value; 
                }
            }
        }

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
