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
    /// Interaction logic for Zanr.xaml
    /// </summary>
    public partial class Zanr : Window
    {

        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Zanr()
        {
            InitializeComponent();
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
                    cmd = new SqlCommand("UPDATE [dbo].[Zanr]   SET [Naziv] = @naziv WHERE ZanrID=@id", con);
                    cmd.Parameters.Add("@naziv", SqlDbType.NVarChar).Value = txtNaziv.Text;

                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = ID;




                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO [dbo].[Zanr](Naziv)VALUES (@naziv)", con);

                    cmd.Parameters.Add("@naziv", SqlDbType.NVarChar).Value = txtNaziv.Text;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                MessageBox.Show("Uspesno");
                this.Close();



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
