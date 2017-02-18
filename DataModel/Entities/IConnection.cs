using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.Models.Entities
{
    public interface IConnection : IMongoCommon
    {
        string Sender { get; set; } //from
        string Reciever { get; set; } //to
        string Contact { get; set; } 
        ConnectionStatus ConnectionStatus { get; set; }
        ConnectionType ConnectionType { get; set; }
    }
}
