using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTraineeHelper.Dal
{
    public interface IMongoEntity<TId>
    {
        TId Id { get; set; }
    }
}
