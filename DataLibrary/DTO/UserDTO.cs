using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DTO
{
    public class UserDTO
    {
        public string id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string roles { get; set; }
        public DateTime birthdate { get; set; }
        public string description { get; set; }
    }
}
