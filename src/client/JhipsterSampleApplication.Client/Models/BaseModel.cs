using System;
using System.Collections.Generic;

namespace JhipsterSampleApplication.Client.Models;

public class BaseModel<TKey>
{
    public TKey Id { get; set; }
}
