using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProjectTemplate
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]


    public class ProjectServices : System.Web.Services.WebService
    {
        ////////////////////////////////////////////////////////////////////////
        ///replace the values of these variables with your database credentials
        ////////////////////////////////////////////////////////////////////////
        private string dbID = "spring2022team5";
        private string dbPass = "spring2022team5";
        private string dbName = "spring2022team5";
        ////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////////////////
        ///call this method anywhere that you need the connection string!
        ////////////////////////////////////////////////////////////////////////
        private string getConString()
        {
            return "SERVER=107.180.1.16; PORT=3306; DATABASE=" + dbName + "; UID=" + dbID + "; PASSWORD=" + dbPass;
        }
        ////////////////////////////////////////////////////////////////////////


        /////////////////////////////////////////////////////////////////////////
        //don't forget to include this decoration above each method that you want
        //to be exposed as a web service!
        [WebMethod]
        public Users[] GetUsers()
        {
            string testQuery = "select * from employers";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            // connect and execute query 
            MySqlCommand cmd = new MySqlCommand(testQuery, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            // create a data table and fill it with our data from query
            DataTable table = new DataTable() { TableName = "Employers" };
            adapter.Fill(table);

            // create a list of users
            List<Users> people = new List<Users>();

            // loop to create a new user based on # of rows in data table
            for (int i = 0; i < table.Rows.Count; i++)
            {
                // create new user with data from datatable
                Users user = new Users();
                user.userid = table.Rows[i].ItemArray[0].ToString();
                user.password = table.Rows[i].ItemArray[1].ToString();
                user.firstname = table.Rows[i].ItemArray[2].ToString();
                user.lastname = table.Rows[i].ItemArray[3].ToString();
                user.companyname = table.Rows[i].ItemArray[4].ToString();
                user.address = table.Rows[i].ItemArray[5].ToString();
                user.city = table.Rows[i].ItemArray[6].ToString();
                user.state = table.Rows[i].ItemArray[7].ToString();
                user.postalcode = table.Rows[i].ItemArray[8].ToString();
                user.country = table.Rows[i].ItemArray[9].ToString();
                user.phone = table.Rows[i].ItemArray[10].ToString();
                user.email = table.Rows[i].ItemArray[11].ToString();
                user.website = table.Rows[i].ItemArray[12].ToString();

                // add to people list
                people.Add(user);
            }
            // return an array of users 
            return people.ToArray();
        }

    }
}
