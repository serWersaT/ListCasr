using System;
using System.Collections.Generic;
using System.Text;

namespace ListCars.DAL.Models
{
    public class RepCarModel
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public string LastFetched { get; set; }
        public string Created { get; set; }
        public string InWork { get; set; }
        public string Number { get; set; }
    }
}
