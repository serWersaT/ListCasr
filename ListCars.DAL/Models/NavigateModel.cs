using System;
using System.Collections.Generic;
using System.Text;

namespace ListCars.DAL.Models
{
    public class NavigateModel
    {
        public int? topmin { get; set; }
        public int? topmax { get; set; }

        public int RepId { get; set; }
        public string Model { get; set; }
    }
}
