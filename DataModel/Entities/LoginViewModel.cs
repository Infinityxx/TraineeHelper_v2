﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Entities
{
    public class LoginViewModel
    {
        //[Required()]
        public string Username  { get; set; }

        //[Required()]
        public string Password { get; set; }
    }
}
