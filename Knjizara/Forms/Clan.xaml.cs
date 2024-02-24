using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace Knjizara.Forms
{
    /// <summary>
    /// Interaction logic for Clan.xaml
    /// </summary>
    public partial class Clan : Window
    {

        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Clan()
        {
            InitializeComponent();
            UcitajPodatke();
        }

        private void UcitajPodatke()
        {
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select Vrsta,KarticaID from Kartica", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbxKartica.ItemsSource = dt.DefaultView;
            da.Dispose();
            dt.Dispose();
            con.Close();


        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIme.Text) ||
                    string.IsNullOrEmpty(txtPrezime.Text) ||
                    string.IsNullOrEmpty(txtAdresa.Text) )
                {

                    throw new Exception("Sve vrednosti moraju biti unesene");
                }

                SqlCommand cmd;



                if (isedit)
                {
                    cmd = new SqlCommand("UPDATE clan SET Ime = @ime     ,Prezime = @prezime      ,Adresa = @adresa           ,KarticaID= @kartica WHERE clanID=@id", con);
                    cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                    cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                    cmd.Parameters.Add("@adresa", SqlDbType.NVarChar).Value = txtAdresa.Text;
                    cmd.Parameters.Add("@kartica", SqlDbType.Int).Value = cbxKartica.SelectedValue;



                    cmd.Parameters.AddWithValue("@id", ID);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO clan VALUES (@ime,@prezime,@adresa,@kartica)", con);

                    cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                    cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                    cmd.Parameters.Add("@adresa", SqlDbType.NVarChar).Value = txtAdresa.Text;
                    cmd.Parameters.Add("@kartica", SqlDbType.Int).Value = cbxKartica.SelectedValue;



                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }

                Close();



            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            finally { con.Close(); }
        }
    }
}
