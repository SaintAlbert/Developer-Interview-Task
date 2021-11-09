using System;
using System.Collections.Generic;

namespace InterviewTask.Models
{
    public class HelperServiceModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string WeatherLocation { get; set; }
        public string Description { get; set; }
        public string TelephoneNumber { get; set; }

        public List<int> MondayOpeningHours { get; set; }
        public List<int> TuesdayOpeningHours { get; set; }
        public List<int> WednesdayOpeningHours { get; set; }
        public List<int> ThursdayOpeningHours { get; set; }
        public List<int> FridayOpeningHours { get; set; }
        public List<int> SaturdayOpeningHours { get; set; }
        public List<int> SundayOpeningHours { get; set; }
        public OpeningHour Opening { get; set; }
    }

    //Added model to display OpeningHour
    public class OpeningHour
    {
        public OpeningHour(string hourFormat, string styleFormat) { FormatedHour = hourFormat;  FormatedStyle= styleFormat; }
        public string FormatedHour { get; set; }
        public string FormatedStyle { get; set; }
    }
}

