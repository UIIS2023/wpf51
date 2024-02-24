using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Knjizara.Forms;
using System.Windows;

namespace Knjizara
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    
    public partial class MainWindow : Window
    {

        #region Select upiti
        private static string zanrSelect = @" select ZanrID as ID,Naziv from Zanr";
        private static string dobavljacSelect = @"select DobavljacID as ID, Naziv,Adresa,Kontakt from dobavljac";
        private static string izdanjeSelect = @"select IzdanjeID as ID,Izdanje.Naziv,Cena,GodinaIzdanja,IzdavajucaKuca,dobavljac.naziv as Dobavljac
                                            from Izdanje join dobavljac on izdanje.DobavljacID=dobavljac.dobavljacid";
        private static string knjigaSelect = @"select Knjiga.KnjigaID as ID,knjiga.Naziv,Autor.Ime+' '+Autor.Prezime as Autor,zanr.Naziv as Zanr
from zanr join(knjiga join Autor on Knjiga.AutorID=Autor.AutorID)on zanr.ZanrID=Knjiga.ZanrID";
        private static string autorSelect = @"Select autorid as ID,Ime,Prezime from autor";

        private static string clanSelect = @"Select ClanID as ID,Ime,Prezime,Adresa,Kartica.vrsta from clan join kartica on clan.KarticaID=Kartica.KarticaID";
        private static string izdanje_narudzbinaSelect = @"select izdanje_narudzbina.izdanje_narudzbinaID as ID,ime+prezime as Kupac,izdanje.Naziv,Narudzbina.NarudzbinaID 
from Clan join(narudzbina join(izdanje_narudzbina join Izdanje 
on izdanje.IzdanjeID=izdanje_narudzbina.izdanjeID)
on narudzbina.narudzbinaID=izdanje_narudzbina.NarudzbinaID)
on clan.ClanID=narudzbina.kupacID order by narudzbinaID";
        private static string izdanje_racunSelect = @"select  racun_izdanje.racun_izdanje_ID as ID,Racun.racunid as Racun,racun.vreme as 'Vreme kupovine',zaposleni.Ime+' '+zaposleni.Prezime as Prodavac,
clan.Ime+clan.Prezime as Kupac from clan join (zaposleni join( racun join racun_izdanje
on racun.RacunID=racun_izdanje.racunID)
on zaposleni.ZaposleniID=Racun.zaposleniID)
on clan.ClanID=Racun.clanID";
        private static string karticaSelect = @"select KarticaID as ID,Vrsta,Popust from kartica";
        private static string narudzbinaSelect = @"select Narudzbina.narudzbinaID as ID,clan.Ime+clan.Prezime as Clan,narudzbina.stanje from narudzbina join clan on narudzbina.kupacID=Clan.ClanID";
        private static string racunSelect = @"select RacunID as ID,zaposleni.Ime+zaposleni.Prezime as Prodavac,Clan.Ime+' '+Clan.Prezime as Kupac,racun.Vreme 
from zaposleni join (racun join clan on Racun.clanID=clan.ClanID) on zaposleni.ZaposleniID=Racun.zaposleniID";
        private static string zaposleniSelect = @"select zaposleniID as ID,Ime,Prezime,Adresa,JMBG from zaposleni";



        #endregion
        #region selectuslov
        private static string zanr = "Select * from zanr where Zanrid=@id";
        private static string zaposleni = "Select * from zaposleni where zaposleniid=@id";
        private static string racun = "Select * from racun where racunid=@id";
        private static string narudzbina = "Select * from narudzbina where narudzbinaid=@id";
        private static string knjiga = "Select * from knjiga where knjigaid=@id";
        private static string izdanje_racun = "Select * from racun_izdanje where racun_izdanje_ID=@id";
        private static string izdanje_narudzbina = "Select * from izdanje_narudzbina where izdanje_narudzbinaID=@id";
        private static string izdanje = "Select * from izdanje where izdanjeid=@id";
        private static string dobavljac = "Select * from dobavljac where dobavljacid=@id";
        private static string clan = "Select * from clan where clanid=@id";
        private static string autor = "Select * from autor where autorid=@id";

        private static string kartica = "Select * from kartica where KarticaID=@id";



        #endregion
        #region delete upiti
        private static string zanrDelete = "Delete from zanr where ZanrID=@id";
        private static string zaposleniDelete = "Delete from zaposleni where zaposleniid=@id";
        private static string racunDelete = "Delete from racun where racunID=@id";
        private static string narudzbinaDelete = "Delete from narudzbina where narudzbinaID=@id";
        private static string knjigaDelete = "Delete from knjiga where knjigaID=@id";
        private static string karticaDelete = "Delete from kartica where karticaID=@id";
        private static string izdanje_racunDelete = "Delete from racun_Izdanje where racun_izdanje_ID=@id";
        private static string izdanje_narudzbindaDelete = "Delete from izdanje_narudzbina where izdanje_narudzbinaID=@id";
        private static string izdanjeDelete = "Delete from izdanje where izdanjeID=@id";
        private static string DobavljacDelete = "Delete from dobavljac where dobavljacid=@id";
        private static string clanDelete = "Delete from clan where clanID=@id";
        private static string autorDelete = "Delete from autor where autorID=@id";
        #endregion


        SqlConnection con = new SqlConnection();
        private string UcitanaTabela;
        public MainWindow()
        {
            InitializeComponent();
            con = Konekcija.KreirajKonekciju();
            UcitajPodatke(izdanjeSelect);
        }

        public void UcitajPodatke(string comand)
        {
            try
            {
                
                SqlCommand com = new SqlCommand(comand, con);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(com);
                DataTable dt = new DataTable();
                con.Open();
                dataAdapter.Fill(dt);
               // if (dt.Rows.Count > 0)
                    DataGridCentral.ItemsSource = dt.DefaultView;
                UcitanaTabela = comand;
                dataAdapter.Dispose();
                dt.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }

        }
      

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {
            Window prozor;
            if (UcitanaTabela == zanrSelect)
            {
                prozor = new Zanr();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == zaposleniSelect)
            {
                prozor = new Zaposleni();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == racunSelect)
            {
                prozor = new Racun();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == narudzbinaSelect)
            {
                prozor = new Narudzbina();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == knjigaSelect)
            {
                prozor = new Knjiga();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == karticaSelect)
            {
                prozor = new Kartica();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == izdanje_racunSelect)
            {
                prozor = new Izdanje_racun();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == izdanje_narudzbinaSelect)
            {
                prozor = new Izdanje_Narudzbina();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == izdanjeSelect)
            {
                prozor = new Izdanje();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == dobavljacSelect)
            {
                prozor = new Dobavljac();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == clanSelect)
            {
                prozor = new Clan();
                prozor.ShowDialog();
            }
            else if (UcitanaTabela == autorSelect)
            {
                prozor = new AutorFrm();
                prozor.ShowDialog();
            }
            UcitajPodatke(UcitanaTabela);
        }

        private void btnIzmeni_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int red = DataGridCentral.SelectedIndex;
            if (red == -1)
            {
                MessageBox.Show("Nije izabran red");
            }
            else
            {

                DataRowView row = (DataRowView)DataGridCentral.Items[red];
                id = int.Parse(row["ID"].ToString());
                // MessageBox.Show(id.ToString());
                SqlDataAdapter da;
                con.Open();
                SqlCommand cmd = new SqlCommand("", con);
                
                SqlDataReader reader;
                if (UcitanaTabela == zanrSelect)
                {
                    cmd.CommandText = zanr;
                    cmd.Parameters.AddWithValue("id", id);
                    




                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Zanr prozor = new Zanr();
                    prozor.txtNaziv.Text = dt.Rows[0]["naziv"].ToString();
                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(zanrSelect);
                }
                else if (UcitanaTabela == zaposleniSelect)
                {
                    cmd.CommandText = zaposleni;
                    cmd.Parameters.AddWithValue("id", id);





                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Zaposleni prozor = new Zaposleni();
                    prozor.txtIme.Text = dt.Rows[0]["Ime"].ToString();
                    prozor.txtPrezime.Text = dt.Rows[0]["Prezime"].ToString();
                    prozor.txtAdresa.Text = dt.Rows[0]["Adresa"].ToString();
                    prozor.txtJMBG.Text = dt.Rows[0]["Jmbg"].ToString();
                    prozor.cbxKartica.SelectedValue = dt.Rows[0]["KarticaID"].ToString();

                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(zaposleniSelect);

                }
                else if (UcitanaTabela == racunSelect)
                {
                    cmd.CommandText = racun;
                    cmd.Parameters.AddWithValue("id", id);





                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Racun prozor = new Racun();
                    prozor.cbxClan.SelectedValue = dt.Rows[0]["ClanID"].ToString();
                    prozor.cbxZaposleni.SelectedValue = dt.Rows[0]["ZaposleniID"].ToString();
                   

                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(racunSelect);
                }
                else if (UcitanaTabela == narudzbinaSelect)
                {
                    cmd.CommandText = narudzbina;
                    cmd.Parameters.AddWithValue("id", id);





                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Narudzbina prozor = new Narudzbina();
                    prozor.cbxClan.SelectedValue = dt.Rows[0]["KupacID"].ToString();
                    prozor.txtStanje.Text = dt.Rows[0]["Stanje"].ToString();
                    



                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(narudzbinaSelect);

                }
                else if (UcitanaTabela == knjigaSelect)
                {
                    cmd.CommandText = knjiga;
                    cmd.Parameters.AddWithValue("id", id);





                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Knjiga prozor = new Knjiga();
                    prozor.txtNaziv.Text = dt.Rows[0]["Naziv"].ToString();
                    prozor.cbxAutor.SelectedValue = dt.Rows[0]["AutorID"].ToString();
                    prozor.cbxZanr.SelectedValue = dt.Rows[0]["ZanrID"].ToString();





                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(knjigaSelect);
                }
                else if (UcitanaTabela == karticaSelect)
                {
                    cmd.CommandText = kartica;
                    cmd.Parameters.AddWithValue("id", id);

                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Kartica prozor = new Kartica();
                    prozor.txtVrsta.Text = dt.Rows[0]["Vrsta"].ToString();
                    prozor.txtPopust.Text = dt.Rows[0]["Popust"].ToString();





                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(karticaSelect);
                }
                else if (UcitanaTabela == izdanje_racunSelect)
                {
                    cmd.CommandText = izdanje_racun;
                    cmd.Parameters.AddWithValue("id", id);

                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Izdanje_racun prozor = new Izdanje_racun();
                    prozor.cbxIzdanje.SelectedValue = dt.Rows[0]["IzdanjeID"].ToString();
                    prozor.txtBroj.Text = dt.Rows[0]["brojKnjiga"].ToString();
                    prozor.cbxRacun.SelectedValue = dt.Rows[0]["RacunID"].ToString();






                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(izdanje_racunSelect);
                }
                else if (UcitanaTabela == izdanje_narudzbinaSelect)
                {
                    cmd.CommandText = izdanje_narudzbina;
                    cmd.Parameters.AddWithValue("id", id);

                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Izdanje_Narudzbina prozor = new Izdanje_Narudzbina();
                    prozor.cbxIzdanje.SelectedValue = dt.Rows[0]["IzdanjeID"].ToString();
                    prozor.cbxNarudzbina.SelectedValue = dt.Rows[0]["NarudzbinaID"].ToString();






                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(izdanje_narudzbinaSelect);
                }
                else if (UcitanaTabela == izdanjeSelect)
                {
                    cmd.CommandText = izdanje;
                    cmd.Parameters.AddWithValue("id", id);

                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Izdanje prozor = new Izdanje();
                    prozor.txtNaziv.Text = dt.Rows[0]["naziv"].ToString();
                    prozor.txtCena.Text = dt.Rows[0]["cena"].ToString();

                    prozor.txtGodina.Text = dt.Rows[0]["GodinaIzdanja"].ToString();
                    prozor.txtKuca.Text = dt.Rows[0]["IzdavajucaKuca"].ToString();

                    prozor.cbxDobavljac.SelectedValue = dt.Rows[0]["DobavljacID"].ToString();
                    prozor.cbxKnjiga.SelectedValue = dt.Rows[0]["KnjigaID"].ToString();







                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(izdanjeSelect);
                }
                else if (UcitanaTabela == dobavljacSelect)
                {
                    cmd.CommandText = dobavljac;
                    cmd.Parameters.AddWithValue("id", id);

                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Dobavljac prozor = new Dobavljac();
                    prozor.txtNaziv.Text = dt.Rows[0]["Naziv"].ToString();
                    prozor.txtAdresa.Text = dt.Rows[0]["Adresa"].ToString();
                    prozor.txtKontakt.Text = dt.Rows[0]["Kontakt"].ToString();









                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(dobavljacSelect);
                }
                else if (UcitanaTabela == clanSelect)
                {
                    cmd.CommandText = clan;
                    cmd.Parameters.AddWithValue("id", id);

                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    Clan prozor = new Clan();
                    prozor.txtIme.Text = dt.Rows[0]["Ime"].ToString();
                    prozor.txtAdresa.Text = dt.Rows[0]["Adresa"].ToString();
                    prozor.txtPrezime.Text = dt.Rows[0]["Prezime"].ToString();
                    prozor.cbxKartica.SelectedValue = dt.Rows[0]["KarticaID"].ToString();










                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(clanSelect);
                }
                else if (UcitanaTabela == autorSelect)
                {
                    cmd.CommandText = autor;
                    cmd.Parameters.AddWithValue("id", id);

                    da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();



                    AutorFrm prozor = new AutorFrm();
                    prozor.txtIme.Text = dt.Rows[0]["Ime"].ToString();
                    
                    prozor.txtPrezime.Text = dt.Rows[0]["Prezime"].ToString();
                   










                    prozor.isedit = true;
                    prozor.ID = id;


                    prozor.ShowDialog();
                    UcitajPodatke(autorSelect);
                }
            }
        }

        private void btnObrisi_Click(object sender, RoutedEventArgs e)
        {
            int id;
            int red =  DataGridCentral.SelectedIndex;
            if (red == -1)
            {
                MessageBox.Show("Nije izabran red");
            }
            else {

                DataRowView row = (DataRowView)DataGridCentral.Items[red];
                id = int.Parse(row["ID"].ToString());
               // MessageBox.Show(id.ToString());
                string a= MessageBox.Show("Da li ste sigurni da hocete da obrisete", "Brisanje", MessageBoxButton.YesNo).ToString();
                
                if (a == "No")
                {
                    return;
               }
                con.Open();
                SqlCommand cmd = new SqlCommand("", con);
                if (UcitanaTabela == zanrSelect)
                {
                        cmd.CommandText=zanrDelete; 
                }
                else if (UcitanaTabela == zaposleniSelect)
                {
                    cmd.CommandText = zaposleniDelete;
                }
                else if (UcitanaTabela == racunSelect)
                {
                    cmd.CommandText = racunDelete;
                }
                else if (UcitanaTabela == narudzbinaSelect)
                {
                    cmd.CommandText = narudzbinaDelete;
                }
                else if (UcitanaTabela == knjigaSelect)
                {
                    cmd.CommandText = knjigaDelete;
                }
                else if (UcitanaTabela == karticaSelect)
                {
                    cmd.CommandText = karticaDelete;
                }
                else if (UcitanaTabela == izdanje_racunSelect)
                {
                    cmd.CommandText = izdanje_racunDelete;
                }
                else if (UcitanaTabela == izdanje_narudzbinaSelect)
                {
                    cmd.CommandText = izdanje_narudzbindaDelete;
                }
                else if (UcitanaTabela == izdanjeSelect)
                {
                    cmd.CommandText = izdanjeDelete;
                }
                else if (UcitanaTabela == dobavljacSelect)
                {
                    cmd.CommandText = DobavljacDelete;
                }
                else if (UcitanaTabela == clanSelect)
                {
                    cmd.CommandText = clanDelete;
                }
                else if (UcitanaTabela == autorSelect)
                {
                    cmd.CommandText = autorDelete;
                }
                cmd.Parameters.Add("@id",SqlDbType.Int).Value=id;
                cmd.ExecuteNonQuery();

                con.Close();
                UcitajPodatke(UcitanaTabela);




            }
        }
        #region Clikovi
        private void btnZanr_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(zanrSelect);
        }

        private void brnAutor_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(autorSelect);
        }

        private void brnZaposleni_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(zaposleniSelect);
        }

        private void btnRacun_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(racunSelect);
        }

        private void btnNarudzbina_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(narudzbinaSelect);
        }

        private void btnKnjiga_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(knjigaSelect);
        }

        private void btnKartica_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(karticaSelect);
        }

        private void btnIzdanje_racun_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(izdanje_racunSelect);
        }

        private void btnIzdanjeNarudzbina_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(izdanje_narudzbinaSelect);
        }

        private void btnIzdanje_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(izdanjeSelect);
        }

        private void btnDobavljac_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(dobavljacSelect);
        }

        private void btnClan_Click(object sender, RoutedEventArgs e)
        {
            UcitajPodatke(clanSelect);
        }
        #endregion
    }
}