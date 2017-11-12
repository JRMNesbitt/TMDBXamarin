using System;
using System.Globalization;

namespace TMDBXamarin.Helpers
{
    public class TMDBApiConfig
    {

        //* TMDB config settings *//
        public static string ApiKey = "1f54bd990f1cdfb230adb312546d765d";
        public static string BaseUrl = "https://api.themoviedb.org/3";
        public static string Language {
            get
            {
                return CultureInfo.CurrentCulture.Name;
            }
          }

    }
}
