using Newtonsoft.Json;
using System.Collections.Generic;

namespace TMDBXamarin.Models
{
    public class MovieGenreResult
    {
        [JsonProperty(PropertyName = "genres")]
        public List<MovieGenre> ListofGenres { get; set; }
    }
}
