using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models
{
    public class RegionModel : BaseModel<long>
    {
        public string RegionName { get; set; }
    }
}
