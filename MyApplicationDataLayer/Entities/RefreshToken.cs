﻿using System.ComponentModel.DataAnnotations.Schema;

namespace MyApplicationDataLayer.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public User User { get; set; }
        public DateTime Expired { get; set; }
    }
}
