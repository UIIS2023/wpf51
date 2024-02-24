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
    /// Interaction logic for Narudzbina.xaml
    /// </summary>
    public partial class Narudzbina : Window
    {

        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Narudzbina()
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
                    cmd = new SqlCommand("UPDATE Narudzbina SET KupacID=@clan,stanje=@stanje  WHERE narudzbinaID=@id", con);

                    cmd.Parameters.Add("@clan", SqlDbType.Int).Value = cbxClan.SelectedValue;
                    cmd.Parameters.Add("@stanje", SqlDbType.NVarChar).Value = txtStanje.Text;



                    cmd.Parameters.AddWithValue("@id", ID);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Narudzbina values(@clan,@stanje)", con);
                    MessageBox.Show(cbxClan.SelectedValue.ToString());
                    cmd.Parameters.Add("@clan", SqlDbType.Int).Value = cbxClan.SelectedValue;
                    cmd.Parameters.Add("@stanje", SqlDbType.NVarChar).Value =txtStanje.Text;




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
