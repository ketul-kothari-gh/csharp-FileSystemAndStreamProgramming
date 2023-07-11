using System.Text;

namespace FileSystemAndStreamProgramming
{
    // File and FileInfo
    // File provides static methods
    // FileInfo works on instance level
    // Both class provides operations to manipulate file contents 
    // FileInfo class additionally provides ways to check file details - Creatied time, modified time, attributes etc
    class FileHandler
    {
        public static void HandleFileOperations()
        {
            FileInfo fi = new FileInfo("NewFile.txt");
            // creates file stream .. can be used to read or write
            // has overloaded method with verious options to setup read write access, buffer size etc
            using (FileStream fs = fi.Create())
            {
                byte[] contents = Encoding.UTF8.GetBytes("Written via synchronous method");
                fs.Write(contents, 0, contents.Length);
                fs.Flush();
            }

            using (FileStream fs = fi.Open(FileMode.Open))
            {
                byte[] contents = new byte[fi.Length];
                // just for example, may have performance impact if the file is too big and task is to just print content
                // can use StreamReader etc to read line by line or if using FileStream.Read use loop to read 
                fs.Read(contents, 0, contents.Length);
                Console.WriteLine(Encoding.UTF8.GetString(contents));
            }


            // These can be controller when creating FileStream using create method as well by passing appropriate parameters
            // Read Only stream
            using (FileStream fs = fi.OpenRead())
            {
                byte[] contents = new byte[fi.Length];
                // just for example, may have performance impact if the file is too big and task is to just print content
                // can use StreamReader etc to read line by line or if using FileStream.Read use loop to read 
                fs.Read(contents, 0, contents.Length);
                Console.WriteLine(Encoding.UTF8.GetString(contents));
            }


            // WriteOnly Stream
            using (FileStream fs = fi.OpenWrite())
            {
                byte[] contents = Encoding.UTF8.GetBytes("Written via write only stream");
                fs.Write(contents, 0, contents.Length);
                fs.Flush();
            }

            // These can be controller when creating FileStream using create method as well by passing appropriate parameters
            // Read Only stream
            using (FileStream fs = fi.OpenRead())
            {
                byte[] contents = new byte[fi.Length];
                // just for example, may have performance impact if the file is too big and task is to just print content
                // can use StreamReader etc to read line by line or if using FileStream.Read use loop to read 
                fs.Read(contents, 0, contents.Length);
                Console.WriteLine(Encoding.UTF8.GetString(contents));
            }

            // Reads all text in one go.. easy to use but may be inefficient in case of huge files
            string fileContents = File.ReadAllText("NewFile.txt");
            Console.WriteLine(fileContents);
            
        }
    }
}
