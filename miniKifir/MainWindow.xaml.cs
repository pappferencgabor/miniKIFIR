using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.TextFormatting;
using MySql.Data.MySqlClient;

namespace miniKifir
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Felvetelizo> diakok = new ObservableCollection<Felvetelizo>();

        public MainWindow()
        {
            LinearGradientBrush gradientBrush = new LinearGradientBrush(Color.FromRgb(0, 212, 255), Color.FromRgb(9, 9, 121), new Point(0.5, 0), new Point(0.5, 1));
            this.Background = gradientBrush;

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
                    if (File.Exists(sfd.FileName))
                    {
                        MessageBoxResult mbr = MessageBox.Show("Felülírod a már meglévő fájlt?", "Fájl mentése", MessageBoxButton.YesNo);

                        if (mbr == MessageBoxResult.Yes)
                        {
                            StreamWriter sw = new StreamWriter(sfd.FileName);
                            foreach (var diak in diakok)
                            {
                                sw.WriteLine($"{diak.CSVSortAdVissza()}");
                            }
                            sw.Close();
                            MessageBox.Show("Sikeres exportálás.");
                        }
                    }
                    else
                    {
                        StreamWriter sw = new StreamWriter(sfd.FileName);
                        foreach (var diak in diakok)
                        {
                            sw.WriteLine($"{diak.CSVSortAdVissza()}");
                        }
                        sw.Close();
                        MessageBox.Show("Sikeres exportálás.");
                    }
                }
                else if (sfd.FileName.EndsWith(".json"))
                {
                    if (File.Exists(sfd.FileName))
                    {
                        MessageBoxResult mbr = MessageBox.Show("Felülírod a már meglévő fájlt?", "Fájl mentése", MessageBoxButton.YesNo);

                        if (mbr == MessageBoxResult.Yes)
                        {
                            var opciok = new JsonSerializerOptions();
                            opciok.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                            opciok.WriteIndented = true;

                            string jsonLines = JsonSerializer.Serialize(diakok, opciok);
                            File.WriteAllText(sfd.FileName, jsonLines);
                            MessageBox.Show("Sikeres exportálás.");
                        }
                    }
                    else
                    {
                        var opciok = new JsonSerializerOptions();
                        opciok.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                        opciok.WriteIndented = true;

                        string jsonLines = JsonSerializer.Serialize(diakok, opciok);
                        File.WriteAllText(sfd.FileName, jsonLines);
                        MessageBox.Show("Sikeres exportálás.");
                    }
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
               List<string> om = new List<string>();
               foreach(Felvetelizo f in dgDiakok.SelectedItems)
                {
                    om.Add(f.OM_Azonosito);
                }
                
               for (int i = 0; i < om.Count; i++)
                {
                    diakok.Remove(diakok.Where(x => x.OM_Azonosito == om[i]).FirstOrDefault());
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
                if (Path.GetExtension(ofp.FileName) == ".json")
                {
                    string json = "";

                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        json = sr.ReadToEnd();
                    }
                    var readjson = JsonSerializer.Deserialize<ObservableCollection<Felvetelizo>>(json);
                    var dr = MessageBox.Show("Hozzaszeretne fuzni? Amennyiben nem,akkor felulirasra kerulnek", "Biztos hozzaadja?", MessageBoxButton.YesNo);
                    if (dr == MessageBoxResult.Yes)
                    {
                        foreach (Felvetelizo diak in readjson)
                        {
                            diakok.Add(diak);
                        }
                    }
                    else
                    {
                        List<Felvetelizo> temp = new List<Felvetelizo>();
                        temp = diakok.ToList();
                        foreach(var diak in temp)
                        {
                            diakok.Remove(diak);
                        }
                        foreach (Felvetelizo diak in readjson)
                        {
                            diakok.Add(diak);
                        }
                    }
                    

                }
                if (Path.GetExtension(ofp.FileName) == ".csv")
                {
                    string csv = "";
                    using (StreamReader sr = new StreamReader(fileStream))
                    {
                        csv = sr.ReadToEnd();
                    }
                    List<string> lines = csv.Split("\n").ToList();
                    lines.RemoveAt(lines.Count -1);

                    if (dgDiakok.Items.Count > 0)
                    {
                        var dr = MessageBox.Show("Hozzaszeretne fuzni? Amennyiben nem,akkor felulirasra kerulnek", "Biztos hozzaadja?", MessageBoxButton.YesNo);
                        if (dr == MessageBoxResult.Yes)
                        {
                            foreach (string diak in lines)
                            {
                                foreach (string line in lines)
                                {
                                    diakok.Append(new Felvetelizo(line));
                                }
                            }
                        }
                        else
                        {
                            diakok = new ObservableCollection<Felvetelizo>();
                            foreach (string line in lines)
                            {
                                diakok.Append(new Felvetelizo(line));
                            }

                        }
                    }   else
                    {
                        foreach (string line in lines)
                        {
                            diakok.Append(new Felvetelizo(line));
                        }
                    }
                    
                }
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

        private void btnAdatbazis_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "datasource=localhost;" +
                                      "port=3306;" +
                                      "username=root;" +
                                      "password=;" +
                                      "database=minikifir;";
            MySqlConnection connection = new MySqlConnection(connectionString);

            connection.Open();

            string queryText = $"delete from tanulok";
            MySqlCommand command = new MySqlCommand(queryText, connection);

            command.ExecuteReader();
            connection.Close();

            foreach (Felvetelizo diak in diakok)
            {
                connection.Open();
                queryText = $"insert into tanulok values(\"{diak.OM_Azonosito}\", \"{diak.Neve}\", \"{diak.ErtesitesiCime}\", \"{diak.Email}\", \"{diak.SzuletesiDatum}\", {diak.Matematika}, {diak.Magyar})";
                command = new MySqlCommand(queryText, connection);

                command.ExecuteReader();
                connection.Close();
            }

            MessageBox.Show("Adatbázisba írás megtörtént!");
        }
    }
}
