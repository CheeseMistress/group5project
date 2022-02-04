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
				DataTable table = new DataTable();
				adapter.Fill(table);

                // get columns
                string idcolumn = table.Columns[0].ColumnName.ToString();
                string namecolumn = table.Columns[1].ColumnName.ToString();
                string addresscolumn = table.Columns[2].ColumnName.ToString();
                string citycolumn = table.Columns[3].ColumnName.ToString();
                string statecolumn = table.Columns[4].ColumnName.ToString();
                string postalcodecolumn = table.Columns[5].ColumnName.ToString();
                string countrycolumn = table.Columns[6].ColumnName.ToString();
                string phonecolumn = table.Columns[7].ColumnName.ToString();
                string websitecolumn = table.Columns[8].ColumnName.ToString();

                // get data
                string id = table.Rows[0].ItemArray[0].ToString();
                string name = table.Rows[0].ItemArray[1].ToString();
                string address = table.Rows[0].ItemArray[2].ToString();
                string city = table.Rows[0].ItemArray[3].ToString();
                string state = table.Rows[0].ItemArray[4].ToString();
                string postalcode = table.Rows[0].ItemArray[5].ToString();
                string country = table.Rows[0].ItemArray[6].ToString();
                string phone = table.Rows[0].ItemArray[7].ToString();
                string website = table.Rows[0].ItemArray[8].ToString();

                // return test query
                return idcolumn + ": " + id + "\n" 
                    + namecolumn + ": " + name + "\n"
                    + addresscolumn + ": " + address + "\n"
                    + citycolumn + ": " + city + "\n"
                    + statecolumn + ": " + state + "\n"
                    + postalcodecolumn + ": " + postalcode + "\n"
                    + countrycolumn + ": " + country + "\n"
                    + phonecolumn + ": " + phone + "\n"
                    + websitecolumn + ": " + website + "\n";
			}
			catch (Exception e)
			{
				return "Something went wrong, please check your credentials and db name and try again.  Error: "+e.Message;
			}
		}
	}
}
