using System;
using System.IO; // types for managing the filesystem
using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Xml;
using System.IO.Compression;
namespace WorkingWithStreams
{
    class Program
    {
        // define an array of Viper pilot call signs
        static string[] callsigns = new string[] {
        "Husker", "Starbuck", "Apollo", "Boomer",
        "Bulldog", "Athena", "Helo", "Racetrack" };
/// <summary>
/// Compress XML Document
/// </summary>
static void WorkWithCompression()
{
// compress the XML output

    string gzipFilePath = Combine(
    CurrentDirectory, "streams.gzip");
    FileStream gzipFile = File.Create(gzipFilePath);
    using (GZipStream compressor = new GZipStream(
    gzipFile, CompressionMode.Compress))
    {
        using (XmlWriter xmlGzip = XmlWriter.Create(compressor))
        {
            xmlGzip.WriteStartDocument();
            xmlGzip.WriteStartElement("callsigns");
            foreach (string item in callsigns)
            {
                xmlGzip.WriteElementString("callsign", item);
            }
            // the normal call to WriteEndElement is not necessary
            // because when the XmlWriter disposes, it will
            // automatically end any elements of any depth
            xmlGzip.WriteEndElement();
        }
    } // also closes the underlying stream
            // output all the contents of the compressed file
    WriteLine("{0} contains {1:N0} bytes.",
    gzipFilePath, new FileInfo(gzipFilePath).Length);
    WriteLine($"The compressed contents:");
    WriteLine(File.ReadAllText(gzipFilePath));
    // read a compressed file
    WriteLine("Reading the compressed XML file:");
    gzipFile = File.Open(gzipFilePath, FileMode.Open);
    using (GZipStream decompressor = new GZipStream(
    gzipFile, CompressionMode.Decompress))
    {
        using (XmlReader reader = XmlReader.Create(decompressor))
        {
            while (reader.Read()) // read the next XML node
            {
                // check if we are on an element node named callsign
                if ((reader.NodeType == XmlNodeType.Element)
                && (reader.Name == "callsign"))
                {
                    reader.Read(); // move to the text inside element
                    WriteLine($"{reader.Value}"); // read its value

                }
            }
        }
    }
}


        static void WorkWithXml()
        {
            // define a file to write to
            string xmlFile = Combine(CurrentDirectory, "streams.xml");
            // create a file stream
            using (FileStream xmlFileStream = File.Create(xmlFile)){
                // wrap the file stream in an XML writer helper
                // and automatically indent nested elements
                using (XmlWriter xml = XmlWriter.Create(xmlFileStream,
                new XmlWriterSettings { Indent = true })){
                    // write the XML declaration
                    try{
                        xml.WriteStartDocument();
                        // write a root element
                        xml.WriteStartElement("callsigns");
                        // enumerate the strings writing each one to the stream
                        foreach (string item in callsigns)
                        {
                            xml.WriteElementString("callsign", item);
                        }
                        // write the close root element
                        xml.WriteEndElement();
                        // close helper and stream
                        xml.Close();
                        xmlFileStream.Close();
                        // output all the contents of the file
                        WriteLine("{0} contains {1:N0} bytes.",
                        arg0: xmlFile,
                        arg1: new FileInfo(xmlFile).Length);
                        WriteLine(File.ReadAllText(xmlFile));
                    }
                    catch (Exception ex){
                        WriteLine("Exception {0} says {1}", ex.GetType(), ex.Message);
                    }
                }
            }
        }
        static void WorkWithText()
        {
            // define a file to write to
            string textFile = Combine(CurrentDirectory, "streams.txt");
            string textFile2 = GetTempFileName();
            // create a text file and return a helper writer
            // file written using UTF-8 encoding text
            StreamWriter text = File.CreateText(textFile);
            StreamWriter text2 = File.CreateText(textFile2);
            // enumerate the strings, writing each one
            // to the stream on a separate line
            foreach (string item in callsigns)
            {
                text.WriteLine(item);
                text2.WriteLine(item);
            }
            text.Close(); // release resources
            text2.Close(); // release resources
            // output the contents of the file
            WriteLine("{0} contains {1:N0} bytes.",
            arg0: textFile,
            arg1: new FileInfo(textFile).Length);
            WriteLine(File.ReadAllText(textFile));
            WriteLine(File.ReadAllText(textFile));
            WriteLine("{0} contains {1:N0} bytes.",
            textFile2, new FileInfo(textFile2).Length);
            WriteLine(File.ReadAllText(textFile2));
        }

        static void WorkingWithFinally(){
            int[] array1 = new int[2]{0, 0};
            int[] array2 = new int[2]{0, 0};

            try
            {
                Array.Copy(array1, array2, -1);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("Error: {0}", e);
                throw;
            }
            finally
            {
                Console.WriteLine("This statement is always executed.");
            }
        }
        static void Main(string[] args)
        {
            // WorkWithText();
            // WorkWithXml();
            // WorkingWithFinally();
            WorkWithCompression();

        }
    }
}
