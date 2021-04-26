using System;
using System.IO;
using System.Threading;

namespace FileLocker
{
    class Program
    {
        static void Main(string[] args)
        {
            var running = true;

            do
            {
                if (args.Length > 0)
                {
                    var fileName = args[0];
                    LockFile(fileName);
                }
                else
                {
                    Console.WriteLine("Please enter a file name with the full file path");
                    var fileName = Console.ReadLine();
                    LockFile(fileName);
                }

                Console.Clear();
                Console.WriteLine("Press Q to quit or any other key to lock another file.");
                var keyPressed = Console.ReadLine();

                if (keyPressed != null && keyPressed.ToUpper() == "Q")
                {
                    running = false;
                }

            } while (running);

            Environment.Exit(0);
        }

        private static void LockFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                using var f = File.Open(fileName, FileMode.Open);
                for (var i = 30; i > 0; i--)
                {
                    Console.Clear();
                    Console.WriteLine($"File {Path.GetFileName(fileName)} is locked for {i} seconds.");
                    Thread.Sleep(1000);
                }

                Console.Clear();
                Console.WriteLine($"File {Path.GetFileName(fileName)} is no longer locked.");
            }
            else
            {
                Console.WriteLine("File doesn't exist, please try again!");
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}
