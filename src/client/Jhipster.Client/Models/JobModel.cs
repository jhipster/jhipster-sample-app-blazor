using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models
{
    public class JobModel : BaseModel<long>
    {
        public string JobTitle { get; set; }
        public long? MinSalary { get; set; }
        public long? MaxSalary { get; set; }
        public IList<PieceOfWorkModel> Chores { get; set; } = new List<PieceOfWorkModel>();

        public long? EmployeeId { get; set; }

    }
}
