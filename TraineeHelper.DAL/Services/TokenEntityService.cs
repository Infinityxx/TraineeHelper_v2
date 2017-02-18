using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel.DataModel;
using TraineeHelper.DAL;
using System.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace TraineeHelper.DAL.Services
{
    public class TokenEntityService : ITokenService
    {
        #region Private member variables
        private readonly MongoConnectionHandler<TokenEntity> MongoConnectionHandler;
        #endregion

        #region Public constructor.
        /// <summary>
        /// Public constructor.
        /// </summary>
        public TokenEntityService()
        {
            MongoConnectionHandler = new  MongoConnectionHandler<TokenEntity>();
        }
        #endregion

        #region Public member methods.
        /// <summary>
        ///  Function to generate unique token with expiry against the provided userId.
        ///  Also add a record in database for generated token.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public void InsertToken(TokenEntity tokenEntity)
        {
            //string token = Guid.NewGuid().ToString();
            //DateTime issuedOn = DateTime.Now;
            //DateTime expiredOn = DateTime.Now + TimeSpan.FromMinutes(5);//.AddSeconds(Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));
            //var tokendomain = new TokenEntity()
            //{
            //    UserId = userId,
            //    AuthToken = token,
            //    IssuedOn = issuedOn,
            //    ExpiresOn = expiredOn
            //};

            MongoConnectionHandler.MongoCollection.InsertOneAsync(tokenEntity);
            //MongoConnectionHandler.MongoCollection.Save();
            //var tokenModel = new TokenEntity()
            //{
            //    UserId = userId,
            //    IssuedOn = issuedOn,
            //    ExpiresOn = expiredOn,
            //    AuthToken = token
            //};

            //return tokenModel;
        }

        /// <summary>
        /// Method to validate token against expiry and existence in database.
        /// </summary>
        /// <param name="tokenId"></param>
        /// <returns></returns>
        public TokenEntity ValidateToken(string tokenId)
        {
            var result = MongoConnectionHandler.MongoCollection.FindAsync(t => t.AuthToken == tokenId && t.ExpiresOn > DateTime.Now);
            var token = result.Result.First();
            return token;
        }

        public bool UpdateToken(TokenEntity token)
        {
            var filter = Builders<TokenEntity>.Filter.Eq(s => s.AuthToken, token.AuthToken);
            var update = Builders<TokenEntity>.Update
                .Set("UserId", token.UserId)
                .Set("AuthToken", token.AuthToken)
                .Set("IssuedOn", token.IssuedOn)
                .Set("ExpiresOn", token.ExpiresOn);
            var updatedResult = MongoConnectionHandler.MongoCollection.UpdateOne(filter, update);
            if (updatedResult.IsAcknowledged)
                return true;
            return false;
        }

        public bool Kill(string tokenId)
        {
            //var filter = new BsonDocument("_id", id);
            var filter = Builders<TokenEntity>.Filter.Eq(a => a.AuthToken, tokenId);
            var result = this.MongoConnectionHandler.MongoCollection.DeleteOneAsync(filter);

            var isNotDeleted = MongoConnectionHandler.MongoCollection.FindAsync(x => x.AuthToken == tokenId);
            if (isNotDeleted.Result.FirstOrDefault() != null) { return false; }
            return true;
        }

        public bool DeleteByUserId(string userId)
        {
            MongoConnectionHandler.MongoCollection.DeleteOneAsync(x => x.UserId == userId);
            var isNotDeleted = MongoConnectionHandler.MongoCollection.FindAsync(x => x.UserId == userId);
            if (isNotDeleted.Result.FirstOrDefault() != null) { return false; }
            return true;
        }

        /*public override void Update(TokenEntity enitity)
        {
            throw new NotImplementedException();
        }
        */
        #endregion
    }

}
