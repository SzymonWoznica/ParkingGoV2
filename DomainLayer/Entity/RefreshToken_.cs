﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entity
{
    [Table("RefreshToken")]

    public class RefreshToken_
    {
        [Key]
        public Guid UserId { get; set; }
        public string TokenId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpirationTime { get; set; }
        public bool IsActive { get; set; }

    }
}
