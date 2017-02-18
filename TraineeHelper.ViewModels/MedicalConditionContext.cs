using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class MedicalConditionContext
    {
        public double MuscleMass { get; set; }
        public double FatPercent { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
    }
}
