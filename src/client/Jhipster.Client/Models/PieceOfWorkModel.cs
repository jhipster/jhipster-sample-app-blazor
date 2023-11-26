using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models;

public class PieceOfWorkModel : BaseModel<long>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IList<JobModel> Jobs { get; set; } = new List<JobModel>();

}
