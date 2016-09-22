using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace printernet
{
  public partial class config : Form
  {
    public config()
    {
      InitializeComponent();
    }

    private void config_Load(object sender, EventArgs e)
    {
      string configXml = configFiles.configXml;
      if (!File.Exists(configXml))
      {
        XDocument xDoc = new XDocument(
          new XDeclaration("1.0", "windows-1252", null),
          new XElement("printernet_settings",
            new XElement("orders_folder", ""),
            new XElement("order_submit_url", ""),
            new XElement("shared_secret", ""),
            new XElement("ftp_server", ""),
            new XElement("cxml_folder",""),
            new XElement("sql_server_connection_info",
              new XAttribute("server_name", ""),
              new XAttribute("database", ""),
              new XAttribute("username", ""),
              new XAttribute("password", ""))));
            
        xDoc.Save(Path.Combine(Application.StartupPath, "config.xml"));
      }

      XDocument appConfigXml = XDocument.Load(configXml);

      var orderFolder = appConfigXml.Descendants("orders_folder").ElementAt(0).Value;
      orderFolderPath.Text = orderFolder;

      var orderSubmitUrl = appConfigXml.Descendants("order_submit_url").ElementAt(0).Value;
      orderSubmitURL.Text = orderSubmitUrl;

      var sharedSecretValue = appConfigXml.Descendants("shared_secret").ElementAt(0).Value;
      sharedSecret.Text = sharedSecretValue;

      var ftpServerUrl = appConfigXml.Descendants("ftp_server").ElementAt(0).Value;
      ftpServer.Text = ftpServerUrl;

      var cXmlFolder = appConfigXml.Descendants("cxml_folder").ElementAt(0).Value;
      cXmlSaveFolderPath.Text = cXmlFolder;

      bool sqlServerInfo = appConfigXml.Descendants("sql_server_connection_info").Any();
      if (sqlServerInfo)
      {
        var sqlServerNameValue = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("server_name").Value;
        sqlServerName.Text = sqlServerNameValue;

        var sqlDatabaseNameValue = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("database").Value;
        sqlDatabaseName.Text = sqlDatabaseNameValue;

        var sqlUsernameValue = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("username").Value;
        sqlUsername.Text = sqlUsernameValue;

        var sqlPasswordValue = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("password").Value;
        sqlPassword.Text = sqlPasswordValue;
      }
      else
      {
        appConfigXml.Element("printernet_settings").Add(
          new XElement("sql_server_connection_info",
            new XAttribute("server_name", ""),
            new XAttribute("database", ""),
            new XAttribute("username", ""),
            new XAttribute("password", "")));
        appConfigXml.Save(Path.Combine(Application.StartupPath, "config.xml"));
      }

    }

    private void btnOrderFolderBrowse_Click(object sender, EventArgs e)
    {
      string orderFolder = string.Empty;
      DialogResult result = orderFolderBrowserDialog.ShowDialog();

      if (result == DialogResult.OK)
      {
        orderFolder = orderFolderBrowserDialog.SelectedPath;
        Environment.SpecialFolder root = orderFolderBrowserDialog.RootFolder;
        orderFolderPath.Text = orderFolder;
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
      string configXml = configFiles.configXml;
      XDocument appConfigXml = XDocument.Load(configXml);

      var oldOrderFolder = appConfigXml.Descendants("orders_folder").ElementAt(0);
      string newOrderFolder = orderFolderPath.Text;
      oldOrderFolder.Value = newOrderFolder;

      var oldOrderSubmitUrl = appConfigXml.Descendants("order_submit_url").ElementAt(0);
      string newOrderSubmitUrl = orderSubmitURL.Text;
      oldOrderSubmitUrl.Value = newOrderSubmitUrl;

      var oldSharedSecretValue = appConfigXml.Descendants("shared_secret").ElementAt(0);
      string newSharedSecretValue = sharedSecret.Text;
      oldSharedSecretValue.Value = newSharedSecretValue;

      var oldFtpServerUrl = appConfigXml.Descendants("ftp_server").ElementAt(0);
      string newFtpServerUrl = ftpServer.Text;
      oldFtpServerUrl.Value = newFtpServerUrl;

      var oldCxmlFolderValue = appConfigXml.Descendants("cxml_folder").ElementAt(0);
      string newCxmlFolder = cXmlSaveFolderPath.Text;
      oldCxmlFolderValue.Value = newCxmlFolder;

      var oldSqlServerName = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("server_name");
      string newSqlServerName = sqlServerName.Text;
      oldSqlServerName.Value = newSqlServerName;

      var oldSqlServerDatabase = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("database");
      string newSqlServerDatabase = sqlDatabaseName.Text;
      oldSqlServerDatabase.Value = newSqlServerDatabase;

      var oldSqlUsername = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("username");
      string newSqlUsername = sqlUsername.Text;
      oldSqlUsername.Value = newSqlUsername;

      var oldSqlPassword = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("password");
      string newSqlPassword = sqlPassword.Text;
      oldSqlPassword.Value = newSqlPassword;

      appConfigXml.Save(configXml);
      this.Close();
    }

    private void btncXmlSaveFolderBrowse_Click(object sender, EventArgs e)
    {
      string cXmlSaveFolder = string.Empty;
      DialogResult result = cXmlSaveFolderBrowserDialog.ShowDialog();

      if (result == DialogResult.OK)
      {
        cXmlSaveFolder = cXmlSaveFolderBrowserDialog.SelectedPath;
        Environment.SpecialFolder root = cXmlSaveFolderBrowserDialog.RootFolder;
        cXmlSaveFolderPath.Text = cXmlSaveFolder;
      }
    }
  }
}
