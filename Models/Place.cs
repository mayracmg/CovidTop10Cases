using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Covid.Models
{
    public class Place
    {
        public string iso { get; set; }
        public virtual string name { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int confirmed { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        public int deaths { get; set; }

        
    }

    public class DataList
    {
        public List<Place> data { get; set; }

    }
}