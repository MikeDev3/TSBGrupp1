using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loinprojekt_admin
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int AccountStatus { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int? PersonalCodeNumber { get; set; }
        public string Address { get; set; }
        public int? ZipCode { get; set; }
        public string City { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
    }
}