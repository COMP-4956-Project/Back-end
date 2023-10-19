using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MVC_Backend_Frontend.Models
{
    public class CustomRole : IdentityRole
    {

        public CustomRole() : base() { }

        public CustomRole(string roleName) : base(roleName) { }

        public CustomRole(string roleName, string description,
          DateTime createdDate)
          : base(roleName)
        {
            base.Name = roleName;

            this.Description = description;
            this.CreatedDate = createdDate;
        }

        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}