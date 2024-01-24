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
        Felvetelizo felvetelizoAdatai = new Felvetelizo();
        public WinUjFelvetelizo()
        {
            InitializeComponent();
            this.DataContext = felvetelizoAdatai;
        }

        public WinUjFelvetelizo(Felvetelizo ujdiak) : this()
        {
            felvetelizoAdatai = ujdiak;
            this.DataContext = felvetelizoAdatai;
            this.Title = "Tanuló adatainak rögzítése";
        }


        private void btnRogzit_Click(object sender, RoutedEventArgs e)
        {
            //Lehet és kell hibát vizsgálni, de most csak itt adok példát rá!

            // OM vizsgálat
            if (txtOMazonosito.Text.Trim() != "")
            {
                if (txtOMazonosito.Text.Length != 11)
                {
                    MessageBox.Show("Az OM azonosító hossza nem helyes!");
                    txtOMazonosito.Text = "";
                    txtOMazonosito.Focus();
                    return;
                }
                else
                {
                    try
                    {
                        this.felvetelizoAdatai.OM_Azonosito = txtOMazonosito.Text;
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Az OM azonosító csak számokat tartalmazhat!");
                        txtOMazonosito.Text = "";
                        txtOMazonosito.Focus();
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Nem hagyhatod üresen a mezőt!");
                txtOMazonosito.Text = "";
                txtOMazonosito.Focus();
                return;
            }

            // Név vizsgálat
            if (txtNeve.Text.Split(' ').Length >= 2)
            {
                bool megfelel = true;
                foreach (string nev in txtNeve.Text.Split(' '))
                {
                    if (nev[0].ToString() != nev[0].ToString().ToUpper())
                    {
                        megfelel = false;
                        break;
                    }
                }
                if (megfelel)
                {
                    this.felvetelizoAdatai.Neve = txtNeve.Text.Trim();
                }
                else
                {
                    MessageBox.Show("A név minden kezdőbetűjének nagynak kell lennie!");
                    txtNeve.Text = "";
                    txtNeve.Focus();
                    return;
                }
            }
            else
            {
                MessageBox.Show("A névnek legalább két szónak kell lennie!");
                txtNeve.Text = "";
                txtNeve.Focus();
                return;
            }

            // Cim vizsgálat
            if (txtCim.Text.Trim() != "")
            {
                this.felvetelizoAdatai.ErtesitesiCime = txtCim.Text;
            }
            else
            {
                MessageBox.Show("Ne hagyd üresen az értesítési címet!");
                txtCim.Text = "";
                txtCim.Focus();
                return;
            }

            // Email vizsgálat
            if (txtEmail.Text.Contains(" "))
            {
                MessageBox.Show("Az email cím nem tartalmazhat szóközt!");
                txtEmail.Text = "";
                txtEmail.Focus();
                return;
            }
            else
            {
                if (txtEmail.Text.Count(x => x == '@') != 1)
                {
                    MessageBox.Show("Az email cím csak egy @ jelet tartalmazhat!");
                    txtEmail.Text = "";
                    txtEmail.Focus();
                    return;
                }
                else
                {
                    this.felvetelizoAdatai.Email = txtEmail.Text;
                }
            }

            // Matematika vizsgálat
            try
            {
                this.felvetelizoAdatai.Matematika = int.Parse(txtMatematika.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Nem számformátum!");
                txtMatematika.Text = "";
                txtMatematika.Focus();
                return;
            }
            if (felvetelizoAdatai.Matematika < 0 || felvetelizoAdatai.Matematika > 50)
            {
                MessageBox.Show("Nem lehet ennyi pontja!");
                return;
            }

            // Magyar vizsgálat
            try
            {
                this.felvetelizoAdatai.Magyar = int.Parse(txtMagyar.Text);
            }
            catch (Exception)
            {

                MessageBox.Show("Nem számformátum!");
                txtMagyar.Text = "";
                txtMagyar.Focus();
                return;
            }
            if (felvetelizoAdatai.Magyar < 0 || felvetelizoAdatai.Magyar > 50)
            {
                MessageBox.Show("Nem lehet ennyi pontja!");
                return;
            }

            Close();
        }
    }
}
