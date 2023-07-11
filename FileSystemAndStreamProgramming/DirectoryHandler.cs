using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemAndStreamProgramming
{
    // Direcotry and DirectoryInfo
    // Directory provides static methods - returned types are generally string
    // DirectroyInfo works on instance level - return types etc are strongly typed
    // DirectoryInfo creates objects - in case dealing with thousands of directories a lot of objects may be created
    class DirectoryHandler
    {
        public static void HandleDirectoryOperations()
        {
            string directory = Directory.GetCurrentDirectory();
            Console.WriteLine("Naigating directory : " + directory);

            // Returned types are strings
            foreach(var file in Directory.EnumerateFileSystemEntries(directory))
            {
                // since returned type is not strongly type object
                // to do any checks further will have to write futher code

                FileAttributes atr = File.GetAttributes(file);
                if(atr.HasFlag(FileAttributes.Directory))
                {
                    Console.WriteLine("Directory : " + file);
                }
                else
                {
                    Console.WriteLine("File : " + file);
                }
            }

            // doing same using DirectoryInfo is easier as strongly typed objects are created packed with info and methods

            DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
            foreach(var fileInfo in dir.EnumerateFileSystemInfos())
            {
                if (fileInfo.Attributes.Equals(FileAttributes.Directory))
                {
                    Console.WriteLine("Directory : " + fileInfo.FullName);
                }
                else
                {
                    Console.WriteLine("File : " + fileInfo.FullName);
                }
            }

            
        }
    }
}
