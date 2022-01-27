using System;
using System.Data.SqlClient;

namespace Utilities
{
    public static class DataManipulation
    {

        public static void connectDatabase()
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            csb.DataSource = @"(LocalDB)\MSSQLLocalDB";
            csb.InitialCatalog = "UsersDB";
            csb.IntegratedSecurity = true;
            string connectionString = csb.ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("The application has connected to the database successfully.");
            }
            Console.ReadKey();
        }
    }
}
