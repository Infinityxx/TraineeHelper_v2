using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;

namespace TraineeHelper.ViewModels
{
    [Serializable]
    public class ConnectionContext
    {
        public string Id { get; set; }
        public string Sender { get; set; } // for requests
        public string Reciever { get; set; } // for requests
        public string Contact { get; set; } // for active connections
        public ConnectionStatus ConnectionStatus { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }

        public ConnectionContext()
        {

        }

        public ConnectionContext(string sender, string reciever, ConnectionType connectionType)
        {
            Sender = sender;
            Reciever = reciever;
            ConnectionType = connectionType;
            Contact = null;
            Created = DateTime.Now;
            Modified = DateTime.Now;
            ConnectionStatus = ConnectionStatus.REQUEST;
        }

        public ConnectionContext(string contact, ConnectionType connectionType)
        {
            Contact = contact;
            ConnectionType = connectionType;
            Created = DateTime.Now;
            Modified = DateTime.Now;
            ConnectionStatus = ConnectionStatus.ACCEPTED;
        }
    }
}
