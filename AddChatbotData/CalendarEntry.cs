using System;
namespace AddChatbotData
{
    public class CalendarEntry
    {
        private string summary;
        private string dStart;
        private string location;
        private string catagories;
        private string description;

        public CalendarEntry(string summary, string dStart,string location, string catagories, string description)
        {
            this.summary = summary;
            this.dStart = dStart;
            this.location = location;
            this.catagories = catagories;
            this.description = description;
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
        public string getCatagories()
        {
            return catagories;
        }
        public string getDescription()
        {
            return description;
        }
      
    }
}
