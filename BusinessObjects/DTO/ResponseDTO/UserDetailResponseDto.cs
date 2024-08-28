using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.ResponseDTO
{
    internal class UserDetailResponseDto
    {
        public string? FullName { get; set; }
        public int? Yob { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool? IsDeleted { get; set; } 
    }
}
