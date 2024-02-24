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
    /// Interaction logic for Izdanje_Narudzbina.xaml
    /// </summary>
    public partial class Izdanje_Narudzbina : Window
    {

        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Izdanje_Narudzbina()
        {
            InitializeComponent();
            UcitajPodatke();
        }


        private void UcitajPodatke()
        {
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select NarudzbinaID as Naziv,NarudzbinaID as ID from Narudzbina", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbxNarudzbina.ItemsSource = dt.DefaultView;
            da.Dispose();
            dt.Dispose();



            da = new SqlDataAdapter("select Izdanje.naziv as Naziv,IzdanjeID as ID from Izdanje", con);
            dt = new DataTable();
            da.Fill(dt);
            cbxIzdanje.ItemsSource = dt.DefaultView;
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
                    cmd = new SqlCommand("UPDATE izdanje_narudzbina SET izdanjeID=@izdanje,narudzbinaID=@narudzbina  WHERE izdanje_narudzbinaID=@id", con);

                    cmd.Parameters.Add("@izdanje", SqlDbType.Int).Value = cbxIzdanje.SelectedValue;
                    cmd.Parameters.Add("@narudzbina", SqlDbType.Int).Value = cbxNarudzbina.SelectedValue;



                    cmd.Parameters.AddWithValue("@id", ID);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO izdanje_narudzbina VALUES (@izdanje,@narudzbina)", con);


                   
                    cmd.Parameters.Add("@izdanje", SqlDbType.Int).Value = cbxIzdanje.SelectedValue;
                    cmd.Parameters.Add("@narudzbina", SqlDbType.Int).Value = cbxNarudzbina.SelectedValue;




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
