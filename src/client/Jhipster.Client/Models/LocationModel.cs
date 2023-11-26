using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models;

public class LocationModel : BaseModel<long>
{
    public string StreetAddress { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public long? CountryId { get; set; }

}
