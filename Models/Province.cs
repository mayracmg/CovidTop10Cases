using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Covid.Models
{
    public class Province : Place
    {
        [JsonProperty("province")]
        public override string name { get; set; }
        
    }
    
    public class ProvinceList : DataList
    {
        public List<Province> data { get; set; }

    }

}