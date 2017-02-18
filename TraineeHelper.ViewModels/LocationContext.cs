using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class LocationContext
    {
        public string City { get; set; }
        public string Country { get; set; }
        //public double CoordX { get; set; }
        //public double CoordY { get; set; }
    }
}
