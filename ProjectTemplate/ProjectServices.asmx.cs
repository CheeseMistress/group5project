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
		private string getConString() {
			return "SERVER=107.180.1.16; PORT=3306; DATABASE=" + dbName+"; UID=" + dbID + "; PASSWORD=" + dbPass;
		}
		////////////////////////////////////////////////////////////////////////



		/////////////////////////////////////////////////////////////////////////
		//don't forget to include this decoration above each method that you want
		//to be exposed as a web service!
		[WebMethod(EnableSession = true)]
		/////////////////////////////////////////////////////////////////////////
		public string TestConnection()
		{
			try
			{
				string testQuery = "select * from employers";

				////////////////////////////////////////////////////////////////////////
				///here's an example of using the getConString method!
				////////////////////////////////////////////////////////////////////////
				MySqlConnection con = new MySqlConnection(getConString());
				////////////////////////////////////////////////////////////////////////

				MySqlCommand cmd = new MySqlCommand(testQuery, con);
				MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
				DataTable table = new DataTable() { TableName = "Employers" };
				adapter.Fill(table);

                
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

                // return test query
                return "Employer 1 \n" + useridcolumn + ": " + userid1 + "\n"
                    + passwordcolumn + ": " + password1 + "\n"
                    + firstnamecolumn + ": " + firstname1 + "\n"
                    + lastnamecolumn + ": " + lastname1 + "\n"
                    + companynamecolumn + ": " + companyname1 + "\n"
                    + addresscolumn + ": " + address1 + "\n"
                    + citycolumn + ": " + city1 + "\n"
                    + statecolumn + ": " + state1 + "\n"
                    + postalcodecolumn + ": " + postalcode1 + "\n"
                    + countrycolumn + ": " + country1 + "\n"
                    + phonecolumn + ": " + phone1 + "\n"
                    + websitecolumn + ": " + website1 + "\n\n"
                    + "Employer 2 \n" + useridcolumn + ": " + userid2 + "\n"
                    + passwordcolumn + ": " + password2 + "\n"
                    + firstnamecolumn + ": " + firstname2 + "\n"
                    + lastnamecolumn + ": " + lastname2 + "\n"
                    + companynamecolumn + ": " + companyname2 + "\n"
                    + addresscolumn + ": " + address2 + "\n"
                    + citycolumn + ": " + city2 + "\n"
                    + statecolumn + ": " + state2 + "\n"
                    + postalcodecolumn + ": " + postalcode2 + "\n"
                    + countrycolumn + ": " + country2 + "\n"
                    + phonecolumn + ": " + phone2 + "\n"
                    + websitecolumn + ": " + website2 + "\n"
                    + "Employer 3 \n" + useridcolumn + ": " + userid3 + "\n"
                    + passwordcolumn + ": " + password3 + "\n"
                    + firstnamecolumn + ": " + firstname3 + "\n"
                    + lastnamecolumn + ": " + lastname3 + "\n"
                    + companynamecolumn + ": " + companyname3 + "\n"
                    + addresscolumn + ": " + address3 + "\n"
                    + citycolumn + ": " + city3 + "\n"
                    + statecolumn + ": " + state3 + "\n"
                    + postalcodecolumn + ": " + postalcode3 + "\n"
                    + countrycolumn + ": " + country3 + "\n"
                    + phonecolumn + ": " + phone3 + "\n"
                    + websitecolumn + ": " + website3 + "\n\n"
                    + "Employer 4 \n" + useridcolumn + ": " + userid4 + "\n"
                    + passwordcolumn + ": " + password4 + "\n"
                    + firstnamecolumn + ": " + firstname4 + "\n"
                    + lastnamecolumn + ": " + lastname4 + "\n"
                    + companynamecolumn + ": " + companyname4 + "\n"
                    + addresscolumn + ": " + address4 + "\n"
                    + citycolumn + ": " + city4 + "\n"
                    + statecolumn + ": " + state4 + "\n"
                    + postalcodecolumn + ": " + postalcode4 + "\n"
                    + countrycolumn + ": " + country4 + "\n"
                    + phonecolumn + ": " + phone4 + "\n"
                    + websitecolumn + ": " + website4 + "\n\n"
                    + "Employer 5" + useridcolumn + ": " + userid5 + "\n"
                    + passwordcolumn + ": " + password5 + "\n"
                    + firstnamecolumn + ": " + firstname5 + "\n"
                    + lastnamecolumn + ": " + lastname5 + "\n"
                    + companynamecolumn + ": " + companyname5 + "\n"
                    + addresscolumn + ": " + address5 + "\n"
                    + citycolumn + ": " + city5 + "\n"
                    + statecolumn + ": " + state5 + "\n"
                    + postalcodecolumn + ": " + postalcode5 + "\n"
                    + countrycolumn + ": " + country5 + "\n"
                    + phonecolumn + ": " + phone5 + "\n"
                    + websitecolumn + ": " + website5;

            }
			catch (Exception e)
			{
                return "Something went wrong, please check your credentials and db name and try again.  Error: "+e.Message;
			}
		}
	}
}
