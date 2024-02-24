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
    /// Interaction logic for Knjiga.xaml
    /// </summary>
    public partial class Knjiga : Window
    {


        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Knjiga()
        {
            InitializeComponent();
            UcitajPodatke();
        }




        private void UcitajPodatke()
        {
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select ime+' '+prezime as Naziv,AutorID from Autor", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbxAutor.ItemsSource = dt.DefaultView;
            da.Dispose();
            dt.Dispose();



            da = new SqlDataAdapter("select naziv as Naziv,ZanrID from Zanr", con);
            dt = new DataTable();
            da.Fill(dt);
            cbxZanr.ItemsSource = dt.DefaultView;
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
                if (string.IsNullOrEmpty(txtNaziv.Text))
                {

                    throw new Exception("Sve vrednosti moraju biti unesene");
                }

                SqlCommand cmd;



                if (isedit)
                {
                    cmd = new SqlCommand("UPDATE Knjiga SET Naziv=@naziv,AutorID=@autor,ZanrID=@zanr WHERE KnjigaID=@id", con);

                    cmd.Parameters.Add("@naziv", SqlDbType.NVarChar).Value = txtNaziv.Text;
                    cmd.Parameters.Add("@autor", SqlDbType.Int).Value = cbxAutor.SelectedValue;
                    cmd.Parameters.Add("@zanr", SqlDbType.Int).Value = cbxZanr.SelectedValue;



                    cmd.Parameters.AddWithValue("@id", ID);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Knjiga VALUES (@naziv,@autor,@zanr)", con);

                    
                    cmd.Parameters.Add("@naziv", SqlDbType.NVarChar).Value = txtNaziv.Text;
                    cmd.Parameters.Add("@autor", SqlDbType.Int).Value = cbxAutor.SelectedValue;
                    cmd.Parameters.Add("@zanr", SqlDbType.Int).Value = cbxZanr.SelectedValue;




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
