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
        static string[] callsigns_2 = new string[] {
        "2Husker", "2Starbuck", "2Apollo", "2Boomer",
        "2Bulldog", "2Athena", "2Helo", "2Racetrack" };
        /// <summary>
        /// Compress XML Document
        /// </summary>
        static void WorkWithCompression(bool useBrotli = true)
        {
        // compress the XML output
            string fileExt = useBrotli ? "brotli" : "gzip";
            string gzipFilePath = Combine(
            CurrentDirectory, "streams.gzip");
            string filePath = Combine(
            CurrentDirectory, $"streams.{fileExt}");
            FileStream file = File.Create(filePath);
            // using base class Stream to hold different compressor
            Stream compressor;
            if (useBrotli)
            {
                compressor = new BrotliStream(file, CompressionMode.Compress);
            }
            else
            {
                compressor = new GZipStream(file, CompressionMode.Compress);
            }
            using (compressor)
            {
                using (XmlWriter xmlCompressWrite = XmlWriter.Create(compressor))
                {
                    xmlCompressWrite.WriteStartDocument();
                    xmlCompressWrite.WriteStartElement("callsigns");
                    foreach (string item in callsigns)
                    {
                        xmlCompressWrite.WriteElementString("callsign", item);
                    }
                    // the normal call to WriteEndElement is not necessary
                    // because when the XmlWriter disposes, it will
                    // automatically end any elements of any depth
                    xmlCompressWrite.WriteEndElement();
                }
            } // also closes the underlying stream
                    // output all the contents of the compressed file
            WriteLine("-----------------------------------");
            if(compressor is BrotliStream){
                WriteLine("{0} compressor method: {1}, length: {2}", filePath, compressor.ToString(), new FileInfo(filePath).Length);
            }
            if(compressor is GZipStream){
                WriteLine("{0} compressor method: {1}, length: {2}", filePath, compressor.ToString(), new FileInfo(filePath).Length);
            }
            WriteLine("-----------------------------------");

            WriteLine($"The compressed contents:");
            WriteLine(File.ReadAllText(gzipFilePath));
            // read a compressed file
            WriteLine("Reading the compressed XML file:");
            file = File.Open(filePath, FileMode.Open);
            Stream decompressor;
            if (useBrotli)
            {
                decompressor = new BrotliStream(file, CompressionMode.Decompress);
            }
            else
            {
                decompressor = new GZipStream(file, CompressionMode.Decompress);
            }
            using (decompressor)
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

        /// <summary>
        /// XML write to plain file
        /// </summary>
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
        /// <summary>
        /// Write to text file with UTF-8
        /// </summary>
        static void WorkWithText()
        {
            // define a file to write to
            string textFile = Combine(CurrentDirectory, "streams.txt");
            string textFile2 = GetTempFileName();
            // create a text file and return a helper writer
            // file written using UTF-8 encoding text
            using (StreamWriter text = File.CreateText(textFile)){
                foreach (string item in callsigns)
                {
                    text.WriteLine(item);
                }
            }
            using (StreamWriter text2 = File.CreateText(textFile2)){
                foreach (string item in callsigns)
                {
                    text2.WriteLine(item);
                }
            }
            // output the contents of the file
            WriteLine("{0} contains {1:N0} bytes.",
            arg0: textFile,
            arg1: new FileInfo(textFile).Length);
            WriteLine(File.ReadAllText(textFile));
            WriteLine("{0} contains {1:N0} bytes.",
            textFile2, new FileInfo(textFile2).Length);
            WriteLine(File.ReadAllText(textFile2));

            using (StreamWriter textA = File.AppendText(textFile)){
                foreach (string item in callsigns_2)
                {
                    textA.WriteLine(item);
                }
            }
            WriteLine(File.ReadAllText(textFile));
        }
        /// <summary>
        /// finally block will be executed even Exception occurs
        /// </summary>
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
        static void WorkingWithFinally2(){
            WriteLine("Default Encoding:{0}", System.Text.Encoding.Default);
            using (FileStream file2 = File.OpenWrite(
                Path.Combine(Environment.CurrentDirectory, "file2.txt")))
            {
                using (StreamWriter writer2 = new StreamWriter(file2))
                {
                    try
                    {
                        writer2.WriteLine("Welcome, .NET!");
                    }
                    catch(Exception ex)
                    {
                        WriteLine($"{ex.GetType()} says {ex.Message}");
                    }
                } // automatically calls Dispose if the object is not null
            } //
        }
        static void Main(string[] args)
        {
            WorkWithText();
            // WorkWithXml();
            // WorkingWithFinally();
            // WorkingWithFinally2();
            // WorkWithCompression();
            // WorkWithCompression(useBrotli:false);

        }
    }
}
