using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Loinprojekt_admin.Models
{
    public class StatisticModel
    {
        public int ActiveRow { get; set; }
        public int FlaggedRow { get; set; }
        public int BlockedRow { get; set; }
    }
}