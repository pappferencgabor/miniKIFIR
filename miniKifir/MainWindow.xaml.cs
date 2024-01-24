using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;

namespace miniKifir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<IFelvetelizo> diakok = new ObservableCollection<IFelvetelizo>();

        public MainWindow()
        {
            InitializeComponent();
            AdatokBetoltese();
            dgDiakok.ItemsSource = diakok;
        }

        private void AdatokBetoltese()
        {
            List<String> sorok = new List<string>() {
            "78655218932;Szabó Anna;Budapest, Gellért tér 15.;anna@example.com;1998.07.19;14;35",
            "15963702584;Nagy Zsófia;Debrecen, Szent István utca 8.;zsofia@example.com;2000.02.22;27;4",
            "30351479261;Kovács Máté;Szeged, Erzsébet körút 45.;mate@example.com;1995.11.29;48;15",
            "97401028543;Tóth Bence;Pécs, Váci utca 33.;bence@example.com;1997.03.17;8;47",
            "88765031624;Horváth Eszter;Székesfehérvár, Bartók Béla út 12.;eszter@example.com;1996.09.08;34;7",
            "64189075351;Kiss Attila;Miskolc, József Attila utca 18.;attila@example.com;1993.12.05;13;48",
            "18734250658;Fekete Laura;Győr, Széchenyi tér 9.;laura@example.com;1999.06.30;2;30",
            "51698072427;Bíró Gábor;Kecskemét, Deák Ferenc utca 21.;gabor@example.com;1994.10.14;9;33",
            "60157349268;Mészáros Péter;Nyíregyháza, Petőfi Sándor utca 26.;peter@example.com;2001.04.01;36;21",
            "72948316750;Varga Noémi;Szombathely, Kossuth Lajos utca 3.;noemi@example.com;1992.08.18;24;23",
            "84052731649;Lakatos Dóra;Veszprém, Ady Endre utca 7.;dora@example.com;2000.01.03;43;41",
            "85273941680;Németh Tamás;Szolnok, Béke tér 14.;tamas@example.com;1998.05.27;5;49",
            "41593260701;Orbán Katalin;Eger, Szabadság utca 32.;katalin@example.com;1996.02.11;37;20",
            "10486732952;Simon Balázs;Debrecen, Király utca 28.;balazs@example.com;1995.07.07;20;48",
            "92740581643;Papp Viktória;Kaposvár, Alkotmány utca 5.;viktor@example.com;1997.11.24;32;9",
            "10637851454;Molnár Zoltán;Szekszárd, Párizsi utca 17.;zoltan@example.com;1993.01.16;3;46",
            "44025967885;Fekete Márton;Pécs, Rákóczi út 13.;marton@example.com;1992.04.29;42;31",
            "30381425616;Pál Júlia;Sopron, Szent Gellért tér 10.;julia@example.com;1999.09.02;49;19",
            "65082317920;Takács Orsolya;Eger, Andrássy út 22.;orsolya@example.com;1994.06.13;31;18",
            "15374680221;Kovács Ádám;Székesfehérvár, Bajnai út 8.;adam@example.com;1996.08.06;18;10"
                };
            foreach (String s in sorok)
            {
                diakok.Add(new Felvetelizo(s));
            }
        }

        private void btnUjTanulo_Click(object sender, RoutedEventArgs e)
        {
            Felvetelizo ujdiak = new Felvetelizo();

            WinUjFelvetelizo ujablak = new WinUjFelvetelizo(ujdiak);
            ujablak.ShowDialog();

            diakok.Add(ujdiak);
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV file (*.csv)|*.csv|JSON file (*.json)|*.json";
            sfd.ShowDialog();

            if (sfd.FileName != "")
            {
                if (sfd.FileName.EndsWith(".csv"))
                {
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    foreach (var diak in diakok)
                    {
                        sw.WriteLine($"{diak.CSVSortAdVissza()}");
                    }
                    sw.Close();
                    MessageBox.Show("Sikeres exportálás.");
                }
                else if (sfd.FileName.EndsWith(".json"))
                {
                    var opciok = new JsonSerializerOptions();
                    opciok.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    opciok.WriteIndented = true;

                    string jsonLines = JsonSerializer.Serialize(diakok, opciok);
                    File.WriteAllLines(sfd.FileName, jsonLines.Split("\n"));
                    MessageBox.Show("Sikeres exportálás.");
                }
                else
                {
                    MessageBox.Show("Helytelen kiterjesztés.");
                }
            }
            else
            {
                MessageBox.Show("Nem adtál meg mentési helyet! Az exportálás sikertelen.");
            }
        }

        private void btnTorles_Click(object sender, RoutedEventArgs e)
        {
            if (dgDiakok.SelectedIndex == -1)
            {
                MessageBox.Show("Nem lehet torolni kivalasztott mezo nelkul!");
            } else
            {
                List<int> indexes = new List<int>();
                foreach (Felvetelizo diak in dgDiakok.SelectedItems)
                {
                    for(int i = 0; i < dgDiakok.Items.Count; i++)
                    {
                        if (dgDiakok.Items.GetItemAt(i) == diak)
                        {
                            indexes.Add(i);
                        }
                    }
                }

                foreach (int i in indexes)
                {
                    diakok.RemoveAt(i);
                }
            }
        }

        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofp = new OpenFileDialog();
            ofp.InitialDirectory = "c:\\";
            ofp.Filter = "CSV files (*.csv)|*.csv|JSON files (*.json)|*.json";
            ofp.RestoreDirectory = true;

            if (ofp.ShowDialog() == true)
            {
                var fileStream = ofp.OpenFile();
                /*
                if (Path.GetExtension(ofp.FileName) == "json") {
                    string json = "";

                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        json = sr.ReadToEnd()
                    }
                }
                */
            }

        }

        private void btnModosit_Click(object sender, RoutedEventArgs e)
        {
            if (dgDiakok.SelectedItems.Count > 1)
            {
                MessageBox.Show("Csak egy elem lehet kivalasztva!"); 
            } else
            {
                Felvetelizo felvetelizo = dgDiakok.SelectedItem as Felvetelizo;
                int index = dgDiakok.SelectedIndex;
                WinUjFelvetelizo wn = new WinUjFelvetelizo(felvetelizo);

                wn.ShowDialog();

                diakok.RemoveAt(index);
                diakok.Insert(index, felvetelizo);
            }
            
        }
    }
}
