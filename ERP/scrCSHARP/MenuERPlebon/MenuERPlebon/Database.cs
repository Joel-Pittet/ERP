using MySqlConnector;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace MenuERPlebon
{
    class Database
    {
        static string connectString = "SERVER=localhost; port=6033; Database=db_ERP; Uid=root; Pwd=root;";
        static MySqlConnection db;

        public static void Connect()
        {
            if (db == null || db.State != ConnectionState.Open)
            {
                db = new MySqlConnection(connectString);
                db.Open();

                //Console.WriteLine("Connection Open!");
            }
        }
        static public bool InsertInto(string name, string lastname, string companyname, string adresse, string city, string county, string state, int zip, string phone1, string phone2, string email, string web, string username, string password)
        {

            using (MySqlCommand cmd = db.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                cmd.CommandText = $"INSERT INTO t_client (`cliFirstName`,`cliLastName`,`cliCompanyName`,`cliAddress`,`cliCity`,`cliCounty`,`cliState`,`cliZip`,`cliPhone1`,`cliPhone2`,`cliEmail`,`cliWeb`,`cliUsername`,`cliPassword`) " +
                "VALUES (" +
                "@name," +
                "@lastname," +
                "@companyname," +
                "@adresse," +
                "@city," +
                "@county," +
                "@state," +
                "@zip," +
                "@phone1," +
                "@phone2," +
                "@email," +
                "@web," +
                "@username," +
                "'" + GetHash(password) + "')";

                //Use bind parameters for security reasons
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@lastName", lastname);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@companyname", companyname);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@county", county);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@phone1", phone1);
                cmd.Parameters.AddWithValue("@phone2", phone2);
                cmd.Parameters.AddWithValue("@web", web);
                cmd.Parameters.AddWithValue("@username", username);


                return cmd.ExecuteNonQuery() == 1;

            }

        }

        static string GetHash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private static MySqlDataReader Query(string sql)
        {
            using (MySqlCommand cmd = db.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                cmd.CommandText = sql;

                return cmd.ExecuteReader();

            }

        }
        static public bool InsertOrUpdate(string username)
        {
            var reader = Query("SELECT * FROM t_client WHERE cliUsername='" + username + "'");
            bool result = false;

            // Always call Read before accessing data.
            while (reader.Read())
            {

                if ((string)reader["cliUsername"] == username)
                {
                    result = true;
                    break;
                }

            }

            // always call Close when done reading.
            reader.Close();

            return result;
        }



        static public bool Update(string name, string lastname, string companyname, string adresse, string city, string county, string state, int zip, string phone1, string phone2, string email, string web, string username)
        {

            using (MySqlCommand cmd = db.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 300;
                cmd.CommandText = $"UPDATE t_client SET `cliFirstName` = @name,`cliLastName`= @lastname,`cliCompanyName`=@companyname,`cliAddress`=@adresse,`cliCity`=@city,`cliCounty`=@county,`cliState`=@state,`cliZip`=@zip,`cliPhone1`=@phone1,`cliPhone2`=@phone2,`cliWeb`=@web,`cliUsername`=@username WHERE `cliEmail` = @email";

                //Use bind parameters for security reasons
                cmd.Prepare();
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@lastName", lastname);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@companyname", companyname);
                cmd.Parameters.AddWithValue("@adresse", adresse);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@county", county);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@zip", zip);
                cmd.Parameters.AddWithValue("@phone1", phone1);
                cmd.Parameters.AddWithValue("@phone2", phone2);
                cmd.Parameters.AddWithValue("@web", web);
                cmd.Parameters.AddWithValue("@username", username);


                return cmd.ExecuteNonQuery() == 1;

            }

        }




















    }
}