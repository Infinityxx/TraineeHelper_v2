using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraineeHelperNew.Dal
{
    public interface IMongoEntity<TId>
    {
        TId Id { get; set; }
    }
}
