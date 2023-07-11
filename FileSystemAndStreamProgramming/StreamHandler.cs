using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAndStreamProgramming
{
    /*
     * Stream represents chunk of data flowing from source to destination
     * It represents data as sequence of bytes
     * Source/destination could be file, in memory, device like printer etc 
     * 
     * All StreamTypes derive from Stream abstract class:
     * MemoryStream
     * FileStream - Stream type that deals with files. It is primitive stream - can read single bye or array of bytes
     * NetworkStream
     * BufferedStream
     * PipeStream
     * 
     * 
     * To operate on stream is difficult, so various reader and writers are available
     * StreamReader, StreamWriter - Supports reading writing character or string (char[]) to the provide stream
     * BinaryReader, BinaryWriter - derives from object, provides read and writes of discrete data types to underlying stream
     * StringReader, String Writer - Write to StringBuilder, read from string as stream
     * 
     * StreamReader can be used to get text data from a binary representation of text 
     * BinaryReader can be used to get arbitrary binary data. Ex: Reading JPEG file. Can also MemoryStream to do this.
     */


    class StreamHandler
    {
        public static void HandleFileViaFileStream()
        {
            using (FileStream fs = new FileStream("NewFile.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                Console.WriteLine("Write operation");
                string msg = "Writing to the file.";
                byte[] bytes = Encoding.UTF8.GetBytes(msg);
                // FileStream supports only methods dealing with byte or bytes
                Console.WriteLine("Current position : " + fs.Position);

                fs.Write(bytes, 0, bytes.Length);
                fs.Flush();
                Console.WriteLine("Current position : " + fs.Position);


                // Postion is very important when doing multiple operations on single FileStream object
                // it reflects where the operation will happen
                // Any read operation will happen from current position
                byte[] contents = new byte[fs.Length];
                fs.Read(contents, 0, contents.Length);
                Console.WriteLine("Read following " + Encoding.UTF8.GetString(contents));

                // Resetting position
                Console.WriteLine("Resetting position");
                byte[] allContents = new byte[fs.Length];
                fs.Position = 0;
                fs.Read(contents, 0, contents.Length);
                Console.WriteLine("Read following " + Encoding.UTF8.GetString(contents));
            }
        }

        public static void HandlingFileStreamViaStreamReaderWriters()
        {
            // StreamWriter - Derives from TextWriter
            // can write to any stream
            // writes char or char[] - string as well as pritive data types
            using (StreamWriter sw = new StreamWriter(File.Open("NewFile.txt", FileMode.OpenOrCreate)))
            {
                sw.WriteLine("Line followed by terminator");
                sw.Write("This is written using Stream Writer");
                sw.WriteLine();
                sw.Write(10000);
                sw.Flush();
            }

            using (StreamReader sr = new StreamReader(File.OpenRead("NewFile.txt")))
            {
                string str;
                // ReadLine reads the line and moves the position to the next line, retunrs null if EOF
                while ((str = sr.ReadLine()) != null)
                {
                    Console.WriteLine(str);
                }
            }
        }

        /// <summary>
        /// StringReader/ StringWriter treats string as stream (sequence of chracters)
        /// </summary>
        public static void HandlingStringAsStreamViaStringReaderWriters()
        {
            // StringWriter - Derives from TextWriter
            // writes or reads from stringbuilder as stream
            // writes char or char[] - string as well as pritive data types

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                sw.WriteLine("Line followed by terminator");
                sw.Write("This is written using Stream Writer");
                sw.WriteLine();
                sw.Write(10000);
                sw.Flush();
            }

            using (StringReader sr = new StringReader(sb.ToString()))
            {
                string str;
                // ReadLine reads the line and moves the position to the next line, retunrs null if EOF
                while ((str = sr.ReadLine()) != null)
                {
                    Console.WriteLine(str);
                }
            }
        }

        public static void HandlingMemoryStreamViaStreamReaderWriters()
        {
            // StreamWriter - Derives from TextWriter
            // can write to any stream
            // writes char or char[] - string as well as pritive data types
            MemoryStream ms = new MemoryStream();
            using (StreamWriter sw = new StreamWriter(ms))
            {
                sw.WriteLine("Writing to memory stream");
                sw.Flush();
            }
            // dispose on stream will also close the underlying memory stream
            // If a memory stream is closed it cannot be opened again. 
            // this won't work
            //using (StreamReader sr = new StreamReader(ms))
            //{
            //    string str;
            //    // ReadLine reads the line and moves the position to the next line, retunrs null if EOF
            //    while ((str = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(str);
            //    }
            //}

            byte[] bytes = ms.ToArray();
            Console.WriteLine(Encoding.UTF8.GetString(bytes));
        }

   }
}
