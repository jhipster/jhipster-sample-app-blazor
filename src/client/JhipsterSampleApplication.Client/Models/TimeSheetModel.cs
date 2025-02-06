using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JhipsterSampleApplication.Client.Models;

public class TimeSheetModel : BaseModel<Guid?>
{
    public DateTime? TimeSheetDate { get; set; }
    public IList<TimeSheetEntryModel> TimeSheetEntries { get; set; } = new List<TimeSheetEntryModel>();

    public long? EmployeeId { get; set; }

}
