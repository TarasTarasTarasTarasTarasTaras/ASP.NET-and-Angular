﻿using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class LoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
