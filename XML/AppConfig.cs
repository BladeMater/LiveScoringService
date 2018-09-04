using System;
using System.Configuration;

namespace XML_Processing
{
    public class AppConfig
    {
        public static string XmlFile
        {
            get { return ConfigurationManager.AppSettings["XmlFile"]; }
            
        }
        public static string XmlFile1
        {
          
            get { return ConfigurationManager.AppSettings["XmlFile1"]; }
        }

    }
}