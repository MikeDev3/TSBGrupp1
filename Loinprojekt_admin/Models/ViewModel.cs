using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loinprojekt_admin.Models
{
    public class ViewModel
    {
        public Userobjekt userObjekt { get; set; }

        public string reason { get; set; }
        public DateTime dateTo { get; set; }
        public int userID { get; set; }
        public int adminID { get; set; }

    }
}