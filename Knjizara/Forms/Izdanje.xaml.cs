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
    /// Interaction logic for Izdanje.xaml
    /// </summary>
    public partial class Izdanje : Window
    {


        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();

        public Izdanje()
        {
            InitializeComponent();
            UcitajPodatke();
        }



        private void UcitajPodatke()
        {
            con.Open();

            SqlDataAdapter da = new SqlDataAdapter("select Naziv,KnjigaID from Knjiga", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cbxKnjiga.ItemsSource = dt.DefaultView;
            da.Dispose();
            dt.Dispose();



            da = new SqlDataAdapter("select  Naziv,DobavljacID  from Dobavljac", con);
            dt = new DataTable();
            da.Fill(dt);
            cbxDobavljac.ItemsSource = dt.DefaultView;
            da.Dispose();
            dt.Dispose();
            con.Close();
        }
        

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtKuca.Text) ||
                    string.IsNullOrEmpty(txtGodina.Text) ||
                    string.IsNullOrEmpty(txtCena.Text) ||
                    string.IsNullOrEmpty(txtNaziv.Text))
                {

                    throw new Exception("Sve vrednosti moraju biti unesene");
                }
                SqlCommand cmd;
                if (isedit)
                {
                    cmd = new SqlCommand("UPDATE Izdanje SET Naziv=@naziv,Cena=@cena,GodinaIzdanja=@godina,IzdavajucaKuca=@kuca,KnjigaID=@knjiga,DobavljacID=@dobavljac  WHERE IzdanjeID=@id", con);


                    cmd.Parameters.Add("@naziv", SqlDbType.NVarChar).Value = txtNaziv.Text;
                    cmd.Parameters.Add("@cena", SqlDbType.Real).Value = txtCena.Text;
                    cmd.Parameters.Add("@godina", SqlDbType.Int).Value = txtGodina.Text;
                    cmd.Parameters.Add("@knjiga", SqlDbType.Int).Value = cbxKnjiga.SelectedValue;
                    cmd.Parameters.Add("@kuca", SqlDbType.NVarChar).Value = txtKuca.Text;
                    cmd.Parameters.Add("@dobavljac", SqlDbType.Int).Value = cbxDobavljac.SelectedValue;
                    cmd.Parameters.AddWithValue("@id", ID);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Izdanje VALUES (@naziv,@cena,@godina,@knjiga,@kuca,@dobavljac)", con);
                    cmd.Parameters.Add("@naziv", SqlDbType.NVarChar).Value = txtNaziv.Text;
                    cmd.Parameters.Add("@cena", SqlDbType.Real).Value = txtCena.Text;
                    cmd.Parameters.Add("@godina", SqlDbType.Int).Value = txtGodina.Text;
                    cmd.Parameters.Add("@knjiga", SqlDbType.Int).Value = cbxKnjiga.SelectedValue;
                    cmd.Parameters.Add("@kuca", SqlDbType.NVarChar).Value = txtKuca.Text;
                    cmd.Parameters.Add("@dobavljac", SqlDbType.Int).Value = cbxDobavljac.SelectedValue;
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
