using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jhipster.Client.Models;

public class RegisterResultRequest
{
    public string Type { get; set; }

    public int Status { get; set; }

    public string Detail { get; set; }

    public string Params { get; set; }

    public string Message { get; set; }

    public string TraceId { get; set; }
}
