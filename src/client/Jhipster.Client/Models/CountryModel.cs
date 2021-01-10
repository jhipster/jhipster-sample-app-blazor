using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models
{
    public class CountryModel
    {
        public long Id { get; set; }
        public string CountryName { get; set; }
        public long? RegionId { get; set; }

    }
}
