using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DaniaCalender_console
{
    public class CreateCalendarFile
    {
        public string CalenderFile { get; set; }
        public string OneEvent { get; set; }

        private string Path;
        private List<(DateTime, string)> allEvents;
        private List<(DateTime, string)> filteredEvents = new List<(DateTime, string)>();

        public CreateCalendarFile(string path, int days, params (DateTime, string)[] allEvents)
        {
            this.allEvents = allEvents.ToList();
            Path = path;
            FilterEvents(this.allEvents, days);
            CalenderFile = CreateEvents(filteredEvents);
            WriteToFile(Path + ".ics", CalenderFile);
        }

        private void WriteToFile(string path, string text)
        {
            File.WriteAllText(path, text);
        }

        private string CreateEvents(List<(DateTime, string)> allEvents)
        {
            StringBuilder sb = new StringBuilder();

            // we add the start
            // start the calendar item
            sb.AppendLine("BEGIN:VCALENDAR");
            sb.AppendLine("VERSION:2.0");
            sb.AppendLine("PRODID: Julia Sommer");
            sb.AppendLine("CALSCALE:GREGORIAN");
            sb.AppendLine("METHOD:PUBLISH");

            // create a time zone if needed, TZID to be used in the event itself
            sb.AppendLine("BEGIN:VTIMEZONE");
            sb.AppendLine("TZID:Europe/Amsterdam");
            sb.AppendLine("BEGIN:STANDARD");
            sb.AppendLine("TZOFFSETTO:+0100");
            sb.AppendLine("TZOFFSETFROM:+0100");
            sb.AppendLine("END:STANDARD");
            sb.AppendLine("END:VTIMEZONE");

            // add events for each tuple pair
			foreach (var item in allEvents)
            {
                sb.AppendLine(CreateSingleEvent(item));
            }

            // end calendar item
            sb.AppendLine("END:VCALENDAR");

            return sb.ToString();
        }


        private string CreateSingleEvent((DateTime, string) singleEvent)
        {
            DateTime dateTime = singleEvent.Item1;
            DateTime dateStart = dateTime.AddHours(8.00);
            DateTime DateEnd = dateStart.AddMinutes(300);

            string info = singleEvent.Item2;
            string Summary = info;
            string Location = "Dania";
            string Description = "";

            StringBuilder sb = new StringBuilder();
            
			// add the event
            sb.AppendLine("BEGIN:VEVENT");

            //with time zone specified
            sb.AppendLine("DTSTART;TZID=Europe/Amsterdam:" + dateStart.ToString("yyyyMMddTHHmm00"));
            sb.AppendLine("DTEND;TZID=Europe/Amsterdam:" + DateEnd.ToString("yyyyMMddTHHmm00"));
            
			// or without
            sb.AppendLine("DTSTART:" + dateStart.ToString("yyyyMMddTHHmm00"));
            sb.AppendLine("DTEND:" + DateEnd.ToString("yyyyMMddTHHmm00"));
			sb.AppendLine("SUMMARY:" + Summary + "");
            sb.AppendLine("LOCATION:" + Location + "");
            sb.AppendLine("DESCRIPTION:" + Description + "");
            sb.AppendLine("PRIORITY:3");
            sb.AppendLine("END:VEVENT");

            return sb.ToString();
        }

        private void FilterEvents(List<(DateTime, string)> unfilteredEvents, int daysAhead)
        {
            DateTime maxDate = DateTime.Now.AddDays(daysAhead);
            for (int i = 0; i < unfilteredEvents.Count; i++)
            {
                if (unfilteredEvents[i].Item1 > DateTime.Now && unfilteredEvents[i].Item1 <= maxDate)
                {
                    filteredEvents.Add(unfilteredEvents[i]);
                }
            }
        }
    }
}
