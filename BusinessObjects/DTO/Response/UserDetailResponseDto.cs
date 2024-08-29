﻿namespace BusinessObjects.DTO.Response
{
    public class UserDetailResponseDto
    {
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public int? Yob { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool? IsDeleted { get; set; } 
    }
}
