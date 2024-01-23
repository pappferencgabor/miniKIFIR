using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace miniKifir
{
    /// <summary>
    /// Interaction logic for WinUjFelvetelizo.xaml
    /// </summary>
    public partial class WinUjFelvetelizo : Window
    {
        Felvetelizo felvetelizoAdatai;
        public WinUjFelvetelizo()
        {
            InitializeComponent();

        }

        public WinUjFelvetelizo(Felvetelizo ujdiak) : this()
        {
            this.felvetelizoAdatai = ujdiak;

            this.Title = $"{felvetelizoAdatai.Neve} adatainak rögzítése";
            txtNeve.Text = felvetelizoAdatai.Neve;
        }


        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            felvetelizoAdatai.OM_Azonosito = txtOMazonosito.Text;
            felvetelizoAdatai.Neve = txtNeve.Text;
            felvetelizoAdatai.ErtesitesiCime = txtCim.Text;
            felvetelizoAdatai.Email = txtEmail.Text;
            felvetelizoAdatai.SzuletesiDatum = Convert.ToDateTime(txtSzuletesiDatum.Text);

            //Lehet és kell hibát vizsgálni, de most csak itt adok példát rá!
            try
            {
                this.felvetelizoAdatai.Matematika = int.Parse(txtMatematika.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Nem számformátum!");
                //txtMatematika.Text = "";
                //txtMatematika.Focus();
                return;
            }
            if (felvetelizoAdatai.Matematika < 0 || felvetelizoAdatai.Matematika > 50)
            {
                MessageBox.Show("Nem lehet ennyi pontja!");
                return;
            }

            this.felvetelizoAdatai.Magyar = int.Parse(txtMagyar.Text);
            Close();
        }
    }
}
