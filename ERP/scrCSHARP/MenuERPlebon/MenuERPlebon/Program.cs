using MenuERPlebon;
using System.Data.SqlClient;
Database.Connect();
Random random = new Random();
Console.CursorLeft = 10;
Console.WriteLine("Bienvenue dans votre ERP\n\n");

Console.WriteLine("A. Clients\n");
Console.WriteLine("B. Produits\n");
Console.WriteLine("C. Commandes\n");

string reponse = Console.ReadLine().ToLower();

bool isFirstLine = true;

if (reponse == "a")
{


    foreach (string line in File.ReadAllLines("../../../ListeClients.csv"))
    {

        if (isFirstLine)
        {
            isFirstLine = false;

        }
        else
        {
            var columns = line.Split(";");

            string firstname = columns[0];
            string lastname = columns[1];
            string companyname = columns[2];
            string address = columns[3];
            string city = columns[4];
            string county = columns[5];
            string state = columns[6];
            int zip = Convert.ToInt32(columns[7]);
            string phone1 = columns[8];
            string phone2 = columns[9];
            string email = columns[10];
            string web = columns[11];
            //string username = columns[12];
            //string password = columns[13];


            var username = "";
            if (firstname.Length > 2)
            {
                username = $"{firstname[0]}{firstname[1]}{firstname[2]}{lastname}";
            }
            else
            {
                username = $"{firstname[0]}{firstname[1]}{lastname}";
            }

            var password = "";
            for (int i = 0; i < 32; i++)
            {
                var ranChar = Convert.ToChar(random.Next(33,126));
                password = password + ranChar;
            }
            bool insorup = Database.InsertOrUpdate(username);
            if(insorup)
            {
                Database.Update(firstname, lastname, companyname, address, city, county, state, zip, phone1, phone2, email, web, username);
            }
            else
            {
                Database.InsertInto(firstname, lastname, companyname, address, city, county, state, zip, phone1, phone2, email, web, username, password);
            }
            

        }

        



            

        



    }
}