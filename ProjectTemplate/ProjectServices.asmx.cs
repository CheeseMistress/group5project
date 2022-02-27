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

        [WebMethod(EnableSession = true)]
        public Users[] LogOn(string id, string pass)
        {
            // SQL query
            string sqlSelect = "SELECT userid, status, admin FROM users WHERE userid=@id and password=@pass";

            // set up connection object to be ready to use our connection string 
            MySqlConnection con = new MySqlConnection(getConString());
            // set up our command object to use our connection, and our query
            MySqlCommand cmd = new MySqlCommand(sqlSelect, con);

            // tell our command to replace parameters with real values 
            // we decode them because they came to us via the web so they were encoded for transmission
            cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
            cmd.Parameters.AddWithValue("@pass", HttpUtility.UrlDecode(pass));

            //a data adapter acts like a bridge between our command object and 
            //the data we are trying to get back and put in a table object
            MySqlDataAdapter sqlDa = new MySqlDataAdapter(cmd);

            //here's the table we want to fill with the results from our query
            DataTable sqlDt = new DataTable();

            //here we go filling it!
            sqlDa.Fill(sqlDt);
            //check to see if any rows were returned.  If they were, it means it's 
            //a legit account
            if (sqlDt.Rows.Count > 0)
            {
                // if we found an account, store id and admin status 
                Session["userid"] = sqlDt.Rows[0]["userid"];
                Session["admin"] = sqlDt.Rows[0]["admin"];

                // create a list of users
                List<Users> people = new List<Users>();

                // create new user with data from datatable
                Users user = new Users();
                user.userid = sqlDt.Rows[0].ItemArray[0].ToString();
                user.status = sqlDt.Rows[0].ItemArray[1].ToString();
                user.admin = sqlDt.Rows[0].ItemArray[2].ToString();

                // add to people list
                people.Add(user);

                // return the result
                return people.ToArray();
            }
            else
            {
                return null;
            }
        }

        [WebMethod(EnableSession = true)]
        public bool logOff()
        {
            // if they log off, then we remove the session, that way if they access again later they have
            // to log back in in order for their ID to be in the session
            Session.Abandon();
            return true;
        }

        /////////////////////////////////////////////////////////////////////////
        //don't forget to include this decoration above each method that you want
        //to be exposed as a web service!
        [WebMethod(EnableSession = true)]
        public Users[] GetUsers()
        {
            string testQuery = "select * from users";

            ////////////////////////////////////////////////////////////////////////
            ///here's an example of using the getConString method!
            ////////////////////////////////////////////////////////////////////////
            MySqlConnection con = new MySqlConnection(getConString());
            ////////////////////////////////////////////////////////////////////////

            // connect and execute query 
            MySqlCommand cmd = new MySqlCommand(testQuery, con);
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            // create a data table and fill it with our data from query
            DataTable table = new DataTable() { TableName = "Users" };
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
                user.admin = table.Rows[i].ItemArray[14].ToString();

                // add to people list
                people.Add(user);
            }
            // return an array of users 
            return people.ToArray();
        }

        [WebMethod]
        public void InsertUser(string id, string pass, string first, string last, string company,
        string address, string city, string state, string code, string country, string phone,
        string email, string website, string status, string admin)
        {
            // insert statement
            string addUser = "INSERT INTO users (userid, password, firstname, lastname, companyname, address, city, state, postalcode, country, phone, email, website, status, admin) " +
                "VALUES (@id, @pass, @first, @last, @company, @address, @city, @state, @code, @country, @phone, @email, @website, @status, @admin)";

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
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@pass", HttpUtility.UrlDecode(pass));
                cmd.Parameters.AddWithValue("@first", HttpUtility.UrlDecode(first));
                cmd.Parameters.AddWithValue("@last", HttpUtility.UrlDecode(last));
                cmd.Parameters.AddWithValue("@company", HttpUtility.UrlDecode(company));
                cmd.Parameters.AddWithValue("@address", HttpUtility.UrlDecode(address));
                cmd.Parameters.AddWithValue("@city", HttpUtility.UrlDecode(city));
                cmd.Parameters.AddWithValue("@state", HttpUtility.UrlDecode(state));
                cmd.Parameters.AddWithValue("@code", HttpUtility.UrlDecode(code));
                cmd.Parameters.AddWithValue("@country", HttpUtility.UrlDecode(country));
                cmd.Parameters.AddWithValue("@phone", HttpUtility.UrlDecode(phone));
                cmd.Parameters.AddWithValue("@email", HttpUtility.UrlDecode(email));
                cmd.Parameters.AddWithValue("@website", HttpUtility.UrlDecode(website));
                cmd.Parameters.AddWithValue("@status", HttpUtility.UrlDecode(status));
                cmd.Parameters.AddWithValue("@admin", HttpUtility.UrlDecode(admin));
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
        public Users[] GetUnapprovedUsers()
        {
            // checking if they are logged in and also an admin to view account requests
            if (Session["userid"] != null && Convert.ToInt32(Session["admin"]) == 1)
            {
                string query = "select * from users where status ='0'";

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
                    user.admin = table.Rows[i].ItemArray[14].ToString();

                    // add to people list
                    unpeople.Add(user);
                }
                // return an array of users 
                return unpeople.ToArray();
            }
            else
            {
                // if they are not logged in, return empty array
                return new Users[0];
            }
        }


        [WebMethod(EnableSession = true)]
        public void ApproveUser(string id)
        {
            if (Convert.ToInt32(Session["admin"]) == 1)
            {
                // update statement
                string approveUser = "UPDATE users SET STATUS = '1' WHERE userid = @id";

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
                    cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));

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
        }

        [WebMethod(EnableSession = true)]
        public void DenyUser(string id)
        {
            if (Session["userid"] != null && Convert.ToInt32(Session["admin"]) == 1)
            {
                // delete statement
                string deleteUser = "DELETE FROM users WHERE userid = @id";

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
                    cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));

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
                cmd.Parameters.AddWithValue("@r1", HttpUtility.UrlDecode(r1));
                cmd.Parameters.AddWithValue("@r2", HttpUtility.UrlDecode(r2));
                cmd.Parameters.AddWithValue("@r3", HttpUtility.UrlDecode(r3));
                cmd.Parameters.AddWithValue("@r4", HttpUtility.UrlDecode(r4));
                cmd.Parameters.AddWithValue("@r5", HttpUtility.UrlDecode(r5));
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
        public Responses[] GetsurveyResults()
        {
            if (Session["userid"] != null)
            {
                // get session id
                var id = Session["userid"].ToString();

                string query = "SELECT responses.responseid, responses.surveyid, responses.time, responses.response1, responses.response2, responses.response3, responses.response4, responses.response5 FROM responses, surveys WHERE responses.surveyid = surveys.surveyid AND surveys.userid=" + "'" + id + "'";

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
            else
            {
                return null;
            }

        }


        [WebMethod(EnableSession = true)]
        public void InsertSurvey(string q1, string q2, string q3, string q4, string q5)
        {
            if (Session["userid"] != null)
            {
                // insert statement
                string insertResponse = "INSERT INTO surveys (userid, question1, question2, question3, question4, question5) " +
                "VALUES (@id, @q1, @q2, @q3, @q4, @q5)";

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

                    // get session id
                    var id = Session["userid"].ToString();

                    //Pass values to Parameters
                    cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                    cmd.Parameters.AddWithValue("@q1", HttpUtility.UrlDecode(q1));
                    cmd.Parameters.AddWithValue("@q2", HttpUtility.UrlDecode(q2));
                    cmd.Parameters.AddWithValue("@q3", HttpUtility.UrlDecode(q3));
                    cmd.Parameters.AddWithValue("@q4", HttpUtility.UrlDecode(q4));
                    cmd.Parameters.AddWithValue("@q5", HttpUtility.UrlDecode(q5));
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
        }

        [WebMethod(EnableSession = true)]
        public Surveys[] GetSurveyQuestionsId()
        {
            if (Session["userid"] != null)
            {
                // get session id
                var id = Session["userid"].ToString();

                string query = "select * from surveys where userid=" + "'" + id + "'";

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
                    survey.userid = table.Rows[i].ItemArray[1].ToString();
                    survey.question1 = table.Rows[i].ItemArray[2].ToString();
                    survey.question2 = table.Rows[i].ItemArray[3].ToString();
                    survey.question3 = table.Rows[i].ItemArray[4].ToString();
                    survey.question4 = table.Rows[i].ItemArray[5].ToString();
                    survey.question5 = table.Rows[i].ItemArray[6].ToString();

                    // add to list of surveys
                    surveylist.Add(survey);
                }
                // return an array of surveys
                return surveylist.ToArray();
            }
            else
            {
                return null;
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
                survey.userid = table.Rows[i].ItemArray[1].ToString();
                survey.question1 = table.Rows[i].ItemArray[2].ToString();
                survey.question2 = table.Rows[i].ItemArray[3].ToString();
                survey.question3 = table.Rows[i].ItemArray[4].ToString();
                survey.question4 = table.Rows[i].ItemArray[5].ToString();
                survey.question5 = table.Rows[i].ItemArray[6].ToString();

                // add to list of surveys
                surveylist.Add(survey);
            }
            // return an array of surveys
            return surveylist.ToArray();
        }

        [WebMethod]
        public void UpdatePassword(string id, string password)
        {
            // update statement
            string updatePassword = "UPDATE users SET PASSWORD = @password WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updatePassword, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@password", HttpUtility.UrlDecode(password));

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
        public void UpdateFirst(string id, string firstname)
        {
            // update statement
            string updateFirst = "UPDATE users SET firstname = @firstname WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updateFirst, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@firstname", HttpUtility.UrlDecode(firstname));

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
        public void UpdateLast(string id, string lastname)
        {
            // update statement
            string updateLast = "UPDATE users SET lastname = @lastname WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updateLast, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@lastname", HttpUtility.UrlDecode(lastname));

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
        public void UpdateAddress(string id, string address)
        {
            // update statement
            string updateAddress = "UPDATE users SET ADDRESS = @address WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updateAddress, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@address", HttpUtility.UrlDecode(address));

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
        public void UpdateCity(string id, string city)
        {
            // update statement
            string updateCity = "UPDATE users SET CITY = @city WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updateCity, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@city", HttpUtility.UrlDecode(city));

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
        public void UpdateState(string id, string state)
        {
            // update statement
            string updateState = "UPDATE users SET STATE = @state WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updateState, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@state", HttpUtility.UrlDecode(state));

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
        public void UpdatePostal(string id, string postalcode)
        {
            // update statement
            string updatePostal = "UPDATE users SET POSTALCODE = @postalcode WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updatePostal, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@postalcode", HttpUtility.UrlDecode(postalcode));

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
        public void UpdateCountry(string id, string country)
        {
            // update statement
            string updateCountry = "UPDATE users SET COUNTRY = @country WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updateCountry, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@country", HttpUtility.UrlDecode(country));

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
        public void UpdatePhone(string id, string phone)
        {
            // update statement
            string updatePhone = "UPDATE users SET PHONE = @phone WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updatePhone, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@phone", HttpUtility.UrlDecode(phone));

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
        public void UpdateEmail(string id, string email)
        {
            // update statement
            string updateEmail = "UPDATE users SET EMAIL = @email WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updateEmail, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@email", HttpUtility.UrlDecode(email));

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
        public void UpdateWeb(string id, string website)
        {
            // update statement
            string updateWeb = "UPDATE users SET WEBSITE = @website WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(updateWeb, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));
                cmd.Parameters.AddWithValue("@website", HttpUtility.UrlDecode(website));

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
        public void DisableAccount(string id)
        {
            // update statement
            string disableAccount = "UPDATE users SET STATUS = '0' WHERE userid = @id";

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
                MySqlCommand cmd = new MySqlCommand(disableAccount, con);

                //Pass values to Parameters
                cmd.Parameters.AddWithValue("@id", HttpUtility.UrlDecode(id));

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
    }
}
