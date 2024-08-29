using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessObjects.Entities
{
    public class UserDetail
    { 
        public required string UserId { get; set; }

        [StringLength(50)]
        public string? FullName { get; set; }

        public int Yob { get; set; }

        [StringLength(50)]
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public string? Role { get; set; }
        public bool IsDeleted { get; set; }

        public int LoginAttempt { get; set; }

        public bool IsBanned { get; set; }
    }
}