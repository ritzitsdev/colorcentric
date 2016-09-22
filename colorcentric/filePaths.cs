using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace printernet
{
  static class configFiles
  {
    private static string configXmlPath = Path.Combine(Application.StartupPath, "config.xml");
    private static string logFilePath = Path.Combine(Application.StartupPath, "log.txt");
    
    public static string configXml
    {
      get { return configXmlPath; }
      set { configXmlPath = value; }
    }

    public static string logFile
    {
      get { return logFilePath; }
      set { logFilePath = value; }
    }
  }
}