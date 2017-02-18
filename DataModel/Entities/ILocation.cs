using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.Models
{
    public interface ILocation
    {
        string City { get; set; }
        string Country { get; set; }
        //double CoordX { get; set; }
        //double CoordY { get; set; }
    }
}

