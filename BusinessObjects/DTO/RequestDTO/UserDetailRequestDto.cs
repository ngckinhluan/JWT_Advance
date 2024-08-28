﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTO.RequestDTO
{
    public class UserDetailRequestDto
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public int Yob { get; set; }
        public string? Address { get; set; }
    }
}
