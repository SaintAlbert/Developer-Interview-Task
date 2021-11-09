
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using InterviewTask.Logger;
using InterviewTask.Models;
using InterviewTask.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace InterviewTask.Controllers
{
    public class HomeController : Controller
    {
        //Read-only IHelper service repository property
        private readonly IHelperServiceRepository helperService;
        private readonly IList<HelperServiceModel> serviceDataResult;
        private readonly ILogWriter mLogger;
        //Adding default Construtor
        public HomeController() : this(new HelperServiceRepository(), new LogWriter("HomeController Default constructor"))
        {
        }
        //Adding parameterised Construtor with IoC
        public HomeController(IHelperServiceRepository helperService_, ILogWriter logger_)
        {
            //Innitialize Ihelper interface
            helperService = helperService_;
            mLogger = logger_;
            serviceDataResult = new List<HelperServiceModel>();

        }
        /*
         * Prepare your opening times here using the provided HelperServiceRepository class.       
         */

        public ActionResult Index()
        {
            mLogger.LogWrite("Action call on Index ");
            foreach (var rep in helperService.Get())
            {
                rep.Opening = WorkingHoursHelper.HoursOfOperation(rep);
                serviceDataResult.Add(rep);
            }
           
            return View(serviceDataResult);
        }

        [HttpPost]
        public async Task<ActionResult> GetWeather(string weather)
        {
            string apiKey = "2624e47a1e91f9ffecbcf5011e77af64";
            string weatherApi = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", weather, apiKey);
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(weatherApi);
                if (response.IsSuccessStatusCode)
                {
                    string jsondata = await response.Content.ReadAsStringAsync();
                    return Content(jsondata, "application/json");
                }
                return Json(1, JsonRequestBehavior.AllowGet);
            }
        }
    }

    //Declare internal helper class 
    internal static class WorkingHoursHelper
    {

        public static OpeningHour HoursOfOperation(HelperServiceModel helperS)
        {
            OpeningHour openingResult = (default);

            var day_In_Now = Enum.GetName(typeof(DayOfWeek), DateTime.Now.DayOfWeek);
            foreach (var keyValue in DeserializeToDictionary(helperS))
            {
                if (keyValue.Key.StartsWith(day_In_Now))
                {
                    string endofBusinessHour = string.Empty;//TimeSpan.Zero;
                    var hours = keyValue.Value.ToObject<List<int>>();

                    if (IsBusinessOpen(hours))
                    {
                        endofBusinessHour = DateTime.Parse(TimeSpan.FromHours(hours[1]).ToString("hh':'mm"), CultureInfo.CurrentCulture).ToString("h tt");
                        string opening = string.Format("OPEN TODAY UNTIL {0}", endofBusinessHour);
                        openingResult = new OpeningHour(opening, "bg-color-donation-orange");
                        break;
                    }
                    else
                    {
                        string day = string.Empty;
                        foreach (var nextDay in GetNextDay(helperS))
                        {
                            day = nextDay.Key;
                            endofBusinessHour = DateTime.Parse(TimeSpan.FromHours(nextDay.Value[0]).ToString("hh':'mm"), CultureInfo.CurrentCulture).ToString("h tt");
                        }
                        string opening = string.Format(" REOPENS {0} at {1} ", day, endofBusinessHour);
                        openingResult = new OpeningHour(opening, "bg-color-light-grey");
                        break;
                    }
                }
            }
            return openingResult;
        }

        //Condiction to check Business Open  
        static bool IsBusinessOpen(List<int> hours)
        {
            int StartHour = hours[0];
            int EndHour = hours[1];
            if (StartHour == 0 && EndHour == 0)
            {
                return false;
            }

            TimeSpan start = TimeSpan.FromHours(StartHour); //Start Hour o'clock
            TimeSpan end = TimeSpan.FromHours(EndHour); // End Hour o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;
            //if (IsWeekday() && !IsWeekend()) {
            if ((now > start) && (now < end))
            {
                //match found
                return true;
            }
            else
            {
                return false;
            }
            //}

        }

        static Dictionary<string, List<int>> GetNextDay(HelperServiceModel helperS)
        {
            //Get next day from DateTime now
            int dayToAdd = 1;

            Dictionary<string, List<int>> keyValueMap = new Dictionary<string, List<int>>();
            var nextday = Enum.GetName(typeof(DayOfWeek), DateTime.Now.AddDays(dayToAdd).DayOfWeek);
            foreach (var keyValue in DeserializeToDictionary(helperS))
            {
                if (keyValue.Key.StartsWith(nextday))
                {

                    var hours = keyValue.Value.ToObject<List<int>>();
                    keyValueMap.Add(nextday, hours);
                    int StartHour = hours[0];
                    int EndHour = hours[1];
                    if (StartHour > 0 && EndHour > 0)
                    {
                        break;
                    }
                    else
                    {
                        //Advance to next day 
                        ++dayToAdd;
                        nextday = Enum.GetName(typeof(DayOfWeek), DateTime.Now.AddDays(dayToAdd).DayOfWeek);
                        continue;
                    }
                }

            }
            return keyValueMap;
        }


        static Dictionary<string, JToken> DeserializeToDictionary(HelperServiceModel helperS)
        {
            //Need this to convert object to dictionary key
            JObject converted = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(helperS));
            Dictionary<string, JToken> keyValueMap = new Dictionary<string, JToken>();
            if (converted != null)
            {

                foreach (KeyValuePair<string, JToken> keyValuePair in converted)
                {
                    keyValueMap.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            return keyValueMap;
        }


        static bool IsWeekend()
        {
            DateTime date = DateTime.Now;
            return date.DayOfWeek == DayOfWeek.Saturday
                || date.DayOfWeek == DayOfWeek.Sunday;
        }

        static bool IsWeekday()
        {
            DateTime date = DateTime.Now;
            return date.DayOfWeek != DayOfWeek.Saturday
                || date.DayOfWeek != DayOfWeek.Sunday;
        }



    }


}