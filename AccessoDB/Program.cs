using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;


namespace AccessoDB
{
    class Program
    {
        private static int CreateCommand1(string queryString, string newCodice, string conectionString)
        {
            using (SqlConnection connection = new SqlConnection(conectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@newCOD", SqlDbType.NVarChar);
                    command.Parameters["newCOD"].Value = newCodice;
                    try
                    {
                        command.Connection.Open();
                        int affectedRows = command.ExecuteNonQuery(); // non retorna rows
                        MessageBox.Show($"Columns {affectedRows}");
                        return affectedRows;    
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return -1;
                    }
                }

            }
        }
        //
        private static string CommandExecureQuery(string codfam, string desfam, string conectionString)
        {
            string sql =  "INSERT INTO DEMOFAM_ARTI(FACODICE,FADESCRI,cpccchk) VALUES " +
                "(@CODICE,@DESCRI,'aaaaaaaaaa');" +
                " SELECT MAX(FACODICE) FROM DEMOFAM_ARTI;";

            using (SqlConnection connection = new SqlConnection(conectionString))
            {
                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.Add("@CODICE", SqlDbType.NVarChar);
                    command.Parameters.Add("@DESCRI", SqlDbType.NVarChar);
                    
                    command.Parameters["@CODICE"].Value = codfam;
                    command.Parameters["@DESCRI"].Value = desfam;
                    command.CommandType = CommandType.Text;
                    try
                    {
                        command.Connection.Open();
                        string maxCodFam = (string)command.ExecuteScalar(); // non retorna rows
                        MessageBox.Show($"Max cod fami {maxCodFam}");
                        return maxCodFam;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return "null";
                    }
                }

            }
        }
        static void Main(string[] args)
        {
            var cnn = ConfigurationManager.ConnectionStrings["DemoAHR"].ConnectionString;

           // CreateCommand1("dbo.creaTest", cnn);  // Storeprocedure

            CommandExecureQuery("ZvA", "Descri Zaa", cnn);
            CommandExecureQuery("ZlA", "Descri Zaa", cnn);

            //
        }
    }
}
