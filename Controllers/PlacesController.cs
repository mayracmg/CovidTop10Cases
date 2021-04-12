using Covid.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Covid.Controllers
{
    public class PlacesController : Controller
    {
        string BaseUrl = "https://covid-19-statistics.p.rapidapi.com";
        string ApiKey = "d3953997c4mshf099740d2750e39p138295jsn44a942417b7c";
        string ApiHost = "covid-19-statistics.p.rapidapi.com";
        string UseQueryString = "true";

        public async Task<string> getPlaces(string partApiUrl, string partDetailApiURL)
        {
            string apiUrl = BaseUrl + partApiUrl;
            DataList places = new DataList();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                client.DefaultRequestHeaders.Add("x-rapidapi-key", ApiKey);
                client.DefaultRequestHeaders.Add("x-rapidapi-host", ApiHost);
                client.DefaultRequestHeaders.Add("useQueryString", UseQueryString);

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    return data;
                    
                }

            }
            return null;
        }

        public async Task<List<Place>> fill_cases_deaths(List<Place> places, string partApiUrl, bool parar)
        {

            string apiUrl = BaseUrl + partApiUrl;

            if (places.Count > 0) {
                foreach (var Place in places.ToList())
                {
                    string apiUrlRegion = apiUrl + Place.name;
                    DataList PlaceData = new DataList();

                    var client = new HttpClient();
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Get,
                        RequestUri = new Uri(apiUrlRegion),
                        Headers =
                    {
                        { "x-rapidapi-key", ApiKey },
                        { "x-rapidapi-host", ApiHost },
                    },
                    };

                    using (var response = await client.SendAsync(request))
                    {
                        response.EnsureSuccessStatusCode();

                        var data = await response.Content.ReadAsStringAsync();
                        PlaceData = JsonConvert.DeserializeObject<DataList>(data);
             
                        if (PlaceData.data.Count > 0)
                        {
                            Place.confirmed = PlaceData.data.First<Place>().confirmed;
                            Place.deaths = PlaceData.data.First<Place>().deaths;
                        }
                        else
                        {
                            Place.confirmed = 0;
                            Place.deaths = 0;
                        }
                    }
                    if (parar)
                    {
                        break;
                    }
                }
            }
            return places;
        }

        public List<Place> filter_top(List<Place> places, int top_list)
        {
            var OrderedPlaces = places.OrderByDescending(place => place.confirmed).ToList();

            places = OrderedPlaces.GetRange(0, top_list);
            return places;
        }
    }
}
