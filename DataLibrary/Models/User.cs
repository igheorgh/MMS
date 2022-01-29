using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public partial class User : IdentityUser
    {
        public bool Active { get; set; }
        public DateTime? Birthdate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Roles { get; set; }
        public string Description { get; set; }
    }
}
