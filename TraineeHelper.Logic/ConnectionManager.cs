using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.Common;
using TraineeHelper.DAL.Services;
using TraineeHelper.Logic.Converters;
using TraineeHelper.Models.Entities;
using TraineeHelper.ViewModels;

namespace TraineeHelper.Logic
{
    public class ConnectionManager
    {
        private ConnectionEntityService ConnectionEntityService;

        public ConnectionManager()
        {
            ConnectionEntityService = new ConnectionEntityService();
        }


        public async Task<ConnectionContext> SaveConnection(ConnectionContext connectionContext)
        {
            if (string.IsNullOrEmpty(connectionContext.Id))
            {
                Connection connection = new Connection();
                connection = connectionContext.ConvertToConnection(true);
                var result = await ConnectionEntityService.CreateConnection(connection);
                return result.ConvertToConnectionContext();
            }
            var update = connectionContext.ConvertToConnection();
            await ConnectionEntityService.Update(update);
            return connectionContext;
        }

        //public async Task<bool> CreateConnection(ConnectionContext connectionContext)
        //{
        //    if (connectionContext == null)
        //        return false;
        //    Connection connection = connectionContext.ConvertToConnection(true);
        //    var result = await ConnectionEntityService.CreateConnection(connection);
        //    return result;
        //}

        public async Task<ConnectionContext> UpdateConnection(ConnectionContext connectionContext)
        {
            if (connectionContext == null)
                return null;
            Connection connection = connectionContext.ConvertToConnection();
            var result = await ConnectionEntityService.Update(connection);
            return result.ConvertToConnectionContext();
        }

        public async Task<bool> UpdateConnectionStatus(string id, ConnectionStatus status)
        {
            if (id == null)
                return false;

            var connectionContext = await FindConnectionById(id);
            connectionContext.ConnectionStatus = status;

            await UpdateConnection(connectionContext);
            return true;
        }

        public async Task<bool> DeleteConnection(ConnectionContext connectionContext)
        {
            if (connectionContext == null)
                return false;
            Connection connection = connectionContext.ConvertToConnection();
            var result = await ConnectionEntityService.Delete(connection.Id.ToString());
            return result;
        }

        public async Task<ConnectionContext> FindConnectionById(string ConnectionId)
        {
            if (ConnectionId == null)
                return null;

            var result = await ConnectionEntityService.GetById(ConnectionId);

            return result.ConvertToConnectionContext();
        }

        public async Task<IEnumerable<ConnectionContext>> FindUserActiveConnections(string userId)
        {
            if (userId == null)
                return null;
            UserManager userManager = new UserManager();
            var result = await ConnectionEntityService.GetAllActiveConnectionRelatedToUser(userId);
            //result.ConvertToConnectionContexts();
            foreach (Connection c in result)
            {
                if(userId != c.Reciever)
                {
                    c.Contact = await userManager.GetUserNameById(c.Reciever);
                }
                else
                {
                    c.Contact = await userManager.GetUserNameById(c.Sender);
                }
            }
            return result.ConvertToConnectionContexts();        
        }

        public async Task<IEnumerable<ConnectionContext>> FindUserRequests(string userId)
        {
            if (userId == null)
                return null;
            UserManager userManager = new UserManager();
            var result = await ConnectionEntityService.GetUserConnectionRequests(userId);
            foreach (Connection c in result)
            {
                c.Reciever = await userManager.GetUserNameById(c.Reciever);
                c.Sender = await userManager.GetUserNameById(c.Sender);
            }
            return result.ConvertToConnectionContexts();
        }

        public async Task<bool> CheckConnectionExistence(string senderId, string recieverId)
        {
            var result = await ConnectionEntityService.GetConnectivityByUsers(senderId, recieverId);
            if(result.Count() > 0)
                return true;
            return false;
        }

        public async Task<ConnectionType> GetConnectionType(string senderId, string recieverId)
        {
            var result = await ConnectionEntityService.GetConnectivityByUsers(senderId, recieverId);
            return result.FirstOrDefault().ConnectionType;
        }

        public async Task<ConnectionStatus> GetConnectionStatus(string senderId, string recieverId)
        {
            var result = await ConnectionEntityService.GetConnectivityByUsers(senderId, recieverId);
            return result.FirstOrDefault().ConnectionStatus;
        }
    }

   
}
