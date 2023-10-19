using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVC_Backend_Frontend.Models
{
    public class CustomUser : IdentityUser
    {
        public CustomUser() : base() { }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

}