using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationLayer.DTOs.Auth;

namespace EVO.DomainLayer.Entity.Models.Auth
{
    [Table("User")]
    public class User
    {
        public int RecordId { get; set; }
        public string UserId { get; set; }

        public string EmailAddress { get; set; }

        public string Password { get; set; }

        public EnumRoleUser RoleUser { get; set; }

    }
}
