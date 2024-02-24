using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Data.SqlTypes;

namespace Knjizara.Forms
{
    /// <summary>
    /// Interaction logic for Zaposleni.xaml
    /// </summary>
    public partial class Zaposleni : Window
    {

        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Zaposleni()
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

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(txtIme.Text) ||
                    string.IsNullOrEmpty(txtPrezime.Text) ||
                    string.IsNullOrEmpty(txtAdresa.Text)||
                    string.IsNullOrEmpty(txtJMBG.Text)) {

                    throw new Exception("Sve vrednosti moraju biti unesene");
                }

                SqlCommand cmd;



                if (isedit)
                {
                    cmd = new SqlCommand("UPDATE zaposleni SET Ime = @ime     ,Prezime = @prezime      ,Adresa = @adresa      ,jmbg = @jmbg       ,KarticaID= @kartica WHERE ZaposleniID=@id", con);
                    cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                    cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                    cmd.Parameters.Add("@adresa", SqlDbType.NVarChar).Value = txtAdresa.Text;
                    cmd.Parameters.Add("@jmbg", SqlDbType.NVarChar).Value = txtJMBG.Text;
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
                    cmd = new SqlCommand("INSERT INTO zaposleni VALUES (@ime,@prezime,@adresa,@jmbg,@kartica)", con);
                    
                    cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                    cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;
                    cmd.Parameters.Add("@adresa", SqlDbType.NVarChar).Value = txtAdresa.Text;
                    cmd.Parameters.Add("@jmbg", SqlDbType.NVarChar).Value = txtJMBG.Text;
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

                MessageBox.Show(ex.Message );
            }
            finally { con.Close();  }
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
