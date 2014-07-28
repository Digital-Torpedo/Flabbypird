using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.IO;

namespace Flabbypird
{
    class Settings
    {
        static Settings INSTANCE;

        public static Settings I
        {
            get
            {
                if (INSTANCE == null)
                    INSTANCE = new Settings();

                return INSTANCE;
            }
        }

        internal bool VolumeEnabled;
        internal double Volume;
        internal double ScreenWidth;
        internal double ScreenHeight;

        static class Screen
        {
            static internal int Width = 800;
            static internal int Height = 600;
        }

        public Settings()
        {
            if (!File.Exists(@".\config.xml"))
                GameConfig.Generate();

            Hashtable ht = GameConfig.Read();
            Volume = Convert.ToDouble(ht["volumeValue"]);
            VolumeEnabled = Convert.ToBoolean(ht["volumeEnabled"]);
            ScreenWidth = Convert.ToDouble(ht["resolutionWidth"]);
            ScreenHeight = Convert.ToDouble(ht["resolutionHeight"]);
        }

        ~Settings()
        {
            GameConfig.Write("Volume", "Value", Volume.ToString());
            GameConfig.Write("Volume", "Enabled", VolumeEnabled.ToString());
            GameConfig.Write("Resolution", "Width", ScreenWidth.ToString());
            GameConfig.Write("Resolution", "Height", ScreenHeight.ToString());
        }

        static class GameConfig
        {
            /// <summary>
            /// In case the config is damaged use this method to reset config to default
            /// </summary>
            public static string path = Environment.CurrentDirectory;

            public static void Generate()
            {
                XmlTextWriter writer = new XmlTextWriter(Path.Combine(path, "config.xml"), null);  //inititalize writer Variable with XML-Docuent 
                writer.WriteStartDocument();  // start parameter ==> Start Writitng Document
                writer.WriteStartElement("Settings"); //Define first Element called "Settings"
                writer.WriteStartElement("Volume"); //Define Child Element called "Volume". Parent: Element named "Settings" 
                writer.WriteAttributeString("Value", "1.0"); // add Attribute "Value" to Element named "Volume" and set its value to "100"
                writer.WriteAttributeString("Enabled", "true");
                writer.WriteEndElement(); // Close Element named "Volume"
                writer.WriteStartElement("Resolution");
                writer.WriteAttributeString("Height", "600");
                writer.WriteAttributeString("Width", "800");
                writer.WriteEndElement();
                writer.WriteStartElement("Language");
                writer.WriteAttributeString("Value", "DE");
                writer.WriteEndElement(); // Close Element named "Language"
                writer.WriteEndDocument(); // Close main Element named "Settings"
                writer.Close(); // Stop Writing to Document and save
            }

            /// <summary>
            /// Read gameconfig. This Method will return a Hashtable.
            /// </summary>
            /// <returns>Hashtable ( string volumeValue, string volumeEnabled, string resolutionHeight, string resolutionWidth, string languageValue)</returns>
            public static Hashtable Read()
            {
                Hashtable settings = new Hashtable();

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(path, "config.xml"));
                XmlNode volume = xmlDoc.SelectSingleNode("//Settings/Volume");
                XmlAttribute valueVolume = volume.Attributes["Value"];
                XmlAttribute enabledVolume = volume.Attributes["Enabled"];
                XmlNode lang = xmlDoc.SelectSingleNode("//Settings/Language");
                XmlAttribute valueLang = lang.Attributes["Value"];
                XmlNode resolution = xmlDoc.SelectSingleNode("//Settings/Resolution");
                XmlAttribute height = resolution.Attributes["Height"];
                XmlAttribute width = resolution.Attributes["Width"];

                settings.Add("volumeValue", valueVolume.Value);
                settings.Add("volumeEnabled", enabledVolume.Value);
                settings.Add("resolutionHeight", height.Value);
                settings.Add("resolutionWidth", width.Value);
                settings.Add("languageValue", valueLang.Value);

                return settings;
            }

            /// <summary>
            /// Write to config. 
            /// </summary>
            /// <param name="parentattribute">Main attribute you want to edit. Example: "Volume"</param>
            /// <param name="attribute">Attribute you want to change from main attribute. Example: "Value"</param>
            /// <param name="value">Value you want to set to the attribute. Example: "75"</param>
            public static void Write(string parentattribute, string attribute, string value)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Path.Combine(path, "config.xml"));
                XmlNode volume = xmlDoc.SelectSingleNode("//Settings/" + parentattribute);
                XmlAttribute attributes = volume.Attributes[attribute];
                attributes.Value = value;
                xmlDoc.Save(@".\config.xml");
            }
        }
    }
}
