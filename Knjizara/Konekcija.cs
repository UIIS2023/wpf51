using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Knjizara
{
    internal class Konekcija
    {



        public static SqlConnection KreirajKonekciju()
        {

            SqlConnectionStringBuilder consb = new SqlConnectionStringBuilder
            {
                DataSource = @"ANDRIJA",
                InitialCatalog = "Knjizara2",
                IntegratedSecurity = true,

            };


            SqlConnection con = new SqlConnection(consb.ToString());



            return con;
        }
    }
}
