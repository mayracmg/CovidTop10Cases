using Covid.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace Covid.Controllers
{
    public class RegionsController : PlacesController
    {     

        public async Task<ActionResult> Regions()
        {
            string partApiUrl = "/regions";
            string partDetailApiURL = "/reports?region_name=";
            var data = await getPlaces(partApiUrl, partDetailApiURL);

            List<Place> places = JsonConvert.DeserializeObject<DataList>(data).data;
            places = await fill_cases_deaths(places, partDetailApiURL, true);
            places = filter_top(places, 10);

            return View(places.ToList());            
        }

    }
}
