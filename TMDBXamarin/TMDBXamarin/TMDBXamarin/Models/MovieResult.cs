
using Newtonsoft.Json;
using System.Collections.Generic;


namespace TMDBXamarin.Models
{
    public class MovieResult
    {
        /*
            "page": 1,
            "total_results": 329,
            "total_pages": 17
         */
        [JsonProperty(PropertyName = "page")]
        public int CurrentPage { get; set; }

        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }

        [JsonProperty(PropertyName = "total_pages")]
        public int TotalPages { get; set; }

        [JsonProperty(PropertyName = "results")]
        public List<MovieInfo> ListOfUpComingMovies { get; set; }

    }
}
