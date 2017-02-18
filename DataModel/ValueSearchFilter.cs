using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelper.Models
{
    public class ValueSearchFilter<K> : SearchFilter
    {
        public K Value { get; set; }
    }
}
