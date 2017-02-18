using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.Models
{
    public class RangeSearchFilter : SearchFilter
    {
        public string FromValue { get; set; }
        public string ToValue { get; set; }
    }
}
