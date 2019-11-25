using System;
using System.IO;

namespace DaniaCalender_console
{
    class Program
    {
        static void Main(string[] args)
        {
            Run();

            Console.ReadKey();
        }

        private static void Run()
        {
            string message = @"Welcome to Dania calendar creater! v.01.00 
To use his app you need a txt file. 
This file is created by downloading the docx file 
from Moodle and copy pasting ALL text into an empty txt file.
It is that files path(including file extension) you need to type here.";

            Console.WriteLine(message);
            Console.WriteLine();
            Console.WriteLine("Where is your file located?");
            string path = Console.ReadLine();

            while (!File.Exists(path))
            {
                Console.WriteLine("File does not exist, try to check path and type it in again:");
                path = Console.ReadLine();
            }

            int days = UI_console.GetIntFromUser("How many days ahead do you want events to be?");

            ExtractingInfomation infomation = new ExtractingInfomation(path);
            CreateCalendarFile createCalendarFile = new CreateCalendarFile(path, days, infomation.OrderedResultsTuple.ToArray());

            Console.WriteLine("File successfully created at:");
            Console.WriteLine(path + ".ICS");

            Console.WriteLine("Press any key a few times to exit this application. Have a nice day!");

        }

    }
}
