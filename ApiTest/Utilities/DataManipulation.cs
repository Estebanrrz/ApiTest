/// < summary >
/// Utilitie to recreate  code  done on  API https://reqres.in 
// </summary> 
using ServiceStack;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTest.Utilities
{
    public static class DataManipulation
    {
        /// <summary>
        /// Create user On Database
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="name">USer name</param>
        /// <param name="salary">User salary</param>
        public static void CreateUserDatabase(string id, string name, string salary)
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Crami151\source\repos\ApiTest\Utilities\UsersDB.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                Console.WriteLine("The application has connected to the database successfully.");

                SqlCommand command = new SqlCommand($"INSERT dbo.Users (id,name,salary) VALUES ({id}, '{name}',{salary})", conn);

                command.ExecuteNonQuery();
                command.Dispose();
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder(); for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            finally
            {
                conn.Close();
            }

        }

        /// <summary>
        /// Delete User by id
        /// </summary>
        /// <param name="id">user id to be deleted</param>
        public static void DeleteUserDatabaseById(string id)
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Crami151\source\repos\ApiTest\Utilities\UsersDB.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"DELETE FROM dbo.Users WHERE id = {id}", conn);
                command.ExecuteNonQuery();
                command.Dispose();
            }

            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder(); for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Select user by id
        /// </summary>
        /// <param name="id">User id to be query</param>
        public static string SelectUserByIDDatabase(string id)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Crami151\source\repos\ApiTest\Utilities\UsersDB.mdf;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            string output = string.Empty;

            try
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM dbo.Users WHERE id = {id}", conn);
                // fetch all the rows
                // from the demo table
                SqlDataReader dReader = command.ExecuteReader();

                // for one by one reading row
                while (dReader.Read())
                {
                    output = $"{dReader.GetValue(0)};{dReader.GetValue(1).ToString().Trim()};{dReader.GetValue(2)}";
                }
                command.Dispose();
            }

            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder(); for (int i = 0; i < ex.Errors.Count; i++)
                {
                    errorMessages.Append("Index #" + i + "\n" +
                        "Message: " + ex.Errors[i].Message + "\n" +
                        "LineNumber: " + ex.Errors[i].LineNumber + "\n" +
                        "Source: " + ex.Errors[i].Source + "\n" +
                        "Procedure: " + ex.Errors[i].Procedure + "\n");
                }
                Console.WriteLine(errorMessages.ToString());
            }
            finally
            {
                conn.Close();
            }
            return output;
        }
    }
}
