﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Identity
{
   public class AuthResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public DateTime ExpiryTimeStamp { get; set; }
        public string? isExpired { get; set; }
        public int ExpiresIn { get; set; }
    }
}
