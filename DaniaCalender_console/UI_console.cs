using System;

namespace DaniaCalender_console
{
    public class UI_console
    {
        public static int GetIntFromUser(string message, int maxValue = 100000, string errorMessage = "Incorect use input. Please try again.")
        {
        start:
            int userInput = 0;
            string errMessage = errorMessage;
            try
            {
                Console.WriteLine(message);
                userInput = Convert.ToInt32(Console.ReadLine());
                if (userInput > maxValue)
                {
                    Console.WriteLine(errMessage + " Number to big.");
                    goto start;
                }
            }
            catch (Exception)
            {
                Console.WriteLine(errMessage);
                goto start;
            }
            return userInput;
        }

    }
}
