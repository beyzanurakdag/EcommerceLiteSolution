﻿using EcommerceLiteEntity.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceLiteEntity.Models
{
    [Table("PassiveUsers")]
     public class PassiveUser:PersonBase
     {
        public string UserId { get; set; } //Identity Model'in ID değeri burada ForeignKey olacaktır.
        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
     }
}
