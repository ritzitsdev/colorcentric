using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace printernet
{
  public partial class Form1 : Form
  {

    public Form1()
    {
      InitializeComponent();
      _Form1 = this;
      Shown += Form1_Shown;
    }
    public static Form1 _Form1;

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      config form = new config();
      form.Show();
    }
    private void Form1_Load(object sender, EventArgs e)
    {
    }
    private void Form1_Shown(object sender, EventArgs e)
    {
      string configXml = configFiles.configXml;
      if (File.Exists(configXml))
      {
        XDocument appConfigXml = XDocument.Load(configFiles.configXml);
        var ordersFolder = appConfigXml.Descendants("orders_folder").ElementAt(0).Value;
        var orderSubmitUrl = appConfigXml.Descendants("order_submit_url").ElementAt(0).Value;
        var sharedSecret = appConfigXml.Descendants("shared_secret").ElementAt(0).Value;
        var ftpServer = appConfigXml.Descendants("ftp_server").ElementAt(0).Value;
        var cxmlFolder = appConfigXml.Descendants("cxml_folder").ElementAt(0).Value;
        bool sqlServerInfo = appConfigXml.Descendants("sql_server_connection_info").Any();
        //var sqlServer = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("server_name").Value;

        if(ordersFolder == "" || 
          orderSubmitUrl == "" || 
          sharedSecret == "" || 
          ftpServer == "" || 
          cxmlFolder == "" ||
          sqlServerInfo == false)
        {
          config form = new config();
          form.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
          form.Show();
        }
        else if (sqlServerInfo)
        {
          var sqlServer = appConfigXml.Descendants("sql_server_connection_info").ElementAt(0).Attribute("server_name").Value;
          if (sqlServer == "")
          {
            config form = new config();
            form.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
            form.Show();
          }
          else
          {
            processExisting();
            startMonitoring();
          }
        }
        else
        {
         processExisting();
         startMonitoring();
        }
      }
      else
      {
        config form = new config();
        form.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
        form.Show();
      }
    }

    void Form1_FormClosed(object sender, FormClosedEventArgs e)
    {
      processExisting();
      startMonitoring();
    }
    private void processExisting()
    {
      addUserFeedback("Processing Existing Orders.", Color.Black);

      string configXml = configFiles.configXml;
      XDocument appConfigXml = XDocument.Load(configXml);
      var ordersFolder = appConfigXml.Descendants("orders_folder").ElementAt(0).Value;
      if (Directory.Exists(ordersFolder))
      {
        //string[] orderXmls = Directory.GetFiles(orderFolder, "*.xml", SearchOption.TopDirectoryOnly);
        string[] orders = Directory.GetDirectories(ordersFolder, "*.order");

        foreach(string order in orders){
          string orderFolder = order;
          string orderDirName = orderFolder.Split('\\').Last();
          string orderXmlFile = orderDirName.Replace("order", "xml");
          string orderXml = Path.Combine(orderFolder, orderXmlFile);
          string orderID = orderXmlFile.Replace(".xml", "");

          addUserFeedback("Processing order... ", Color.Black, orderID);
          orderCXML cXml = new orderCXML();
          cXml.createXml(orderXml, orderID);
        }
      }
      else
      {
        addUserFeedback("Could not find orders folder.  Please check settings.", Color.Red);
      }

      addUserFeedback("Finished Processing Existing Orders.", Color.Black);
    }

    public void startMonitoring()
    {
      string configXml = configFiles.configXml;
      XDocument appConfigXml = XDocument.Load(configXml);
      var ordersFolder = appConfigXml.Descendants("orders_folder").ElementAt(0).Value;
      if (Directory.Exists(ordersFolder))
      {
        FileSystemWatcher fsWatcher = new FileSystemWatcher();
        fsWatcher.Path = ordersFolder;
        fsWatcher.Filter = "*.order";
        fsWatcher.IncludeSubdirectories = false;
        fsWatcher.NotifyFilter = NotifyFilters.DirectoryName;
        //fsWatcher.Changed += new FileSystemEventHandler(OnChanged);
        //fsWatcher.Deleted += new FileSystemEventHandler(OnChanged);
        fsWatcher.Renamed += new RenamedEventHandler(OnRenamed);
        fsWatcher.EnableRaisingEvents = true;

        addUserFeedback("Monitoring orders folder...", Color.Black);
      }
      else
      {
        addUserFeedback("Could not find orders folder. Please check settings.", Color.Red);
      }
    }

    private static void OnChanged(object sender, RenamedEventArgs e)
    {
      string order = e.FullPath;
    }

    public void OnRenamed(object sender, RenamedEventArgs e)
    {
        //MessageBox.Show(order);
      //try
      //{
        string order = e.FullPath;
        if(order.Substring(order.Length-5) == "order")
        {
          string orderDirName = order.Split('\\').Last();
          string orderXmlFile = orderDirName.Replace("order", "xml");
          string orderXml = Path.Combine(order, orderXmlFile);
          string orderID = orderXmlFile.Replace(".xml", "");

          this.Invoke((MethodInvoker)delegate { addUserFeedback("Processing order...", Color.Black, orderID); });
          logIt logEntry = new logIt(orderID, "Processing order...");

          orderCXML cXml = new orderCXML();
          cXml.createXml(orderXml, orderID);
        }
      //}
      //catch (Exception ex) { 
        //MessageBox.Show(ex.ToString()); 
      //  string order = e.FullPath;
      //  this.Invoke((MethodInvoker)delegate { addUserFeedback("Could not start processing order " + order, Color.Red, order); });
      //  logIt logEntry = new logIt(order, "Could not start processing order. Message: " + ex.ToString());
      //  Directory.Move(order, order.Replace("order", "ERROR"));
      //}
      
    }

    public void addUserFeedback(string text, Color textcolor, string orderID = "")
    {
          Int32 maxsize = 1024000;
          Int32 dropsize = maxsize / 4;

      if (userFeedbackField.Text.Length > maxsize)
      {
        Int32 endmarker = userFeedbackField.Text.IndexOf('\n', dropsize) + 1;
        if (endmarker < dropsize)
        {
          endmarker = dropsize;
        }
        userFeedbackField.Select(0, endmarker);
        userFeedbackField.Cut();
      }

      try
      {
        userFeedbackField.SelectionStart = userFeedbackField.Text.Length;
        userFeedbackField.SelectionLength = 0;
        userFeedbackField.SelectionColor = textcolor;
        userFeedbackField.AppendText(orderID + " -- " + DateTime.Now.ToString("HH:mm:ss.mmm") + " -- " + text + "\n");
      }
      catch (Exception ex) { }
    }


  }
}
