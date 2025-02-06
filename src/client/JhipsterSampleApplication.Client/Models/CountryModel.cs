using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JhipsterSampleApplication.Client.Models;

public class CountryModel : BaseModel<long?>
{
    public string? CountryName { get; set; }
    public long? RegionId { get; set; }

}
