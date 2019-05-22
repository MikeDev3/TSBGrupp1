using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loinprojekt_admin.Models
{
    public class Userobjekt
    {
        public int ID { get; set; }
        public string adress { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string username { get; set; }
        public int personalcodenumber { get; set; }
        public string phonenumber { get; set; }
        public string picture { get; set; }
        public int zipcode { get; set; }
        
    }
}