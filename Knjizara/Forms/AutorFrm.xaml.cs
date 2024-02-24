using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;

namespace Knjizara.Forms
{
    /// <summary>
    /// Interaction logic for AutorFrm.xaml
    /// </summary>
    public partial class AutorFrm : Window
    {
        public bool isedit = false;
        public int ID;

        SqlConnection con = Konekcija.KreirajKonekciju();
        public AutorFrm()
        {
            InitializeComponent();
        }



        

        private void btnOtkazi_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtIme.Text) ||
                    string.IsNullOrEmpty(txtPrezime.Text) 
                    )
                {

                    throw new Exception("Sve vrednosti moraju biti unesene");
                }


                SqlCommand cmd;
               


                if (isedit)
                {
                    cmd = new SqlCommand("UPDATE [dbo].[Autor]   SET [Ime] = @ime      ,[Prezime] =@prezime WHERE AutorID=@id", con);

                    cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                    cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;



                    cmd.Parameters.Add("@id", SqlDbType.NVarChar).Value = ID;


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                else
                {
                    cmd = new SqlCommand("INSERT INTO [dbo].[Autor]([Ime],[Prezime])VALUES (@ime,@prezime)",con);
                    cmd.Parameters.Add("@ime", SqlDbType.NVarChar).Value = txtIme.Text;
                    cmd.Parameters.Add("@prezime", SqlDbType.NVarChar).Value = txtPrezime.Text;

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    cmd.Dispose();
                    con.Dispose();


                }
                this.Close();




            }
            catch (Exception ex)
            {
                
            MessageBox.Show(ex.Message);
            }
            finally { con.Close();  }
        }
    }
}
