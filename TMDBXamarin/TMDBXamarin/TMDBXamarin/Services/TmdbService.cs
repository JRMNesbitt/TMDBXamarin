using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TMDBXamarin.Helpers;
using TMDBXamarin.Interfaces;
using TMDBXamarin.Models;
using Xamarin.Forms;

namespace TMDBXamarin.Services
{
    public class TmdbService : ITmdbService
    {
      

        private HttpClient httpClient;

        public TmdbService()
        {
            
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<MovieResult> GetUpComingMovies(int page)
        {
            var Uri = string.Format("{0}/movie/upcoming?api_key={1}&page={2}&language={3}",TMDBApiConfig.BaseUrl, TMDBApiConfig.ApiKey, page, TMDBApiConfig.Language);  //$"{baseUrl}/movie/upcoming?api_key={apiKey}&page={page}&language={language}";
            try
            {
                using (var response = await httpClient.GetAsync(Uri).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<MovieResult>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public async Task<List<MovieGenre>> GetMovieGenres()
        {
            var Uri = string.Format("{0}/genre/list?api_key={1}&language={2}", TMDBApiConfig.BaseUrl, TMDBApiConfig.ApiKey, TMDBApiConfig.Language);                
            //$"{baseUrl}/genre/list?api_key={apiKey}&language={language}";
            try
            {
                using (var response = await httpClient.GetAsync(Uri).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            var genreList = JsonConvert.DeserializeObject<MovieGenreResult>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                            return genreList?.ListofGenres;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return null;
        }

        public async Task<MovieResult> MovieSearchAsync(string searchTerm, int page)
        {
            var restUrl = string.Format("{0}/search/movie?api_key={1}&query={2}&page={3}&language={4}", TMDBApiConfig.BaseUrl, TMDBApiConfig.ApiKey, searchTerm, page, TMDBApiConfig.Language);
            //$"{baseUrl}/search/movie?api_key={apiKey}&query={searchTerm}&page={page}&language={language}";

            try
            {
                using (var response = await httpClient.GetAsync(restUrl).ConfigureAwait(false))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                        {
                            return JsonConvert.DeserializeObject<MovieResult>(
                                await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        }
    }
}
