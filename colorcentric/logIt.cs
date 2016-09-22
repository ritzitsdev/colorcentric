using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;

namespace printernet
{
  public class logIt
  {
    public logIt(string orderID, string message, string responseXml = "", int code = 0)
    {
      XDocument appConfigXml = XDocument.Load(configFiles.configXml);
      var sqlServer = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("server_name").Value;
      var sqlDatabase = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("database").Value;
      var sqlUsername = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("username").Value;
      var sqlPassword = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("password").Value;

      string connString = "Data Source=" + sqlServer + ";Initial Catalog=" + 
        sqlDatabase + ";User ID=" + sqlUsername + ";Password=" + sqlPassword + ";";
      
      string createTable = "If not exists (select name from sysobjects where name = 'PrinterNetLog') " +
        "CREATE TABLE PrinterNetLog(ID int IDENTITY(1,1) not null, orderID varchar(255), Log_Time datetime, Code int, " + 
        "Message nvarchar(MAX), Response_Xml nvarchar(MAX))";

      DateTime dateToday = DateTime.Now;
      string timestamp = dateToday.ToString();

      SqlConnection sqlConn = new SqlConnection(connString);
      sqlConn.Open();

      SqlCommand sqlCreateTable = new SqlCommand(createTable, sqlConn);

      sqlCreateTable.ExecuteNonQuery();

      SqlCommand sqlEnterLog = new SqlCommand("INSERT PrinterNetLog (orderID, Log_Time, Code, Message, Response_Xml)" +
        " VALUES (@orderID, @dateToday, @code, @message, @responseXml)", sqlConn);
      sqlEnterLog.Parameters.AddWithValue("@orderID", orderID);
      sqlEnterLog.Parameters.AddWithValue("@dateToday", DateTime.Now);
      sqlEnterLog.Parameters.AddWithValue("@code", code);
      sqlEnterLog.Parameters.AddWithValue("@message", message);
      sqlEnterLog.Parameters.AddWithValue("@responseXml", responseXml);

      sqlEnterLog.ExecuteNonQuery();

      sqlConn.Close();


      //DateTime dateToday = DateTime.Now;
      //string timestamp = dateToday.ToString();
      //string logFilePath = configFiles.logFile;

      //File.AppendAllText(logFilePath, orderID + " -- " + timestamp + " -- " + message + Environment.NewLine);
   }

  }
}


