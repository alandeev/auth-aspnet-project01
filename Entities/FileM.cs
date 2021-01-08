using System.ComponentModel.DataAnnotations;

namespace backend_aspnet_crud.Entities {
    public class FileM {
        [Key]
        public int id { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string link { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }     
    }
}