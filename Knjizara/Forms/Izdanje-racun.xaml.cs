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
    /// Interaction logic for Izdanje_racun.xaml
    /// </summary>
    public partial class Izdanje_racun : Window
    {

        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Izdanje_racun()
        {
            InitializeComponent();
            UcitajPodatke();
        }

        private void UcitajPodatke()
        {
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select racunID as Naziv,racunID as ID from racun", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbxRacun.ItemsSource = dt.DefaultView;
            da.Dispose();
            dt.Dispose();



            da = new SqlDataAdapter("select Izdanje.naziv Naziv,IzdanjeID as ID from Izdanje", con);
            dt = new DataTable();
            da.Fill(dt);
            cbxIzdanje.ItemsSource = dt.DefaultView;
            da.Dispose();
            dt.Dispose();
            con.Close();
        }
        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {


                SqlCommand cmd;



                if (isedit)
                {
                    cmd = new SqlCommand("UPDATE [racun_izdanje] SET izdanjeID=@izdanje,racunID=@racun,brojKnjiga=@broj  WHERE racun_izdanje_ID=@id", con);

                    cmd.Parameters.Add("@izdanje", SqlDbType.Int).Value = cbxIzdanje.SelectedValue;
                    cmd.Parameters.Add("@racun", SqlDbType.Int).Value = cbxRacun.SelectedValue;
                    cmd.Parameters.Add("@broj", SqlDbType.NVarChar).Value = txtBroj.Text;



                    cmd.Parameters.AddWithValue("@id", ID);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO [racun_izdanje] VALUES (@izdanje,@racun,@broj)", con);



                    cmd.Parameters.Add("@izdanje", SqlDbType.Int).Value = cbxIzdanje.SelectedValue;
                    cmd.Parameters.Add("@racun", SqlDbType.Int).Value = cbxRacun.SelectedValue;
                    cmd.Parameters.Add("@broj", SqlDbType.NVarChar).Value = txtBroj.Text;





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

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
