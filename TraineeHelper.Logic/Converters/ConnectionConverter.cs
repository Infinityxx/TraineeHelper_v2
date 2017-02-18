using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Models.Entities;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic.Converters
{
    public static class ConnectionConverter
    {
        public static List<Connection> ConvertToNewConnections(this List<ConnectionContext> contexts)
        {
            List<Connection> connections = new List<Connection>();
            return connections;
        }

        public static Connection ConvertToNewConnection(this ConnectionContext context)
        {
            Connection connection = new Connection();
            return connection;
        }

        public static ConnectionContext ConvertToConnectionContext(this IConnection connection)
        {
            ConnectionContext connectionContext = new ConnectionContext();

            connectionContext.Sender = connection.Sender;
            connectionContext.Reciever = connection.Reciever;
            connectionContext.Contact = connection.Contact;
            connectionContext.Description = connection.Description;
            connectionContext.ConnectionStatus = connection.ConnectionStatus;
            connectionContext.ConnectionType = connection.ConnectionType;
            connectionContext.Created = connection.Created;
            connectionContext.Id = connection.Id.ToString();
            connectionContext.IsActive = connection.IsActive;
            connectionContext.Modified = connection.Modified;

            return connectionContext;
        }

        public static Connection ConvertToConnection(this ConnectionContext context, bool generateId = false)
        {
            Connection connection = new Connection();
            if (null == context)
                return connection;
            connection.Sender = context.Sender;
            connection.Reciever = context.Reciever;
            connection.Description = context.Description;
            connection.Contact = context.Contact;
            connection.ConnectionStatus = context.ConnectionStatus;
            connection.ConnectionType = context.ConnectionType;
            connection.Created = context.Created;
            connection.Id = generateId ? ObjectId.GenerateNewId() : ObjectId.Parse(context.Id);
            connection.IsActive = context.IsActive;
            connection.Modified = context.Modified;

            return connection;
        }

        public static List<ConnectionContext> ConvertToConnectionContexts(this IEnumerable<IConnection> connections)
        {
            List<ConnectionContext> connectionContexts = new List<ConnectionContext>();
            if (null == connections)
                return connectionContexts;
            foreach (Connection c in connections)
            {
                connectionContexts.Add(c.ConvertToConnectionContext());
            }
            return connectionContexts;
        }

        public static List<Connection> ConvertToConnections(this List<ConnectionContext> contexts)
        {
            List<Connection> connections = new List<Connection>();
            if (null == contexts)
                return connections;
            foreach (ConnectionContext cc in contexts)
            {
                connections.Add(cc.ConvertToConnection());
            }
            return connections;
        }
    }
}
