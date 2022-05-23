using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Jhipster.Client.Models
{
    public class DepartmentModel : BaseModel<long>
    {
        [Required]
        public string DepartmentName { get; set; }
        public long? LocationId { get; set; }

        public IList<EmployeeModel> Employees { get; set; } = new List<EmployeeModel>();

    }
}
