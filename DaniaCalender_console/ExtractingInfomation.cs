using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DaniaCalender_console
{
    public class ExtractingInfomation
    {
        public string FullPath { get; set; }
        private string tempPath;
        public Dictionary<DateTime, string> OrderedResults = new Dictionary<DateTime, string>();
        public List<(DateTime, string)> OrderedResultsTuple = new List<(DateTime, string)>();

        public string[] allLines { get; set; }

        public ExtractingInfomation(string fullPath)
        {
            FullPath = fullPath;
            tempPath = fullPath;



            allLines = ReadFile(tempPath);

            FindDates(allLines);
        }
        private bool isItADate(string text)
        {
            DateTime dateTime;
            bool isDateTime = false;

            isDateTime = DateTime.TryParse(text, out dateTime);
            return isDateTime;
        }
        private void FindDates(string[] lines)
        {
            //List<string> results = new List<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] words = lines[i].Trim().Split(' ');
                if (isItADate(words[0]))
                {
                    DateTime date = Convert.ToDateTime(words[0]);
                    string info = "";

                    // add rest of line
                    foreach (var item in words)
                    {
                        if (isItADate(item)) continue;
                        info += item + "_";
                    }
                    if (info.Length < 1 && lines.Length > i + 1)
                    {
                        int addIndex = lines[i + 1].Trim().Length == 0 ? 2 : 1;

                        string extraLine = lines[i + addIndex].Replace('\t', ' ');
                        extraLine.Trim();
                        info += extraLine.Substring(0, Math.Min(20, extraLine.Length));
                        //info += lines[i + 1].Substring(0, Math.Min(20, lines[i+1].Length));

                    }
                    OrderedResultsTuple.Add((date, info));
                    //if (words.Length == 1 && lines.Length > i + 1)
                    //{
                    //    results.Add(lines[i + 1]);
                    //}

                    // trying to add to dic instead
                    //DateTime dateTime;
                    //DateTime.TryParse(words[0], out dateTime);

                    //if (OrderedResults.ContainsKey(dateTime))
                    //{
                    //    continue;
                    //}
                    //string temp = string.Join(" ", words);
                    //OrderedResults.Add(dateTime, temp);
                }
            }
        }

        private string[] ReadFile(string path)
        {
            string[] allLines = File.ReadAllLines(path);

            return allLines;
        }

    }
}
