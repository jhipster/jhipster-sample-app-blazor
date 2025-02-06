using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JhipsterSampleApplication.Client.Models;

public class RegionModel : BaseModel<long?>
{
    public string? RegionName { get; set; }
}
