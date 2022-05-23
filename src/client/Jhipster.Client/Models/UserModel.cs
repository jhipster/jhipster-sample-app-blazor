using System;
using System.Collections.Generic;

namespace Jhipster.Client.Models
{
    public class UserModel : BaseModel<string>
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ImageUrl { get; set; }
        public bool Activated { get; set; }
        public string LangKey { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public IEnumerable<string> Authorities { get; set; }
    }
}
