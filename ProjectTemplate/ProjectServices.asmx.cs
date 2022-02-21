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
                user.status = table.Rows[i].ItemArray[13].ToString();

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


                // connect and execute query 
                MySqlCommand cmd = new MySqlCommand(addUser, con);


                //Pass values to Parameters
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
        public void InsertResponse(int surveyid, string r1, string r2, string r3, string r4, string r5)
        {
            // insert statement
            string insertResponse = "INSERT INTO responses (surveyid, time, response1, response2, response3, response4, response5) " +
                "VALUES (@surveyid, @time, @r1, @r2, @r3, @r4, @r5)";

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
                cmd.Parameters.AddWithValue("@surveyid", surveyid);
                cmd.Parameters.AddWithValue("@time", now);
                cmd.Parameters.AddWithValue("@r1", r1);
                cmd.Parameters.AddWithValue("@r2", r2);
                cmd.Parameters.AddWithValue("@r3", r3);
                cmd.Parameters.AddWithValue("@r4", r4);
                cmd.Parameters.AddWithValue("@r5", r5);
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
        public Responses[] GetSurveyResults()
        {
            string query = "select * from responses";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            // connect and execute query 
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            // create a data table and fill it with our data from query
            DataTable table = new DataTable() { TableName = "resultstable" };
            adapter.Fill(table);

            // create a list of survey results
            List<Responses> results = new List<Responses>();

            // loop to create a new user based on # of rows in data table
            for (int i = 0; i < table.Rows.Count; i++)
            {
                // create new user with data from datatable
                Responses result = new Responses();
                result.responseid = table.Rows[i].ItemArray[0].ToString();
                result.surveyid = table.Rows[i].ItemArray[1].ToString();
                result.time = table.Rows[i].ItemArray[2].ToString();
                result.response1 = table.Rows[i].ItemArray[3].ToString();
                result.response2 = table.Rows[i].ItemArray[4].ToString();
                result.response3 = table.Rows[i].ItemArray[5].ToString();
                result.response4 = table.Rows[i].ItemArray[6].ToString();
                result.response5 = table.Rows[i].ItemArray[7].ToString();

                // add to list of surveys
                results.Add(result);
            }
            // return an array of users 
            return results.ToArray();
        }

        [WebMethod]
        public void InsertSurvey(string q1, string q2, string q3, string q4, string q5)
        {
            // insert statement
            string insertResponse = "INSERT INTO surveys (question1, question2, question3, question4, question5) " +
                "VALUES (@q1, @q2, @q3, @q4, @q5)";

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

                //Pass values to Parameters
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


        [WebMethod]
        public Surveys[] GetSurveyQuestions()
        {
            string query = "select * from surveys";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            // connect and execute query 
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            // create a data table and fill it with our data from query
            DataTable table = new DataTable() { TableName = "surveystable" };
            adapter.Fill(table);

            // create a list of survey questions
            List<Surveys> surveylist = new List<Surveys>();

            // loop to create a new survey based on # of rows in data table
            for (int i = 0; i < table.Rows.Count; i++)
            {
                // create new survey with data from datatable
                Surveys survey = new Surveys();
                survey.surveyid = table.Rows[i].ItemArray[0].ToString();
                survey.question1 = table.Rows[i].ItemArray[1].ToString();
                survey.question2 = table.Rows[i].ItemArray[2].ToString();
                survey.question3 = table.Rows[i].ItemArray[3].ToString();
                survey.question4 = table.Rows[i].ItemArray[4].ToString();
                survey.question5 = table.Rows[i].ItemArray[5].ToString();

                // add to list of surveys
                surveylist.Add(survey);
            }
            // return an array of surveys
            return surveylist.ToArray();
        }

    }
}
