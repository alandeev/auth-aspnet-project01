using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend_aspnet_crud.Entities {
    public class Post {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }        
        public DateTime date { get; set; }
    }
}