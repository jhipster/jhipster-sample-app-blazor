using System;
using System.Collections.Generic;

namespace Jhipster.Client.Models
{
    public class BaseModel<TKey>
    {
        public TKey Id { get; set; }
    }
}
