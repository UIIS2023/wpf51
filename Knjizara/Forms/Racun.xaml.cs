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

namespace Knjizara.Forms
{
    /// <summary>
    /// Interaction logic for Racun.xaml
    /// </summary>
    /// 

    public partial class Racun : Window
    {

        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Racun()
        {
            InitializeComponent();
            UcitajPodatke();
        }


        private void UcitajPodatke()
        {
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select ime+prezime as Naziv,ClanID as ID from Clan", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbxClan.ItemsSource = dt.DefaultView;
            da.Dispose();
            dt.Dispose();
            


            da = new SqlDataAdapter("select ime+prezime as Naziv,zaposleniID as ID from Zaposleni", con);
             dt = new DataTable();
            da.Fill(dt);
            cbxZaposleni.ItemsSource = dt.DefaultView;
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
                

                SqlCommand cmd;



                if (isedit)
                {
                    cmd = new SqlCommand("UPDATE Racun SET clanID = @clan     ,ZaposleniID = @zaposleni  WHERE RacunID=@id", con);

                    cmd.Parameters.Add("@clan", SqlDbType.NVarChar).Value = cbxClan.SelectedValue;
                    cmd.Parameters.Add("@zaposleni", SqlDbType.NVarChar).Value = cbxZaposleni.SelectedValue;



                    cmd.Parameters.AddWithValue("@id", ID);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Racun VALUES (@clan,@zaposleni,CURRENT_TIMESTAMP)", con);

                    cmd.Parameters.Add("@clan", SqlDbType.NVarChar).Value = cbxClan.SelectedValue;
                    cmd.Parameters.Add("@zaposleni", SqlDbType.NVarChar).Value = cbxZaposleni.SelectedValue;
                    



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
