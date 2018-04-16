using System;
namespace AddChatbotData
{
    public class CalendarEntry
    {
        private string summary = "";
        private string dStart = "";
        private string location = "None";
        private string categories = "";
        private string description = "";

        public CalendarEntry(string summary, string dStart,string location, string categories, string description)
        {
            this.summary = summary;
            this.dStart = DateConvert(dStart);
            if (!string.IsNullOrEmpty(location)) {
                this.location = location;
            }
            this.categories = categories;
            this.description = description;
        }

        private string DateConvert(string dstart)
        {
            return dstart;
        }

        public string getSummary()
        {
            return summary;
        }
        public string getDStart() {
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
