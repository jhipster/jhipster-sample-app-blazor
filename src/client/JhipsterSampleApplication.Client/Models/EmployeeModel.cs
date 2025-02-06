using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JhipsterSampleApplication.Client.Models;

public class EmployeeModel : BaseModel<long?>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public long? Salary { get; set; }
    public long? CommissionPct { get; set; }
    public IList<JobModel> Jobs { get; set; } = new List<JobModel>();

    public IList<TimeSheetModel> TimeSheets { get; set; } = new List<TimeSheetModel>();

    public long? ManagerId { get; set; }

    public long? DepartmentId { get; set; }

}
