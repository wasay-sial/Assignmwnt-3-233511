using System;
using System.Xml;

class Program
{
    static void Main()
    {
        XmlWriterSettings settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "\t"
        };

        string filePath = "GPS.xml";

        using (XmlWriter writer = XmlWriter.Create(filePath, settings))
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("GPS_Log");

            writer.WriteStartElement("Position");
            writer.WriteAttributeString("DateTime", DateTime.Now.ToString());
            writer.WriteElementString("x", "65.8973342");
            writer.WriteElementString("y", "72.3452346");

            writer.WriteStartElement("SatteliteInfo");
            writer.WriteElementString("Speed", "40");
            writer.WriteElementString("NoSatt", "7");
            writer.WriteEndElement();

            writer.WriteEndElement();

            writer.WriteStartElement("Image");
            writer.WriteAttributeString("Resolution", "1024x800");
            writer.WriteElementString("Path", @"\images\1.jpg");
            writer.WriteEndElement();

            writer.WriteEndElement();
            writer.WriteEndDocument();
        }




        using (XmlReader reader = XmlReader.Create(filePath))
        {
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    Console.Write(new string(' ', reader.Depth * 2) + $"<{reader.Name}");
                    if (reader.HasAttributes)
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            Console.Write($" {reader.Name}=\"{reader.Value}\"");
                        }
                        reader.MoveToElement();
                    }
                    Console.WriteLine(">");
                }
                else if (reader.NodeType == XmlNodeType.Text)
                {
                    Console.WriteLine(new string(' ', reader.Depth * 2) + reader.Value);
                }
                else if (reader.NodeType == XmlNodeType.EndElement)
                {
                    Console.WriteLine(new string(' ', reader.Depth * 2) + $"</{reader.Name}>");
                }
            }
        }
    }
}
