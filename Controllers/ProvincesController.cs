using Covid.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Covid.Controllers 
{
    public class ProvincesController : PlacesController
    {

        public async Task<ActionResult> Provinces(string isoCode)
        {
            string partApiUrl = "/provinces?iso=" + isoCode;
            string partDetailApiURL = "/reports?region_province=";
            var data = await getPlaces(partApiUrl, partDetailApiURL);

            List<Place> places = JsonConvert.DeserializeObject<ProvinceList>(data).data.ToList<Place>();
            places = await fill_cases_deaths(places, partDetailApiURL, false);
            places = filter_top(places, 10);

            return View(places.ToList());

        }

    }
}