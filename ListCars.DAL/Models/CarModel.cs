using System;
using System.Collections.Generic;
using System.Text;

namespace ListCars.DAL.Models
{
    public class CarModel
    {
        public long? Id { get; set; }
        public long? NumberRow { get; set; }
        public string NumberCar { get; set; }
        public string ModelCar { get; set; }
        public string ColorCar { get; set; }
        public string YearCar { get; set; }
        public string Created { get; set; }
        public string LastFetched { get; set; }
        public long? Active { get; set; }
    }
}
