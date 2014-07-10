using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.OleDb;
using System.Collections;

namespace Flabbypird
{
    public class GameConfig
    {
        /// <summary>
        /// In case the config is damaged use this method to reset config to default
        /// </summary>
        public static void New()
        {
            XmlTextWriter writer = new XmlTextWriter(@"C:\\Users\warmhold\test.xml", null);  //inititalize writer Variable with XML-Docuent 
            writer.WriteStartDocument();  // start parameter ==> Start Writitng Document
            writer.WriteStartElement("Settings"); //Define first Element called "Settings"
            writer.WriteStartElement("Volume"); //Define Child Element called "Volume". Parent: Element named "Settings" 
            writer.WriteAttributeString("Value", "100"); // add Attribute "Value" to Element named "Volume" and set its value to "100"
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
            xmlDoc.Load(@"C:\\Users\warmhold\test.xml");
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
        /// Write the config. In case you want to use default part just send an empty string (language) or a specific value(see parameter informatons).
        /// Example: GameConfig.GameConfig.Write(-1,true,600,800,"");
        /// </summary>
        /// <param name="volumeValue">int. Set your Volume here. Anything smaller than 0 and bigger than 100 will use default value.</param>
        /// <param name="volumeEnabled">bool. Volume enabled or not ? Nothing else to explain here I hope.</param>
        /// <param name="resolutionHeight">int. Resolution height. Won't change anything. Changing resolution is not implemented yet.</param>
        /// <param name="resolutionWidth">int. Resolution width. Won't change anything. Changing resolution is not implemented yet.</param>
        /// <param name="languageValue">string. Language. At the moment we only support german. So here we have a useles variable again you have to set :P</param>
        public static void Write(int volumeValue, bool volumeEnabled, int resolutionHeight, int resolutionWidth, string languageValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"C:\\Users\warmhold\test.xml");
            XmlNode volume = xmlDoc.SelectSingleNode("//Settings/Volume");
            XmlAttribute valueVolume = volume.Attributes["Value"];
            XmlAttribute enabledVolume = volume.Attributes["Enabled"];
            XmlNode lang = xmlDoc.SelectSingleNode("//Settings/Language");
            XmlAttribute valueLang = lang.Attributes["Value"];
            XmlNode resolution = xmlDoc.SelectSingleNode("//Settings/Resolution");
            XmlAttribute height = resolution.Attributes["Height"];
            XmlAttribute width = resolution.Attributes["Width"];
            if (volumeValue >= 0 && volumeValue <= 100)
            {
                valueVolume.Value = Convert.ToString(volumeValue);
            }
                enabledVolume.Value = volumeEnabled.ToString();
            if (languageValue == "DE")
            {
            valueLang.Value = languageValue;
            }
            if (resolutionHeight == 600) // Changing resolution not implemented yet.
            {
            height.Value = Convert.ToString(resolutionHeight);
            }
            if (resolutionWidth == 800) // Changing resolution not implemented yet.
            {
            width.Value = Convert.ToString(resolutionWidth);
            }
        }
    }
}
