using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs.Auth;

namespace DomainLayer.Entity
{
    public class User
    {
        
        [Required][MaxLength(36)] 
        public string Id { get; set; }
        
        [Required][MaxLength(100)]
        public string EmailAddress { get; set; } = string.Empty;
        
        [Required][MaxLength(64)] 
        public string Password { get; set; }

        [Required]
        public EnumRoleUser RoleUser {get; set; }

    }
}
