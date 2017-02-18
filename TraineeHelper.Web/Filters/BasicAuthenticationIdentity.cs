using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace TraineeHelper.Web.Filters
{
    /// <summary>
    /// Basic Authentication identity
    /// </summary>
    public class BasicAuthenticationIdentity : GenericIdentity
    {
        public string Token { get; set; }
        public int UserId { get; set; }

        /// <summary>
        /// Basic Authentication Identity Constructor
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public BasicAuthenticationIdentity(string token)
            : base(token, "Basic")
        {
            Token = token;
        }
    }
}