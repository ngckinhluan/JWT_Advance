using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BusinessObjects.Entities
{
    public class UserDetail
    { 
        public required string UserId { get; set; }
        [StringLength(50)]
        public string? FullName  { get; set; }
        public int Yob  { get; set; }
        [StringLength(50)]
        public string? Address { get; set; }

        // Navigation property
        public virtual IdentityUser? User { get; set; }

    }
}
