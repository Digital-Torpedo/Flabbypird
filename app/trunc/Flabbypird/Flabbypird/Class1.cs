using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Flabbypird
{
    static public class Check
    {
        static public bool Valid;
        static public bool Do(string xsd, string xml)
        {

            Valid = true;
            XmlReaderSettings booksSettings = new XmlReaderSettings();
            booksSettings.Schemas.Add("", @xsd);
            booksSettings.ValidationType = ValidationType.Schema;
            booksSettings.ValidationEventHandler += new ValidationEventHandler(booksSettingsValidationEventHandler);

            XmlReader books = XmlReader.Create(@xml, booksSettings);

            while (books.Read()) { }
            return Valid;
        }

        static void booksSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Valid = e.Severity != XmlSeverityType.Warning && e.Severity != XmlSeverityType.Error;
        }
    }
}
