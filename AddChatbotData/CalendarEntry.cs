using System;
using System.Globalization;
namespace AddChatbotData
{
    public class CalendarEntry
    {
        private string summary = "None";
        private string dStart = "None";
        private string location = "None";
        private string categories = "None";
        private string description = "None";

        public CalendarEntry(string summary, string dStart,string location, string categories, string description)
        {
            if (!string.IsNullOrEmpty(summary))
            {
                this.summary = summary;
            }

            if(!string.IsNullOrEmpty(dStart))
            {
                this.dStart = DateConvert(dStart);
            }            

            if (!string.IsNullOrEmpty(location))
            {
                this.location = location;
            }

            if(!string.IsNullOrEmpty(categories))
            {
                this.categories = categories;
            }
          
            if(!string.IsNullOrEmpty(description))
            {
                this.description = description;
            }
        }

        private string DateConvert(string dstart)
        {
            var dateSplit = dstart.Split(':');
            String dateTime = dateSplit[1];

            //example date time format --> 20180131T070000
            string[] formats = { "yyyyMMddThhmmss", "yyyyMMdd" };

            string s = "";
            DateTime dt = DateTime.Now;
            if(DateTime.TryParseExact(s, formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
            {
                
            }

            return dt.ToString();
        }

        public string getSummary()
        {
            return summary;
        }
        public string getDStart()
        {
            return dStart;
        }
        public string getLocation()
        {
            return location;
        }
        public string getCategories()
        {
            return categories;
        }
        public string getDescription()
        {
            return description;
        }
      
    }
}
