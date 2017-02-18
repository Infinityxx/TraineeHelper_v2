using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entities
{
    public interface IMedicalCondition
    {
        double MuscleMass { get; set; }
        double FatPercent { get; set; }
        double Weight { get; set; }
        double Height { get; set; }
        int Age { get; set; }
    }
}
