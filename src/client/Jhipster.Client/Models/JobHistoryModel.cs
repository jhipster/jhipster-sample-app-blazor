using Jhipster.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models
{
    public class JobHistoryModel
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Language Language { get; set; }
        public long? JobId { get; set; }

        public long? DepartmentId { get; set; }

        public long? EmployeeId { get; set; }

    }
}
