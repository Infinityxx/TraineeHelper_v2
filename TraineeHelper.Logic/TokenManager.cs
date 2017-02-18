using DataModel.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeHelper.DAL.Services;

namespace TraineeHelper.Logic
{
    public class TokenManager
    {
        private TokenEntityService TokenEntityService;

        public TokenManager()
        {
            TokenEntityService = new TokenEntityService();
        }

        public TokenEntity GenerateToken(string userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now + TimeSpan.FromMinutes(5);
            var tokendomain = new TokenEntity()
            {
                UserId = userId,
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };
            TokenEntityService.InsertToken(tokendomain);

            var tokenModel = new TokenEntity()
            {
                UserId = userId,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                AuthToken = token
            };
            return tokenModel;
        }

        public bool ValidateToken(string tokenId)
        {
            var token = TokenEntityService.ValidateToken(tokenId);
            if(token != null && !(DateTime.Now > token.ExpiresOn))
            {
                token.ExpiresOn = token.ExpiresOn + TimeSpan.FromMinutes(5);
                var result = TokenEntityService.UpdateToken(token);
                return result;
            }
            return false;
        }

        public bool KillToken(string tokenId)
        {
            var result = TokenEntityService.Kill(tokenId);
            return result;
        }

        public bool DeleteByUserId(string userId)
        {
            return TokenEntityService.DeleteByUserId(userId);
        }
    }
}
