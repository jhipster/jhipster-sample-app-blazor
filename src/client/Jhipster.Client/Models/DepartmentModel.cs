using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models
{
    public class DepartmentModel
    {
        public long Id { get; set; }
        [Required]
        public string DepartmentName { get; set; }
        public long? LocationId { get; set; }

        public IList<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

    }
}
