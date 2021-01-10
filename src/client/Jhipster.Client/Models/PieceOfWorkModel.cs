using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models
{
    public class PieceOfWorkModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IList<JobModel> Jobs { get; set; } = new List<JobModel>();

    }
}
