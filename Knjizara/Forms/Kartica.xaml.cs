using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for Kartica.xaml
    /// </summary>
    public partial class Kartica : Window
    {

        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public Kartica()
        {
            InitializeComponent();
        }

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(txtVrsta.Text) ||
                    string.IsNullOrEmpty(txtPopust.Text)  )
                {

                    throw new Exception("Sve vrednosti moraju biti unesene");
                }

                SqlCommand cmd;



                if (isedit)
                {
                    cmd = new SqlCommand("UPDATE Kartica   SET Vrsta = @vrsta,popust =@popust WHERE KarticaID=@id", con);
                    
                    cmd.Parameters.Add("@vrsta", SqlDbType.NVarChar).Value = txtVrsta.Text;
                    cmd.Parameters.Add("@popust", SqlDbType.Int).Value = txtPopust.Text;

                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = ID;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO Kartica VALUES (@vrsta,@popust)", con);
                    cmd.Parameters.Add("@vrsta", SqlDbType.NVarChar).Value = txtVrsta.Text;
                    cmd.Parameters.Add("@popust", SqlDbType.Int).Value = txtPopust.Text;

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
            finally { con.Close();   }
        }
    }
}
