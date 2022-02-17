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

        [WebMethod]
        public void InsertUser(string id, string pass, string first, string last, string company,
        string address, string city, string state, int code, string country, string phone,
        string email, string website, string status)
        {
            // insert statement
            string addUser = "INSERT INTO employers (userid, password, firstname, lastname, companyname, address, city, state, postalcode, country, phone, email, website, status) " +
                "VALUES (@id, @pass, @first, @last, @company, @address, @city, @state, @code, @country, @phone, @email, @website, @status)";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            try
            {
                // open connection
                con.Open();


                // set up the command
                MySqlCommand cmd = new MySqlCommand(addUser, con);


                // Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@pass", pass);
                cmd.Parameters.AddWithValue("@first", first);
                cmd.Parameters.AddWithValue("@last", last);
                cmd.Parameters.AddWithValue("@company", company);
                cmd.Parameters.AddWithValue("@address", address);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@state", state);
                cmd.Parameters.AddWithValue("@code", code);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@website", website);
                cmd.Parameters.AddWithValue("@status", status);

                // execute 
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // close connection
                con.Close();
            }
            finally
            {
                // close connection
                con.Close();
            }
        }


        [WebMethod]
        public Users[] GetUnapprovedUsers()
        {
            string query = "select * from employers where status ='0'";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            // connect and execute query 
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            // create a data table and fill it with our data from query
            DataTable table = new DataTable() { TableName = "UnapprovedUsers" };
            adapter.Fill(table);

            // create a list of users
            List<Users> unpeople = new List<Users>();

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
                user.status = table.Rows[i].ItemArray[13].ToString();

                // add to people list
                unpeople.Add(user);
            }
            // return an array of users 
            return unpeople.ToArray();
        }


        [WebMethod]
        public void ApproveUser(string id)
        {
            // update statement
            string approveUser = "UPDATE employers SET STATUS = '1' WHERE userid = @id";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            try
            {
                // open connection
                con.Open();


                // connect and execute query 
                MySqlCommand cmd = new MySqlCommand(approveUser, con);


                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", id);

                // execute command
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // close connection
                con.Close();
            }
            finally
            {
                // close connection
                con.Close();
            }
        }

        [WebMethod]
        public void DenyUser(string id)
        {
            // delete statement
            string deleteUser = "DELETE FROM employers WHERE userid = @id";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            try
            {
                // open connection
                con.Open();


                // connect and execute query 
                MySqlCommand cmd = new MySqlCommand(deleteUser, con);


                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", id);

                // execute command
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // close connection
                con.Close();
            }
            finally
            {
                // close connection
                con.Close();
            }
        }

        [WebMethod]
        public void InsertResponse(int q1, string q2, int q3, string q4, int q5)
        {
            // insert statement
            string insertResponse = "INSERT INTO responses (time, question1, question2, question3, question4, question5) " +
                "VALUES (@time, @q1, @q2, @q3, @q4, @q5)";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            try
            {
                // open connection
                con.Open();


                // connect and execute query 
                MySqlCommand cmd = new MySqlCommand(insertResponse, con);


                // get current time
                DateTime now = DateTime.Now;

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@time", now);
                cmd.Parameters.AddWithValue("@q1", q1);
                cmd.Parameters.AddWithValue("@q2", q2);
                cmd.Parameters.AddWithValue("@q3", q3);
                cmd.Parameters.AddWithValue("@q4", q4);
                cmd.Parameters.AddWithValue("@q5", q5);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // close connection
                con.Close();
            }
            finally
            {
                // close connection
                con.Close();
            }
        }

        [WebMethod(EnableSession = true)]
        /////////////////////////////////////////////////////////////////////////
        public string[] TestConnection()
        {
            try
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

                // creating a string array of users grabbing data from datatable rows
                string[] users = new string[10];
                users[0] = table.Rows[0].ItemArray[0].ToString();
                users[1] = table.Rows[0].ItemArray[1].ToString();
                users[2] = table.Rows[1].ItemArray[0].ToString();
                users[3] = table.Rows[1].ItemArray[1].ToString();
                users[4] = table.Rows[2].ItemArray[0].ToString();
                users[5] = table.Rows[2].ItemArray[1].ToString();
                users[6] = table.Rows[3].ItemArray[0].ToString();
                users[7] = table.Rows[3].ItemArray[1].ToString();
                users[8] = table.Rows[4].ItemArray[0].ToString();
                users[9] = table.Rows[4].ItemArray[1].ToString();

                // return the array
                return users;

                /* here are all of the columns and rows if need in the future for copy/paste
                // columns
                string useridcolumn = table.Columns[0].ColumnName.ToString();
                string passwordcolumn = table.Columns[1].ColumnName.ToString();
                string firstnamecolumn = table.Columns[2].ColumnName.ToString();
                string lastnamecolumn = table.Columns[3].ColumnName.ToString();
                string companynamecolumn = table.Columns[4].ColumnName.ToString();
                string addresscolumn = table.Columns[5].ColumnName.ToString();
                string citycolumn = table.Columns[6].ColumnName.ToString();
                string statecolumn = table.Columns[7].ColumnName.ToString();
                string postalcodecolumn = table.Columns[8].ColumnName.ToString();
                string countrycolumn = table.Columns[9].ColumnName.ToString();
                string phonecolumn = table.Columns[10].ColumnName.ToString();
                string websitecolumn = table.Columns[11].ColumnName.ToString();
            
                // employer1
                string userid1 = table.Rows[0].ItemArray[0].ToString();
                string password1 = table.Rows[0].ItemArray[1].ToString();
                string firstname1 = table.Rows[0].ItemArray[2].ToString();
                string lastname1 = table.Rows[0].ItemArray[3].ToString();
                string companyname1 = table.Rows[0].ItemArray[4].ToString();
                string address1 = table.Rows[0].ItemArray[5].ToString();
                string city1 = table.Rows[0].ItemArray[6].ToString();
                string state1 = table.Rows[0].ItemArray[7].ToString();
                string postalcode1 = table.Rows[0].ItemArray[8].ToString();
                string country1 = table.Rows[0].ItemArray[9].ToString();
                string phone1 = table.Rows[0].ItemArray[10].ToString();
                string website1 = table.Rows[0].ItemArray[11].ToString();
                // employer2
                string userid2 = table.Rows[1].ItemArray[0].ToString();
                string password2 = table.Rows[1].ItemArray[1].ToString();
                string firstname2 = table.Rows[1].ItemArray[2].ToString();
                string lastname2 = table.Rows[1].ItemArray[3].ToString();
                string companyname2 = table.Rows[1].ItemArray[4].ToString();
                string address2 = table.Rows[1].ItemArray[5].ToString();
                string city2 = table.Rows[1].ItemArray[6].ToString();
                string state2 = table.Rows[1].ItemArray[7].ToString();
                string postalcode2 = table.Rows[1].ItemArray[8].ToString();
                string country2 = table.Rows[1].ItemArray[9].ToString();
                string phone2 = table.Rows[1].ItemArray[10].ToString();
                string website2 = table.Rows[1].ItemArray[11].ToString();
                // employer3
                string userid3 = table.Rows[2].ItemArray[0].ToString();
                string password3 = table.Rows[2].ItemArray[1].ToString();
                string firstname3 = table.Rows[2].ItemArray[2].ToString();
                string lastname3 = table.Rows[2].ItemArray[3].ToString();
                string companyname3 = table.Rows[2].ItemArray[4].ToString();
                string address3 = table.Rows[2].ItemArray[5].ToString();
                string city3 = table.Rows[2].ItemArray[6].ToString();
                string state3 = table.Rows[2].ItemArray[7].ToString();
                string postalcode3 = table.Rows[2].ItemArray[8].ToString();
                string country3 = table.Rows[2].ItemArray[9].ToString();
                string phone3 = table.Rows[2].ItemArray[10].ToString();
                string website3 = table.Rows[2].ItemArray[11].ToString();
                // employer4
                string userid4 = table.Rows[3].ItemArray[0].ToString();
                string password4 = table.Rows[3].ItemArray[1].ToString();
                string firstname4 = table.Rows[3].ItemArray[2].ToString();
                string lastname4 = table.Rows[3].ItemArray[3].ToString();
                string companyname4 = table.Rows[3].ItemArray[4].ToString();
                string address4 = table.Rows[3].ItemArray[5].ToString();
                string city4 = table.Rows[3].ItemArray[6].ToString();
                string state4 = table.Rows[3].ItemArray[7].ToString();
                string postalcode4 = table.Rows[3].ItemArray[8].ToString();
                string country4 = table.Rows[3].ItemArray[9].ToString();
                string phone4 = table.Rows[3].ItemArray[10].ToString();
                string website4 = table.Rows[3].ItemArray[11].ToString();
                // employer5
                string userid5 = table.Rows[4].ItemArray[0].ToString();
                string password5 = table.Rows[4].ItemArray[1].ToString();
                string firstname5 = table.Rows[4].ItemArray[2].ToString();
                string lastname5 = table.Rows[4].ItemArray[3].ToString();
                string companyname5 = table.Rows[4].ItemArray[4].ToString();
                string address5 = table.Rows[4].ItemArray[5].ToString();
                string city5 = table.Rows[4].ItemArray[6].ToString();
                string state5 = table.Rows[4].ItemArray[7].ToString();
                string postalcode5 = table.Rows[4].ItemArray[8].ToString();
                string country5 = table.Rows[4].ItemArray[9].ToString();
                string phone5 = table.Rows[4].ItemArray[10].ToString();
                string website5 = table.Rows[4].ItemArray[11].ToString();
                    */
            }
            catch (Exception e)
            {
                string[] error = new string[1];
                error[0] = e.Message;
                return error;
            }
        }
    }
}