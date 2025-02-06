using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JhipsterSampleApplication.Client.Models;

public class TimeSheetEntryModel : BaseModel<long?>
{
    public string? ActivityName { get; set; }
    public int? StartTimeMilitary { get; set; }
    public int? EndTimeMilitary { get; set; }
    public decimal? TotalTime { get; set; }
    public Guid? TimeSheetId { get; set; }

}
